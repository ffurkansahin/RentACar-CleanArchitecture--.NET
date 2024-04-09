using Core.CroosCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CroosCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly HttpExceptionHandler _httpExceptionHandler;

	public ExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
		_httpExceptionHandler = new HttpExceptionHandler();
	}

	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(httpContext.Response, ex);
		}
	}

	private Task HandleExceptionAsync(HttpResponse response,Exception ex)
	{
		response.ContentType = "application/json";
		_httpExceptionHandler.Response = response;
		return _httpExceptionHandler.HandleExceptionAsync(ex);
	}
}
