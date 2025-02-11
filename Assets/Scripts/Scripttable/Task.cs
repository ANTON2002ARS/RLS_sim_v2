using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Action_Blocks
{
    public GameObject of_Blocks;
    public List<switch_position> Command_need;
    [TextArea(3, 10)] public string Text_Learnihg_for_Block;
}

[CreateAssetMenu(fileName = "Task_", menuName = "Task", order = 0)]
public class Task : Abst_Task
{
<<<<<<< HEAD
    [Multiline] public string Text_Button;
=======
    [TextArea(3, 10)]public string Text_Button;
    
>>>>>>> Test_building
    public List<Action_Blocks> block_need;
    [TextArea(3, 10)] public string Text_Learnihg_All;    
}



