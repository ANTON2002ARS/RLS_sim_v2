using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abst_Block : MonoBehaviour
{   
    public abstract List<switch_position> Action_Toggles{get; set;}

    public abstract void Set_Status(switch_position switch_position);  
    
}
