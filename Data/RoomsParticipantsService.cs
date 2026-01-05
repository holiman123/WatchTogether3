namespace WatchTogether3.Data;

public class RoomsParticipantsService : Dictionary<int, List<ApplicationUser?>>
{
    public void AddRoomParticipant(int roomId, ApplicationUser? user)
    {
        if (!this.ContainsKey(roomId))
        {
            this[roomId] = new List<ApplicationUser?>();
        }

        this[roomId].Add(user);
    }
}
