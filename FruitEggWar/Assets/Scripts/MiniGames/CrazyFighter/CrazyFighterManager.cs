using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CrazyFighterManager : MonoBehaviour
{
    public static CrazyFighterManager Instance { get; private set; }

    [Header("Settings")]
    public int maxPlayers = 15;
    public int defaultHP = 100;
    public float matchDuration = 480f;

    [Header("PowerUp Prefabs")]
    public GameObject shieldPickup;
    public GameObject speedPickup;
    public GameObject damagePickup;

    private List<FighterPlayerData> players = new List<FighterPlayerData>();
    private bool isMatchActive = false;

    void Awake()
    {
        Instance = this;
    }

    public void StartMatch(List<GameObject> playerObjects)
    {
        players.Clear();
        for (int i = 0; i < playerObjects.Count && i < maxPlayers; i++)
        {
            FighterPlayerData data = new FighterPlayerData
            {
                playerObject = playerObjects[i],
                playerId = i,
                currentHP = defaultHP,
                isAlive = true
            };
            players.Add(data);
        }

        isMatchActive = true;
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (isMatchActive)
        {
            yield return new WaitForSeconds(10f);
            SpawnRandomPowerUp();
        }
    }

    private void SpawnRandomPowerUp()
    {
        Vector3 randomPos = new Vector3(Random.Range(-20f, 20f), 1f, Random.Range(-20f, 20f));
        GameObject[] pickups = { shieldPickup, speedPickup, damagePickup };
        GameObject selected = pickups[Random.Range(0, pickups.Length)];

        if (selected != null)
        {
            Instantiate(selected, randomPos, Quaternion.identity);
        }
    }
}

[System.Serializable]
public class FighterPlayerData
{
    public GameObject playerObject;
    public int playerId;
    public int currentHP;
    public bool isAlive = true;
    public bool usedShield = false;
    public bool usedSpeedBoost = false;
    public bool usedDamageBoost = false;
    public bool shieldActive = false;
    public int shieldBlocksRemaining = 3;
    public float speedMultiplier = 1f;
    public int damagePerShot = 1;
}
