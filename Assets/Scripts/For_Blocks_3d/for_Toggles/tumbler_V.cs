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
    [SerializeField] private bool Use_X;
    [SerializeField] private bool Use_Y;
    [SerializeField] private bool Use_Z;

    private void Start(){        
        if(Block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
            return;
        }

        foreach(var sw in list_switch){
            if(sw.Action_sw ==null)
                Debug.Log("Action_sw is null for block: "+ Block_use.gameObject.name);
        }

        for(int i=0; i < list_switch.Length; i++)
        {
            if(Abst_Toggles.to_isxod_all_tumbler == true){
                if(list_switch[i].Isxod == true){
                    if(Use_X)
                    lever.localEulerAngles = new Vector3(list_switch[i].angle,0f,0f);  
                    else if(Use_Y)
                    lever.localEulerAngles = new Vector3(0f,list_switch[i].angle,  0f);
                    else
                    lever.localEulerAngles = new Vector3(0f, 0f,list_switch[i].angle);
                    _number_turnig = i;
                    return;
                }
            }
            else{
                if(list_switch[i].Isxod == false){
                    if(Use_X)
                    lever.localEulerAngles = new Vector3(list_switch[i].angle,0f,0f);  
                    else if(Use_Y)
                    lever.localEulerAngles = new Vector3(0f,list_switch[i].angle,  0f);
                    else
                    lever.localEulerAngles = new Vector3(0f, 0f,list_switch[i].angle);
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
        
        Establish_pos(list_switch[_number_turnig]);             
    }

    public override void Establish_pos(Position_krutilka position_krutilka)
    {
        //lever.rotation = Quaternion.Euler(position_krutilka.angle,0f,0f);      

        if(Use_X)
            lever.localEulerAngles = new Vector3(position_krutilka.angle,0f,0f);  
        else if(Use_Y)
            lever.localEulerAngles = new Vector3(0f,position_krutilka.angle,  0f);
        else
            lever.localEulerAngles = new Vector3(0f, 0f,position_krutilka.angle);

        foreach (var sw in list_switch){
            if(sw != position_krutilka)
                Del_Action(sw,Block_use);
        }
        position_krutilka.event_state.Invoke();
        Add_Status_to_blocks(position_krutilka.Action_sw, Block_use);
        Debug.Log("Нажата кнопка: " + position_krutilka.Action_sw);
    }    

    
    public override void Reset_Switches(bool is_reset)
    {
        _number_turnig = 0;
        Establish_pos(list_switch[_number_turnig]);    
    }

    
}
