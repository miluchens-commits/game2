using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BuildSettingsSetup
{
    [MenuItem("FruitEggWar/Setup Build Settings")]
    public static void SetupBuildScenes()
    {
        string[] scenePaths = new string[]
        {
            "Assets/Scenes/FruitIsland.unity",
            "Assets/Scenes/AbbyTown.unity",
            "Assets/Scenes/SpaceAdventure.unity",
            "Assets/Scenes/OkinawaIsland.unity",
            "Assets/Scenes/BombMode.unity",
            "Assets/Scenes/CrazyFighter.unity",
            "Assets/Scenes/ClownMode.unity"
        };

        EditorBuildSettingsScene[] buildScenes = new EditorBuildSettingsScene[scenePaths.Length];
        for (int i = 0; i < scenePaths.Length; i++)
        {
            buildScenes[i] = new EditorBuildSettingsScene(scenePaths[i], true);
        }
        EditorBuildSettings.scenes = buildScenes;
        Debug.Log("Build settings updated with all scenes!");
    }
}
