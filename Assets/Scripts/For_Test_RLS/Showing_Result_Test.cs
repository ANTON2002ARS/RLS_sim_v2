using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showing_Result_Test : MonoBehaviour
{
    [SerializeField] private GameObject Msg_Pass;
    [SerializeField] private GameObject Msg_Fail;
    [SerializeField] private GameObject[] Stars;

    public void Show_Pass_Test(int mistakes){
        Show_Star(mistakes);
        Msg_Pass.SetActive(true);
        Msg_Fail.SetActive(false);
    }

    public void Show_Fail_Test(int mistakes){
        Show_Star(mistakes);
        Msg_Pass.SetActive(false);
        Msg_Fail.SetActive(true);
    }

    private void Show_Star(int mistakes){
        foreach(GameObject s in Stars)
            s.SetActive(true);
        
        if(mistakes > Stars.Length)
            return;
        if(mistakes == 0)
            return;
        Debug.Log("off");
        for (int i = 0; i < mistakes; i++)
            Stars[i].SetActive(false);   
                 
    }
}
