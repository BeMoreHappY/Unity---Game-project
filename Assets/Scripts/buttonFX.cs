using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa służąca do generowania dźwięków w grze.
/// </summary>
public class buttonFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    /// <summary>
    /// Metoda generująca dzięk po najechaniu na guzik.
    /// </summary>
    /// <returns></returns>
    public void hoverSound()
    {
        myFX.PlayOneShot(hoverFX);
    }
    
    /// <summary>
    /// Metoda generująca dźwięk po naciśnięciu na guzik.
    /// </summary>
    /// <returns></returns>
    
    public void clickSound()
    {
        myFX.PlayOneShot(clickFX);
    }
}
