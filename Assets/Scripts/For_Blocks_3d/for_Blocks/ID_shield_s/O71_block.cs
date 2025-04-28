using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O71_block : Abst_Block
{
    [SerializeField] private SpriteRenderer folder;
    [SerializeField] private List<Sprite> signals;
       
    [Header("For Test")]
    [SerializeField] private List<switch_position> _need_condition;
    public override List<switch_position> Need_Condition
    {          
        get => _need_condition;        
        set => _need_condition = value;        
    }

    [SerializeField] private List<switch_position> _actionToggles;
    public override List<switch_position> Action_Toggles
    {
        get => _actionToggles;
        set => _actionToggles = value;
    }
    public override void Set_Status(switch_position switch_position)
    {
        Debug.Log("Получен статус переключение" + switch_position.name);
        Action_Toggles.Add(switch_position);  

        if(Need_Condition.Count !=0){
            Debug.Log("________");
            if(Checking_List(Action_Toggles, Need_Condition)){
                Debug.Log("__Checking_List is true__");
                Check_Result();
            }
            else{
                Debug.Log("Checking_List is false");
            }
        } 
    }
    public override void Del_status(switch_position switch_position)=> Delete_Status(this, switch_position);
    public override void Check_Result()
    { 
        if(Need_Condition.Count == Action_Toggles.Count){ 
            for(int i = 0;i < Need_Condition.Count; i++){
                if(Need_Condition[i] != Action_Toggles[i]){
                    Debug.Log("нужен был: " + Need_Condition[i] + " получен: " + Action_Toggles[i]);
                    Scene_Game.test_instance.Calling_Completion_Block(false);
                    return;
                }
                Debug.Log("i: " + i);
            }
            Scene_Game.test_instance.Calling_Completion_Block(true);
            Debug.Log("списки совпали");
        }  
        else{
            Scene_Game.test_instance.Calling_Completion_Block(false);
            Debug.Log("кол-во не равно в списках");
        }     
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            Check_Result();
            Debug.Log("is ENTER");
        }
    }
    
    // Все сигналы из учебника \\
    public void SH79_Submodule()=> Show_Signal(36);
    public void SH79_module()=>Show_Signal(0);
    public void O71_G7()=>Show_Signal(1);
    public void O71_G8()=>Show_Signal(1);
    public void O71_G6()=>Show_Signal(3);
    public void O71_G5()=>Show_Signal(4);
    public void P71_G1()=>Show_Signal(5);
    public void P71_G2()=>Show_Signal(6);
    public void P71_G3()=>Show_Signal(7);
    public void P71_G4()=>Show_Signal(8);
    public void P71_G5()=>Show_Signal(9);
    public void P71_G6_G7_G18_G20()=>Show_Signal(10);
    public void P71_G17()=>Show_Signal(11);
    public void B70_BB_I_70_G1()=>Show_Signal(12);
    public void B70_BB_I_70_G2()=>Show_Signal(13);
    public void B70_BB_I_70_G3()=>Show_Signal(14);
    public void B70_BB_I_70_G4()=>Show_Signal(15);
    public void B70_BB_I_70_G5()=>Show_Signal(16);
    public void B70_BB_I_70_G6()=>Show_Signal(17);
    public void B70_BB_I_70_G7()=>Show_Signal(18);
    public void B70_BB_I_70_G8()=>Show_Signal(19);
    public void B70_BB_II_70_G1()=>Show_Signal(20);
    public void B70_BB_II_70_G2()=>Show_Signal(21);
    public void B70_BB_II_70_G3()=>Show_Signal(22);
    public void B70_BB_II_70_G4()=>Show_Signal(23);
    public void B70_BB_II_70_G5()=>Show_Signal(24);
    public void B70_BB_II_70_G6()=>Show_Signal(25);
    public void B70_BM70_G1()=>Show_Signal(27);
    public void B70_BM70_G2()=>Show_Signal(26);
    public void B70_BM70_G3()=>Show_Signal(26);
    public void B70_BM70_G4()=>Show_Signal(26);
    public void B70_BM70_G5()=>Show_Signal(27);
    public void B70_BM70_G6()=>Show_Signal(26);
    public void B70_BM70_G7()=>Show_Signal(28);
    public void B70_BZU_I_70_Gn_1()=>Show_Signal(29);
    public void B70_BZU_I_70_Gn_2()=>Show_Signal(30);
    public void B70_BZU_II_70_Gn_2()=>Show_Signal(31);
    public void K71_KP_70_G1()=>Show_Signal(32);
    public void K71_KP_70_G2()=>Show_Signal(33);
    public void K71_KP_70_G3()=>Show_Signal(34);
    public void K71_KG_70_G1()=>Show_Signal(35);


    public void Show_Signal(int number){
        if(number >= signals.Count || number < 0 )
            return;
        Debug.Log("signal show: " + number);
        folder.sprite = signals[number];
    } 
    

            
}
