using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Klasa, która odpowiada za odblokowywanie broni dla gracza
/// </summary>
public class OdblokowaneBronie : MonoBehaviour
{
    Button[] buttons;
    /// <summary>
    /// Funkcja, która wykonuje się w momencie załadowania sceny z danym obiektem
    /// </summary>
    void Awake()
    {
        buttons = this.GetComponentsInChildren<Button>(true);
    }
    /// <summary>
    /// Funkcja, która odblokowuje broń
    /// </summary>
    /// <param name="id">ID broni do odblokowania</param>
    public void odblokuj(int id)
    {
        buttons[id].interactable = true;
    }
    /// <summary>
    /// Funkcja, która blokuje broń
    /// </summary>
    /// <param name="id">ID broni do zablokowania</param>
    public void zablokuj(int id)
    {
        buttons[id].interactable = false;
    }
    /// <summary>
    /// Funkcja, która zwraca status broni (aktywna lub nie)
    /// </summary>
    /// <returns>Zwraca tablicę bool wszystkich broni</returns>
    public bool[] status()
    {
        bool[] array = new bool[7];
        for( int i = 0; i < 7; i++)
        {
            array[i] = buttons[i].interactable;
        }
        return array;
    }
}
