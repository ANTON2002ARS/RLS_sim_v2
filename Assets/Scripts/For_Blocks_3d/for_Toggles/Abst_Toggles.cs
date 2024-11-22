using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position_krutilka 
{
    public float angle;
    public switch_position Action_sw;
}

public abstract class Abst_Toggles : MonoBehaviour
{
    public abstract void Establish_pos(Position_krutilka position_Krutilka);
    protected void Add_Status_to_blocks(switch_position switch_is, Abst_Block Block_use){
        Block_use.Set_Status(switch_is);
    }

    protected void Del_Action(Position_krutilka position_krutilka, Abst_Block Block_use)
    {        
        if(Block_use.Action_Toggles.Count ==0){
            Debug.Log("List of block do not obg");
        }
        else if(Block_use.Action_Toggles.Count ==1){
            if(Block_use.Action_Toggles[0] == position_krutilka.Action_sw){
                Block_use.Action_Toggles.RemoveAt(0);    
                Debug.Log("Delete element " + position_krutilka.Action_sw.name);
            }                    
        }
        else{
            if(Block_use.Action_Toggles[Block_use.Action_Toggles.Count -1] == position_krutilka.Action_sw){
                Block_use.Action_Toggles.RemoveAt(Block_use.Action_Toggles.Count -1); 
                Debug.Log("Delete element " + position_krutilka.Action_sw.name);
            }                       
        }        
    }

    public abstract void Reset_Switches(bool is_reset); 

    
}
