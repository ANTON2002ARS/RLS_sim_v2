using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knopka_V : Abst_Toggles
{
    [SerializeField] private switch_position push_button;
    [SerializeField] private Abst_Block block_use;

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
