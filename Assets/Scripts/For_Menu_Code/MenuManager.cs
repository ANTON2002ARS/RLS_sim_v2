using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public static Abst_Task Active_Task;
    public Task Isxod_test;
    public List<Abst_Task> Task_Work;
    public List<Abst_Task> Task_War;
    public War_Interference Simply_Show_Interference;
    [SerializeField] private Transform panel_task_work;
    [SerializeField] private Transform panel_task_war;
    [SerializeField] private GameObject button_task;
    [SerializeField] private float button_spacing;
    [Header("UI")]
    [SerializeField] private Button Button_learhihg;
    [SerializeField] private GameObject Panel_IKO;
    public bool Use_Learning;
    [SerializeField] private GameObject panel_war;
    [SerializeField] private GameObject panel_use;
    [SerializeField] private GameObject SPRAVKA;

    public static MenuManager Menu_Instance { get; private set; }
    private void Awake() => Menu_Instance = this;
    
    //public Abst_Task Active_Task;

    void Start()
    {
        Abst_Toggles.to_isxod_all_tumbler = true;
        Panel_IKO.SetActive(false);
        Set_Button(panel_task_work,Task_Work);
        Set_Button(panel_task_war, Task_War);
    }

    public void Test_Work(int index_button)
    {
        Debug.Log("test work. index: " + index_button.ToString());

        if(Task_Work[index_button] == null){
            Debug.LogError("TaskWork is not faind");
            return;
        }
        Active_Task = Task_Work[index_button];
        
        //Abst_Toggles.to_isxod_all_tumbler
        if (Active_Task is Task){
            Task Use_Simple_Test = Active_Task as Task;
            Abst_Toggles.to_isxod_all_tumbler = Use_Simple_Test.to_isxod_all_tumbler;
            Debug.Log("to_isxod_all_tumbler: " + Abst_Toggles.to_isxod_all_tumbler);
        }        
        SceneManager.LoadScene("Scene_Task");
    }

    private void Test_Interference(int index_button){
        Debug.Log("test war, index: " + index_button.ToString());
        if(Task_War[index_button] == null){
            Debug.LogError("TaskWork is not faind");
            return;
        }
        Active_Task = Task_War[index_button];
        SceneManager.LoadScene("Scene_Task");  
    }

    public void Simply_Interference(){
        Debug.Log("test war, Interfence");        
        Active_Task = Simply_Show_Interference;
        SceneManager.LoadScene("Scene_Task");  
    }

    public void Scene_O71_block(){
        SceneManager.LoadScene("Scene_signal");  
    }


    private void Test_Target(int index_button){
        Debug.Log("test war, index: " + index_button.ToString());
        if(Task_War[index_button] == null){
            Debug.LogError("TaskWork is not faind");
            return;
        }
        Active_Task = Task_War[index_button];
        SceneManager.LoadScene("Scene_Task");  
    }

    public void Start_Test_Isxod(){
        Abst_Toggles.to_isxod_all_tumbler = false;
        //Scene_Game.test_instance.Active_Task = Isxod_test;
        Active_Task = Isxod_test;
        SceneManager.LoadScene("Scene_Task");  
    }    

    private void Set_Button(Transform Children_panel, List<Abst_Task> tasks)
    {        
        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject new_button = Instantiate(button_task, Children_panel);
            new_button.transform.SetParent(Children_panel);
            Button button = new_button.GetComponent<Button>();
            int index = i;
            
            Text text = button.GetComponentInChildren<Text>();
            if(tasks[i] is War_Interference){
                War_Interference t = tasks[i] as War_Interference;
                text.text = t.Text_Button;
                button.onClick.AddListener(() => { Test_Interference(index); });
            }
            else if(tasks[i] is War_targets){
                War_targets t = tasks[i] as War_targets;
                text.text = t.Text_Button;
                button.onClick.AddListener(() => { Test_Target(index); });
            }
            else if(tasks[i] is Task){
                Task t = tasks[i] as Task;
                text.text = t.Text_Button;
                button.onClick.AddListener(() => {Test_Work(index); });
            }            
        }
    }    

    public void Use_learning_Mode(){
        Use_Learning = !Use_Learning;
        if(Use_Learning == true){
            Button_learhihg.GetComponent<Image>().color = new Color(60f / 255, 120f / 255, 0f / 255);
        }
        else{
            Button_learhihg.GetComponent<Image>().color = new Color(145f / 255, 145f / 255,145f / 255);
        }

    }    
    public void Show_Panel_War()
    {
        panel_war.SetActive(true);
        panel_use.SetActive(false);
    }
    public void Show_Panel_Use()
    {
        panel_war.SetActive(false);
        panel_use.SetActive(true);
    }

    public void Show_SPRAVKA()
    {
        SPRAVKA.SetActive(!SPRAVKA.activeSelf);
    }

    public void Show_IKO() => Panel_IKO.SetActive(!Panel_IKO.activeSelf);


    public void Open_Scene_RLS()=>SceneManager.LoadScene("RLS_Scene");

    public void Open_Scene_Test_RLS() => SceneManager.LoadScene("Test_RLS");

    public void Exit_App()=> Application.Quit();







}


