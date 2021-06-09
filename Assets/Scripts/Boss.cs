using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa służąca do obługi bossa - chwilowo nie działa. SKRYPT W BUDOWIE.
/// </summary>
public class Boss : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;
    public TMPro.TextMeshProUGUI interactionText;
    public TMPro.TextMeshProUGUI waveText;
    public GameObject go;
    public Spawner spawner;


    public int enemies;

    
    void spawn(int n=0, int k=0, int l=0){
        enemies++;
        whatToSpawnClone[n] = Instantiate(whatToSpawnPrefab[k],spawnLocations[l].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
    }
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawner = (Spawner) go.GetComponent(typeof(Spawner));
        enemies=0;

    }

    
    public void enemyDied(){
        spawner.points+=1000;
        enemies--;
    }



    // Update is called once per frame
    void Update()
    {
        if (enemies == 0) spawn(0,0,0);
    }
}
