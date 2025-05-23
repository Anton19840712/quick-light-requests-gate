﻿using application.interfaces.messaging;
using domain.models.configurationsettings;
using infrastructure.messaging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Serilog;

namespace infrastructure.configuration
{
	public static class RabbitConfiguration
	{
		public static IServiceCollection AddRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Регистрируем настройки
			services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMqSettings"));

			// Регистрируем IConnectionFactory на основе Uri
			services.AddSingleton<IConnectionFactory>(provider =>
			{
				var rabbitMqSettings = provider.GetRequiredService<IOptions<RabbitMqSettings>>().Value;

				if (string.IsNullOrWhiteSpace(rabbitMqSettings.HostName) ||
					rabbitMqSettings.Port == 0 ||
					string.IsNullOrWhiteSpace(rabbitMqSettings.UserName) ||
					string.IsNullOrWhiteSpace(rabbitMqSettings.Password))
				{
					throw new InvalidOperationException("Некорректные настройки RabbitMQ! Проверьте конфигурацию.");
				}

				var factory = new ConnectionFactory
				{
					Uri = rabbitMqSettings.GetAmqpUri()
				};

				Log.Information("RabbitMQ настроен: {Uri}", factory.Uri);
				return factory;
			});

			// Регистрируем сервисы
			services.AddSingleton<IRabbitMqService, RabbitMqService>();
			services.AddSingleton<IRabbitMqQueueListener<RabbitMqQueueListener>, RabbitMqQueueListener>();

			return services;
		}
	}
}
