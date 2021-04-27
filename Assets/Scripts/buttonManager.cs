using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
	public GameObject player;
    private Color selectedColor = new Color(0.984f, 0.933f, 0.094f, 1f);
    private Color unselectedColor = new Color(0.105f, 0.952f, 0.952f, 0.6f);
    
    
    
    void Start()
    {
        GetComponent<Image>().color = unselectedColor;
    }
    
    public void setSelected()
    {
		string buttonName = gameObject.name;
		int skillID = 0;

		if(buttonName.Equals("DoubleJump")){
			skillID = 0;
		}else if(buttonName.Equals("TripleJump")){
			skillID = 1;
		}

		player.GetComponent<Player>().skillsTreeButtonsAction(skillID);
        GetComponent<Image>().color = selectedColor;
    }
}
