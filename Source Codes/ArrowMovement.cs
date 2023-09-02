using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowMovement : MonoBehaviour
{
    private bool dissappear = true;
    private float gravity = 0;
    private void Start(){
    }
    private void Update() {
        ShootingWithBow bow = FindObjectOfType<ShootingWithBow>();

        if(bow.getGravityStarts()){
            gravity += 1.0f;
            this.GetComponent<Rigidbody2D>().velocity = new Vector3((float) (Math.Cos(bow.getAngleArrow() * (Math.PI / 180.0)) * bow.getArrowVelocity()), (float) (Math.Sin(bow.getAngleArrow() * (Math.PI / 180.0)) * bow.getArrowVelocity()) - gravity, 0);
        }
        if(this.GetComponent<Rigidbody2D>().velocity.y != 0 && this.GetComponent<Rigidbody2D>().velocity.x != 0)
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, (float) ((180.0 / Math.PI) * Math.Atan(this.GetComponent<Rigidbody2D>().velocity.y / this.GetComponent<Rigidbody2D>().velocity.x))); 
    
        if(this.transform.position.y < -15 || this.transform.position.y > 25 || this.transform.position.x < -25 || this.transform.position.x > 25){
            dissappear = true;
            GameObject arrow = GameObject.FindGameObjectWithTag("Arrow");
            Destroy(arrow);
            Destroy(this);
        } 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Platform" || other.gameObject.name == "Foreground" || other.gameObject.name == "Player"){
            dissappear = true;
            GameObject arrow = GameObject.FindGameObjectWithTag("Arrow");
            Destroy(arrow);
            Destroy(this);
        }
    }

    public bool getDissappear(){
        return dissappear;
    }

    public void setDisaappear(bool dissappear){
        this.dissappear = dissappear;
    }
}
