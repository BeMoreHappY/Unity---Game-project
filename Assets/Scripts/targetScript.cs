using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    private SferaPaczaniaCelu firstCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        firstCollider = transform.Find("Pacze1").GetComponent<SferaPaczaniaCelu>();
        firstCollider.OnTriggerEnter_Action += coJaPacze;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        int objectLayer = other.gameObject.layer;
        
        if (other.gameObject.layer == 12){
            //ENEMY
            Debug.Log("Poszczelono mie!");
            //Destroy(this.gameObject);
        } 
    }

    private void coJaPacze(Collider other)
    {
        Debug.Log("CO JA PACZE!!!");
    }
}
