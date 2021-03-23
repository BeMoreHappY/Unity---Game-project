using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SferaPaczaniaCelu : MonoBehaviour
{
    public Action<Collider> OnTriggerEnter_Action;
   
 
    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnter_Action?.Invoke(other);
    }
    
}
