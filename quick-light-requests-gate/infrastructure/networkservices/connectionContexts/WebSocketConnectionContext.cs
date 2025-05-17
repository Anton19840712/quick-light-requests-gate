using System.Net.WebSockets;
using application.interfaces.networking;

namespace infrastructure.networkservices.connectionContexts
{
	public class WebSocketConnectionContext : IConnectionContext
	{
		public WebSocket Socket { get; }

		public string Protocol => "websocket";

		public WebSocketConnectionContext(WebSocket socket) => Socket = socket;
	}
}


