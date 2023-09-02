using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private bool shooted = false;
    private int wayPointIndex = 0;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private AudioSource fire;
    [SerializeField] private AudioSource dying;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private string platformName;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shooted){
            bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, soldier.transform.position, 50 * Time.deltaTime);
        }
        if(wayPointIndex < wayPoints.Length){
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].transform.position, 8 * Time.deltaTime);
            if(wayPoints[wayPointIndex].transform.position.x - 1 < transform.position.x && transform.position.x < wayPoints[wayPointIndex].transform.position.x + 1){
                wayPointIndex++;
            }
        }else{
            wayPointIndex = 0;
        }

        if(wayPointIndex == 1){
            transform.localScale = new Vector3(-2, 2, 1);
        }else{
            transform.localScale = new Vector3(2, 2, 1);
        }
        GameObject platform = GameObject.Find(platformName);
        if(FindObjectOfType<SoldierMovement>() != null && FindObjectOfType<SoldierMovement>().isShootable() && platform.transform.position.y < soldier.transform.position.y && ((transform.localScale.x < 0 && transform.position.x > soldier.transform.position.x) || (transform.localScale.x > 0 && transform.position.x < soldier.transform.position.x))){
            animator.SetBool("shoot", true);
            if(!fire.isPlaying && shooted == false)
                fire.Play();
        }else{
            animator.SetBool("shoot", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Player"){
            wayPointIndex++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Weapon"){
            if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f){
                Destroy(bullet);
                animator.SetBool("die", true);
                dying.Play();
            }
        }
    }

    public void kill(){
        Destroy(enemy);
    }

    public void shoot(){
        bullet.transform.parent = null;
        shooted = true;
    }
}
