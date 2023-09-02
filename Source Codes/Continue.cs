using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject bow;

    public void setScreen(){
        canvas.SetActive(false);
        if(bow != null)
            bow.SetActive(true);
    }
}
