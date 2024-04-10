using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transactions;
using Core.Application.Pipelines.Validations;
using Core.Application.Rules;
using FluentValidation;
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
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

		services.AddMediatR(configuration =>
		{
			configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

			configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
			configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
			configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
		});

		return services;
	}
	public static IServiceCollection AddSubClassesOfType(
	   this IServiceCollection services,
	   Assembly assembly,
	   Type type,
	   Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
   )
	{
		var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
		foreach (var item in types)
			if (addWithLifeCycle == null)
				services.AddScoped(item);

			else
				addWithLifeCycle(services, type);
		return services;
	}

}
