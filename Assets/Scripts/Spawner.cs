using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;

    void spawn(int n=0, int k=0, int l=0){
        whatToSpawnClone[n] = Instantiate(whatToSpawnPrefab[k],spawnLocations[l].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawn(0,0,0);
        spawn(1,0,0);
        spawn(2,0,0);
        spawn(3,0,1);
        spawn(4,0,1);
        spawn(5,0,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
