using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class PlayerMovements : MonoBehaviour
{
    private float previousY;
    private float velocityX;
    private float maxPosition;
    private int currentScore;
    private bool jumping;
    private bool winning = false;
    private bool falling = false;
    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private GameObject rawImage;    
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource fall;
    [SerializeField] private AudioSource win;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private LayerMask JumpableGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0, 0, 0);
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        previousY = rb.transform.position.y;
        maxPosition = transform.position.y;
        jumping = false;
    }

    void Update()
    {
        score.SetText("HEIGHT: " + currentScore);

        if(falling && !fall.isPlaying){
            Destroy(player);
            rawImage.SetActive(true);  
        }

        if(!win.isPlaying && winning){
            SceneManager.LoadScene("SecondEpisode");
        }

        if(currentScore >= 25){
            if(!winning){
                win.Play();
            }
            winning = true;
        }
        if(transform.position.y >= maxPosition - 1)
            jumping = true;
        else
            jumping = false;

        if(currentScore < transform.position.y / 8)
            currentScore = (int) (transform.position.y / 8);

        if(jumping == true && animator.GetBool("jumping")){
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 0.1f, camera.transform.position.z);
        }
        if(rb.transform.position.y < camera.transform.position.y - 21.5){
            if(!fall.isPlaying){
                fall.Play();
                FindObjectOfType<CameraMovement>().stopGravity();
            }
            falling = true;
                 
        }
        if(rb.transform.position.y < previousY){
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
        }
        if(isGround()){
            animator.SetBool("falling", false);
        }
        float dirX = Input.GetAxisRaw("Horizontal"); 
        
        rb.velocity = new Vector3(velocityX, rb.velocity.y, 0);
        if(Input.GetButton("Jump") && isGround() && rb.velocity.y < .1f){
            FindObjectOfType<CameraMovement>().startGravity();
            rb.velocity = new Vector3(rb.velocity.x, 30, 0);
            animator.SetBool("jumping", true);
            jump.Play();
        }

        if(dirX > 0f){
            velocityX = 10;
            animator.SetBool("running", true);
            rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if(dirX < 0f){
            velocityX = -10;
            animator.SetBool("running", true);
            rb.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        
        previousY = rb.transform.position.y;

        if(platforms[1].transform.position.y < camera.transform.position.y - 15){
            System.Random random = new System.Random();
            int randomY = random.Next(8, 12);
            int randomX = random.Next(-15, 10);
            Vector3 spawnPosition = new Vector3((float) randomX, platforms[4].transform.position.y + 8, 0f);
            Quaternion spawnRotation = Quaternion.identity; 
            
            GameObject newObject = Instantiate(platforms[0], spawnPosition, spawnRotation);

            Destroy(platforms[0]);

            for(int i = 0; i < 4; i++){
                platforms[i] = platforms[i + 1];
            }
            platforms[4] = newObject;
        }
        if(transform.position.y > maxPosition && !animator.GetBool("jumping") && !animator.GetBool("falling"))
            maxPosition = transform.position.y;
        
    }
    private bool isGround(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}

