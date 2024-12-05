using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potansiometr :  Abst_Toggles
{
    [SerializeField] private Animator animator;
    [SerializeField] private Position_krutilka position_krutilka1;
    [SerializeField] private Abst_Block block_use;


    private void Start(){
        animator = this.GetComponent<Animator>();
        if(block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
        }            
        if(position_krutilka1.Action_sw == null)
            Debug.Log("Action_sw is null for block: "+ block_use.gameObject.name);
    } 
    public void Turning()=> animator.SetTrigger("purning");

    private void OnMouseUpAsButton()
    {
        Establish_pos(position_krutilka1);
    }

    public override void Establish_pos(Position_krutilka position_Krutilka)
    {
        position_Krutilka.event_state.Invoke();
        Del_Action(position_Krutilka, block_use);
        Add_Status_to_blocks(position_Krutilka.Action_sw, block_use);
        Turning();
    }
    
    public override void Reset_Switches(bool is_reset)=> throw new System.NotImplementedException();

}
