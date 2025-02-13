using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "War_", menuName = "Wars", order = 0)]
public class War_Interferent : Abst_Task
{
    [Multiline] public string Text_Button;

    [Header("Interferent use")]
    public bool use_passive;
    public bool use_local;
    public bool use_nip;
    public bool use_active_noise;
    public bool use_respons_answer; 
    public List<Action_Blocks> block_need;  


}
