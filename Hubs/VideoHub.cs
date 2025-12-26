using Microsoft.AspNetCore.SignalR;
using WatchTogether3.Data;

namespace WatchTogether3.Hubs;

public class VideoHub : Hub
{
    public VideoHub()
    {
        _ = 0;
    }

    public async Task EnterRoom(string roomName, string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        await Clients.OthersInGroup(roomName).SendAsync("UserJoinedFromHub", userId);
    }

    public async Task LeaveRoom(string roomName, string userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        await Clients.OthersInGroup(roomName).SendAsync("UserLeftFromHub", userId);
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

    public async Task VideoRemoved(string roomName, string videoUrl)
    {
        await Clients.OthersInGroup(roomName).SendAsync("VideoRemovedFromHub", videoUrl);
    }

    public async Task VideoUploaded(string roomName, string videoUrl)
    {
        await Clients.OthersInGroup(roomName).SendAsync("VideoUploadedFromHub", videoUrl);
    }
}
