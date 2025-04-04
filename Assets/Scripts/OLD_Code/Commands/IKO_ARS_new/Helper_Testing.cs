using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper_Testing : MonoBehaviour
{
    [SerializeField]
    private Text text_set;
    private bool Can_Continue;
    
    public void Call_Helper(string  explanation, bool   _can_continue)
    {
        Can_Continue = _can_continue;
        text_set.text = explanation;
        IKO_Controll.Instance.Stop_Test(true);
        this.gameObject.SetActive(true);
    }

    public void Continue_Test()
    {
        // продолжаем убираем \\        
        IKO_Controll.Instance.Stop_Test(!Can_Continue);
        this.gameObject.SetActive(false);
    }



}
