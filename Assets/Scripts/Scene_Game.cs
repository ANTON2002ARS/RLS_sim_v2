using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Game : MonoBehaviour
{    
    private Task Active_Task;
    void Start()
    {
        //  Active_Task = MenuManager.Menu_Instance.Active_Task;
        //  if(Active_Task == null){
        //     Debug.LogError("Task obj not set");
        //     return;
        //  }        
    }


    public void Exit_Scene(){
        SceneManager.LoadScene("Menu_Scene");
        MenuManager.Menu_Instance.Active_Task = null;
    }

    
}
