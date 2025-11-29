using Microsoft.AspNetCore.SignalR;

namespace WatchTogether3.Hubs;

public class VideoHub : Hub
{
    public async Task SendVideoTime(double time)
    {
        await Clients.Others.SendAsync("ReceiveVideoTime", time);
    }
}
