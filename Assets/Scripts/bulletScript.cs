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
        Destroy(this.gameObject, 5);
    }

    private void FixedUpdate()
    {
        
    }
    
     void OnTriggerEnter(Collider Enemy){
    if(Enemy.gameObject.CompareTag("enemy"))
    {
        Enemy.gameObject.SendMessage("OnDamage");
        Destroy(this.gameObject);
    }
    }
}