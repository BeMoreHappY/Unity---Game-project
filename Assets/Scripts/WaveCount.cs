using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCount : MonoBehaviour
{
    public GameObject go;
    public Spawner spawn;
    public TMPro.TextMeshProUGUI interactionText;
    void Start() 
    {
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
        interactionText.text = spawn.fala.ToString();
    }

    void Update()
    {
        
    }
}
