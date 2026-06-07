using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneSetup
{
    [MenuItem("FruitEggWar/Setup Initial Scene")]
    public static void SetupInitialScene()
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
        scene.name = "FruitIsland";
        EditorSceneManager.SetActiveScene(scene);

        GameObject managersGO = new GameObject("[Managers]");
        managersGO.tag = "GameController";

        managersGO.AddComponent<GameManager>();

        GameObject currencyGO = new GameObject("CurrencyManager");
        currencyGO.transform.parent = managersGO.transform;
        currencyGO.AddComponent<CurrencyManager>();

        GameObject roomGO = new GameObject("RoomSystem");
        roomGO.transform.parent = managersGO.transform;
        roomGO.AddComponent<RoomSystem>();

        GameObject achievementGO = new GameObject("AchievementSystem");
        achievementGO.transform.parent = managersGO.transform;
        achievementGO.AddComponent<AchievementSystem>();

        GameObject borderGO = new GameObject("BorderFrameSystem");
        borderGO.transform.parent = managersGO.transform;
        borderGO.AddComponent<BorderFrameSystem>();

        GameObject gachaGO = new GameObject("GachaSystem");
        gachaGO.transform.parent = managersGO.transform;
        gachaGO.AddComponent<GachaSystem>();

        GameObject worldEventGO = new GameObject("WorldEventSystem");
        worldEventGO.transform.parent = managersGO.transform;
        worldEventGO.AddComponent<WorldEventSystem>();

        GameObject soundGO = new GameObject("SoundManager");
        soundGO.transform.parent = managersGO.transform;
        soundGO.AddComponent<SoundManager>();

        GameObject loginGO = new GameObject("LoginRewardSystem");
        loginGO.transform.parent = managersGO.transform;
        loginGO.AddComponent<LoginRewardSystem>();

        GameObject cafeGO = new GameObject("CafeteriaSystem");
        cafeGO.transform.parent = managersGO.transform;
        cafeGO.AddComponent<CafeteriaSystem>();

        GameObject photoGO = new GameObject("PhotoSystem");
        photoGO.transform.parent = managersGO.transform;
        photoGO.AddComponent<PhotoSystem>();

        GameObject isLoading = new GameObject("LoadingScreen");
        isLoading.transform.parent = managersGO.transform;
        isLoading.AddComponent<LoadingScreen>();

        GameObject ui = new GameObject("UIManager");
        ui.transform.parent = managersGO.transform;
        ui.AddComponent<UIManager>();

        GameObject islandManager = new GameObject("IslandManager");
        islandManager.transform.parent = managersGO.transform;
        islandManager.AddComponent<IslandManager>();

        SetupPlayer(scene);

        SetupGround(scene);

        SetupTeleportPoints(scene);

        SetupAquarium(scene);

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/FruitIsland.unity");

        CreateClownModeScene();
        CreateBombModeScene();
        CreateCrazyFighterScene();
        CreateOkinawaScene();

        AssetDatabase.Refresh();
        Debug.Log("FruitEggWar scene setup complete!");
    }

    static void SetupPlayer(Scene scene)
    {
        GameObject egg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        egg.name = "PlayerEgg";
        egg.tag = "Player";
        egg.transform.position = Vector3.zero;
        egg.transform.localScale = Vector3.one;

        EggCharacter character = egg.AddComponent<EggCharacter>();
        character.characterName = "Player";
        character.characterId = 1;

        PlayerInput input = egg.AddComponent<PlayerInput>();
        OutOfBoundsDetector bounds = egg.AddComponent<OutOfBoundsDetector>();

        GameObject face = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        face.name = "Face";
        face.transform.parent = egg.transform;
        face.transform.localPosition = new Vector3(0, 0.3f, 0.5f);
        face.transform.localScale = new Vector3(0.3f, 0.3f, 0.1f);
        face.GetComponent<Renderer>().material.color = Color.white;
        Object.DestroyImmediate(face.GetComponent<Collider>());

        egg.AddComponent<EggExpression>();
        egg.AddComponent<EggDecoration>();

        Rigidbody rb = egg.GetComponent<Rigidbody>();
        if (rb == null) rb = egg.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.mass = 1f;
        Object.DestroyImmediate(egg.GetComponent<SphereCollider>());
        SphereCollider col = egg.AddComponent<SphereCollider>();
        col.radius = 0.5f;

        GameObject spawnPoint = new GameObject("PlayerSpawn");
        spawnPoint.transform.position = Vector3.zero;

        GameObject respawnPoint = new GameObject("RespawnPoint");
        respawnPoint.transform.position = Vector3.zero;
        bounds.respawnPoint = respawnPoint.transform;

        EditorUtility.SetDirty(egg);
    }

    static void SetupGround(Scene scene)
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.position = new Vector3(0, -0.5f, 0);
        ground.transform.localScale = new Vector3(10, 1, 10);
        ground.GetComponent<Renderer>().material.color = new Color(0.2f, 0.8f, 0.2f);
    }

    static void SetupTeleportPoints(Scene scene)
    {
        string[] destinations = { "AbbyTown", "SpaceAdventure", "OkinawaIsland" };
        Vector3[] positions = { new Vector3(10, 0, 0), new Vector3(20, 0, 0), new Vector3(30, 0, 0) };
        Color[] colors = { Color.blue, Color.magenta, Color.cyan };

        for (int i = 0; i < destinations.Length; i++)
        {
            GameObject tp = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tp.name = $"Teleport_{destinations[i]}";
            tp.transform.position = positions[i];
            tp.transform.localScale = new Vector3(1, 0.2f, 1);
            tp.GetComponent<Renderer>().material.color = colors[i];
            Object.DestroyImmediate(tp.GetComponent<Collider>());
            BoxCollider area = tp.AddComponent<BoxCollider>();
            area.isTrigger = true;
            area.size = new Vector3(3, 1, 3);
            TeleportSystem teleport = tp.AddComponent<TeleportSystem>();
            teleport.targetSceneName = destinations[i];
        }
    }

    static void SetupAquarium(Scene scene)
    {
        GameObject aquarium = new GameObject("AquariumViewer");
        aquarium.transform.position = new Vector3(5, 0, 5);
        aquarium.AddComponent<AquariumViewer>();

        GameObject guide = new GameObject("TouristGuide");
        guide.transform.position = new Vector3(-5, 0, 5);
        guide.AddComponent<TouristGuide>();
    }

    static void CreateClownModeScene()
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        scene.name = "ClownMode";
        EditorSceneManager.SetActiveScene(scene);

        GameObject manager = new GameObject("[ClownModeManager]");
        manager.AddComponent<ClownModeManager>();
        GameObject room = new GameObject("[ClownRoomManager]");
        room.transform.parent = manager.transform;
        room.AddComponent<ClownRoomManager>();

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Arena";
        ground.transform.localScale = new Vector3(5, 1, 5);

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/ClownMode.unity");
    }

    static void CreateBombModeScene()
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        scene.name = "BombMode";
        EditorSceneManager.SetActiveScene(scene);

        GameObject manager = new GameObject("[BombModeManager]");
        manager.AddComponent<BombModeManager>();

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Arena";
        ground.transform.localScale = new Vector3(10, 1, 10);

        Transform[] spawns = new Transform[24];
        for (int i = 0; i < 24; i++)
        {
            GameObject sp = new GameObject($"SpawnPoint_{i}");
            float angle = i * (360f / 24);
            sp.transform.position = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * 8, 0, Mathf.Cos(angle * Mathf.Deg2Rad) * 8);
            sp.transform.parent = manager.transform;
            spawns[i] = sp.transform;
        }

        BombModeManager bmm = manager.GetComponent<BombModeManager>();
        if (bmm != null) bmm.spawnPoints = spawns;

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/BombMode.unity");
    }

    static void CreateCrazyFighterScene()
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        scene.name = "CrazyFighter";
        EditorSceneManager.SetActiveScene(scene);

        GameObject manager = new GameObject("[CrazyFighterManager]");
        manager.AddComponent<CrazyFighterManager>();

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Arena";
        ground.transform.localScale = new Vector3(10, 1, 10);

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/CrazyFighter.unity");
    }

    static void CreateOkinawaScene()
    {
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        scene.name = "OkinawaIsland";
        EditorSceneManager.SetActiveScene(scene);

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Beach";
        ground.transform.localScale = new Vector3(15, 1, 15);
        ground.GetComponent<Renderer>().material.color = new Color(0.9f, 0.8f, 0.5f);

        GameObject aquarium = new GameObject("AquariumViewer");
        aquarium.transform.position = new Vector3(5, 0, 5);
        aquarium.AddComponent<AquariumViewer>();

        GameObject guide = new GameObject("TouristGuide");
        guide.transform.position = new Vector3(-5, 0, 5);
        guide.AddComponent<TouristGuide>();

        GameObject shop = new GameObject("SouvenirShop");
        shop.transform.position = new Vector3(0, 0, 10);
        shop.AddComponent<ShopSystem>();

        GameObject toy = new GameObject("ToySystem");
        toy.transform.position = new Vector3(10, 0, 0);
        toy.AddComponent<ToySystem>();

        GameObject cafe = new GameObject("Cafe");
        cafe.transform.position = new Vector3(-10, 0, 0);
        cafe.AddComponent<CafeteriaSystem>();

        EditorSceneManager.SaveScene(scene, "Assets/Scenes/OkinawaIsland.unity");
    }
}
