using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knopka_V : Abst_Toggles
{
    [SerializeField] private Position_krutilka pos_krutilka;

    [SerializeField] private Abst_Block block_use;

    private void OnMouseUpAsButton()
    {
        Establish_pos(pos_krutilka);
     }

   
    public override void Establish_pos(Position_krutilka position_Krutilka)
    {
        Del_Action(position_Krutilka, block_use);
        Add_Status_to_blocks(position_Krutilka.Action_sw, block_use);
    }

    public override void Reset_Switches(bool is_reset)=> throw new System.NotImplementedException();
  
}
