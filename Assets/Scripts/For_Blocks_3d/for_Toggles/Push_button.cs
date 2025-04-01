using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_button : Abst_Toggles
{
    [SerializeField] private Animator animator;
    [SerializeField] private Position_krutilka pos_krutilka;   
    [SerializeField] private Abst_Block block_use;

    private void Start(){        
        animator = this.GetComponent<Animator>();
        if(block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
            return;
        }
        if(pos_krutilka.Action_sw ==null)
            Debug.Log("Action_sw is null for block: "+ block_use.gameObject.name);

    } 

    private void OnMouseUpAsButton()
    {
        Establish_pos(pos_krutilka);
    }
   
    public override void Establish_pos(Position_krutilka position_Krutilka)
    {        
        animator.SetTrigger("push");
        position_Krutilka.event_state.Invoke();
        Del_Action(position_Krutilka, block_use);
        Add_Status_to_blocks(position_Krutilka.Action_sw, block_use);
    }   

    public override void Reset_Switches(bool is_reset)=> throw new System.NotImplementedException();
  
}
