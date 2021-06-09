using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Klasa, która odpowiada za obsługę paska zdrowia
/// </summary>
public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    /// <summary>
    /// Funkcja, która ustawia pasek zdrowia na maksymalną wartość
    /// </summary>
    /// <param name="health">Przyjmuje wartość punktów zdrowia</param>
    public void MaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    /// <summary>
    /// Funkcja, która ustawia pasek zdrowia na konkretną wartość
    /// </summary>
    /// <param name="health">Przyjmuje wartość punktów zdrowia</param>
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
