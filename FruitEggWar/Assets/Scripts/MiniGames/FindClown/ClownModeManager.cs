using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ClownModeManager : MonoBehaviour
{
    public static ClownModeManager Instance { get; private set; }

    [Header("Settings")]
    public int maxPlayers = 20;
    public int goodEggCount = 10;
    public int clownEggCount = 2;
    public int independentCount = 8;

    [Header("Mayor")]
    public bool mayorExists = false;

    [Header("Prophet")]
    public bool prophetExists = false;

    public enum Faction { GoodEgg, ClownEgg, Independent }

    private List<ClownPlayer> players = new List<ClownPlayer>();
    private bool isGameActive = false;
    private int predictedDeathCount = -1;

    void Awake()
    {
        Instance = this;
    }

    public void StartGame(List<GameObject> playerObjects)
    {
        if (playerObjects.Count < 3) return;

        players.Clear();
        int totalPlayers = Mathf.Min(playerObjects.Count, maxPlayers);

        for (int i = 0; i < totalPlayers; i++)
        {
            players.Add(new ClownPlayer
            {
                playerObject = playerObjects[i],
                playerId = i,
                isAlive = true,
                faction = Faction.GoodEgg
            });
        }

        AssignFactions();
        isGameActive = true;
    }

    private void AssignFactions()
    {
        List<ClownPlayer> allPlayers = new List<ClownPlayer>(players);

        int clownAssign = Mathf.Min(clownEggCount, allPlayers.Count / 4);
        int indieAssign = Mathf.Min(independentCount, allPlayers.Count - clownAssign - 1);

        for (int i = 0; i < clownAssign; i++)
        {
            int idx = Random.Range(0, allPlayers.Count);
            allPlayers[idx].faction = Faction.ClownEgg;
            allPlayers.RemoveAt(idx);
        }

        for (int i = 0; i < indieAssign; i++)
        {
            int idx = Random.Range(0, allPlayers.Count);
            allPlayers[idx].faction = Faction.Independent;
            allPlayers.RemoveAt(idx);
        }
    }

    public void AssignMayor(int playerId)
    {
        if (!mayorExists)
        {
            ClownPlayer player = players.FirstOrDefault(p => p.playerId == playerId);
            if (player != null)
            {
                player.isMayor = true;
                player.votesRemaining = 2;
                mayorExists = true;
            }
        }
    }

    public void AssignProphet(int playerId)
    {
        if (!prophetExists)
        {
            ClownPlayer player = players.FirstOrDefault(p => p.playerId == playerId);
            if (player != null)
            {
                player.isProphet = true;
                prophetExists = true;
            }
        }
    }

    public bool SubmitPrediction(int playerId, int predictedDeaths)
    {
        ClownPlayer player = players.FirstOrDefault(p => p.playerId == playerId);
        if (player != null && player.isProphet)
        {
            predictedDeathCount = predictedDeaths;
            return true;
        }
        return false;
    }

    public void OnPlayerEliminated(int playerId)
    {
        ClownPlayer player = players.FirstOrDefault(p => p.playerId == playerId);
        if (player != null)
        {
            player.isAlive = false;
        }

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        List<ClownPlayer> aliveGood = players.Where(p => p.isAlive && p.faction == Faction.GoodEgg).ToList();
        List<ClownPlayer> aliveClown = players.Where(p => p.isAlive && p.faction == Faction.ClownEgg).ToList();
        List<ClownPlayer> aliveIndie = players.Where(p => p.isAlive && p.faction == Faction.Independent).ToList();

        if (aliveClown.Count >= aliveGood.Count)
        {
            Debug.Log("Clown Eggs Win!");
            isGameActive = false;
        }
        else if (aliveClown.Count == 0)
        {
            Debug.Log("Good Eggs Win!");
            isGameActive = false;
        }

        foreach (var indie in aliveIndie)
        {
            if (indie.isProphet && indie.isAlive)
            {
                int actualDeaths = players.Count(p => !p.isAlive);
                if (predictedDeathCount == actualDeaths)
                {
                    Debug.Log($"Prophet {indie.playerId} wins independently!");
                }
            }
        }
    }

    public string GetPlayerFaction(int playerId)
    {
        ClownPlayer player = players.FirstOrDefault(p => p.playerId == playerId);
        return player?.faction.ToString() ?? "Unknown";
    }
}

[System.Serializable]
public class ClownPlayer
{
    public GameObject playerObject;
    public int playerId;
    public bool isAlive = true;
    public bool isMayor = false;
    public bool isProphet = false;
    public int votesRemaining = 1;
    public ClownModeManager.Faction faction;
}
