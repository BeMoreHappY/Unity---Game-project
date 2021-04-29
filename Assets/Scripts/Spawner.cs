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
    public int points;
    void spawn(int n=0, int k=0, int l=0){
        whatToSpawnClone[n] = Instantiate(whatToSpawnPrefab[k],spawnLocations[l].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        points=0;
        fala = 1;
        spawn(0,1,0);
        spawn(1,1,0);
        spawn(2,1,0);
        enemies = 3;

    }

    void fale(){
        fala ++;
        if (fala==2){
            spawn(0,1,0);
            spawn(1,1,0);
            spawn(2,1,0);
            spawn(3,0,1);
            spawn(4,0,1);
            spawn(5,0,1);
        }
        else if (fala==3){
            spawn(0,1,0);
            spawn(1,1,0);
            spawn(2,1,0);
            spawn(3,0,1);
            spawn(4,0,1);
            spawn(5,0,1);
            spawn(6,2,2);
            spawn(7,2,2);
            spawn(8,2,2);

        }
         else if (fala==4){
            spawn(0,1,0);
            spawn(1,1,0);
            spawn(2,1,0);
            spawn(3,0,1);
            spawn(4,0,1);
            spawn(5,0,1);
            spawn(6,2,2);
            spawn(7,2,2);
            spawn(8,2,2);
            System.Threading.Thread.Sleep(5000);
            spawn(9,1,0);
            spawn(10,1,0);
            spawn(11,0,1);
            spawn(12,0,1);
            spawn(13,0,1);
        }
        else if (fala==5){
            spawn(0,1,0);
            spawn(1,1,0);
            spawn(2,1,0);
            spawn(3,0,1);
            spawn(4,0,1);
            spawn(5,0,1);
            spawn(6,2,2);
            spawn(7,2,2);
            spawn(8,2,2);
            System.Threading.Thread.Sleep(5000);
            spawn(9,1,0);
            spawn(10,1,0);
            spawn(11,0,1);
            spawn(12,0,1);
            spawn(13,0,1);
            spawn(14,0,1);
            spawn(15,2,2);
            spawn(16,2,2);
            spawn(17,2,2);
            System.Threading.Thread.Sleep(1500);
            spawn(18,3,3);
            spawn(19,3,3);
            spawn(20,3,3);
            spawn(21,3,3);
            spawn(22,3,3);
            spawn(23,3,3);
            spawn(24,3,3);
            spawn(25,3,3);
            spawn(26,3,3);
            spawn(27,3,3);
            
        }
    }

    public void enemyDied(){
        points+=50;
        enemies--;
    }



    // Update is called once per frame
    void Update()
    {
        if (enemies == 0) Invoke("fale",6);
    }
}
