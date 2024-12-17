using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class krutilka_switch : Abst_Toggles
{
    [SerializeField] private GameObject krutilka;
    [SerializeField] private Position_krutilka[] list_switch;    
    [SerializeField] private Abst_Block block_use;
    [SerializeField] private int _number_turnig;

    private bool forward = true;

    private void Start(){        
        if(block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
            return;
        }
        foreach(var pos in list_switch){
            if(pos.Action_sw ==null)
            Debug.Log("Action_sw is null for block: "+ block_use.gameObject.name);
        }

        for(int i=0; i < list_switch.Length; i++)
        {
            if (Abst_Toggles.to_isxod_all_tumbler == true)
            {
                if (list_switch[i].Isxod == true){
                    krutilka.transform.localEulerAngles = new Vector3(0f,0f,list_switch[i].angle);
                    _number_turnig = i;
                    return;
                }                    
            }
            else
            {
                if (list_switch[i].Isxod == false){
                    krutilka.transform.localEulerAngles = new Vector3(0f,0f,list_switch[i].angle);
                    _number_turnig = i;
                    return;
                }                    
            }
        }
    } 

    private void OnMouseUpAsButton()
    {
        _number_turnig++;
        if(_number_turnig >= list_switch.Length)
            _number_turnig = 0;
        
        // Изменение счетчика
        // if (forward)
        // {
        //     _number_turnig++;
        //     if (_number_turnig >= list_switch.Length)
        //     {
        //         _number_turnig = list_switch.Length -1;
        //         forward = false;
        //     }
        // }
        // else
        // {
        //     _number_turnig--;
        //     if (_number_turnig < 0)
        //     {
        //         _number_turnig = 0;
        //         forward = true;
        //     }
        // }

        Establish_pos(list_switch[_number_turnig]);
    }

    public override void Establish_pos(Position_krutilka position_krutilka)
    {
        //krutilka.transform.rotation = Quaternion.Euler(0f,0f,position_krutilka.angle);
        
        krutilka.transform.localEulerAngles = new Vector3(0f,0f,position_krutilka.angle);
        
        foreach(var sw in list_switch){
            if(sw != position_krutilka)
                Del_Action(sw,block_use);
        }
        position_krutilka.event_state.Invoke();
        Add_Status_to_blocks(position_krutilka.Action_sw, block_use);
        Debug.Log("Нажата кнопка: " + position_krutilka.Action_sw);
    }
    

    public override void Reset_Switches(bool is_reset)
    {
        _number_turnig = 0;
        Establish_pos(list_switch[_number_turnig]);   
    }
}
