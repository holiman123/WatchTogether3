using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

    public async Task RoomRemoved(int roomId)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("RoomRemovedFromHub");
    }

    public async Task Paused(int roomId, double time)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("PauseFromHub", time);
    }

    public async Task Proceeded(int roomId, double time)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("ProceedFromHub", time);
    }

    public async Task RoomDataChanged(int roomId, Room roomData)
    {
        await Clients.OthersInGroup($"room:{roomId}").SendAsync("RoomDataChangedFromHub", roomData);
    }
}
