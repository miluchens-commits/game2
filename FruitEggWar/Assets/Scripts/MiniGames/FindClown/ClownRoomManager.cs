using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ClownRoomManager : MonoBehaviour
{
    public static ClownRoomManager Instance { get; private set; }

    [Header("Room Settings")]
    public int maxRoomSize = 10;
    public int maxRooms = 20;

    private Dictionary<int, ClownRoom> activeRooms = new Dictionary<int, ClownRoom>();
    private int nextRoomId = 1;

    public struct RoomCreationResult
    {
        public bool success;
        public int roomId;
        public string message;
    }

    void Awake()
    {
        Instance = this;
    }

    public RoomCreationResult CreateRoom(GameObject creator, int roomCardCount)
    {
        RoomCreationResult result = new RoomCreationResult();

        if (roomCardCount < 1)
        {
            result.success = false;
            result.message = "Not enough room cards!";
            return result;
        }

        if (activeRooms.Count >= maxRooms)
        {
            result.success = false;
            result.message = "Max rooms reached!";
            return result;
        }

        ClownRoom newRoom = new ClownRoom
        {
            roomId = nextRoomId,
            hostPlayer = creator,
            players = new List<GameObject> { creator },
            isGameStarted = false
        };

        activeRooms.Add(nextRoomId, newRoom);
        result.success = true;
        result.roomId = nextRoomId;
        result.message = $"Room {nextRoomId} created!";
        nextRoomId++;

        return result;
    }

    public bool JoinRoom(int roomId, GameObject player)
    {
        if (!activeRooms.ContainsKey(roomId)) return false;

        ClownRoom room = activeRooms[roomId];
        if (room.players.Count >= maxRoomSize)
        {
            Debug.Log("Room Full");
            return false;
        }

        if (room.isGameStarted) return false;

        room.players.Add(player);
        return true;
    }

    public void LeaveRoom(int roomId, GameObject player)
    {
        if (!activeRooms.ContainsKey(roomId)) return;

        ClownRoom room = activeRooms[roomId];
        room.players.Remove(player);

        if (room.players.Count == 0)
        {
            activeRooms.Remove(roomId);
        }
    }

    public ClownRoom GetRoom(int roomId)
    {
        return activeRooms.TryGetValue(roomId, out ClownRoom room) ? room : null;
    }

    public List<ClownRoom> GetAvailableRooms()
    {
        return activeRooms.Values.Where(r => !r.isGameStarted && r.players.Count < maxRoomSize).ToList();
    }
}

[System.Serializable]
public class ClownRoom
{
    public int roomId;
    public GameObject hostPlayer;
    public List<GameObject> players = new List<GameObject>();
    public bool isGameStarted = false;
    public int roomSize => players.Count;
}
