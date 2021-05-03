using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool canClimb;
    private float speed;

    void Start()
    {
        canClimb = false;
        speed = 10;
    }

    private void OnCollisionEnter(Collision coll) {
        if (coll.gameObject == player){
            canClimb = true;
            Debug.Log("Drabina ON");
        }
    }

    private void OnCollisionExit(Collision coll2) {
        if (coll2.gameObject == player){
            canClimb = false;
            Debug.Log("Drabina OFF");
        }
    }

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
