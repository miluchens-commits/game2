using UnityEngine;
using System.Collections.Generic;

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem Instance { get; private set; }

    [System.Serializable]
    public class AchievementTier
    {
        public string name;
        public int requiredCount;
        public string displayName;
    }

    public AchievementTier[] tiers = new AchievementTier[]
    {
        new AchievementTier { name = "Novice", requiredCount = 10, displayName = "Novice" },
        new AchievementTier { name = "Intermediate", requiredCount = 30, displayName = "Intermediate" },
        new AchievementTier { name = "Advanced", requiredCount = 50, displayName = "Advanced" },
        new AchievementTier { name = "Hard", requiredCount = 100, displayName = "Hard" },
        new AchievementTier { name = "SuperUltra", requiredCount = 180, displayName = "Super Ultimate" }
    };

    [System.Serializable]
    public class Achievement
    {
        public string id;
        public string title;
        public string description;
        public Sprite icon;
        public bool isUnlocked;
        public int fruitCoinReward;
    }

    public List<Achievement> achievements = new List<Achievement>();
    private int totalUnlocked => achievements.FindAll(a => a.isUnlocked).Count;

    public bool UnlockAchievement(string id)
    {
        Achievement achievement = achievements.Find(a => a.id == id);
        if (achievement != null && !achievement.isUnlocked)
        {
            achievement.isUnlocked = true;
            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.AddCurrency(CurrencyType.FruitCoin, achievement.fruitCoinReward);
            }
            CheckTierProgress();
            return true;
        }
        return false;
    }

    private void CheckTierProgress()
    {
        int unlocked = totalUnlocked;
        foreach (var tier in tiers)
        {
            if (unlocked >= tier.requiredCount)
            {
                Debug.Log($"Achievement Tier Reached: {tier.displayName}");
            }
        }
    }

    public string GetCurrentTier()
    {
        int unlocked = totalUnlocked;
        string currentTier = tiers[0].displayName;
        foreach (var tier in tiers)
        {
            if (unlocked >= tier.requiredCount)
            {
                currentTier = tier.displayName;
            }
        }
        return currentTier;
    }

    public int GetTotalUnlocked() => totalUnlocked;
    public int GetTotalAchievements() => achievements.Count;
}
