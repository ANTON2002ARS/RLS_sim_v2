using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VI71_block : Abst_Block
{
    public override List<switch_position> Action_Toggles { get; set; }

    public override void Set_Status(switch_position switch_position)
    {
        Debug.Log("������� ������ ������������" + switch_position.name);
        Action_Toggles.Add(switch_position);
    }
}
