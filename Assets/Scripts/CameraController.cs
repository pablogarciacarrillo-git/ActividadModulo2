using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    /*
    public Camera camera;
    public Transform focusPoint;
    */
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        /*
        Vector3 cameraPosition = new Vector3(focusPoint.transform.position.x, 20, focusPoint.transform.position.y);
        
        camera.transform.position = cameraPosition;
        camera.transform.LookAt(focusPoint.transform);
        */


    }
}
