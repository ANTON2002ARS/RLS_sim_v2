using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin_Threaded : MonoBehaviour
{
    [SerializeField] private switch_position switchs_obgs;
    [SerializeField] private Abst_Block block_use;
    [SerializeField] private bool is_conaction;
    [SerializeField] private GameObject conaction;
    [SerializeField] private GameObject cap;

    private void Start()
    {
        if (block_use == null)
        {
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
        }
       
        if (switchs_obgs == null)
            Debug.Log("Action_sw is null for block: " + block_use.gameObject.name);
    }

    private void OnMouseUpAsButton() => Establish_pos();

    public void Establish_pos()
    {
        conaction.SetActive(is_conaction);
        cap.SetActive(!is_conaction);       

        if (Machine_1.instance_car_1 != null)
        {
            if (switchs_obgs.Text_Learnihg_for_Center != "")
                Machine_1.instance_car_1.Show_Text_Center(switchs_obgs.Text_Learnihg_for_Center);
            else
                Debug.Log("Текст центра на тумблере отсутствует");
            if (switchs_obgs.Text_Learnihg_for_Panel != "")
                Machine_1.instance_car_1.Show_Text_Panel(switchs_obgs.Text_Learnihg_for_Panel);
            else
            {
                Debug.Log("Текст панели на тумблере отсутствует");
                Machine_1.instance_car_1.Use_Panel(false);
            }
        }        
    }

    private void Change_State()
    {        
        // not use \\\
        if(is_conaction == true)
        {
            block_use.Del_status(switchs_obgs);
            is_conaction = false;            
        }
        else
        {
            block_use.Set_Status(switchs_obgs);
            is_conaction = true;
        }
    }

    public void Reset_Switches()=>  Establish_pos();
    
}
