using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Game : MonoBehaviour
{    
    [Header("Use of Task")]
    public Abst_Task task_test;
    [SerializeField] private Task Active_Task;
    [SerializeField] private War_Task war_task;
    [SerializeField] private War_targets war_targets;
    [SerializeField] private Transform folder_blocks;
    
    [Header("UI")]
    [SerializeField] private GameObject SureCheck;
    [SerializeField] private GameObject CheckResult;
    [SerializeField] private GameObject Pass_Test;
    [SerializeField] private GameObject Faid_Test;
    [SerializeField] private int index_block;
    [SerializeField] private GameObject Button_Show_IKO;
    [SerializeField] private GameObject Button_Kill_Interference;
    [Header("list blocks")]
    [SerializeField] private GameObject pedalka;
    [SerializeField] private GameObject pos_72;
    [SerializeField] private GameObject pov_71;
    [Header("Text of Target")]
    [SerializeField] private GameObject Panel_Blocks;
    [SerializeField] private GameObject Panel_Target; 
    [SerializeField] private GameObject Buttons_Target_Test;
    [SerializeField] private GameObject Buttons_Target_Test_Report; 
    [SerializeField] public Dropdown Choice_target; 

    [SerializeField] private Text Report_Text_UP;
    [SerializeField] private Text report_text;   
    [SerializeField] private GameObject Panel_learning;
    [SerializeField] private GameObject Panel_learning_Start;
    [SerializeField] private Text Panel_Text_Start;
    
    private int choice_target;

    [Header("for IKO")]
    [SerializeField] private P_71 IKO;
    [Header("Search in thr sector")]
    //для Проверки в тесте на положение. Радиус 2.5f, -2.5< Y,X <2.5
    [SerializeField] private float line_X_border;
    [SerializeField] private float line_Y_border;

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
        Buttons_Target_Test_Report.SetActive(false);        
        report_text.gameObject.SetActive(false);   
        Report_Text_UP.gameObject.SetActive(false);     
        //Panel_learning.SetActive(MenuManager.Menu_Instance.Use_Learning);
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
            if (MenuManager.Menu_Instance.Use_Learning == true)
            {
                Close_Panel_learning_Start(true);
                Panel_Text_Start.text = Active_Task.Text_Learnihg_All;

            }
        }
        else if(task is War_Task){
            Debug.Log("Задачи памех");
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
        else if(task is War_targets){ 
             war_targets = task as War_targets;
             if(war_targets.use_sector){
                Start_Test_Sector_Review(); 
                IKO.gameObject.SetActive(true);
             }
             else if(war_targets.use_reguest_target){
                Start_Test_Reguest();
                IKO.gameObject.SetActive(true);
             }
             else if(war_targets.reguest_height){
                
             }
             else if(war_targets.use_report_for_target){

             }
             else if(war_targets.reguest_height){

             }


        }
        else{
            Debug.LogError("Неизвестный тип данных в Abst_Task");
        }
    }

    void Update(){
        if(Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.P))
            Pass_Testing();        
    }   

    public void Kill_Test_Interference(){
        IKO.gameObject.SetActive(false);
        folder_blocks.gameObject.SetActive(true);
        Show_Block(index_block);
        Button_Kill_Interference.SetActive(false); 
    }

    private void Start_Test_Sector_Review(){
        Report_Text_UP.gameObject.SetActive(true);
        int use_sector = Random.Range(1,5);              
        switch(use_sector){
            case 1:
                line_X_border = -1f;
                line_Y_border = -1f;
                Report_Text_UP.text = "Поиск в секторе 030-060";
                IKO.Span_Target(0f,90f);
                break;
            case 2:
                line_X_border = -1f;
                line_Y_border = 1f;
                Report_Text_UP.text = "Поиск в секторе 120-150";
                IKO.Span_Target(90f,180f);
                break;
            case 3:
                line_X_border = 1f;
                line_Y_border = 1f;
                Report_Text_UP.text = "Поиск в секторе 210-240";
                IKO.Span_Target(180f,240f);
                break;
            case 4:
                line_X_border = 1f;
                line_Y_border = -1f;
                Report_Text_UP.text = "Поиск в секторе 300-330";
                IKO.Span_Target(240f,0f);
                break;
        }
        IKO.Span_Target();
        IKO.Span_Target();
    }

    public void Proverka_Test_Sector_Review(Vector2 Display_IKO_position){
        if(line_X_border == 0 && line_Y_border ==0)
            return;
        
        Vector2 position_display = Display_IKO_position;
        bool x_line_pass = false;
        bool y_line_pass = false;
        
        if(Check_Position(line_X_border,position_display.x)){
            x_line_pass = true;
            if(Check_Position(line_X_border *(-1),position_display.x)){
               // End_Test_Sector_Review(false);
                Debug.Log("Сетка в противоположном направлении по X");
            }
        }

        if(Check_Position(line_Y_border,position_display.y)){
            y_line_pass = true;
            if(Check_Position(line_Y_border*(-1),position_display.y)){
               // End_Test_Sector_Review(false);
                Debug.Log("Сетка в противоположном направлении по Y");
            }
        }

        if(x_line_pass && y_line_pass){
           End_Test_Sector_Review(true);
           Debug.Log("ТЕСТ ПРОЕДЕН");
        }
    } 

    private bool Check_Position(float line, float position){
        return (line > 0) ? (position > line) : (position < line);            
    }

    private void End_Test_Sector_Review(bool is_pass){
        if(is_pass == true)
            Pass_Testing();         
        else
            Faid_Testing();        
    }


    public void Show_IKO()
    {
        bool is_active = !IKO.gameObject.activeSelf;
        IKO.gameObject.SetActive(is_active);
        folder_blocks.gameObject.SetActive(!is_active);
    }
    
    
    //public void Show_Interference()=> IKO.Span_Interference(1);  
    //public void Show_Target()=> IKO.Span_Target();

    public void Show_Block( int index){
        if( folder_blocks.childCount > 0){
            GameObject block = folder_blocks.GetChild(0).gameObject;
            Destroy(block);
        }        
        GameObject b = Instantiate(Active_Task.block_need[index].of_Blocks);
        b.transform.SetParent(folder_blocks);
        b.GetComponent<Abst_Block>().Need_Condition = Active_Task.block_need[index].Command_need;
        if(MenuManager.Menu_Instance.Use_Learning == true)
            Panel_learning_Start.transform.GetChild(0).gameObject.GetComponent<Text>().text = Active_Task.block_need[index].Text_Learnihg_for_Block;
        
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
                report_text.gameObject.SetActive(true);
                Invoke("Off_Report_Text",2f);
                Show_Block(index_block);
                Debug.Log("next block texting");
            }
        }

        Debug.Log("отравотка блока, статус: " + is_pass);
    }



    private void Start_Test_Reguest(){
        IKO.Span_Target();
        Report_Text_UP.gameObject.SetActive(true);
        Report_Text_UP.text = "Определить государственную принадлежность и кол-во целей";
        Panel_Target.SetActive(true);    

    }

    public void Show_Pedalka(){
        IKO.gameObject.SetActive(false);
        if( folder_blocks.childCount > 0){            
            Destroy(folder_blocks.GetChild(0).gameObject);
        }  
        GameObject block_pedalka = Instantiate(pedalka);
        block_pedalka.transform.SetParent(folder_blocks);  
        block_pedalka.GetComponent<Block_Pedalka>().jump_foot.AddListener(Ckick_Pedalka);

    }

    public void Show_POV_71(){
        IKO.gameObject.SetActive(false);
        if( folder_blocks.childCount > 0){            
            Destroy(folder_blocks.GetChild(0).gameObject);
        }  
        GameObject block_pov71 = Instantiate(pov_71);
        block_pov71 .transform.SetParent(folder_blocks);  
    }

    public void Show_POS_71(){
        IKO.gameObject.SetActive(false);
        if( folder_blocks.childCount > 0){            
            Destroy(folder_blocks.GetChild(0).gameObject);
        }  
        GameObject block_pov71 = Instantiate(pov_71);
        block_pov71 .transform.SetParent(folder_blocks); 
    }


    public void Ckick_Pedalka(){
        IKO.gameObject.SetActive(true);
        folder_blocks.gameObject.SetActive(false);       
        IKO.Request_Target_last(); 
    }

    private int Status_Test_Target;

    // 2 из функции получаем
    public void  Click_Button_Target(int number){
        switch (number){
            case 1:
                // Одиночная
                if(!IKO.Last_Target_is_Group())
                    Faid_Testing();
                Status_Test_Target++;
                break;
            case 2:
                // Групповая
                if(IKO.Last_Target_is_Group())
                    Faid_Testing();
                Status_Test_Target++;
                break;
            case 3:
                // Цель 00 нет ответа
                if(!IKO.Last_Target_is_Our())
                    Faid_Testing();
                    Status_Test_Target++;                
                break;
            case 4:
                // Цель 00 свой
                if(IKO.Last_Target_is_Our())
                    Faid_Testing();

                break;
            case 5:
                // Цель 00 сигнал бедствие
                if(!IKO.Last_Target_is_Helper())
                    Faid_Testing();
                    Status_Test_Target++;                
                break;            
        }
        Buttons_Target_Test_Report.SetActive(true);
        End_Test_Target();
    }
    
    

    private void End_Test_Target(){
        if(Status_Test_Target == 2){
            Pass_Testing();
            Status_Test_Target =0;
        }
    }
    

    public void Remove_Option(int optionIndex)
    {
        List<Dropdown.OptionData> options = Choice_target.options;
 
        options.RemoveAt(optionIndex);
  
        Choice_target.options = options;        
        Choice_target.value = 0;
    }

    public int Find_Free_Number(List<int> numbers)
    {     
        numbers.Sort(); 
        int lastNumber = -1;    
        foreach (int number in numbers)
        {
            if (number - lastNumber > 1)
                return lastNumber + 1;
            lastNumber = number;
        }   
        return lastNumber + 1;
    }

    public void Request_Targets()
    {            
        int namber = 0;        
        Choice_target.SetValueWithoutNotify(namber);                
        Choice_target.GetComponentInChildren<Text>().text = Choice_target.options[namber].text;
        //Choice_target.options.Add(new Dropdown.OptionData() { text = "???? 0" + Targets.Count });

    }








    public void Close_Panel_learning_Start(bool is_active) => Panel_learning_Start.SetActive(is_active);

    private void Off_Report_Text()=> report_text.gameObject.SetActive(false);

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
