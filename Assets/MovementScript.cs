﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
