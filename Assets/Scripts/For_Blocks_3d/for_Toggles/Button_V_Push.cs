using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_V_Push : Abst_Toggles
{
    [SerializeField] private Transform krutilka;
    [SerializeField] private Position_krutilka pos;    
    [SerializeField] private Abst_Block block_use;    
    private bool is_push = false;

    private void Start(){        
        if(block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
            return;
        }
        
        if(pos.Action_sw ==null)
            Debug.Log("Action_sw is null for block: "+ block_use.gameObject.name);
        
        krutilka.localPosition = Vector3.zero;

    } 

    private void OnMouseUpAsButton()
    {   
        if(is_push == true){
            is_push = false;
            krutilka.localPosition = Vector3.zero;
        }
        else{
            krutilka.localPosition = new Vector3(0.4f,0,0);
            is_push = true;
        }
        Establish_pos(pos);
    }

    public override void Establish_pos(Position_krutilka position_krutilka)
    {
        //krutilka.transform.rotation = Quaternion.Euler(0f,0f,position_krutilka.angle);        
        //krutilka.transform.localEulerAngles = new Vector3(0f,0f,position_krutilka.angle);        
        
        Del_Action(pos,block_use);        
        position_krutilka.event_state.Invoke();
        Add_Status_to_blocks(position_krutilka.Action_sw, block_use);
        Debug.Log("Нажата кнопка: " + position_krutilka.Action_sw);
    }
    

    public override void Reset_Switches(bool is_reset)
    {        
        Establish_pos(pos);   
    }
}
