using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public List<Task> Task_Work;
    public List<Task> Task_War;
    [SerializeField] private Transform panel_task_work;
    [SerializeField] private Transform panel_task_war;
    [SerializeField] private GameObject button_task;
    [SerializeField] private float button_spacing;

    public static MenuManager Menu_Instance { get; private set; }
    private void Awake() => Menu_Instance = this;
    
    public Task Active_Task;

    void Start()
    {
        Set_Button(panel_task_work,Task_Work, false);
        Set_Button(panel_task_war, Task_War, true);
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

    private void Set_Button(Transform Children_panel, List<Task> tasks , bool is_war)
    {
        Vector2 button_Position = new Vector2(0, Children_panel.localPosition.y + 420f);
        for (int i = 0; i < tasks.Count; i++)
        {
            GameObject new_button = Instantiate(button_task, Children_panel);
            new_button.transform.localPosition = button_Position;            
            button_Position.y -= button_spacing;
            Button button = new_button.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() =>
            {
                if(is_war){
                    Test_War(index);
                }
                else{
                    Test_Work(index);                    
                }                                
            });
            Text text = button.GetComponentInChildren<Text>();
            text.text = tasks[i].Text_Button;
        }
    }







}


