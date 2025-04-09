using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "War_target_", menuName = "War_target_", order = 0)]
public class War_targets : Abst_Task
{    
    [Multiline] public string Text_Button;
      //[Header("Use_PRS")]
     // public bool use_PRS;
    
    [Header("use target")]
    public bool use_sector;
    public bool use_reguest_target;    
    public bool reguest_height;

    public bool use_war_with_PRS;
    [TextArea(3, 10)] public string Text_Learning;
}
