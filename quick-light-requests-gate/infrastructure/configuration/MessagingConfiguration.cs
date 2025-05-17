using application.interfaces.services;
using infrastructure.services;
using infrastructure.services.processing;

namespace infrastructure.configuration
{
	static class MessagingConfiguration
	{
		/// <summary>
		/// Регистрация сервисов, участвующих в отсылке и получении сообщений.
		/// </summary>
		public static IServiceCollection AddMessageServingServices(this IServiceCollection services)
		{
			services.AddScoped<IMessageSender, MessageSender>();
			services.AddTransient<IMessageFormatter, MessageFormatter>();
			services.AddTransient<IMessageProcessingService, MessageProcessingService>();

			return services;
		}
	}
}
