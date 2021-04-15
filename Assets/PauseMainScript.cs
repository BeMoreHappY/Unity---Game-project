using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMainScript : MonoBehaviour
{
    public void ResumeGame()
    {
        //TODO: NAPRAWIĆ SZTYWNE WYBIERANIE POZIOMU GRY NA DYNAMICZNE
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}