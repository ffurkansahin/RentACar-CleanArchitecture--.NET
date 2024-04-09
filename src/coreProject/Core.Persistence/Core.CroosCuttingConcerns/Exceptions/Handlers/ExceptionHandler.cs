using Core.CroosCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CroosCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{
	public Task HandleExceptionAsync(Exception exception) =>
		exception switch
		{
			BusinessException businessException => HandleException(businessException),
			_ => HandleException(exception)
		};
	protected abstract Task HandleException(BusinessException businessException);
	protected abstract Task HandleException(Exception businessException);
}
