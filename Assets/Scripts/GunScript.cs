using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa, która odpowiada za obsługę broni
/// </summary>
public class GunScript : MonoBehaviour
{
    public GameObject player;
    public Camera fpscam;
    public ParticleSystem muzzleflash;
    public float damage = 10f;
    private float nextTimeToFire = 0f;
    public float fireRate = 15f;
    public GameObject [] hole;
    public GameObject impact;
    private Player2 player2Script;
    public int ammo;
    public TMPro.TextMeshProUGUI ammoText;

    /// <summary>
    /// Metoda, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany.
    /// </summary>
    public void Start()
    {
        player2Script = player.GetComponent<Player2>();
    }
    /// <summary>
	/// Metoda, która wykonuje się co klatkę
	/// </summary>
    void Update()
    {
        if (!player2Script.gameStopped)
        {
            ammoText.text = ammo.ToString();
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                Debug.Log(ammo);
                nextTimeToFire = Time.time + 1f/fireRate;
                if (ammo>0)
                {
                Shoot();
                ammo--;
                }
            }
        }
    }
    /// <summary>
    /// Metoda, która odpowiada za strzał z broni
    /// </summary>
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
