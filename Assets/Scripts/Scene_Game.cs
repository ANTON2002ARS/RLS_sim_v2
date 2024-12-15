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
    public Abst_Task task_test;
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
    [SerializeField] private GameObject report_text;
    [SerializeField] private GameObject Panel_learning;
    [SerializeField] private Text text_learning;

    [Header("for IKO")]
    [SerializeField] private P_71 IKO;

    public static Scene_Game test_instance { get; private set; }
    private void Awake() => test_instance = this;   

    void Start()
    {
        //Abst_Task task = MenuManager.Menu_Instance.Active_Task;
        Abst_Task task = task_test;

        Show_SureChecker(false);
        CheckResult.SetActive(false);        
        Button_Show_IKO.SetActive(false);
        Button_Kill_Interference.SetActive(false);
        Panel_Target.SetActive(false);
        Panel_Interference.SetActive(false);
        Panel_PRS.SetActive(false);
        report_text.SetActive(false);        
        Panel_learning.SetActive(MenuManager.Menu_Instance.Use_Learning);
        IKO.gameObject.SetActive(false);
        folder_blocks.gameObject.SetActive(true);        

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
            war_task = task as War_Task; 

            Active_Task = new Task();
            Active_Task.block_need = war_task.block_need;

            if( war_task.use_passive){
                IKO.Span_Interference(1);
            }else if( war_task.use_local){
                IKO.Span_Interference(2);
            }else if( war_task.use_nip){
                IKO.Span_Interference(3);
            }else if( war_task.use_active_noise){
                IKO.Span_Interference(4);
            }else if ( war_task.use_respons_answer){
                IKO.Span_Interference(5);
            }
            //else if(war.use_passive || war.use_local || war.use_nip || war.use_active_noise || war.use_respons_answer){
               // Debug.Log("ALL ERROR");}

            Button_Show_IKO.SetActive(true);
            Button_Kill_Interference.SetActive(true);   
            IKO.gameObject.SetActive(true);
            folder_blocks.gameObject.SetActive(false);
        }
        else{
            Debug.LogError("Неизвестный тип данных в Abst_Task");
        }
    }

    void Update(){
        if(Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.P))
            Pass_Testing();        
    }   

    public void Start_Test_Interference(){
        IKO.gameObject.SetActive(false);
        folder_blocks.gameObject.SetActive(true);
        Show_Block(index_block);
        Button_Kill_Interference.SetActive(false); 
    }

    public void Show_IKO()
    {
        bool is_active = !IKO.gameObject.activeSelf;
        IKO.gameObject.SetActive(is_active);
        folder_blocks.gameObject.SetActive(!is_active);
    }
    
    public void Show_Interference(){
        IKO.Span_Interference(1);
    }
   

    public void Show_Block( int index){
        if( folder_blocks.childCount > 0){
            GameObject block = folder_blocks.GetChild(0).gameObject;
            Destroy(block);
        }        
        GameObject b = Instantiate(Active_Task.block_need[index].of_Blocks);
        b.transform.SetParent(folder_blocks);
        b.GetComponent<Abst_Block>().Need_Condition = Active_Task.block_need[index].Command_need;
        text_learning.text = Active_Task.block_need[index].Text_Learnihg_for_Block;
    }

    public void Calling_Completion_Block(bool is_pass){
        if(!is_pass)
            Faid_Testing();
        else{
            index_block++;
            
            if(index_block >= Active_Task.block_need.Count){
                Pass_Testing();
                index_block = 0;
                //GameObject block = folder_blocks.GetChild(0).gameObject;
                //Destroy(block);
            }
            else{
                report_text.SetActive(true);
                Invoke("Off_Report_Text",2f);
                Show_Block(index_block);
                Debug.Log("next block texting");
            }
        }

        Debug.Log("отравотка блока, статус: " + is_pass);
    }

    private void Off_Report_Text()=> report_text.SetActive(false);

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
