namespace application.messagesenders.sending.abstractions
{
	public interface IConnectionMessageSender
	{
		Task SendMessageAsync(string queueForListening, CancellationToken cancellationToken);
	}
}
