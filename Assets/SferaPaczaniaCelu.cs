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
        sfera.radius++;
        Debug.Log("Promień sfery: " + sfera.radius);
        
        OnTriggerEnter_Action?.Invoke(other);
        
    }
    
}
