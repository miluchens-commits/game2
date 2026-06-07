using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public static IslandManager Instance { get; private set; }

    [System.Serializable]
    public class IslandEntry
    {
        public string islandName;
        public string sceneName;
        public string description;
        public Sprite islandIcon;
        public bool isUnlocked = true;
    }

    public IslandEntry[] islands;

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

    public void TeleportToIsland(string islandName)
    {
        foreach (var island in islands)
        {
            if (island.islandName == islandName && island.isUnlocked)
            {
                GameManager.Instance.TeleportToIsland(island.sceneName);
                return;
            }
        }
    }

    public IslandEntry[] GetUnlockedIslands()
    {
        return System.Array.FindAll(islands, i => i.isUnlocked);
    }
}
