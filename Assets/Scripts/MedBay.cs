using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa, która odpowiada za obsługę stacji medycznej
/// </summary>
public class MedBay : Interactable
{
    public GameObject go;
    public Spawner spawn;
    public int val,val2;
    public GameObject go2;
    public Player2 player;
    /// <summary>
    /// Metoda, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany. Pobiera informacje na temat spawnera i gracza.
    /// </summary>
    void Start()
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
        go2 =GameObject.Find("Player");
        player = (Player2) go2.GetComponent(typeof(Player2));
    }
    /// <summary>
    /// Metoda zwracająca tekst do wyświetlenia
    /// </summary>
    public override string GetDescription(){
        if (spawn.points<=val) return "Hold [E] to Heal <color=red>" +val.ToString() + "</color>";
        return "Hold [E] to Heal <color=green>" +val.ToString() + "</color>";
        
    }
    /// <summary>
    /// Metoda obsługująca interakcję z graczem
    /// </summary>
    public override void Interact(){
        if (spawn.points>=val){
            spawn.points-=val;
            player.Heal(val2);
            Debug.Log("Heal");            
        }
    }
}
