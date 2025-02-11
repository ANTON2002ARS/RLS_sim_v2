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
    private void Turning() 
    {
        // if (Abst_Toggles.to_isxod_all_tumbler == true)
        //     animator.speed = -1f;
        // else
        //     animator.speed = 1f;
        animator.SetTrigger("purning"); 
    }

<<<<<<< HEAD
=======
    private void Reverse()
    {
        animator.speed = -1f;
        animator.SetTrigger("purning");        
    }

>>>>>>> Test_building
    private void OnMouseUpAsButton()
    {
        Establish_pos(position_krutilka1);
        Debug.Log("Turn potansiometr!!!");
    }

    public override void Establish_pos(Position_krutilka position_Krutilka)
    {        
        Del_Action(position_Krutilka, block_use);
        Add_Status_to_blocks(position_Krutilka.Action_sw, block_use);
        Turning();
        position_Krutilka.event_state.Invoke();
    }
    
    public override void Reset_Switches(bool is_reset)=> throw new System.NotImplementedException();

}
