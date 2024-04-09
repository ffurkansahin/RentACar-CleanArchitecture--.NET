using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.CroosCuttingConcerns.Exceptions.Extensions;

public static class ProblemDetailsExtensions
{
	public static string AsJson<TProblemDetail>(this TProblemDetail details)
		where TProblemDetail : ProblemDetails => JsonSerializer.Serialize(details);
}
