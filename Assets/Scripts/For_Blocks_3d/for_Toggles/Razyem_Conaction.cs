using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Switchs_Obg
{
   public  switch_position pos_state;
   public GameObject obj_active;
}


public class Razyem_Conaction : MonoBehaviour
{
    [SerializeField] private List<Switchs_Obg> switchs_obgs;
<<<<<<< HEAD
    [SerializeField] private Abst_Block Block_use;
    [SerializeField] private int index;

    private void Start()
    {
=======
    [SerializeField] private Abst_Block block_use;
    [SerializeField] private int index;

    private void Start()
    {        
        if(block_use == null){
            Debug.Log("BLOCK IS NULL, name: " + this.gameObject.name);
        }           
        foreach(var sw in switchs_obgs){
            if(sw.pos_state == null)
                Debug.Log("Action_sw is null for block: "+ block_use.gameObject.name);
        } 
    
>>>>>>> Test_building
        foreach(var obg in switchs_obgs)        
            obg.obj_active.SetActive(false);        
    }

<<<<<<< HEAD
    private void OnMouseUpAsButton()
    {
        Establish_pos();
        Debug.Log("Click Razyem!!!");
    } 
    
    public void Establish_pos()
    {
        if (index >= switchs_obgs.Count)
            index = switchs_obgs.Count;
        Disble_only(switchs_obgs[index]);
        Block_use.Set_Status(switchs_obgs[index].pos_state);
    }

    private void Disble_only(Switchs_Obg not_disble)
    {
        foreach(var obg in switchs_obgs)
        {
            if (obg.pos_state != not_disble.pos_state)
                obg.obj_active.SetActive(false);
        }
        not_disble.obj_active.SetActive(true);
=======
    private void OnMouseUpAsButton()=> Establish_pos();
    
    public void Establish_pos()
    {        
        if (index >= switchs_obgs.Count)
            index = 0;        
        Disble_only(switchs_obgs[index]);
        if (Machine_1.instance_car_1 != null)
        {
            if (switchs_obgs[index].pos_state.Text_Learnihg_for_Center != "")
                Machine_1.instance_car_1.Show_Text_Center(switchs_obgs[index].pos_state.Text_Learnihg_for_Center);
            else
                Debug.Log("Текст центра на тумблере отсутствует");
            if (switchs_obgs[index].pos_state.Text_Learnihg_for_Panel != "")
                Machine_1.instance_car_1.Show_Text_Panel(switchs_obgs[index].pos_state.Text_Learnihg_for_Panel);
            else
            {
                Debug.Log("Текст панели на тумблере отсутствует");
                Machine_1.instance_car_1.Use_Panel(false);
            }

        }
        index++;        
    }

    private void Disble_only(Switchs_Obg not_disble)
    {
        foreach(var obg in switchs_obgs)
        {
            if (obg.pos_state != not_disble.pos_state){
                obg.obj_active.SetActive(false);
                block_use.Del_status(obg.pos_state);
            }                
        }
        not_disble.obj_active.SetActive(true);
        block_use.Set_Status(not_disble.pos_state);
>>>>>>> Test_building
    }

    public void Reset_Switches(bool is_reset)
    {
        index = 0;
        Establish_pos();        
    }
}
