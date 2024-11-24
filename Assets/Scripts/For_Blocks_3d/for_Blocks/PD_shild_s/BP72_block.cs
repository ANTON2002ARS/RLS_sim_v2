using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP72_block : Abst_Block
{
    [SerializeField] private List<switch_position> _actionToggles;
    public override List<switch_position> Action_Toggles
    {
        get => _actionToggles;
        set => _actionToggles = value;
    }
    public override void Set_Status(switch_position switch_position)
    {
        Debug.Log("Получен статус переключение" + switch_position.name);
        Action_Toggles.Add(switch_position);        
    }
    public override void Del_status(switch_position switch_position)=> Delete_Status(this, switch_position);     
}
