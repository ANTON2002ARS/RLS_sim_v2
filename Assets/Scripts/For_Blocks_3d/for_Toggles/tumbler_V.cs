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

    private void Start(){        
        if(Block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
            return;
        }

        foreach(var sw in list_switch){
            if(sw.Action_sw ==null)
                Debug.Log("Action_sw is null for block: "+ Block_use.gameObject.name);
        }

        foreach(var pos in list_switch)
        {
            if(Abst_Toggles.to_isxod_all_tumbler == true)
            {
                if(pos.Isxod == true)
                    lever.localEulerAngles = new Vector3(pos.angle, 0f, 0f);
            }
            else
            {
                if(pos.Isxod == false)
                    lever.localEulerAngles = new Vector3(pos.angle, 0f, 0f);
            }
        }
    } 

    private void OnMouseUpAsButton()
    {
        _number_turnig++;
        if(_number_turnig >= list_switch.Length)
            _number_turnig = 0;
        
        Establish_pos(list_switch[_number_turnig]);             
    }

    public override void Establish_pos(Position_krutilka position_krutilka)
    {
        //lever.rotation = Quaternion.Euler(position_krutilka.angle,0f,0f);      

        lever.localEulerAngles = new Vector3(position_krutilka.angle,0f,0f);  

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
