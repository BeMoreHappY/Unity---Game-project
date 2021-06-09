using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa, która odpowiada za obsługę skrzynek
/// </summary>
public class Box : Interactable
{
    public GameObject go;
    public Spawner spawn;
    public int val;
    public int val2 = 10;
    public GameObject go2;
    public Player2 weap;
    public GameObject go3;
    public GunScript weap2;
    public string name;
    public int indeks;

    /// <summary>
    /// Metoda, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany. Pobiera informacje na temat spawnera, gracza i obsługiwanej broni.
    /// </summary>
    void Start()
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
        go2 =GameObject.Find("Player");
        weap = (Player2) go2.GetComponent(typeof(Player2));
        weap2 = (GunScript) go3.GetComponent(typeof(GunScript));
    }
    /// <summary>
    /// Metoda zwracająca tekst do wyświetlenia
    /// </summary>
    public override string GetDescription(){
        if(weap.idWeaponIsActive[indeks]==false){
            if (spawn.points<val) return "Press [E] to unclock "+name+" <color=red>" +val.ToString() + "</color>";
            return "Press [E] to unclock "+name+" <color=green>" +val.ToString() + "</color>";
        }
        else
        {
            if (spawn.points<val2) return "Press [E] to add 10 ammo to "+name+" <color=red>" +val2.ToString() + "</color>";
            return "Press [E] to add 10 ammo to "+name+" <color=green>" +val2.ToString() + "</color>";
        }
    }

    /// <summary>
    /// Metoda obsługująca interakcję z graczem
    /// </summary>
    public override void Interact(){
        if(weap.idWeaponIsActive[indeks]==false){
            if (spawn.points>=val){
                spawn.points-=val;
                weap.idWeaponIsActive[indeks]=true;
            }
        }
        else
        {
            if (spawn.points>=val2){
                spawn.points-=val2;
                weap2.ammo+=10;
            }
        }
    }


}
