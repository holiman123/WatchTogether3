namespace WatchTogether3.Data;

public class RoomsParticipantsService : Dictionary<string, List<ApplicationUser?>>
{
    public void AddRoomParticipant(string roomName, ApplicationUser? user)
    {
        if (!this.ContainsKey(roomName))
        {
            this[roomName] = new List<ApplicationUser?>();
        }

        this[roomName].Add(user);
    }
}
