using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance { get; private set; }

    public GameObject loadingPanel;
    public GameObject progressBar;

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

    public void ShowLoadingScreen()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(false);
    }

    public void UpdateProgress(float progress)
    {
        if (progressBar != null)
            progressBar.transform.localScale = new Vector3(progress, 1, 1);
    }
}
