using infrastructure.messaging;

namespace infrastructure.configuration
{
	static class CommonConfiguration
	{
		/// <summary>
		/// Регистрация сервисов общего назначения.
		/// </summary>
		public static IServiceCollection AddCommonServices(this IServiceCollection services)
		{
			services.AddCors();
			services.AddScoped<ConnectionMessageSenderFactory>();

			return services;
		}
	}
}
