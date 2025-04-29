using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Machine_2 : MonoBehaviour
{
    [SerializeField]  private GameObject LineObject; 
    [SerializeField] private float LineRotationSpeed_6rpm = -36f;
    [SerializeField] private float LineRotationSpeed_12rpm = -72f;
    public bool use_round;
    public bool round_mode; 
    public bool is_backward;
    private float LineRotationSpeed
    {
        get
        {
            switch (round_mode)
            {
                case true:                    
                    return LineRotationSpeed_12rpm;                                                     
                default:
                    return LineRotationSpeed_6rpm;
            }
        }
    }
    public static  Machine_2 Instance_Car_2;
    private void Awake() => Instance_Car_2 = this;

    void Start()
    {
        
    }

    void Update()
    {
        Work_of_Line();
    }

    private void Work_of_Line(){        
        if(use_round == false)
            return;
        var angles = LineObject.transform.localEulerAngles;
        if(is_backward){
            angles.y -= LineRotationSpeed * Time.deltaTime;
        }
        else{
            angles.y += LineRotationSpeed * Time.deltaTime;
        }        
        LineObject.transform.localEulerAngles = angles;
    }


}
