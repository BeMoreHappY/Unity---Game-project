using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OdblokowaneBronie : MonoBehaviour
{
    Button[] buttons;
    // Start is called before the first frame update
    void Awake()
    {
        buttons = this.GetComponentsInChildren<Button>(true);
    }

    // Update is called once per frame
    public void odblokuj(int id)
    {
        buttons[id].interactable = true;
    }
    public void zablokuj(int id)
    {
        buttons[id].interactable = false;
    }
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
