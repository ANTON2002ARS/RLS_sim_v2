using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SW_", menuName = "SW_blocks", order = 0)]
public class switch_position : ScriptableObject
{
   //[TextArea]public string Text_Button; 
   public UnityEngine.Events.UnityEvent WhenFinished;
   [TextArea(3, 10)] public string Text_Learnihg_for_Center;
   [TextArea(3, 10)] public string Text_Learnihg_for_Panel;
}
