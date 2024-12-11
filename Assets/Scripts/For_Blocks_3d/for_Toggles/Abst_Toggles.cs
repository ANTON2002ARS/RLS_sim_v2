using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Position_krutilka 
{
    public float angle;
    public switch_position Action_sw;
    public UnityEvent event_state;
    public bool Isxod;
}

public abstract class Abst_Toggles : MonoBehaviour
{
    public abstract void Establish_pos(Position_krutilka position_Krutilka);
    protected void Add_Status_to_blocks(switch_position switch_is, Abst_Block Block_use){
        Block_use.Set_Status(switch_is);
    }

    public static bool to_isxod_all_tumbler = true;

        

    protected void Del_Action(Position_krutilka position_krutilka, Abst_Block Block_use) => Block_use.Del_status(position_krutilka.Action_sw);        

    public abstract void Reset_Switches(bool is_reset); 
    
}
