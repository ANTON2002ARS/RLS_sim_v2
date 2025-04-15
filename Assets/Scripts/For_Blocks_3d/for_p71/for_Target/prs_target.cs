using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prs_target : MonoBehaviour
{
    public bool flag_move;
    [SerializeField] private GameObject trace;
    public float speed = 0.02f; // Скорость движения объекта
    private Vector3 targetPosition = Vector3.zero; // Цель - центр координат

    void Update()
    {
        // Перемещение объекта к центру координат
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, targetPosition, speed * Time.deltaTime);
        if(this.transform.localPosition.x == 0 && this.transform.localPosition.y == 0){
            P_71.Instance_IKO.PRS_End_Trace();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Line" && flag_move == true)
        {
            flag_move = false;
            P_71.Instance_IKO.Set_Trace_of_PRS_on_IKO( this.transform.localPosition, trace);              
        }
    }

    
}
