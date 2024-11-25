using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abst_Block : MonoBehaviour
{   
    public abstract List<switch_position> Action_Toggles{get; set;}       

    public abstract void Set_Status(switch_position switch_position);  

    public abstract void Del_status(switch_position switch_position);

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

}
