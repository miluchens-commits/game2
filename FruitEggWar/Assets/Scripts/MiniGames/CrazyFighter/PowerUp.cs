using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        Shield,
        SpeedBoost,
        DamageBoost
    }

    public PowerUpType powerUpType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FighterPlayer fighter = other.GetComponent<FighterPlayer>();
            if (fighter != null)
            {
                switch (powerUpType)
                {
                    case PowerUpType.Shield:
                        fighter.UseShield();
                        break;
                    case PowerUpType.SpeedBoost:
                        fighter.UseSpeedBoost();
                        break;
                    case PowerUpType.DamageBoost:
                        fighter.UseDamageBoost();
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}
