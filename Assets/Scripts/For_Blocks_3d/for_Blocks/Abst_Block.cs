using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abst_Block : MonoBehaviour
{ 
    public abstract List<switch_position> Action_Toggles{get; set;}   
    public abstract List<switch_position> Need_Condition{get;set;}    

    public abstract void Set_Status(switch_position switch_position);  

    public abstract void Del_status(switch_position switch_position);  

    public abstract void Check_Result();  

    protected void Delete_Status(Abst_Block block_use, switch_position switch_position){
        if(block_use.Action_Toggles.Count == 0){
            //Debug.Log("Action_Toggles is null");
            return;
        }
        else if(block_use.Action_Toggles.Count == 1){            
            if(block_use.Action_Toggles[0] == switch_position){
                block_use.Action_Toggles.RemoveAt(0);    
                Debug.Log("Delete element " + switch_position.name);
            }             
        }
        else{
            if(block_use.Action_Toggles[Action_Toggles.Count -1] == switch_position){
                block_use.Action_Toggles.RemoveAt(block_use.Action_Toggles.Count -1); 
                Debug.Log("Delete element " + switch_position.name);
            } 
        }   
    }

    // вызывается в процессе переключения при каждом добавлении элемента для завершение теста
    protected bool Checking_List(List<switch_position> action, List<switch_position> need){
        if(action.Count == 1){            
            if(need.Count == 1){
                if(need[0] == action[0]){
                    Debug.Log("count of Need_Condition is 1");
                    return true;
                }
            }            
        }
        else if(action.Count == need.Count){
            if(action[action.Count -1 ] != need[need.Count - 1]){
                Debug.Log("списки равны, последний элементы не равны");
                return false;                
            }
            else{
                Debug.Log("списки равны, последний элементы равны");
                return true;  
            }
        }
        else if(action.Count > need.Count){
            Debug.Log("переполнение списка");
            return true;
        }
        else{            
            if(action[action.Count - 1] != need[need.Count - 1]){
                Debug.Log("Преведущие НЕ равны элементы");
                return false;
            }
            else{
                Debug.Log("Преведужие равны элементы");                             
            }
        }      
        return false;
    }

    
}
