using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// Klasa służąca do obsługi guzików interfejsu - głównie drzewa umiejętności i kółka broni.
/// </summary>
public class buttonManager : MonoBehaviour
{
	public GameObject player;
	public GameObject skillPointsTextUI;
	public GameObject nextButton;

    private Color selectedColor = new Color(0.984f, 0.933f, 0.094f, 1f);
    private Color unselectedColor = new Color(0.105f, 0.952f, 0.952f, 0.6f);
    
    /// <summary>
    /// Metoda wywoływana przy tworzeniu instancji klasy.
    /// </summary>
    void Start()
    {
        GetComponent<Image>().color = unselectedColor;
		updateUI();
    }
    
    /// <summary>
    /// Metoda służąca do obsługi drzewa umiejętności i do komunikacji z głównym skryptem gracza.
    /// </summary>
    public void setSelected()
    {
		string buttonName = gameObject.name;
		int skillPoints = player.GetComponent<Player2>().skillPoints;
	
		if(skillPoints>0){
			int skillID = 0;

			if(buttonName.Equals("DoubleJump")){
				skillID = 0;
			}else if(buttonName.Equals("TripleJump")){
				skillID = 1;
			}else if(buttonName.Equals("WallRun")){
				skillID = 2;
			}else if(buttonName.Equals("WL5%G")){
				skillID = 3;
			}else if(buttonName.Equals("WL5%G01")){
				skillID = 4;
			}else if(buttonName.Equals("WL5%G02")){
				skillID = 5;
			}else if(buttonName.Equals("AirDash")){
				skillID = 6;
			}else if(buttonName.Equals("AD5%G")){
				skillID = 7;
			}else if(buttonName.Equals("AD5%G01")){
				skillID = 8;
			}else if(buttonName.Equals("AD5%G02")){
				skillID = 9;
			}

			player.GetComponent<Player2>().skillsTreeButtonsAction(skillID);
        	GetComponent<Image>().color = selectedColor;
			GetComponent<Button>().interactable = false;
			player.GetComponent<Player2>().skillPoints--;

			updateUI();
			activateNextButton();
		}else{
			Debug.Log("I co tera? Punkta brak!");
		}

    }

    /// <summary>
    /// Metoda ukatywniająca koleny guzik w kolejności >> drzewo umiejętności.
    /// </summary>
    /// <returns></returns>
	public void	activateNextButton(){
		try{
			nextButton.GetComponent<Button>().interactable = true;
	
		}catch(Exception e){
			//Debug.Log("Ni mo komponentu SKILL POINTS TEXT w UI");
		}
	}

	/// <summary>
	/// Metoda wywoływana przez silnik UI - aktualizacja wyświetlanych na ekranie punktów umiejętności. 
	/// </summary>
	/// <returns></returns>
	public void updateUI(){
		try{
			skillPointsTextUI.GetComponent<Text>().text = "SKILL POINTS: "+player.GetComponent<Player2>().skillPoints;
	
		}catch(Exception e){
			//Debug.Log("Ni mo komponentu SKILL POINTS TEXT w UI");
		}
	}
}
