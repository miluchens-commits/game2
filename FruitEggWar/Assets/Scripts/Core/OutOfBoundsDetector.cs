using UnityEngine;

public class OutOfBoundsDetector : MonoBehaviour
{
    [Header("Bounds")]
    public float minX = -50f;
    public float maxX = 50f;
    public float minZ = -50f;
    public float maxZ = 50f;
    public float fallThreshold = -10f;

    [Header("Respawn")]
    public Transform respawnPoint;

    void Update()
    {
        Vector3 pos = transform.position;

        bool outOfBounds = pos.x < minX || pos.x > maxX ||
                          pos.z < minZ || pos.z > maxZ ||
                          pos.y < fallThreshold;

        if (outOfBounds)
        {
            ReturnToSpawn();
        }
    }

    private void ReturnToSpawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            transform.position = Vector3.zero;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
