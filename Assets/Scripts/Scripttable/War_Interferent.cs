using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "War_Interference", menuName = "War_Interference", order = 0)]
public class War_Interference : Abst_Task
{
    [Multiline] public string Text_Button;
    public bool Visualize_Interfence;
    
    [Header("use target")]
    public bool use_target;
    [Header("Use_PRS")]
    public bool use_PRS;

    [Header("Interferent use")]
    public bool use_passive;
    public bool use_local;
    public bool use_nip;
    public bool use_active_noise;
    public bool use_respons_answer; 
    public List<Action_Blocks> block_need;  


}
