using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Action_Blocks
{
    public Abst_Toggles of_Blocks;
    public List<switch_position> Command_need;      
}

[CreateAssetMenu(fileName = "Task_", menuName = "Task", order = 0)]
public class Task : ScriptableObject
{
    [Multiline] public string Text_Button;
    public List<Action_Blocks> block_need;

    //public List<GameObject> Command_need;  

    public bool IKO_PRS;   
    

    
}



