using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creativeButtons : MonoBehaviour
{
	public GameObject player;

    void Start()
    {
        //https://www.youtube.com/watch?v=m1lBHP5lxeY
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

	public void buttonPressed(){
		string buttonName = gameObject.name;
		int buttonID = 0;
		if(buttonName.Equals("item0")){
			buttonID=0;
		}else if(buttonName.Equals("item1")){
			buttonID=1;
		}else if(buttonName.Equals("item2")){
			buttonID=2;
		}else if(buttonName.Equals("item3")){
			buttonID=3;
		}else if(buttonName.Equals("item4")){
			buttonID=4;
		}else if(buttonName.Equals("item5")){
			buttonID=5;
		}else if(buttonName.Equals("item6")){
			buttonID=6;
		}

		player.GetComponent<Player2>().weaponChoosePanelAction(buttonID);
	}

}
