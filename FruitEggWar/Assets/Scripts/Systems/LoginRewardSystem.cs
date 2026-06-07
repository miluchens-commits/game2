using UnityEngine;
using System.Collections;

public class LoginRewardSystem : MonoBehaviour
{
    public static LoginRewardSystem Instance { get; private set; }

    [Header("Daily Rewards")]
    public int fruitCoinReward = 50;
    public float checkInterval = 1f;

    private string lastLoginDateKey = "LastLoginDate";
    private bool hasClaimedToday = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CheckDailyLogin();
    }

    private void CheckDailyLogin()
    {
        string lastDate = PlayerPrefs.GetString(lastLoginDateKey, "");
        string today = System.DateTime.Now.ToString("yyyy-MM-dd");

        if (lastDate != today)
        {
            ClaimDailyReward();
            PlayerPrefs.SetString(lastLoginDateKey, today);
            PlayerPrefs.Save();
        }
    }

    private void ClaimDailyReward()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.RewardDailyLogin();
            Debug.Log($"Daily Login Reward: {fruitCoinReward} Fruit Coins!");
        }
    }
}
