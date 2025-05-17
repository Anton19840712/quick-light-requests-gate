using application.messagesenders.sending.abstractions;
using infrastructure.configuration.middleware;
using Serilog;
using servers_api.api.streaming.clients;
using servers_api.api.streaming.core;
using servers_api.api.streaming.servers;

Console.Title = "integration api";

var builder = WebApplication.CreateBuilder(args);

LoggingConfiguration.ConfigureLogging(builder);

ConfigureServices(builder);

var app = builder.Build();

try
{
	// ��������� ������������� ����� (����� ������������������ ������)
	var gateConfigurator = app.Services.GetRequiredService<GateConfiguration>();
	var (httpUrl, httpsUrl) = await gateConfigurator.ConfigureDynamicGateAsync(args, builder);

	// ��������� ��������� ����������
	ConfigureApp(app, httpUrl, httpsUrl);

	// ���������
	await app.RunAsync();
}
catch (Exception ex)
{
	Log.Fatal(ex, "����������� ������ ��� ������� ����������");
	throw;
}
finally
{
	Log.CloseAndFlush();
}

static void ConfigureServices(WebApplicationBuilder builder)
{
	
	var configuration = builder.Configuration;

	var services = builder.Services;
	services.AddSingleton<INetworkServer, TcpNetworkServer>();
	services.AddSingleton<INetworkClient, TcpNetworkClient>();

	services.AddSingleton<INetworkServer, UdpNetworkServer>();
	services.AddSingleton<INetworkClient, UdpNetworkClient>();

	services.AddSingleton<INetworkServer, WebSocketNetworkServer>();
	services.AddSingleton<INetworkClient, WebSocketNetworkClient>();

	services.AddSingleton<NetworkServerManager>();
	services.AddSingleton<NetworkClientManager>();

	services.AddHostedService<NetworkServerHostedService>();
	services.AddScoped<ConnectionMessageSenderFactory>();

	services.AddControllers();

	services.AddCommonServices();
	services.AddHttpServices();
	services.AddRabbitMqServices(configuration);
	services.AddMessageServingServices();
	services.AddMongoDbServices(configuration);
	services.AddMongoDbRepositoriesServices(configuration);
	services.AddValidationServices();
	services.AddHostedServices();

	// ������������ GateConfiguration
	services.AddSingleton<GateConfiguration>();
}

static void ConfigureApp(WebApplication app, string httpUrl, string httpsUrl)
{
	try
	{
		if (!string.IsNullOrEmpty(httpUrl))
			app.Urls.Add(httpUrl);

		if (!string.IsNullOrEmpty(httpsUrl))
			app.Urls.Add(httpsUrl);

		Log.Information($"���������� �������:");
		if (!string.IsNullOrEmpty(httpUrl))
			Log.Information($"[HTTP] {httpUrl}");

		if (!string.IsNullOrEmpty(httpsUrl))
			Log.Information($"[HTTPS] {httpsUrl}");

		app.UseSerilogRequestLogging();

		app.UseCors(cors => cors
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());

		app.MapControllers();
	}
	catch (Exception ex)
	{
		Log.Error(ex, "������ ��� ��������� ���������� (��������, �������� � SSL-������������)");
	}
}
