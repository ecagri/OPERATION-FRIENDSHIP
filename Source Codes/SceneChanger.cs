using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] string SceneName;
    public void changeScene(){
        SceneManager.LoadScene(SceneName);
    }
}
