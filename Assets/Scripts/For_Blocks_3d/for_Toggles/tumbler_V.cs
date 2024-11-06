using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class tumbler_V : Abst_Toggles
{
    [SerializeField] private Position_krutilka[] action_v;
    [SerializeField] private float first_pos_z;
    [SerializeField] private float secont_pos_z;  
    [SerializeField] private Transform lever;

    public override void Add_Status_to_blocks(switch_position switch_is)
    {
        throw new System.NotImplementedException();
    }

    public override void Establish_pos(Position_krutilka position_Krutilka)
    {
        throw new System.NotImplementedException();
    }

    public override void Reset_Switches(bool is_reset)
    {
        throw new System.NotImplementedException();
    }
}
