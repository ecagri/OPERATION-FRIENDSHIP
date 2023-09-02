using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlayer : MonoBehaviour
{
    private bool collideTrampoline = false;
    private bool collideGround = false;
    private bool collideBullet = false;
    [SerializeField] private Animator friendAnimator;

    public void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Trampoline"){
            collideTrampoline = true;
        }else if(other.gameObject.name == "Platform" || other.gameObject.name == "Platform1" || other.gameObject.name == "Platform2"){
            collideGround = true;
        }else if(other.gameObject.name == "Platform3"){
            friendAnimator.SetBool("speak", true);
        }else if(other.gameObject.name == "Bullet"){
            collideBullet = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.name == "Trampoline"){
            collideTrampoline = false;
        }else if(other.gameObject.name == "Platform" || other.gameObject.name == "Platform1" || other.gameObject.name == "Platform2"){
            collideGround = false;
        }else if(other.gameObject.name == "Bullet"){
            collideBullet = false;
        }
    }

    public bool getCollideTrampoline(){
        return collideTrampoline;
    }

    public bool getCollideGround(){
        return collideGround;
    }

    public bool getCollideBullet(){
        return collideBullet;
    }
}
