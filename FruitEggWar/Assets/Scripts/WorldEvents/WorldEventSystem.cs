using UnityEngine;
using System.Collections;

public class WorldEventSystem : MonoBehaviour
{
    public static WorldEventSystem Instance { get; private set; }

    public enum WorldEventType
    {
        None,
        Tsunami,
        Earthquake,
        Volcano
    }

    public WorldEventType currentEvent = WorldEventType.None;
    public float eventIntervalMin = 120f;
    public float eventIntervalMax = 300f;

    [Header("Earthquake Settings")]
    public float earthquakeDuration = 3f;
    public float earthquakeIntensity = 0.3f;

    [Header("Tsunami Settings")]
    public float tsunamiForce = 20f;
    public float tsunamiDuration = 5f;

    [Header("Volcano Settings")]
    public GameObject rockPrefab;
    public float rockDropInterval = 0.5f;
    public int rockCount = 10;

    private Camera mainCamera;

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
        mainCamera = Camera.main;
        StartCoroutine(EventScheduler());
    }

    private IEnumerator EventScheduler()
    {
        while (true)
        {
            float waitTime = Random.Range(eventIntervalMin, eventIntervalMax);
            yield return new WaitForSeconds(waitTime);
            TriggerRandomEvent();
        }
    }

    public void TriggerRandomEvent()
    {
        WorldEventType[] events = { WorldEventType.Tsunami, WorldEventType.Earthquake, WorldEventType.Volcano };
        currentEvent = events[Random.Range(0, events.Length)];
        StartCoroutine(ExecuteEvent(currentEvent));
    }

    private IEnumerator ExecuteEvent(WorldEventType eventType)
    {
        switch (eventType)
        {
            case WorldEventType.Tsunami:
                yield return StartCoroutine(TsunamiEvent());
                break;
            case WorldEventType.Earthquake:
                yield return StartCoroutine(EarthquakeEvent());
                break;
            case WorldEventType.Volcano:
                yield return StartCoroutine(VolcanoEvent());
                break;
        }
        currentEvent = WorldEventType.None;
    }

    private IEnumerator TsunamiEvent()
    {
        float elapsed = 0f;
        while (elapsed < tsunamiDuration)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 force = (player.transform.position - Vector3.zero).normalized * tsunamiForce;
                    rb.AddForce(force, ForceMode.Impulse);
                }
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator EarthquakeEvent()
    {
        float elapsed = 0f;
        Vector3 originalPos = mainCamera.transform.position;

        while (elapsed < earthquakeDuration)
        {
            float offsetX = Random.Range(-earthquakeIntensity, earthquakeIntensity);
            float offsetY = Random.Range(-earthquakeIntensity, earthquakeIntensity);
            mainCamera.transform.position = originalPos + new Vector3(offsetX, offsetY, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
    }

    private IEnumerator VolcanoEvent()
    {
        for (int i = 0; i < rockCount; i++)
        {
            if (rockPrefab != null)
            {
                Vector3 randomPos = new Vector3(Random.Range(-15f, 15f), 20f, Random.Range(-15f, 15f));
                Instantiate(rockPrefab, randomPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(rockDropInterval);
        }
    }
}
