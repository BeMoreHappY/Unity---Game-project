using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa, która odpowiada za obsługę zmiany broni
/// </summary>
public class weaponSwitcher : MonoBehaviour
{

    public int weaponSelect = 0;
    public GameObject[] weapon;
    /// <summary>
    /// Funkcja, która aktywuje lub dezaktywuje broń
    /// </summary>
    /// <param name="idWeapon">Przyjmuje ID broni</param>
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
