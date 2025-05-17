﻿namespace servers_api.api.streaming.core
{
	//1
	public class NetworkServerHostedService : BackgroundService
	{
		private readonly ILogger<NetworkServerHostedService> _logger;
		private readonly NetworkServerManager _serverManager;
		private readonly NetworkClientManager _clientManager;
		private readonly IConfiguration _configuration;

		public NetworkServerHostedService(
			ILogger<NetworkServerHostedService> logger,
			NetworkServerManager serverManager,
			NetworkClientManager clientManager,
			IConfiguration configuration)
		{
			_logger = logger;
			_serverManager = serverManager;
			_clientManager = clientManager;
			_configuration = configuration;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			// читаем из конфигурации параметр протокола:
			var protocol = _configuration["Protocol"]?.ToLowerInvariant();
			var mode = _configuration["Mode"]?.ToLowerInvariant(); // client или server

			if (string.IsNullOrEmpty(protocol) || string.IsNullOrEmpty(mode))
			{
				_logger.LogWarning("Протокол или режим запуска не указан.");
				return;
			}

			switch (mode)
			{
				case "server":
					_logger.LogInformation($"Автозапуск сервера: {protocol}");

					// передаем название протокола:
					await _serverManager.StartServerAsync(protocol, stoppingToken);
					break;

				case "client":
					_logger.LogInformation($"Автозапуск клиента: {protocol}");
					await _clientManager.StartClientAsync(protocol, stoppingToken);
					break;

				default:
					_logger.LogWarning($"Неизвестный режим запуска: {mode}");
					break;
			}
		}
	}
}
