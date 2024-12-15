using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public List<Abst_Task> Task_Work;
    public List<Abst_Task> Task_War;
    [SerializeField] private Transform panel_task_work;
    [SerializeField] private Transform panel_task_war;
    [SerializeField] private GameObject button_task;
    [SerializeField] private float button_spacing;
    [Header("UI")]
    [SerializeField] private Button Button_learhihg;
    [SerializeField] private GameObject Panel_IKO;
    public bool Use_Learning;

    public static MenuManager Menu_Instance { get; private set; }
    private void Awake() => Menu_Instance = this;
    
    public Abst_Task Active_Task;

    void Start()
    {
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
        SceneManager.LoadScene("Scene_Task");
    }

    private void Test_War(int index_button){
        Debug.Log("test war, index: " + index_button.ToString());
        if(Task_War[index_button] == null){
            Debug.LogError("TaskWork is not faind");
            return;
        }
        Active_Task = Task_War[index_button];
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
            if(tasks[i] is War_Task){
                War_Task t = tasks[i] as War_Task;
                text.text = t.Text_Button;
                button.onClick.AddListener(() =>
            {  
                Test_War(index);         
            });
            }
            else if(tasks[i] is Task){
                Task t = tasks[i] as Task;
                text.text = t.Text_Button;
                button.onClick.AddListener(() =>
            {  
                Test_Work(index);         
            });
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

    public void Show_IKO() => Panel_IKO.SetActive(!Panel_IKO.activeSelf);


    public void Open_Scene_RLS()=>SceneManager.LoadScene("RLS_Scene");

    public void Exit_App()=> Application.Quit();







}


