using Microsoft.AspNetCore.SignalR;

namespace WatchTogether3.Hubs;

public class VideoHub : Hub
{
    public VideoHub()
    {
        _ = 0;
    }

    public async Task EnterGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
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

    public async Task VideoChanged(string videoUrl)
    {
        await Clients.Others.SendAsync("VideoChangedFromHub", videoUrl);
    }
}
