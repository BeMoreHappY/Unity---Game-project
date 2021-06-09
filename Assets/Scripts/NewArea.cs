using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa, która odpowiada za odblokowywanie nowych obszarów
/// </summary>
public class NewArea : Interactable
{
    public GameObject go;
    public Spawner spawn;
    public int val;
    /// <summary>
    /// Metoda, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany. Pobiera informacje na temat spawnera
    /// </summary>
    private void Start()
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
    }


    /// <summary>
    /// Metoda zwracająca tekst do wyświetlenia
    /// </summary>
    public override string GetDescription(){
        if (spawn.points<val) return "Press [E] to unclock <color=red>" + val.ToString() + "</color>";
        return "Press [E] to unclock <color=green>" + val.ToString() + "</color>";
    }
    /// <summary>
    /// Metoda obsługująca interakcję z graczem
    /// </summary>
    public override void Interact(){
        if (spawn.points>=val){
            spawn.points-=val;
            Destroy(gameObject);
        }
    }
}
