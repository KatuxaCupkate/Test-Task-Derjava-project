using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawManager : MonoBehaviour
{
    [SerializeField] GameObject goulPrefab;
    
    [SerializeField] Transform spawnPos;
   void SpawnGoul()
    {
        Instantiate(goulPrefab,spawnPos.position,spawnPos.rotation);
    }
}
