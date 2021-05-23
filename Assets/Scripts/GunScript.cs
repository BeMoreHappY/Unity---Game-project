using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Camera fpscam;
    public ParticleSystem muzzleflash;
    public float damage = 10f;
    private float nextTimeToFire = 0f;
    public float fireRate = 15f;
    public GameObject [] hole;
    public GameObject impact;
    private Player2 player2Script;
    
    
    public void Start()
    {
        player2Script = player.GetComponent<Player2>();
    }
    
    void Update()
    {
        if (!player2Script.gameStopped)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f/fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit))
        {
            EnemyAi target = hit.transform.GetComponent<EnemyAi>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                //if (target.isActive()) target.agentStop();
                target.impact(-hit.normal);
                //target.Invoke("agentStart", 1f);   
            }
            if (hit.collider != null && hit.rigidbody == null)
            {
                Debug.Log("elooo");
                int random = Random.Range(0, 0);
                GameObject newHole = Instantiate(hole[random], hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal)) as GameObject;
                Destroy(newHole, 5f);
            }
            if (hit.collider != null)
            {
                GameObject impactBullet = Instantiate(impact, hit.point + hit.normal * 0.1f, Quaternion.LookRotation(hit.normal)) as GameObject;
                Destroy(impactBullet, 2f);
            }

        }
    }

    
}
