using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BombModeManager : MonoBehaviour
{
    public static BombModeManager Instance { get; private set; }

    [Header("Settings")]
    public int maxPlayers = 24;
    public float bombTimer = 15f;
    public float matchDuration = 300f;

    [Header("References")]
    public GameObject bombPrefab;
    public Transform[] spawnPoints;

    private List<BombPlayer> players = new List<BombPlayer>();
    private Bomb currentBomb;
    private int unluckyPlayerIndex = -1;
    private bool isMatchActive = false;

    void Awake()
    {
        Instance = this;
    }

    public void StartMatch(List<GameObject> playerObjects)
    {
        if (playerObjects.Count < 2) return;

        players.Clear();
        for (int i = 0; i < playerObjects.Count && i < maxPlayers; i++)
        {
            BombPlayer bp = new BombPlayer
            {
                playerObject = playerObjects[i],
                isAlive = true,
                playerId = i
            };
            players.Add(bp);

            if (i < spawnPoints.Length)
            {
                playerObjects[i].transform.position = spawnPoints[i].position;
            }
        }

        isMatchActive = true;
        StartCoroutine(MatchLoop());
    }

    private IEnumerator MatchLoop()
    {
        yield return new WaitForSeconds(3f);

        SelectUnluckyPlayer();
        StartBombTimer();

        while (isMatchActive)
        {
            yield return null;
            CheckMatchEnd();
        }
    }

    private void SelectUnluckyPlayer()
    {
        List<BombPlayer> alivePlayers = players.Where(p => p.isAlive).ToList();
        if (alivePlayers.Count == 0) return;

        unluckyPlayerIndex = Random.Range(0, alivePlayers.Count);
        BombPlayer unlucky = alivePlayers[unluckyPlayerIndex];

        if (bombPrefab != null)
        {
            GameObject bombObj = Instantiate(bombPrefab, unlucky.playerObject.transform.position, Quaternion.identity);
            currentBomb = bombObj.GetComponent<Bomb>();
            if (currentBomb != null)
            {
                currentBomb.Initialize(unlucky, bombTimer);
                unlucky.hasBomb = true;
            }
        }
    }

    private void StartBombTimer()
    {
        if (currentBomb != null)
        {
            currentBomb.StartTimer();
        }
    }

    public void TransferBomb(BombPlayer fromPlayer, BombPlayer toPlayer)
    {
        if (fromPlayer == null || toPlayer == null || !isMatchActive) return;

        fromPlayer.hasBomb = false;
        toPlayer.hasBomb = true;

        if (currentBomb != null)
        {
            currentBomb.TransferTo(toPlayer);
            currentBomb.ResetTimer(bombTimer);
        }
    }

    public void OnBombExploded(BombPlayer player)
    {
        if (player != null)
        {
            player.isAlive = false;
            player.hasBomb = false;
        }

        CheckMatchEnd();

        if (isMatchActive)
        {
            SelectUnluckyPlayer();
            StartBombTimer();
        }
    }

    private void CheckMatchEnd()
    {
        List<BombPlayer> alivePlayers = players.Where(p => p.isAlive).ToList();

        if (alivePlayers.Count <= 1)
        {
            isMatchActive = false;
            if (alivePlayers.Count == 1)
            {
                OnMatchEnd(alivePlayers[0]);
            }
        }
    }

    private void OnMatchEnd(BombPlayer winner)
    {
        Debug.Log($"Bomb Mode Winner: Player {winner.playerId}");
    }

    public BombPlayer GetPlayerBombStatus(int playerId)
    {
        return players.FirstOrDefault(p => p.playerId == playerId);
    }
}

[System.Serializable]
public class BombPlayer
{
    public GameObject playerObject;
    public int playerId;
    public bool isAlive = true;
    public bool hasBomb = false;
}
