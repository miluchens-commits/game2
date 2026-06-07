using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    [Header("Target")]
    public string targetSceneName;
    public int teleportPointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.gameObject);
        }
    }

    public void TeleportPlayer(GameObject player)
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            GameManager.Instance.TeleportToIsland(targetSceneName);
        }
    }
}
