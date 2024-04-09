using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Pipelines.Transactions;

public class TransactionScopeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>, ITransactionalRequest
{
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		using TransactionScope transactionScope = new(TransactionScopeAsyncFlowOption.Enabled);
		TResponse response;
		try
		{
			response = await next();
			transactionScope.Complete();
		}
		catch (Exception ex)
		{
			transactionScope.Dispose();
			throw;
		}
		return response;
	}
}
