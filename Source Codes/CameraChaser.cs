using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaser : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y + 15, transform.position.z);
    }
}
