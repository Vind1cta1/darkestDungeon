using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public List<GameObject> characterPrefab;
    public Transform player;
    public Vector3 spawnPosition;
    public Vector3 distanceBetweenCharacters;

    private int currentCharacterPrefab;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            currentCharacterPrefab = Random.Range(0, characterPrefab.Count);
            Instantiate(characterPrefab[currentCharacterPrefab], spawnPosition, Quaternion.identity, player);
            spawnPosition = spawnPosition - distanceBetweenCharacters;
        }
    }
}
