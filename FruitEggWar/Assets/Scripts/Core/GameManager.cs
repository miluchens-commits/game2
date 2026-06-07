using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameState
{
    Login,
    Exploring,
    MiniGame,
    Casual
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Exploring;

    [Header("Player Settings")]
    public int maxPartySize = 4;
    public List<GameObject> players = new List<GameObject>();

    [Header("Scene Names")]
    public string mainIslandScene = "FruitIsland";
    public string townScene = "AbbyTown";
    public string spaceIslandScene = "SpaceAdventure";
    public string okinawaScene = "OkinawaIsland";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;
    }

    public void TeleportToIsland(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private System.Collections.IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public void RegisterPlayer(GameObject player)
    {
        if (!players.Contains(player) && players.Count < maxPartySize)
        {
            players.Add(player);
        }
    }

    public void RemovePlayer(GameObject player)
    {
        players.Remove(player);
    }
}
