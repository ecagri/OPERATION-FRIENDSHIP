using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject SpeechBuble;
    [SerializeField] private Animator soldierAnimator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void speak(){
        SpeechBuble.SetActive(true);
        animator.SetBool("speak", false);
    }

    public void soldierSpeak(){
        soldierAnimator.SetBool("speak", true);
    }
}
