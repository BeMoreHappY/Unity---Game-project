using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Klasa, która odpowiada za interakcje gracza z elementami otoczenia
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;

    Camera cam;

    /// <summary>
    /// Metoda, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany. Pobiera informacje na temat kamery
    /// </summary>
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    /// <summary>
    /// Metoda, sprawdzająca czy dochodzi do interakcji
    /// </summary>
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2f,Screen.height/2f,0f));
        RaycastHit hit;

        bool succesfulHit = false;

        if (Physics.Raycast(ray, out hit, interactionDistance)){
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null){
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                succesfulHit = true;
            }
        }
        if (!succesfulHit)interactionText.text = "";
    }
    /// <summary>
    /// Metoda, obsługująca poszczególne interakcje
    /// </summary>
    void HandleInteraction(Interactable interactable){
        KeyCode key = KeyCode.E;
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                if (Input.GetKeyDown(key)){
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                if (Input.GetKey(key)){
                    interactable.IncreaseHoldTime();
                    if(interactable.GetHoldTime() > 1f){
                        interactable.Interact();
                        interactable.ResetHoldTime();
                    }
                }else{
                    interactable.ResetHoldTime();
                }
                break;
            default:
                throw new System.Exception("Unsupported type of interactable");
        }
    }
}
