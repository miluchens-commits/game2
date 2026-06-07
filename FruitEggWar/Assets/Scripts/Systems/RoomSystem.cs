using UnityEngine;
using System.Collections.Generic;

public class RoomSystem : MonoBehaviour
{
    public static RoomSystem Instance { get; private set; }

    [Header("Room Cards")]
    public int newPlayerRoomCards = 10;

    private int roomCards;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            roomCards = newPlayerRoomCards;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasRoomCards(int amount = 1)
    {
        return roomCards >= amount;
    }

    public bool UseRoomCard()
    {
        if (roomCards > 0)
        {
            roomCards--;
            return true;
        }
        return false;
    }

    public void AddRoomCards(int amount)
    {
        roomCards += amount;
    }

    public int GetRoomCards() => roomCards;

    public void RewardRoomCard()
    {
        AddRoomCards(1);
    }
}
