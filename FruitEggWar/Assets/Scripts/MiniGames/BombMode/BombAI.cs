using UnityEngine;
using System.Collections.Generic;

public class BombAI : MonoBehaviour
{
    private float detectionRadius = 10f;
    private float moveSpeed = 3.5f;

    void Update()
    {
        if (BombModeManager.Instance == null) return;

        BombPlayer myBp = BombModeManager.Instance.GetPlayerBombStatus(GetComponent<BombPlayer>()?.playerId ?? -1);
        if (myBp == null || !myBp.hasBomb) return;

        GameObject nearestPlayer = FindNearestPlayer();
        if (nearestPlayer != null)
        {
            Vector3 direction = (nearestPlayer.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, nearestPlayer.transform.position);
            if (distance < 1.5f)
            {
                BombPlayer targetBp = nearestPlayer.GetComponent<BombPlayer>();
                BombPlayer myBpFull = GetComponent<BombPlayer>();
                if (targetBp != null && myBpFull != null)
                {
                    BombModeManager.Instance.TransferBomb(myBpFull, targetBp);
                }
            }
        }
    }

    private GameObject FindNearestPlayer()
    {
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var col in colliders)
        {
            if (col.gameObject != gameObject && col.CompareTag("Player"))
            {
                BombPlayer bp = col.GetComponent<BombPlayer>();
                if (bp != null && bp.isAlive && !bp.hasBomb)
                {
                    float dist = Vector3.Distance(transform.position, col.transform.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearest = col.gameObject;
                    }
                }
            }
        }
        return nearest;
    }
}
