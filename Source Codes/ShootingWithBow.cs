using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class ShootingWithBow : MonoBehaviour
{
    private int currentArrow = 0;
    private int number_of_arrows = 5;
    private float VelocityArrow = 0;
    private float AngleArrow;
    private bool gravityStarts = false;
    private bool winning = false;
    private bool isDragging = false;
    private SpriteRenderer spriteRenderer;
    private Vector3 previousMousePosition;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private GameObject rawImage;
    [SerializeField] private TextMeshProUGUI remainArrows;
    [SerializeField] private TextMeshProUGUI remainEnemies;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource shooting;

    private void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        previousMousePosition = Input.mousePosition;
    }

    private void Update(){
        if(remainEnemies.text[9]  == '0' && !winning){
            win.Play();
            winning = true;
        }
        if(!win.isPlaying && winning){
            SceneManager.LoadScene("LastEpisode");
        }
        bool anyArrows = false;
        for(int i = 0; i < arrows.Length; i++){
            if(arrows[i] != null){
                anyArrows = true;
                break;
            }
        }
        if(!anyArrows && !winning){
            rawImage.SetActive(true);
        }


        ArrowMovement arrowMovement = FindObjectOfType<ArrowMovement>();
        if (Input.GetMouseButtonDown(0) && arrowMovement.getDissappear()){
            isDragging = true;
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && arrowMovement.getDissappear()){
            shooting.Play();
            number_of_arrows -=1 ;
            remainArrows.SetText("Arrows: " + number_of_arrows + "\\5");
            arrowMovement.setDisaappear(false);
            isDragging = false;
            if(spriteRenderer.sprite.name == sprites[1].name){
                VelocityArrow = 30;
            }else if(spriteRenderer.sprite.name == sprites[2].name){
                VelocityArrow = 60;
            }
            GameObject.FindGameObjectWithTag("Arrow").transform.parent = null;
            AngleArrow = transform.eulerAngles.z;
            spriteRenderer.sprite = sprites[0];
            gravityStarts = true;
            GameObject.FindGameObjectWithTag("Arrow").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        if(isDragging){
            Vector3 currentMousePosition = Input.mousePosition;
            float mouseDeltaX = (currentMousePosition.x - previousMousePosition.x) * - 1;
            float mouseDeltaY = (currentMousePosition.y - previousMousePosition.y) * - 1;
            if(mouseDeltaX >= 50){
                spriteRenderer.sprite = sprites[2];
            }
            else if(25 <= mouseDeltaX  && mouseDeltaX < 50){
                spriteRenderer.sprite = sprites[1];
            }
            else if(mouseDeltaX < 25){
                spriteRenderer.sprite = sprites[0];
            }

            if(-90 <= mouseDeltaY && mouseDeltaY <= 90){
                transform.rotation = Quaternion.Euler(0, 0, mouseDeltaY);
            }
        }
        if(arrows[currentArrow] == null){
            VelocityArrow = 0;
            gravityStarts = false;
            currentArrow += 1;
            arrows[currentArrow].SetActive(true);
        }
    }

    public bool getGravityStarts(){
        return gravityStarts;
    }

    public float getAngleArrow(){
        return AngleArrow;
    }

    public float getArrowVelocity(){
        return VelocityArrow;
    }

}   
