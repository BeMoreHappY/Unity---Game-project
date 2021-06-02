using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;
    public TMPro.TextMeshProUGUI interactionText;
    public TMPro.TextMeshProUGUI waveText;
    public int fala;
    public int enemies;
    public int points;
    private bool flag=true;
    void spawn(int n=0, int k=0, int l=0){
        whatToSpawnClone[n] = Instantiate(whatToSpawnPrefab[k],spawnLocations[l].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
    }
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        points=0;
        fala = 1;
        waveText.text = fala.ToString();
        spawn(0,1,0);
        spawn(1,1,0);
        spawn(2,1,0);
        enemies = 3;

    }

    void fale(){
        flag = false;
        fala ++;
        waveText.text = fala.ToString();
        if (fala==2){
            spawn(0,1,0);
            enemies++;
            spawn(1,1,0);
            enemies++;
            spawn(2,1,0);
            enemies++;
            spawn(3,0,1);
            enemies++;
            spawn(4,0,1);
            enemies++;
            spawn(5,0,1);
            enemies++;
            
        }
        else if (fala==3){
            spawn(0,1,0);
            enemies++;
            spawn(1,1,0);
            enemies++;
            spawn(2,1,0);
            enemies++;
            spawn(3,0,1);
            enemies++;
            spawn(4,0,1);
            enemies++;
            spawn(5,0,1);
            enemies++;
            spawn(6,2,2);
            enemies++;
            spawn(7,2,2);
            enemies++;
            spawn(8,2,2);
            enemies++;

        }
         else if (fala==4){
            spawn(0,1,0);
            enemies++;
            spawn(1,1,0);
            enemies++;
            spawn(2,1,0);
            enemies++;
            spawn(3,0,1);
            enemies++;
            spawn(4,0,1);
            enemies++;
            spawn(5,0,1);
            enemies++;
            spawn(6,2,2);
            enemies++;
            spawn(7,2,2);
            enemies++;
            spawn(8,2,2);
            enemies++;
            System.Threading.Thread.Sleep(5000);
            spawn(9,1,0);
            enemies++;
            spawn(10,1,0);
            enemies++;
            spawn(11,0,1);
            enemies++;
            spawn(12,0,1);
            enemies++;
            spawn(13,0,1);
            enemies++;
        }
        else if (fala==5){
            spawn(0,1,0);
            enemies++;
            spawn(1,1,0);
            enemies++;
            spawn(2,1,0);
            enemies++;
            spawn(3,0,1);
            enemies++;
            spawn(4,0,1);
            enemies++;
            spawn(5,0,1);
            enemies++;
            spawn(6,2,2);
            enemies++;
            spawn(7,2,2);
            enemies++;
            spawn(8,2,2);
            enemies++;
            System.Threading.Thread.Sleep(5000);
            spawn(9,1,0);
            enemies++;
            spawn(10,1,0);
            enemies++;
            spawn(11,0,1);
            enemies++;
            spawn(12,0,1);
            enemies++;
            spawn(13,0,1);
            enemies++;
            spawn(14,0,1);
            enemies++;
            spawn(15,2,2);
            enemies++;
            spawn(16,2,2);
            enemies++;
            spawn(17,2,2);
            enemies++;
            System.Threading.Thread.Sleep(1500);
            spawn(18,3,3);
            enemies++;
            spawn(19,3,3);
            enemies++;
            spawn(20,3,3);
            enemies++;
            spawn(21,3,3);
            enemies++;
            spawn(22,3,3);
            enemies++;
            spawn(23,3,3);
            enemies++;
            spawn(24,3,3);
            enemies++;
            spawn(25,3,3);
            enemies++;
            spawn(26,3,3);
            enemies++;
            spawn(27,3,3);
            enemies++;
        }
        flag = true;
    }

    public void enemyDied(){
        points+=50;
        enemies--;
    }



    // Update is called once per frame
    void Update()
    {
        if (enemies == 0 && flag) fale();
        interactionText.text = points.ToString();
    }
}
