using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.CroosCuttingConcerns.Exceptions.Types;

public class ValidationException : Exception
{
    public IEnumerable<ValidationExceptionModel> Errors { get; set; }

	public ValidationException() : base()
	{
		Errors = Array.Empty<ValidationExceptionModel>();
	}
    public ValidationException(string? message) : base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }
	public ValidationException(string? message,Exception? innerEx) : base(message,innerEx)
	{
		Errors = Array.Empty<ValidationExceptionModel>();
	}
    public ValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }
    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        IEnumerable<string> arr = errors.Select(
            x => $"{Environment.NewLine}--{x.Property}: {string.Join(Environment.NewLine,values:x.Errors)}"
            );
        return $"Validation Failed: {string.Join(string.Empty, arr)}";
    }

}

public class ValidationExceptionModel
{
    public string? Property { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}