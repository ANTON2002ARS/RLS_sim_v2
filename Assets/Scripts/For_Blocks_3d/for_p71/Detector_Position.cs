using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detector_Position : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision" + collision.name);
        foreach(var target in P_71.List_Target_On_IKO){
            target.GetComponent<Target_Main>().flag_move = true;
        }
    }
}
