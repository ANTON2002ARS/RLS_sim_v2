using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Action_Blocks
{
    public Abst_Block of_Blocks;
    public List<switch_position> Command_need;      
}

[CreateAssetMenu(fileName = "Task_", menuName = "Task", order = 0)]
public class Task : ScriptableObject
{
    public string Text_Button;
    public List<Action_Blocks> block_need;

    //public List<GameObject> Command_need;  

    public bool IKO_PRS;    
    
}



