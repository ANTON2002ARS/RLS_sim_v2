using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Game : MonoBehaviour
{    
    [SerializeField] private Task Active_Task;
    [SerializeField] private Transform folder;

    [Header("UI")]
    [SerializeField] private GameObject SureCheck;
    [SerializeField] private GameObject CheckResult;
    [SerializeField] private GameObject Pass_Test;
    [SerializeField] private GameObject Faid_Test;

    void Start()
    {
        //  Active_Task = MenuManager.Menu_Instance.Active_Task;
         if(Active_Task == null){
            Debug.LogError("Task obj not set");
            return;
         }        

         Show_Block(Active_Task.block_need[0].of_Blocks);
         Show_SureChecker(false);
         CheckResult.SetActive(false);

    }

    void Update(){
        if(Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.P))
            Pass_Testing();        
    }
    
    public static Scene_Game test_Instance { get; private set; }
    private void Awake() => test_Instance = this;    

    public void Show_Block(GameObject block){
        GameObject b = Instantiate(block);
        b.transform.SetParent(folder);
    }

    public void Show_SureChecker(bool is_active)=> SureCheck.SetActive(is_active);
    public void Pass_Testing(){
        CheckResult.SetActive(true);
        Pass_Test.SetActive(true);
        Faid_Test.SetActive(false);
    }
    public void Faid_Testing(){
        CheckResult.SetActive(true);
        Pass_Test.SetActive(false);
        Faid_Test.SetActive(true);
    }

    public void Exit_Scene(){
        SceneManager.LoadScene("Menu_Scene");
        MenuManager.Menu_Instance.Active_Task = null;
    }    
}
