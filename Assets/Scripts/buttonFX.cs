using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public void hoverSound()
    {
        myFX.PlayOneShot(hoverFX);
    }
    
    public void clickSound()
    {
        myFX.PlayOneShot(clickFX);
    }
}
