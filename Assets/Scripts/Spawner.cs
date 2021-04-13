using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;
    private int fala;
    public int enemies;
    void spawn(int n=0, int k=0, int l=0){
        whatToSpawnClone[n] = Instantiate(whatToSpawnPrefab[k],spawnLocations[l].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        fala = 1;
        spawn(0,1,1);
        spawn(1,1,1);
        spawn(2,1,1);
        enemies = 3;

    }

    void fale(){
        fala ++;
        if (fala==2){
            spawn(0,1,1);
            spawn(1,1,1);
            spawn(2,1,1);
            spawn(3,0,0);
            spawn(4,0,0);
            spawn(5,0,0);
        }

    }

    void enemyDied(){
        enemies--;
    }



    // Update is called once per frame
    void Update()
    {
        if (enemies == 0) fale();
    }
}
