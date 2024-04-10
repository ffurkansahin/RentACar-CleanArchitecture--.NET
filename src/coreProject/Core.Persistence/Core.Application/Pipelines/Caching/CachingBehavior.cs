using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>, ICachableRequest
{
	private readonly CacheSettings _cacheSettings;
	private readonly IDistributedCache _cache;
	private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

	public CachingBehavior(IDistributedCache cache, IConfiguration configuration, ILogger<CachingBehavior<TRequest, TResponse>> logger)
	{
		_cacheSettings = configuration.GetSection("CacheSetting").Get<CacheSettings>() ?? throw new InvalidOperationException();
		_cache = cache;
		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if(request.BypassCache)
		{
			return await next();
		}
		else
		{
			TResponse response;
			byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey,cancellationToken);
            if (cachedResponse !=null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.Default.GetString(cachedResponse));
				_logger.LogInformation($"Fetched from Cache --> {request.CacheKey}");
            }
			else
			{
				response = await GetResponseAndAddToCache(request,next,cancellationToken);
			}
			return response;
        }
	}

	private async Task<TResponse?> GetResponseAndAddToCache(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		TResponse? response = await next();

		TimeSpan slidingExpiration = request.SlidingExpiration??TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
		DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration};

		byte[] serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

		await _cache.SetAsync(request.CacheKey,serializedData,cancellationToken);
		_logger.LogInformation($"Added to Cache --> {request.CacheKey}");
		return response;
	}
}
