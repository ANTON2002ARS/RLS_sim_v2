using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class krutilka_switch : Abst_Toggles
{
    [SerializeField] private GameObject krutilka;
    [SerializeField] private List<Position_krutilka> list_switch;    
    [SerializeField] private int index_ckicks;
    [SerializeField] private Abst_Block Block_use;


    public override void Establish_pos(Position_krutilka position_Krutilka)
    {

        throw new System.NotImplementedException();
    }

    public override void Add_Status_to_blocks(switch_position switch_is)
    {
        
        throw new System.NotImplementedException();
    }

    public override void Reset_Switches(bool is_reset)
    {
        throw new System.NotImplementedException();
    }

}
