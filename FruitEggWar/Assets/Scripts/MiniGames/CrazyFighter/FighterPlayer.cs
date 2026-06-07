using UnityEngine;

public class FighterPlayer : MonoBehaviour
{
    [Header("Combat")]
    public int damagePerShot = 1;
    public float fireRate = 0.5f;
    public float projectileSpeed = 20f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Shield")]
    public bool shieldActive = false;
    public int shieldBlocksRemaining = 3;
    public bool usedShield = false;

    [Header("Boosts")]
    public bool usedSpeedBoost = false;
    public bool usedDamageBoost = false;
    public float speedMultiplier = 1f;

    private float lastFireTime;
    private FighterPlayerData myData;
    private int playerId;

    void Start()
    {
        myData = GetComponent<FighterPlayerData>();
        if (myData != null)
        {
            playerId = myData.playerId;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > lastFireTime + fireRate)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.forward * projectileSpeed;
            }
            lastFireTime = Time.time;
        }
    }

    public void UseShield()
    {
        if (usedShield) return;
        usedShield = true;
        shieldActive = true;
        shieldBlocksRemaining = 3;
    }

    public void UseSpeedBoost()
    {
        if (usedSpeedBoost) return;
        usedSpeedBoost = true;
        speedMultiplier = 1.1f;
    }

    public void UseDamageBoost()
    {
        if (usedDamageBoost) return;
        usedDamageBoost = true;
        damagePerShot = 5;
    }

    public void TakeDamage(int damage)
    {
        if (shieldActive && shieldBlocksRemaining > 0)
        {
            shieldBlocksRemaining--;
            if (shieldBlocksRemaining <= 0)
            {
                shieldActive = false;
            }
            return;
        }

        if (myData != null)
        {
            myData.currentHP -= damage;
            if (myData.currentHP <= 0)
            {
                myData.currentHP = 0;
                myData.isAlive = false;
                gameObject.SetActive(false);
            }
        }
    }

    public FighterPlayerData GetData()
    {
        if (myData == null) myData = GetComponent<FighterPlayerData>();
        return myData;
    }
}
