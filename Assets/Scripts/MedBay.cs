using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBay : Interactable
{
    public GameObject go;
    public Spawner spawn;
    public int val,val2;
    public GameObject go2;
    public Player2 player;

    void Start()
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
        go2 =GameObject.Find("Player");
        player = (Player2) go2.GetComponent(typeof(Player2));
    }

    public override string GetDescription(){
        if (spawn.points<=val) return "Hold [E] to Heal <color=red>" +val.ToString() + "</color>";
        return "Hold [E] to Heal <color=green>" +val.ToString() + "</color>";
        
    }

    public override void Interact(){
        if (spawn.points>=val){
            spawn.points-=val;
            player.Heal(val2);
            Debug.Log("Heal");            
        }
    }
}
