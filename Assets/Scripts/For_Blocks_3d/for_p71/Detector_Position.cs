using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detector_Position : MonoBehaviour
{    
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision" + collision.name);
        foreach(var target in P_71.Instance_IKO.List_Target_On_IKO){
            if(target !=null)
                target.GetComponent<Target_Main>().flag_move = true;
        }
    }
}
