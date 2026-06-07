using UnityEngine;

public class TouristGuide : MonoBehaviour
{
    [Header("Tour Settings")]
    public string[] tourStops;
    public int currentStop = 0;
    public bool tourCompleted = false;

    [Header("Reward")]
    public string memorialBadgeName = "Okinawa Memorial Badge";
    public bool hasReceivedBadge = false;

    public void StartTour()
    {
        currentStop = 0;
        tourCompleted = false;
        Debug.Log("Tour started! Follow the guide.");
    }

    public void NextStop()
    {
        currentStop++;
        if (currentStop >= tourStops.Length)
        {
            CompleteTour();
        }
    }

    private void CompleteTour()
    {
        tourCompleted = true;
        if (!hasReceivedBadge)
        {
            hasReceivedBadge = true;
            Debug.Log($"Reward: {memorialBadgeName} obtained!");
        }
    }
}
