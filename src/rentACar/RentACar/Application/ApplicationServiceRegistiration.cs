using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ApplicationServiceRegistiration
{
	public static IServiceCollection AddAplicationServices(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(configuration =>
		{
			configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
		});
		return services;
	}
}
