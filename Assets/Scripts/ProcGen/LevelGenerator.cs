using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f;
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float minMoveSpeed = 2f;

    //GameObject[] chunks = new GameObject[12]; //will create array of 12 objects
    List<GameObject> chunks = new List<GameObject>();
    void Start()
    {
        SpawnStartingChunks();
    }
    private void Update()
    {
        MoveChunks();
    }
    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        moveSpeed += speedAmount;
        if (moveSpeed < minMoveSpeed) 
        {
            moveSpeed = minMoveSpeed;
        }
        // with changing speed we will change Z speed of objects that spawn
        //to kinda balance it
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);

        cameraController.ChangeCameraFOV(speedAmount);
    }
    private void SpawnStartingChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);
        //chunks [i] = newChunk;
        chunks.Add(newChunk);
    }

    private float CalculateSpawnPositionZ()
    {
        float spawnPositionZ;
        if (chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count-1].transform.position.z + chunkLength;
        }
        return spawnPositionZ;
    }
    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks [i];
            chunks[i].transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));
            //remove chunks that got far beyond camera
            if (chunk.transform.position.z <= Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                SpawnChunk();
            }
        }
    }
}
