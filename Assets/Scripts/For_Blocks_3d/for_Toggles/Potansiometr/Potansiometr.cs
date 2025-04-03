using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Potansiometr :  Abst_Toggles
{
    [SerializeField] private Animator animator;
    [SerializeField] private Position_krutilka position_krutilka1;
    [SerializeField] private Abst_Block block_use;
    public UnityEvent Mouse_lift_Input;
    public UnityEvent Mouse_Rigth_Input; 

    private void Start(){
        animator = this.GetComponent<Animator>();
        if(block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
        }            
        if(position_krutilka1.Action_sw == null)
            Debug.Log("Action_sw is null for block: "+ block_use.gameObject.name);
    }
    private void Turning_Right()=> animator.SetTrigger("purning"); 

        // if (Abst_Toggles.to_isxod_all_tumbler == true)
        //     animator.speed = -1f;
        // else
        //     animator.speed = 1f;
      
    
    private void Turning_lift()=> animator.SetTrigger("lift_turnig");
    

    private void Reverse()
    {
        animator.speed = -1f;
        animator.SetTrigger("purning");        
    }

    

    // private void OnMouseUpAsButton()
    // {
    //     Debug.Log("Q is: " + is_push_Q);    
    //     if (is_push_Q == true)
    //     {
    //         Mouse_lift_Input.Invoke();
    //         Debug.Log("Левая вращение");   
    //         Turning_lift();         
    //     }
    //     else 
    //     {            
    //         Turning_Right();
    //         Mouse_Rigth_Input.Invoke();
    //         Debug.Log("Правое вращение");            
    //     }
    //     Establish_pos(position_krutilka1);
    // }


    public void Ckick_to_Lift(){
        Mouse_lift_Input.Invoke();   
        Turning_lift(); 
        Establish_pos(position_krutilka1);
    }

    public void Ckick_to_Right(){
        Turning_Right();
        Mouse_Rigth_Input.Invoke();   
        Establish_pos(position_krutilka1);
    }

  

    public override void Establish_pos(Position_krutilka position_Krutilka)
    { 
        Del_Action(position_Krutilka, block_use);
        Add_Status_to_blocks(position_Krutilka.Action_sw, block_use);        
        position_Krutilka.event_state.Invoke();
    }
    
    public override void Reset_Switches(bool is_reset)=> throw new System.NotImplementedException();

}
