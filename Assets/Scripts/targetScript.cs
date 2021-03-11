using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        int objectLayer = other.gameObject.layer;

        if (other.gameObject.layer == 10){
            Destroy(this.gameObject);
        } 
    }
}
