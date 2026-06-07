using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    [Header("Visual")]
    public GameObject explosionEffect;
    public float explosionRadius = 3f;

    private BombPlayer currentHolder;
    private float timerDuration;
    private float currentTime;
    private bool isActive = false;

    public void Initialize(BombPlayer holder, float duration)
    {
        currentHolder = holder;
        timerDuration = duration;
        currentTime = duration;
    }

    public void StartTimer()
    {
        isActive = true;
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (isActive && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        if (isActive)
        {
            Explode();
        }
    }

    public void ResetTimer(float newDuration)
    {
        currentTime = newDuration;
        timerDuration = newDuration;
    }

    public void TransferTo(BombPlayer newHolder)
    {
        currentHolder = newHolder;
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Player") && currentHolder != null)
            {
                BombPlayer bp = hit.GetComponent<BombPlayer>();
                if (bp != null && bp == currentHolder)
                {
                    BombModeManager.Instance.OnBombExploded(currentHolder);
                }
            }
        }

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        isActive = false;
        StopAllCoroutines();
    }
}
