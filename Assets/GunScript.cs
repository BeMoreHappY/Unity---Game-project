using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera fpscam;
    public ParticleSystem muzzleflash;
    public float damage = 10f;
    
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            EnemyAi target = hit.transform.GetComponent<EnemyAi>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                if (target.isActive()) target.agentStop();
                target.impact(-hit.normal);
                target.Invoke("agentStart", 1f); 
                Debug.Log("ELOOOOO");
                
            }

        }
    }

    
}
