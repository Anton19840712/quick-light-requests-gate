﻿using servers_api.api.streaming.connectionContexts;

namespace application.messagesenders.sending.main
{
	/// <summary>
	/// Отсылает сообщение на внешний клиент.
	/// </summary>
	public interface IMessageSender
	{
		Task SendMessagesToClientAsync(
			IConnectionContext connectionContext,
			string queueForListening,
			CancellationToken cancellationToken);
	}
}
