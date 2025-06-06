using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pos_72 : Abst_Block
{ 
    [SerializeField] private bool MODE_M;
    [SerializeField] private List<switch_position> _need_condition;
    public override List<switch_position> Need_Condition
    {          
        get => _need_condition;        
        set => _need_condition = value;        
    }

    [SerializeField] private List<switch_position> _actionToggles;
    public override List<switch_position> Action_Toggles
    {
        get => _actionToggles;
        set => _actionToggles = value;
    }
    public override void Set_Status(switch_position switch_position)
    {
        Debug.Log("Получен статус переключение" + switch_position.name);
        Action_Toggles.Add(switch_position);  

        if(Need_Condition.Count !=0){
            Debug.Log("________");
            if(Checking_List(Action_Toggles, Need_Condition)){
                Debug.Log("__Checking_List is true__");
                Check_Result();
            }
            else{
                Debug.Log("Checking_List is false");
            }
        } 
    }
    public override void Del_status(switch_position switch_position)=> Delete_Status(this, switch_position);

    public override void Check_Result()
    { 
        if(Need_Condition.Count == Action_Toggles.Count){ 
            for(int i = 0;i < Need_Condition.Count; i++){
                if(Need_Condition[i] != Action_Toggles[i]){
                    Debug.Log("нужен был: " + Need_Condition[i] + " получен: " + Action_Toggles[i]);
                    Scene_Game.test_instance.Calling_Completion_Block(false);
                    return;
                }                
                Debug.Log("i: " + i);
            }
            Scene_Game.test_instance.Calling_Completion_Block(true);
            Debug.Log("списки совпали");
        }  
        else{
            Scene_Game.test_instance.Calling_Completion_Block(false);
            Debug.Log("кол-во не равно в списках");
        }     
    }

    
    public static pos_72 Instance_pos72;
    private void Awake() => Instance_pos72 = this;

    void Start()
    {
        MODE_M = false;
    }

    public void Set_Mode_M(){
        MODE_M = ! MODE_M;
        if(P_71.Instance_IKO != null){
            P_71.Instance_IKO.ON_MODE_M = MODE_M;
            if(Scene_Game.test_instance != null)
                Scene_Game.test_instance.End_Text_war_with_PRS(MODE_M);
        }
    }
    public bool Get_Mode_M(){
        return MODE_M;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            Check_Result();
            Debug.Log("is ENTER");
        }
    } 
    
}
