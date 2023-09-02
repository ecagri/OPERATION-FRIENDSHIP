using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDie : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource dying;
    [SerializeField] private TextMeshProUGUI enemies;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Arrow"){
            animator.SetBool("dying", true);
            dying.Play();
        }
    }

    public void kill(){
        enemies.SetText("Enemies: "+ (enemies.text[9] - '1') + "\\3");
        Destroy(player);
    }
}
