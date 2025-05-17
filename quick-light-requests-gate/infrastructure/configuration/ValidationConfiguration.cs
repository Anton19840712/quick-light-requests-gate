using application.interfaces.services;
using application.validation.common;
using domain.models.dynamicgatesettings.internalusage;
using FluentValidation;

namespace infrastructure.configuration
{
	static class ValidationConfiguration
	{
		/// <summary>
		/// Регистрация сервисов валидации.
		/// </summary>
		public static IServiceCollection AddValidationServices(this IServiceCollection services)
		{
			services.AddScoped<IServerInstanceFluentValidator, ServerInstanceFluentValidator>();
			services.AddScoped<IValidator<ServerInstanceModel>, ServerInstanceModelValidator>();

			return services;
		}
	}
}
