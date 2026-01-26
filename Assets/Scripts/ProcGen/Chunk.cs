using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] float appleSpawnChance = 0.3f;
    [SerializeField] float coinSpawnChance = 0.5f;
    [SerializeField] float coinSeparationLength = 2f;//2 bcs size of chunk is 10 and max coins is 5

    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f };
    List<int> availableLanes = new List<int> { 0,1,2};
    private void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }
    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0,lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0)
            {
                break;
            }
            int randomLaneIndex = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
            Instantiate(fencePrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        if (availableLanes.Count <= 0|| Random.value >appleSpawnChance)
        {
            return;
        }
        int randomLaneIndex = SelectLane();
        Vector3 spawnPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
        Instantiate(applePrefab, spawnPosition, Quaternion.identity, this.transform);
    }
    void SpawnCoins()
    {
        if (availableLanes.Count <= 0 || Random.value > coinSpawnChance)
        {
            return;
        }
        int randomLaneIndex = SelectLane();
        int maxCoinsToSpawn = 6;
        int coinToSpawn = Random.Range(1, maxCoinsToSpawn);
        float topOfChunkZPos = transform.position.z + (coinSeparationLength * 2f);
        for (int i = 0; i < coinToSpawn; i++)
        { 
            float spawnPositionZ = topOfChunkZPos - (i* coinSeparationLength);
            Vector3 spawnPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, spawnPositionZ);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }
    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
