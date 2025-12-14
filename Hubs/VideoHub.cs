using Microsoft.AspNetCore.SignalR;

namespace WatchTogether3.Hubs;

public class VideoHub : Hub
{
    public VideoHub()
    {
        _ = 0;
    }

    public async Task Paused(double time)
    {
        await Clients.Others.SendAsync("PauseFromHub", time);
    }

    public async Task Proceeded()
    {
        await Clients.Others.SendAsync("ProceedFromHub");
    }

    public async Task Seeked(double time)
    {
        await Clients.Others.SendAsync("SeekFromHub", time);
    }
}
