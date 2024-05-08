namespace Application.SignalR;

public interface ISignalR
{
     Task SendMessage(string user, string message);
}