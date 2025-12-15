using Microsoft.AspNetCore.SignalR;

namespace WatchTogether3.Hubs;

public class VideoHub : Hub
{
    public VideoHub()
    {
        _ = 0;
    }

    public async Task EnterRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }

    public async Task LeaveRoom(string roomName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
    }

    public async Task Paused(string roomName, double time)
    {
        await Clients.OthersInGroup(roomName).SendAsync("PauseFromHub", time);
    }

    public async Task Proceeded(string roomName)
    {
        await Clients.OthersInGroup(roomName).SendAsync("ProceedFromHub");
    }

    public async Task Seeked(string roomName, double time)
    {
        await Clients.OthersInGroup(roomName).SendAsync("SeekFromHub", time);
    }

    public async Task VideoChanged(string roomName, string videoUrl)
    {
        await Clients.OthersInGroup(roomName).SendAsync("VideoChangedFromHub", videoUrl);
    }
}
