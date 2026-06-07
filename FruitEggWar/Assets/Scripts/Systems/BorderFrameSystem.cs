using UnityEngine;

public class BorderFrameSystem : MonoBehaviour
{
    public static BorderFrameSystem Instance { get; private set; }

    [System.Serializable]
    public class BorderFrame
    {
        public string name;
        public int id;
        public Sprite frameSprite;
        public bool isUnlocked;
    }

    public BorderFrame[] availableFrames;
    private BorderFrame currentFrame;

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

    public bool UnlockFrame(int frameId)
    {
        foreach (var frame in availableFrames)
        {
            if (frame.id == frameId && !frame.isUnlocked)
            {
                frame.isUnlocked = true;
                return true;
            }
        }
        return false;
    }

    public void EquipFrame(int frameId)
    {
        foreach (var frame in availableFrames)
        {
            if (frame.id == frameId && frame.isUnlocked)
            {
                currentFrame = frame;
                break;
            }
        }
    }

    public Sprite GetCurrentFrameSprite()
    {
        return currentFrame?.frameSprite;
    }
}
