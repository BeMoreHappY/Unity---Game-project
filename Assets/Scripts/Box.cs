using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Interactable
{
    public GameObject go;
    public Spawner spawn;
    public int val;
    public GameObject go2;
    public OdblokowaneBronie weap;
    public string name;
    public int indeks;

    void Start()
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
        go2 =GameObject.Find("GameObject (Weapon Status)");
        weap = (OdblokowaneBronie) go2.GetComponent(typeof(OdblokowaneBronie));
        
    }
    public override string GetDescription(){
        if(weap.status()[indeks]==false){
            if (spawn.points<=val) return "Press [E] to unclock "+name+" <color=red>" +val.ToString() + "</color>";
            return "Press [E] to unclock "+name+" <color=green>" +val.ToString() + "</color>";
        }
        return "TEST";
    }

    public override void Interact(){
        if (spawn.points>=val){
            spawn.points-=val;
            weap.odblokuj(indeks);
            
        }
    }


}
