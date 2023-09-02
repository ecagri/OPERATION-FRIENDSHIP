using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    private Animator animator;
    private bool shootable = true;
    private bool pressedS = false;
    private float xVelocity = 8.0f;
    [SerializeField] private GameObject Rawimage;
    [SerializeField] private GameObject SpeechBuble;
    [SerializeField] private GameObject info;
    [SerializeField] private GameObject[] bodyParts;
    [SerializeField] private Animator[] animatorTrampolines;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private LayerMask JumpableGround;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource winSound;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {   
        float dirX = Input.GetAxisRaw("Horizontal"); 
        if(Input.GetMouseButtonDown(0)){
            animator.SetBool("attack", true);
        }else if(Input.GetMouseButtonUp(0)){
            animator.SetBool("attack", false);
        }
        if(dirX == 0){
            animator.SetBool("run", false);
        }else{
            animator.SetBool("run", true);
        }
        if(isGround()){
            animator.SetBool("jump", false);
        }

        if(Input.GetButton("Jump") && isGround()){
            rb.velocity = new Vector3(rb.velocity.x, 15, 0);
            animator.SetBool("jump", true);
            animator.SetBool("sit", false);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            pressedS = true;
        }else if(Input.GetKeyUp(KeyCode.S)){
            pressedS = false;
        }

        if(pressedS && isGround() && rb.velocity.x == .0f && rb.velocity.y == 0){
            xVelocity = 4.0f;
            animator.SetBool("sit", true);
            shootable = false;
            for(int i = 0; i < bodyParts.Length; i++){
                bodyParts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }else if(pressedS && isGround()){
            xVelocity = 4.0f;
            animator.SetBool("sit", true);
            shootable = true;
            for(int i = 0; i < bodyParts.Length; i++){
                bodyParts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        } else{
            xVelocity = 8.0f;
            animator.SetBool("sit", false);
            shootable = true;
            for(int i = 0; i < bodyParts.Length; i++){
                bodyParts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }

        if(dirX > 0){
            rb.transform.localScale = new Vector3(-1, 1, 1);
            SpeechBuble.transform.localScale = new Vector3(1, 1, 1);
        }else if(dirX < 0){
            rb.transform.localScale = new Vector3(1, 1, 1);
            SpeechBuble.transform.localScale = new Vector3(-1, 1, 1);
        }
        rb.velocity = new Vector3(dirX * xVelocity, rb.velocity.y, 0);

        if(FindObjectOfType<SoldierPlayer>().getCollideTrampoline() && !FindObjectOfType<SoldierPlayer>().getCollideGround()){
            for(int i = 0; i < animatorTrampolines.Length; i++){
                animatorTrampolines[i].SetBool("trampoline", true);
            }
            if(!jump.isPlaying)
                jump.Play();
            rb.velocity = new Vector3(rb.velocity.x, 30, 0);
        }else{
            for(int i = 0; i < animatorTrampolines.Length; i++){
                animatorTrampolines[i].SetBool("trampoline", false);
            }
        }

        if(FindObjectOfType<SoldierPlayer>().getCollideBullet()){
            rb.bodyType = RigidbodyType2D.Static;
            animator.SetBool("die", true);
        }
    }

    private bool isGround(){
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
    
    public bool isShootable(){
        return shootable;
    }

    public void tryAgain(){
        Rawimage.SetActive(true);
    }

    public void speak(){
        SpeechBuble.transform.parent.gameObject.SetActive(true);
    }

    public void win(){
        winSound.Play();
    }

    public void showInfo(){
        info.SetActive(true);
    }
}
