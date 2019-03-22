using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputSystem))]
[RequireComponent(typeof(ProjectileSystem))]
[RequireComponent(typeof(ScoreSystem))]
[RequireComponent(typeof(AudioPlayer))]
public class GameManager : MonoBehaviour
{
    public GameObject flyPrefab;
    public List<PlayerInput> playerInput = new List<PlayerInput>();
    public List<Vector3> playerSpawnPositions = new List<Vector3>();

    public GameObject[] flyPrefabs = new GameObject[4];

    List<Player> players = new List<Player>();

    InputSystem inputSystem;
    ProjectileSystem projectileSystem;
    BubbleSpawner bubbleSpawner;
    ScoreSystem scoreSystem;
    AudioPlayer audioPlayer;

    int spawnPositionIndex = 0;

    private void Awake()
    {
        inputSystem = GetComponent<InputSystem>();
        projectileSystem = GetComponent<ProjectileSystem>();

        // String reference (finding gameobject BubbleSpawner) dangerous because string might be wrong
        bubbleSpawner = GameObject.Find("BubbleSpawner").GetComponent<BubbleSpawner>();
        scoreSystem = GetComponent<ScoreSystem>();
        audioPlayer = GetComponent<AudioPlayer>();
    }

    void Start()
    {
        // Initialize players, add from instantiated prefab to list of players
        foreach(PlayerInput input in playerInput) {
            AddPlayer(input);
        }

        inputSystem.OnPlayerJoinInput += NewPlayerJoin;
    }

    void NewPlayerJoin(int index, PlayerInput input)
    {
        if (players.Count > 4)
            return;

/*        Debug.Log(string.Format("Player with gamepad controller index: {0} wants to join, current players: {1}", index, players.Count));*/
        AddPlayer(input);
    }

    void AddPlayer(PlayerInput input)
    {
        GameObject newPlayerGO;
        // Spawn at selected position
        if (playerSpawnPositions.Count >= spawnPositionIndex)
        {
            newPlayerGO = Instantiate(flyPrefabs[players.Count], playerSpawnPositions[spawnPositionIndex], Quaternion.identity);
            spawnPositionIndex++;
        }
        else // Probably spawns at prefab default position, flies might end up on top of eachother
        {
            newPlayerGO = Instantiate(flyPrefabs[players.Count]);
        }

        scoreSystem.AddScoreVariable(players.Count);

        Player newPlayer = newPlayerGO.AddComponent<Player>();
        newPlayer.Initialize(input);
        newPlayer.controller.Initialize(players.Count);
        players.Add(newPlayer);

        scoreSystem.AddNewPlayerScore(players.Count - 1);

        // Subscribe to events here
        newPlayer.controller.OnFireProjectile += projectileSystem.SpawnProjectile;
        newPlayer.controller.OnFireProjectile += audioPlayer.PlayFlyShootSound;

        newPlayer.controller.OnIncreaseScore += scoreSystem.IncreaseScore;
        newPlayer.controller.OnDecreaseScore += scoreSystem.DecreaseScore;

        //newPlayer.controller.OnIncreaseScore += SendMessageToScoreSystem;
        //newPlayer.controller.OnDecreaseScore += SendMessageToScoreSystem;

        newPlayer.controller.OnHitBubble += SendMessageToScoreSystem;
    }

    public void SendMessageToScoreSystem(Vector3 position, int score)
    {
        scoreSystem.AddOnScreenScoreMessage(position, score);
    }

    void Update()
    {
        projectileSystem.Tick();
        bubbleSpawner.Tick();

        // Update each player
        foreach (Player p in players)
        {
            p.Tick();
        }
    }
}
