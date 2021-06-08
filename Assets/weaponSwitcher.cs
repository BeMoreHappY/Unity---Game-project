using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitcher : MonoBehaviour
{

    public int weaponSelect = 0;
    public GameObject[] weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWeapon(int idWeapon)
    {
        if (idWeapon != weaponSelect)
        {
            weapon[idWeapon].SetActive(true);
            weapon[weaponSelect].SetActive(false);
        }
        weaponSelect = idWeapon;
    }
}
