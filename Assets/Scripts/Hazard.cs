using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.receiveDamage(damage);
        }
    }
}
