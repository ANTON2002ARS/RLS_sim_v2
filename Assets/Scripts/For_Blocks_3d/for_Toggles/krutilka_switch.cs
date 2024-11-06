using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class krutilka_switch : Abst_Toggles
{
    [SerializeField] private GameObject krutilka;
    [SerializeField] private Position_krutilka[] list_switch;    
    [SerializeField] private Abst_Block Block_use;
    [SerializeField] private int _number_turnig;


    private void OnTriggerEnter(Collider other)
    {
        _number_turnig++;
        if(_number_turnig >= list_switch.Length)
            _number_turnig = 0;
        
        Establish_pos(list_switch[_number_turnig]);
    }

    public override void Establish_pos(Position_krutilka position_krutilka)
    {
        krutilka.transform.rotation = Quaternion.Euler(0f,0f,position_krutilka.angle);
        foreach(var sw in list_switch){
            if(sw != position_krutilka)
                Del_Action(sw,Block_use);
        }
        Add_Status_to_blocks(position_krutilka.Action_sw, Block_use);
        Debug.Log("Нажата кнопка: " + position_krutilka.Action_sw);
    }
    

    public override void Reset_Switches(bool is_reset)
    {
        _number_turnig = 0;
        Establish_pos(list_switch[_number_turnig]);   
    }

}
