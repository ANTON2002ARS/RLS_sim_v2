using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Position_krutilka 
{
    public float angle;
    public switch_position Action_sw;
    public UnityEvent event_state;
    public bool Isxod;
    //[TextArea(3, 10)] public string Text_Learnihg_for_Center;
    //[TextArea(3, 10)] public string Text_Learnihg_for_Panel;
}

public abstract class Abst_Toggles : MonoBehaviour
{
    public abstract void Establish_pos(Position_krutilka position_Krutilka);
    protected void Add_Status_to_blocks(switch_position switch_is, Abst_Block Block_use){        
        Block_use.Set_Status(switch_is);        
        //Debug.Log("Текст состояний: " + switch_is.Text_Learnihg_for_Center);
        // Текст для обзора машин\\
        if(Machine_1.instance_car_1 != null){
            if(switch_is.Text_Learnihg_for_Center != "")
                Machine_1.instance_car_1.Show_Text_Center(switch_is.Text_Learnihg_for_Center);
            else
                Debug.Log("Текст центра на тумблере отсутствует");
            if(switch_is.Text_Learnihg_for_Panel != "")
                Machine_1.instance_car_1.Show_Text_Panel(switch_is.Text_Learnihg_for_Panel);
            else{
                Debug.Log("Текст панели на тумблере отсутствует");
                Machine_1.instance_car_1.Use_Panel(false);
            }
                
        }
    }

    public static bool to_isxod_all_tumbler;        

    protected void Del_Action(Position_krutilka position_krutilka, Abst_Block Block_use) => Block_use.Del_status(position_krutilka.Action_sw);        

    public abstract void Reset_Switches(bool is_reset); 
    
}
