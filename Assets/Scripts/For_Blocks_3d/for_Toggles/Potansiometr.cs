using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potansiometr :  Abst_Toggles
{
    [SerializeField] private Animator animator;
    [SerializeField] private Position_krutilka position_krutilka1;


    private void Start() => animator = this.GetComponent<Animator>();
    public void Turning()=> animator.SetTrigger("purning");

    public override void Establish_pos(Position_krutilka position_Krutilka)
    {        
        Turning();
        Add_Status_to_blocks(position_krutilka1.Action_sw);
    }

    public override void Add_Status_to_blocks(switch_position switch_is)
    {
        throw new System.NotImplementedException();
    }

    public override void Reset_Switches(bool is_reset)
    {
        throw new System.NotImplementedException();
    }

    


}
