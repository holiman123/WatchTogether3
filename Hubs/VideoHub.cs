using Microsoft.AspNetCore.SignalR;
using WatchTogether3.Data;

namespace WatchTogether3.Hubs;

public class VideoHub : Hub
{
    public VideoHub()
    {
        _ = 0;
    }

    public async Task EnterRoom(int roomId, string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"room:{roomId}");
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("UserJoinedFromHub", userId);
    }

    public async Task LeaveRoom(int roomId, string userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room:{roomId}");
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("UserLeftFromHub", userId);
    }

    public async Task Paused(int roomId, double time)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("PauseFromHub", time);
    }

    public async Task Proceeded(int roomId)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("ProceedFromHub");
    }

    public async Task Seeked(int roomId, double time)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("SeekFromHub", time);
    }

    public async Task VideoChanged(int roomId, VideoFile video)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("VideoChangedFromHub", video);
    }

    public async Task VideoRemoved(int roomId, VideoFile video)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("VideoRemovedFromHub", video);
    }

    public async Task VideoUploaded(int roomId, VideoFile video)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("VideoUploadedFromHub", video);
    }
}
