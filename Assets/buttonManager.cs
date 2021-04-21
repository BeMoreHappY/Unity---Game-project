using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    private Color selectedColor = new Color(0.984f, 0.933f, 0.094f, 1f);
    private Color unselectedColor = new Color(0.105f, 0.952f, 0.952f, 0.6f);
    
    
    
    void Start()
    {
        GetComponent<Image>().color = unselectedColor;
    }
    
    public void setSelected()
    {
        GetComponent<Image>().color = selectedColor;
    }
}
