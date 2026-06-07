using UnityEngine;
using System.Collections.Generic;

public class EasterEggCollector : MonoBehaviour
{
    private List<string> collectedShells = new List<string>();

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shell"))
        {
            string shellName = other.gameObject.name;
            if (!collectedShells.Contains(shellName))
            {
                collectedShells.Add(shellName);
                Destroy(other.gameObject);
                Debug.Log($"Shell collected: {shellName}");
            }
        }
        else if (other.CompareTag("Souvenir"))
        {
            Debug.Log($"Souvenir found: {other.gameObject.name}");
            Destroy(other.gameObject);
        }
    }

    public int GetShellCount() => collectedShells.Count;
}
