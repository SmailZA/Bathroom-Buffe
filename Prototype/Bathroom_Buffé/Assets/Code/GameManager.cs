using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileSystem))]
[RequireComponent(typeof(ScoreSystem))]
[RequireComponent(typeof(AudioPlayer))]
public class GameManager : MonoBehaviour
{
    public GameObject flyPrefab;
    public List<PlayerInput> playerInput = new List<PlayerInput>();
    public List<Vector3> playerSpawnPositions = new List<Vector3>();

    List<Player> players = new List<Player>();
    
    ProjectileSystem projectileSystem;
    BubbleSpawner bubbleSpawner;
    ScoreSystem scoreSystem;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        projectileSystem = GetComponent<ProjectileSystem>();
        // String reference (finding gameobject BubbleSpawner) dangerous because might be wrong
        bubbleSpawner = GameObject.Find("BubbleSpawner").GetComponent<BubbleSpawner>();
        scoreSystem = GetComponent<ScoreSystem>();
        audioPlayer = GetComponent<AudioPlayer>();
    }

    void Start()
    {
        // Initialize players, add from instantiated prefab to list of players
        int spawnPositionIndex = 0;
        foreach(PlayerInput input in playerInput) {

            GameObject newPlayerGO;

            // Spawn at selected position
            if (playerSpawnPositions.Count >= spawnPositionIndex)
            {
                newPlayerGO = Instantiate(flyPrefab, playerSpawnPositions[spawnPositionIndex], Quaternion.identity);
                spawnPositionIndex++;
            }
            else // Probably spawns at prefab default position, flies might end up on top of eachother
            {
                newPlayerGO = Instantiate(flyPrefab);
            }

            scoreSystem.AddScoreVariable(players.Count);

            Player newPlayer = newPlayerGO.AddComponent<Player>();
            newPlayer.Initialize(input);
            newPlayer.controller.Initialize(players.Count);
            players.Add(newPlayer);

            // Subscribe to events here
            newPlayer.controller.OnFireProjectile += projectileSystem.SpawnProjectile;
            newPlayer.controller.OnFireProjectile += audioPlayer.PlayFlyShootSound;

            newPlayer.controller.OnIncreaseScore += scoreSystem.IncreaseScore;
        }
    }

    void Update()
    {
        projectileSystem?.Tick();
        bubbleSpawner?.Tick();

        // Update each player
        foreach (Player p in players)
        {
            p.Tick();
        }
    }
}
