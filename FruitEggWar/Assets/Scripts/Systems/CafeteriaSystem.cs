using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CafeteriaSystem : MonoBehaviour
{
    public static CafeteriaSystem Instance { get; private set; }

    private List<GameObject> playersInCafe = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void EnterCafe(GameObject player)
    {
        if (!playersInCafe.Contains(player))
        {
            playersInCafe.Add(player);
        }
    }

    public void LeaveCafe(GameObject player)
    {
        playersInCafe.Remove(player);
    }

    public void SendChatMessage(string message, GameObject sender)
    {
        foreach (var player in playersInCafe)
        {
            if (player != sender)
            {
                Debug.Log($"{sender.name}: {message}");
            }
        }
    }

    public void FormParty(GameObject leader, GameObject member)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterPlayer(member);
            Debug.Log($"{member.name} joined {leader.name}'s party!");
        }
    }
}
