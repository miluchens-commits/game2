using UnityEngine;

public class IslandBase : MonoBehaviour
{
    [Header("Island Info")]
    public string islandName;
    public string sceneName;
    [TextArea] public string description;

    [Header("Spawn Point")]
    public Transform playerSpawnPoint;

    [Header("Teleport Points")]
    public Transform[] teleportDestinations;

    public virtual void OnPlayerEnter(GameObject player)
    {
        if (playerSpawnPoint != null)
        {
            player.transform.position = playerSpawnPoint.position;
        }
    }

    public virtual void OnPlayerExit(GameObject player) { }
}
