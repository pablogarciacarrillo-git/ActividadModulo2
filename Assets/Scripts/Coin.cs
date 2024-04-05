using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 10;

    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.score += points;
            
            Destroy(gameObject);
        }
        
    }
}
