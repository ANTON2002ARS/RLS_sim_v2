using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Game : MonoBehaviour
{    
    [SerializeField] private Task Active_Task;
    [SerializeField] private War_Task war_task;
    [SerializeField] private Transform folder_blocks;

    [Header("UI")]
    [SerializeField] private GameObject SureCheck;
    [SerializeField] private GameObject CheckResult;
    [SerializeField] private GameObject Pass_Test;
    [SerializeField] private GameObject Faid_Test;
    [SerializeField] private int index_block;
    [SerializeField] private GameObject Button_Show_IKO;
    [SerializeField] private GameObject Button_Kill_Interference;
    [SerializeField] private GameObject Panel_Target;
    [SerializeField] private GameObject Panel_Interference;
    [SerializeField] private GameObject Panel_PRS;

    [Header("for IKO")]
    [SerializeField] private P_71 IKO;

    public static Scene_Game test_instance { get; private set; }
    private void Awake() => test_instance = this;   

    void Start()
    {
        Show_SureChecker(false);
        CheckResult.SetActive(false);

        Button_Show_IKO.SetActive(false);
        Button_Kill_Interference.SetActive(false);
        Panel_Target.SetActive(false);
        Panel_Interference.SetActive(false);
        Panel_PRS.SetActive(false);
        Show_IKO(false);        
        
        Abst_Task task = MenuManager.Menu_Instance.Active_Task;

        if(task is Task){
            Debug.Log("start task");
            Active_Task = task as Task;
            if(Active_Task == null)
            {
                Debug.LogError("Task obj not set");
                return;
            }
            Show_Block(index_block);
        }
        else if(task is War_Task){
            Debug.Log("start war test");
            War_Task war = task as War_Task;


            

        }
        else{
            Debug.LogError("Неизвестный тип данных в Abst_Task");
        }

    }

    void Update(){
        if(Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.P))
            Pass_Testing();        
    }

    public void Show_IKO(bool is_active)
    {
        IKO.gameObject.SetActive(is_active);
        folder_blocks.gameObject.SetActive(!is_active);
    }
    
   

    public void Show_Block( int index){
        if( folder_blocks.childCount > 0){
            GameObject block = folder_blocks.GetChild(0).gameObject;
            Destroy(block);
        }        
        GameObject b = Instantiate(Active_Task.block_need[index].of_Blocks);
        b.transform.SetParent(folder_blocks);
        b.GetComponent<Abst_Block>().Need_Condition = Active_Task.block_need[index].Command_need;
    }

    public void Calling_Completion_Block(bool is_pass){
        if(!is_pass)
            Faid_Testing();
        else{
            index_block++;
            if(index_block >= Active_Task.block_need.Count){
                Pass_Testing();
                index_block=0;
                GameObject block = folder_blocks.GetChild(0).gameObject;
                //Destroy(block);
            }
            else{
                Show_Block(index_block);
                Debug.Log("next block texting");
            }
        }

        Debug.Log("отравотка блока, статус: " + is_pass);            
        
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
        //MenuManager.Menu_Instance.Active_Task = null;
    }    
}
