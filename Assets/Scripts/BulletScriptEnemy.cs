using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BulletScriptEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 5;
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
        if(Enemy.gameObject.CompareTag("Player"))
        {
            Player2 target = Enemy.GetComponent<Player2>();
            target.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if(Enemy.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}