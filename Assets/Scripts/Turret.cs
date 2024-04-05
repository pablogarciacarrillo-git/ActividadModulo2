using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet, player;
    public Transform bulletCreationPoint, movingPart;
    public float shootTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Shoot();
    }

    // Update is called once per frame
    void Update()
    {
        movingPart.LookAt(player.transform.position + Vector3.up*0.5f);
    }

    void Shoot()
    {
        GameObject currentBullet = Instantiate(bullet, bulletCreationPoint.transform.position, Quaternion.identity);
        currentBullet.transform.LookAt(player.transform.position + Vector3.up*0.5f);
        Invoke("Shoot", shootTime);
    }
}
