using Microsoft.AspNetCore.SignalR;

namespace Core.SignalR;
public class CommunicationHub: Hub
{
    // public async Task SendMessage(string user, string message)
    // {
    //     await Clients.All.SendAsync("ReceiveMessage", user, message);
    // }
    // public async Task Broadcast(string message)
    // {
    //     //"ReceiveMessage" 是客户端注册的接收消息的回调函数的名字
    //     await Clients.All.SendAsync("ReceiveMessage", message);
    // }
}