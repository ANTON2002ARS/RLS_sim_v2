using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vch73_Block : Abst_Block
{
    [SerializeField] private List<Position_krutilka> pos_handle;
    [SerializeField] private GameObject Handler;
    [SerializeField] private int index_pos;

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

    private void OnMouseUpAsButton()
    {
        index_pos++;
        if(index_pos >= pos_handle.Count)
            index_pos = 0;

        //Handler.transform.rotation = Quaternion.Euler(0f,0f,pos_handle[index_pos].angle);             
        Handler.transform.localEulerAngles = new Vector3(0f, 0f, pos_handle[index_pos].angle);
        Del_status(pos_handle[index_pos].Action_sw);
        Set_Status(pos_handle[index_pos].Action_sw);   
    }

}