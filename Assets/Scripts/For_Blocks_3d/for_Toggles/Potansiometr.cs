using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potansiometr :  Abst_Toggles
{
    [SerializeField] private Animator animator;
    [SerializeField] private Position_krutilka position_krutilka1;
    [SerializeField] private Abst_Block Block_use;


    private void Start() => animator = this.GetComponent<Animator>();
    public void Turning()=> animator.SetTrigger("purning");

    private void OnMouseUpAsButton()
    {
        Establish_pos(position_krutilka1);
        Debug.Log("Turn potansiometr!!!");
    }

    public override void Establish_pos(Position_krutilka position_Krutilka)
    {        
        Turning();
        Del_Action(position_krutilka1, Block_use);
        Add_Status_to_blocks(position_Krutilka.Action_sw, Block_use);
    }

    
    public override void Reset_Switches(bool is_reset)=> throw new System.NotImplementedException();

}
