using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject archerPrefab;
    public Transform player;
    public Vector3 spawnPosition;
    public Vector3 distanceBetweenCharacters;

    private void Start()
    {
        Instantiate(archerPrefab, spawnPosition, Quaternion.identity, player);
        spawnPosition = spawnPosition - distanceBetweenCharacters;
        Instantiate(archerPrefab, spawnPosition, Quaternion.identity, player);
        spawnPosition = spawnPosition - distanceBetweenCharacters;
        Instantiate(archerPrefab, spawnPosition, Quaternion.identity, player);
    }
}
