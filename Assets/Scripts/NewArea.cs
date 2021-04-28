using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewArea : Interactable
{
    public GameObject go;
    public Spawner spawn;
    public int val;
    
    private void Start()
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
    }



    public override string GetDescription(){
        if (spawn.points<=val) return "Press [E] to unclock <color=red>" +val.ToString() + "</color>";
        return "Press [E] to unclock <color=green>" +val.ToString() + "</color>";
    }

    public override void Interact(){
        if (spawn.points>=val){
            spawn.points-=val;
            Destroy(gameObject);
        }
    }
}
