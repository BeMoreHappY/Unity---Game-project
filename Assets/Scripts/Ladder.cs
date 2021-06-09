using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa, która odpowiada za obsługę drabiny
/// </summary>
public class Ladder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool canClimb;
    private float speed;
    /// <summary>
    /// Metoda, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany. Ustawia wartości domyślne wspinania
    /// </summary>
    void Start()
    {
        canClimb = false;
        speed = 10;
    }
    /// <summary>
    /// Metoda uruchamiająca wspinanie
    /// </summary>
    private void OnCollisionEnter(Collision coll) {
        if (coll.gameObject == player){
            canClimb = true;
            Debug.Log("Drabina ON");
        }
    }
    /// <summary>
    /// Metoda kończąca wspinania
    /// </summary>
    private void OnCollisionExit(Collision coll2) {
        if (coll2.gameObject == player){
            canClimb = false;
            Debug.Log("Drabina OFF");
        }
    }
    /// <summary>
    /// Metoda sprawdzająca czy gracz się wspina
    /// </summary>
    void Update()
    {
        if (canClimb){
            if(Input.GetKey(KeyCode.W)){
                player.transform.Translate (new Vector3(0,1,0)*Time.deltaTime*speed);
            }
            if(Input.GetKey(KeyCode.S)){
                player.transform.Translate (new Vector3(0,-1,0)*Time.deltaTime*speed);
            }
        } 
    }
}
