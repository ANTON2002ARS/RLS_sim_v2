using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Body : MonoBehaviour
{
    [SerializeField] private int Index;
    [SerializeField] private Text text_button;
    [SerializeField] private Text text_number;
    private TestVariantAction VariantAction;

    public void Start()=> this.GetComponent<Button>().onClick.AddListener(button_click);    
    
    public void Get_Variant(TestVariantAction testVariantAction){
        VariantAction = testVariantAction;
        text_button.text = testVariantAction.ActionName;
        Index = testVariantAction.number;
        text_number.text = Index.ToString();
        if(testVariantAction.number == 0)
            return;
        text_number.text = testVariantAction.number.ToString();
        Set_Color(Game_Test.instance.select_color);        
    }

    public void button_click(){        
        if(Index == Game_Test.Index_Selected_Element && Game_Test.Index_Selected_Element !=0){
            Game_Test.Index_Selected_Element--;
            Index = 0;
            Set_Color(Game_Test.instance.main_color);
            Debug.Log("Index: " + Index + "  Index_Selected_Element: " + Game_Test.Index_Selected_Element);
        }
        else{
            Game_Test.Index_Selected_Element++; 
            Index = Game_Test.Index_Selected_Element;                         
            if(!Game_Test.instance.Verify_Passing_Test(Index,VariantAction)){
                Game_Test.Index_Selected_Element--;
                Index = 0;
                return;
            }

                      
            Set_Color(Game_Test.instance.select_color);
            Debug.Log("Index: " + Index + "  Index_Selected_Element: " + Game_Test.Index_Selected_Element);
        }

        VariantAction.number = Index;
        text_number.text = Index.ToString();
        Debug.Log("Choice: " + VariantAction.ActionName + "  Index: " + Index);
    }

    private void Set_Color(Color color)=>this.GetComponent<Image>().color = color;
    
}
