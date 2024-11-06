using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Position_krutilka : MonoBehaviour
{
    public float angle;
    public switch_position Action_sw;
}

public abstract class Abst_Toggles : MonoBehaviour
{
    public abstract void Establish_pos(Position_krutilka position_Krutilka);
    public abstract void Add_Status_to_blocks(switch_position switch_is);
    public abstract void Reset_Switches(bool is_reset); 

    
}
