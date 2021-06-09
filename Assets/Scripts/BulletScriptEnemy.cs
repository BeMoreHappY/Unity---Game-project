using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Klasa, która odpowiada za pocisk przeciwników
/// </summary>
public class BulletScriptEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 5;
    /// <summary>
	/// Funkcja, która wykonuje się co klatkę
	/// </summary>
    void Update()
    {
        Destroy(this.gameObject, 5);
    }
    /// <summary>
    /// Funkcja, która sprawdza czy obiekt zderzył się z innym obiektem
    /// </summary>
    /// <param name="Enemy">Przyjmuje collider obiektu</param>
    void OnTriggerEnter(Collider Enemy){
        if(Enemy.gameObject.CompareTag("Player"))
        {
            Player2 target = Enemy.GetComponent<Player2>();
            target.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if(Enemy.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
}