using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round_Counter_IKO_mini : MonoBehaviour
{
    public static int _rounds;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rounds++;
        foreach (var Set_Flag in IKO_Showing_Text.Active_Targets)
        {            
            if (Set_Flag != null && Set_Flag.GetComponent<Targets_>() != null)
            {
                Set_Flag.GetComponent<Targets_>().flag_move = true;
                Debug.Log("??½??½??½??½??½??½??½??½: " + _rounds.ToString() + " Flag: " + Set_Flag.GetComponent<Targets_>().flag_move);
            }
        } 
    }
}
