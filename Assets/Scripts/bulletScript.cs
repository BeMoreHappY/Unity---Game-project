using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        int objectLayer = other.gameObject.layer;
        //Debug.Log("XD "+objectLayer);
        
        if (other.gameObject.layer == 8){
            
        } else if (other.gameObject.layer == 9){
            
        }
        else if (other.gameObject.layer == 11) {
            Destroy(this.gameObject);
            //Debug.Log("Zniszczono Cel!");
        }
    }
}
