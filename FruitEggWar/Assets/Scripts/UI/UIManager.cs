using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Currency UI")]
    public GameObject fruitCoinDisplay;
    public GameObject sparkleCoinDisplay;

    [Header("MiniGame UI")]
    public GameObject bombModeHUD;
    public GameObject crazyFighterHUD;
    public GameObject clownModeHUD;

    [Header("Room UI")]
    public GameObject roomCardDisplay;
    public GameObject roomFullWarning;

    [Header("Message Panel")]
    public GameObject messagePanel;

    private string cachedFruitCoinText = "Fruit Coins: 0";
    private string cachedSparkleCoinText = "Sparkle Coins: 0";

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

    void Update()
    {
        UpdateCurrencyDisplay();
    }

    private void UpdateCurrencyDisplay()
    {
        if (CurrencyManager.Instance != null)
        {
            cachedFruitCoinText = $"Fruit Coins: {CurrencyManager.Instance.GetCurrency(CurrencyType.FruitCoin)}";
            cachedSparkleCoinText = $"Sparkle Coins: {CurrencyManager.Instance.GetCurrency(CurrencyType.SparkleCoin)}";
        }
    }

    public void ShowMessage(string msg, float duration = 2f)
    {
        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
            Invoke(nameof(HideMessage), duration);
        }
        Debug.Log(msg);
    }

    private void HideMessage()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    public void ShowRoomFull()
    {
        if (roomFullWarning != null)
        {
            roomFullWarning.SetActive(true);
            Invoke(nameof(HideRoomFull), 2f);
        }
    }

    private void HideRoomFull()
    {
        if (roomFullWarning != null)
            roomFullWarning.SetActive(false);
    }

    public string GetFruitCoinText() => cachedFruitCoinText;
    public string GetSparkleCoinText() => cachedSparkleCoinText;
}
