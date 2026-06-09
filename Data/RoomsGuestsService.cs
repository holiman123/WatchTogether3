namespace WatchTogether3.Data;

public class RoomsGuestsService : Dictionary<int, List<string>>
{
    public int GetRoomGuestCount(int roomId)
    {
        if (this.ContainsKey(roomId))
        {
            return this[roomId].Count;
        }

        return 0;
    }

    public List<string> GetGuests(int roomId)
    {
        if (this.ContainsKey(roomId))
        {
            return this[roomId];
        }

        return new List<string>();
    }

    public void AddRoomGuest(int roomId, string guestId)
    {
        if (!this.ContainsKey(roomId))
        {
            this[roomId] = new List<string>();
        }

        this[roomId].Add(guestId);
    }

    public void RemoveRoomGuest(int roomId, string guestId)
    {
        if (this.ContainsKey(roomId))
        {
            this[roomId].Remove(guestId);
        }
    }
}
