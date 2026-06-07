using UnityEngine;

public class AquariumViewer : MonoBehaviour
{
    public enum SeaCreature
    {
        WhaleShark,
        SeaTurtle,
        TropicalFish,
        Jellyfish
    }

    [Header("Creature Info")]
    public SeaCreature currentCreature;
    public GameObject creatureModel;

    [Header("Rewards")]
    public bool hasAquariumFrame = false;
    public bool hasWhaleSharkHat = false;

    public void ViewCreature(SeaCreature creature)
    {
        currentCreature = creature;
        Debug.Log($"Viewing: {creature}");
    }

    public void UnlockAquariumFrame()
    {
        hasAquariumFrame = true;
        if (BorderFrameSystem.Instance != null)
        {
            BorderFrameSystem.Instance.UnlockFrame(2);
        }
    }

    public void UnlockWhaleSharkHat()
    {
        hasWhaleSharkHat = true;
        Debug.Log("Unlocked Whale Shark Head Decoration!");
    }
}
