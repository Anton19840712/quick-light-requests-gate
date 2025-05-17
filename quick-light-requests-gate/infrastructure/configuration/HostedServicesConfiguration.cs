using infrastructure.services.background;

namespace infrastructure.configuration
{
	static class HostedServicesConfiguration
	{
		/// <summary>
		/// Регистрация фоновых сервисов приложения.
		/// </summary>
		public static IServiceCollection AddHostedServices(this IServiceCollection services)
		{
			services.AddHostedService<QueueListenerBackgroundService>();
			services.AddHostedService<OutboxMongoBackgroundService>();
			services.AddHostedService<NetworkServerHostedService>();

			return services;
		}
	}
}
