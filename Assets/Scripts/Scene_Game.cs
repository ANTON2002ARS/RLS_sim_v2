using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Game : MonoBehaviour
{    
    [SerializeField] private Task Active_Task;
    [SerializeField] private Transform folder;
    void Start()
    {
        //  Active_Task = MenuManager.Menu_Instance.Active_Task;
         if(Active_Task == null){
            Debug.LogError("Task obj not set");
            return;
         }        

         Show_Block(Active_Task.block_need[0].of_Blocks);
    }
    
    public static Scene_Game test_Instance { get; private set; }
    private void Awake() => test_Instance = this;    

    public void Show_Block(GameObject block){
        GameObject b = Instantiate(block);
        b.transform.SetParent(folder);
    }

    public void Exit_Scene(){
        SceneManager.LoadScene("Menu_Scene");
        MenuManager.Menu_Instance.Active_Task = null;
    }    
}
