using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Test : MonoBehaviour
{
    public  Color select_color;
    public  Color main_color;
    [Header("for Menu")]
    [SerializeField] private GameObject menu;
    [SerializeField] private Button[] buttons_roles;
    [SerializeField] private Button buttons_deoloyment;
    [SerializeField] private Button buttons_folding;
    [SerializeField] private GameObject Panel_Sure_Check;
    [SerializeField] private GameObject Panel_Check_Result;

    [Header("for Testing")]
    [SerializeField] private GameObject actions_select;
    [SerializeField] private Command Action_deoloyment;
    [SerializeField] private Command Action_folding;
    [SerializeField] private GameObject[] Stars;
    [SerializeField] private GameObject Button_Prefab;
    [SerializeField] private Transform Content;
    [SerializeField] private Text text_timer;
    [SerializeField] private Button M1_button;
    [SerializeField] private Button M2_button;
    [SerializeField] private Button Outside_button;
    [SerializeField] private Options M1;
    [SerializeField] private Options M2;
    [SerializeField] private Options Outside;
    
    public float totalTime = 540; // 9 минут в секундах    
    private bool start_timer = false;
    public static Game_Test instance;
    private void Awake()=>instance =this;
    private int _mistake;
    public int Mistakes{
        get{return _mistake;}
        set{
            _mistake = value;
            if(_mistake >= Stars.Length)                
                Fall_Test(_mistake); 
            else if(_mistake == 0)
                Active_Stars(true);  
            else
                Stars[_mistake-1].SetActive(false);            
            Debug.Log("Get mistakes, count: " + _mistake);
        }
    }

    public static int Index_Selected_Element;

    private int role;    
    private Option_Test test_rls;
    private enum Option_Test {
        is_deoloyment,
        is_folding,
        none
    }

    public void Start(){
        test_rls = Option_Test.none;
        menu.SetActive(true);
        Panel_Check_Result.SetActive(false);
        Panel_Sure_Check.SetActive(false);
        actions_select.SetActive(false);

        //Creat_File(Action_deoloyment);
        //Creat_File(Action_folding);
    }  

    public void Update()
    {
        if (start_timer)
        {
            totalTime -= Time.deltaTime;
            if (totalTime <= 0)
            {
                totalTime = 0;
                start_timer = false; // Остановить таймер
                Fall_Test(_mistake);
            }
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        text_timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Creat_File(Command command){
        string file_name = "ответы для " + command.Name_Command + ".ini";
        string file_path = "Assets/"; 
        string full_path = Path.Combine(file_path, file_name);
        
        using (StreamWriter writer = new StreamWriter(full_path)){
            writer.WriteLine("Для номера 1:");
            foreach(var s in command.ActionsPos1)
                writer.WriteLine(s.ActionName);
            writer.WriteLine("Для номера 2:");
            foreach(var s in command.ActionsPos2)
                writer.WriteLine(s.ActionName);
                writer.WriteLine("Для номера 3:");
            foreach(var s in command.ActionsPos3)
                writer.WriteLine(s.ActionName);
                writer.WriteLine("Для номера 4:");
            foreach(var s in command.ActionsPos4)
                writer.WriteLine(s.ActionName);
                writer.WriteLine("Для номера 5:");
            foreach(var s in command.ActionsPos5)
                writer.WriteLine(s.ActionName);
        }
    }

    public void Sure_To_Menu()=>Panel_Sure_Check.SetActive(true);
    public void Cansel_Menu()=>Panel_Sure_Check.SetActive(false);

    public void Back_To_Menu(){
        menu.SetActive(true);
        Panel_Sure_Check.SetActive(false);
        Panel_Check_Result.SetActive(false);
        actions_select.SetActive(false);
        role = 0;        
        test_rls = Option_Test.none;
        Reset_Color_Button_Menu();
        Debug.Log("END TEST");
        start_timer = false;
    }
    public bool Verify_Passing_Test(int Index_of_element, TestVariantAction variantaction){
        List<TestVariantAction> target_list =null;
        if(test_rls == Option_Test.is_deoloyment){            
            switch (role)
            {
                case 1:
                target_list = Action_deoloyment.ActionsPos1.ToList();
                break;
                case 2:
                target_list = Action_deoloyment.ActionsPos2.ToList();
                break;
                case 3:
                target_list = Action_deoloyment.ActionsPos3.ToList();
                break;
                case 4:
                target_list = Action_deoloyment.ActionsPos4.ToList();
                break;
                case 5:
                target_list = Action_deoloyment.ActionsPos5.ToList();
                break;                
            }
        }
        else if(test_rls == Option_Test.is_folding){
            switch (role)
            {
                case 1:
                target_list = Action_folding.ActionsPos1.ToList();
                break;
                case 2:
                target_list = Action_folding.ActionsPos2.ToList();
                break;
                case 3:
                target_list = Action_folding.ActionsPos3.ToList();
                break;
                case 4:
                target_list = Action_folding.ActionsPos4.ToList();
                break;
                case 5:
                target_list = Action_folding.ActionsPos5.ToList();
                break;                
            }
        }

        if(target_list.Count ==0){
            Debug.LogError("ERROR!!! List is null");
            return false;
        }        
        
        if(target_list[Index_of_element -1] == variantaction){   
            if(Index_of_element >= target_list.Count){           
                Pass_Test(_mistake);            
            } 
            Debug.Log("Status TRUE Index_of_element: " + Index_of_element.ToString());
            return true;           
        }
        else{
            Debug.Log("Status FALSE");
            Debug.Log("Correct: " + target_list[Index_of_element -1].ActionName);
            Debug.Log("Receiv: "  + variantaction.ActionName);
            Mistakes++;            
            return false;
        }
    }

    private void Start_Test(){
        if(role == 0) return;
        if(test_rls == Option_Test.none) return;
        Debug.Log("START TEST");
        Debug.Log("role: "+ role.ToString()+ " test: " + test_rls);
        menu.SetActive(false);
        actions_select.SetActive(true);
        Reset_ALL_Elements();
        Placement_Outside();
        start_timer =true;
        totalTime = 540;
    }

    private void Pass_Test(int mistakes){
        Panel_Check_Result.SetActive(true);   
        Panel_Check_Result.GetComponent<Showing_Result_Test>().Show_Pass_Test(mistakes);  
        Debug.Log("Count Mistakes: " + mistakes);
    }
    
    private void Fall_Test(int mistakes){
        Panel_Check_Result.SetActive(true);  
        Panel_Check_Result.GetComponent<Showing_Result_Test>().Show_Fail_Test(mistakes);
        Debug.Log("Count Mistakes: " + mistakes);
    }

    

    public void Set_Role(int r){        
        role =r;
        foreach (var b in buttons_roles)
            Switch_color(b, main_color);     
        Switch_color(buttons_roles[r-1], select_color);        
        Start_Test();
    }

    public void Start_Deloyment(){
        test_rls = Option_Test.is_deoloyment;
        Switch_color(buttons_deoloyment,select_color);
        Switch_color(buttons_folding, main_color);
        Start_Test();
    }

    public void Start_Folding(){
        test_rls = Option_Test.is_folding;
        Switch_color(buttons_deoloyment, main_color);
        Switch_color(buttons_folding, select_color);         
        Start_Test();
    }
    
    public void Placement_M1()=>Placement_Text_List(M1, M1_button);
    public void Placement_M2()=>Placement_Text_List(M2,M2_button);
    public void Placement_Outside()=> Placement_Text_List(Outside, Outside_button);

    public void Placement_Text_List(Options option, Button button_click){ 

        Reset_Color_Button_Navigation();
        Switch_color(button_click, select_color);

        if(test_rls == Option_Test.is_deoloyment)
            Initialization_Button(option.variants_deoloyment);
        else if(test_rls == Option_Test.is_folding){
            if(option.use_list_deoloyment== true)
                Initialization_Button(option.variants_deoloyment);
            else
                Initialization_Button(option.variants_folding);            
        }else
            Debug.Log("ERROR!!!");        
        Debug.Log("Button is placement, for: " + test_rls);
    }

    private void Initialization_Button(List<TestVariantAction> variantActions){
        foreach (Transform child in Content)
            Destroy(child.gameObject);  
        foreach (var opt in variantActions)
        {
           GameObject buttonInstance = Instantiate(Button_Prefab,Content);
           buttonInstance.GetComponent<Button_Body>().Get_Variant(opt);                     
        }     
    }

    private void Reset_ALL_Elements(){
        foreach (TestVariantAction v in M1.variants_deoloyment)
            v.number = 0;
        foreach(TestVariantAction v in M1.variants_folding)
            v.number =0;
        foreach(TestVariantAction v in M2.variants_deoloyment)
            v.number =0;
        foreach(TestVariantAction v in M2.variants_folding)
            v.number = 0;
        foreach(TestVariantAction v in Outside.variants_deoloyment)
            v.number = 0;
        foreach(TestVariantAction v in Outside.variants_folding)
            v.number =0;
        Index_Selected_Element = 0;  
        Mistakes = 0;            
    }
    
    private void Reset_Color_Button_Navigation(){
        Switch_color(M1_button,main_color);
        Switch_color(M2_button,main_color);
        Switch_color(Outside_button,main_color);
    }

    private void Reset_Color_Button_Menu(){
        foreach (var b in buttons_roles)
            Switch_color(b, main_color);        
        Switch_color(buttons_deoloyment, main_color);
        Switch_color(buttons_folding, main_color);     
    }
    
    private void Active_Stars(bool is_active){
        foreach(var star in Stars)
            star.SetActive(is_active);
    }

    private void Switch_color (Button b, Color c)=> b.gameObject.GetComponent<Image>().color = c;
        // ColorBlock b_color = b.colors;
        // b_color.normalColor = c;
        // b_color.highlightedColor = c;
        // b_color.pressedColor = c;
        // b.colors = b_color;
    
    public void Exit()=>Application.Quit();
    public void Exit_Scene() => SceneManager.LoadScene("Menu_Scene");
}
