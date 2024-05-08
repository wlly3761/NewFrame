using Core.Attribute;
using Core.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Application.SignalR;
[DynamicApi(ServiceLifeCycle = "Transient")]
public class SignalR:ISignalR
{
    private readonly IHubContext<CommunicationHub> _hubContext;

    public SignalR(IHubContext<CommunicationHub> hubContext)
    {
        _hubContext = hubContext;
    }
        
    public async Task SendMessage(string user, string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}