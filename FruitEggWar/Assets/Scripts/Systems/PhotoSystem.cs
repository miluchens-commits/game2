using UnityEngine;
using System.Collections.Generic;

public class PhotoSystem : MonoBehaviour
{
    public static PhotoSystem Instance { get; private set; }

    [System.Serializable]
    public class PhotoFrame
    {
        public string frameName;
        public Sprite frameSprite;
        public bool isUnlocked;
    }

    public List<PhotoFrame> availableFrames = new List<PhotoFrame>();
    public List<Texture2D> takenPhotos = new List<Texture2D>();

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

    public void TakePhoto()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private System.Collections.IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply();

        takenPhotos.Add(screenshot);
    }

    public void ApplyFrame(int photoIndex, int frameIndex)
    {
        if (photoIndex < takenPhotos.Count && frameIndex < availableFrames.Count)
        {
            PhotoFrame frame = availableFrames[frameIndex];
            if (frame.isUnlocked)
            {
                Debug.Log($"Applied frame {frame.frameName} to photo {photoIndex}");
            }
        }
    }

    public void CollectShell()
    {
        Debug.Log("Collected a shell on Okinawa beach!");
    }
}
