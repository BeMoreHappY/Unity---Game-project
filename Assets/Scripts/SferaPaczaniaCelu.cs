using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SferaPaczaniaCelu : MonoBehaviour
{
    public Action<Collider> OnTriggerEnter_Action;
    [SerializeField] private SphereCollider sfera;
    
   
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 12)
        {
            sfera.radius = 12;
            Debug.Log("Promień: " + sfera.radius);
        }

        if (other.gameObject.layer == 14)
        {
            Debug.Log("Zauważono martwego kolegę " + other.gameObject.layer);
        }
        
        
        OnTriggerEnter_Action?.Invoke(other);
        
    }
    
}
