using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class tumbler_V : Abst_Toggles
{
    [SerializeField] private Position_krutilka[] list_switch;  
    [SerializeField] private Transform lever;
    [SerializeField] private Abst_Block Block_use;
    [SerializeField] private int _number_turnig;

    private void OnMouseUpAsButton()
    {
        _number_turnig++;
        if(_number_turnig >= list_switch.Length)
            _number_turnig = 0;
        
        Establish_pos(list_switch[_number_turnig]);             
    }

    public override void Establish_pos(Position_krutilka position_Krutilka)
    {
        lever.rotation = Quaternion.Euler(position_Krutilka.angle,0f,0f);
        Del_Action(list_switch[_number_turnig == 1 ? 1: 0], Block_use);
        Add_Status_to_blocks(position_Krutilka.Action_sw, Block_use);
        Debug.Log("Click tumbler_V");
    }    

    
    public override void Reset_Switches(bool is_reset)
    {
        _number_turnig = 0;
        Establish_pos(list_switch[_number_turnig]);    
    }

    
}
