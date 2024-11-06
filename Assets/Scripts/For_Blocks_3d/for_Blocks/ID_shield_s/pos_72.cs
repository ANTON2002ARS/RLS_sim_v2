using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pos_72 : Abst_Block
{
    public override List<switch_position> Action_Toggles { get; set; }    

    public override void Set_Status(switch_position switch_position)
    {
        Debug.Log("Получен статус переключение" + switch_position.name);
        Action_Toggles.Add(switch_position);        
    }
}
