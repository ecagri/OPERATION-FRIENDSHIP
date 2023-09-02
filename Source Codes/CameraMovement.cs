using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    private float speed = 0.0f;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
    }

    public void stopGravity(){
        speed = 0.0f;
    }

    public void startGravity(){
        speed = 0.075f;
    }
}
