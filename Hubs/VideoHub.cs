using Microsoft.AspNetCore.SignalR;

namespace WatchTogether3.Hubs;

public class VideoHub : Hub
{
    public VideoHub()
    {
        _ = 0;
    }

    public async Task SendVideoTime(double time)
    {
        await Clients.Others.SendAsync("ReceiveVideoTime", time);
    }

    public async Task Paused(double time)
    {
        await Clients.Others.SendAsync("VideoPaused", time);
    }

    public async Task Proceeded()
    {
        await Clients.Others.SendAsync("VideoProceeded");
    }
}
