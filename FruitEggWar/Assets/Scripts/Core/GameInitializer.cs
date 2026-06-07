using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public GameObject[] managersToSpawn;

    void Awake()
    {
        foreach (var manager in managersToSpawn)
        {
            if (manager != null)
            {
                Instantiate(manager);
            }
        }
    }
}
