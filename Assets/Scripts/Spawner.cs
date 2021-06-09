using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa, która odpowiada za spawn przeciwników i punkty gracza
/// </summary>
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
    /// <summary>
    /// Metoda spawnująca przeciwników
    /// </summary>
    void spawn(int n=0, int k=0, int l=0){
        whatToSpawnClone[n] = Instantiate(whatToSpawnPrefab[k],spawnLocations[l].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
    }
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
    }

    /// <summary>
    /// Metoda ustawiająca wartości domyślne
    /// </summary>
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
    /// <summary>
    /// Metoda obsługująca fale
    /// </summary>
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
        else if (fala==6){
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
            System.Threading.Thread.Sleep(1500);
            spawn(28,1,0);
            enemies++;
            spawn(29,1,0);
            enemies++;
            spawn(30,1,0);
            enemies++;
            spawn(31,0,1);
            enemies++;
            spawn(32,0,1);
            enemies++;
            spawn(33,0,1);
            enemies++;
            spawn(34,2,2);
            enemies++;
            spawn(35,2,2);
            enemies++;
            spawn(36,2,2);
            enemies++;
            System.Threading.Thread.Sleep(5000);
            spawn(37,1,0);
            enemies++;
            spawn(38,1,0);
            enemies++;
            spawn(39,0,1);
            enemies++;
            spawn(40,0,1);
            enemies++;
            spawn(41,0,1);
            enemies++;
            spawn(42,0,1);
            enemies++;
            spawn(43,2,2);
            enemies++;
            spawn(44,2,2);
            enemies++;
            spawn(45,2,2);
            enemies++;
            System.Threading.Thread.Sleep(1500);
            spawn(46,3,3);
            enemies++;
            spawn(47,3,3);
            enemies++;
            spawn(48,3,3);
            enemies++;
            spawn(49,3,3);
            enemies++;
            spawn(50,3,3);
            enemies++;
            spawn(51,3,3);
            enemies++;
            spawn(52,3,3);
            enemies++;
            spawn(53,3,3);
            enemies++;
            spawn(54,3,3);
            enemies++;
            spawn(55,3,3);
            enemies++;
        }

        flag = true;
    }
    /// <summary>
    /// Metoda obsługująca śmierć przeciwników i dodająca punkty
    /// </summary>
    public void enemyDied(){
        points+=50;
        enemies--;
    }



    /// <summary>
    /// Metoda sprawdzająca czy fala trwa dalej
    /// </summary>
    void Update()
    {
        if (enemies == 0 && flag) fale();
        interactionText.text = points.ToString();
    }
}
