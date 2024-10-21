using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_71 : MonoBehaviour
{
    [Header("for Display")]
    [SerializeField] private CanvasGroup Grid;
    [SerializeField] private GameObject Line;

    [Header("for Target")]
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject prs_target;
    [Header("for Line")]
    [SerializeField] private GameObject IKO;
    [SerializeField]  private GameObject LineObject;    
    [SerializeField] private float LineRotationSpeed_6rpm = -36f;
    [SerializeField] private float LineRotationSpeed_12rpm = -72f;

    private float _brightness;
    public float Brightness_IKO{
        private get{return _brightness;}
        set{            
            _brightness = value;
            if(_brightness > 1) Grid.alpha = 1;
            else if(_brightness < 0) Grid.alpha =0;
            else Grid.alpha = value;
        }  
    }
    
    private bool _mode;
    public bool round_mode{
        get => _mode;
        set{
            _mode = value;
            var pos = LineObject.transform.localPosition;
            var angles = LineObject.transform.localEulerAngles;
            if(value == true)pos.y = 0;          
            else pos.y = -250;
            angles.z = 135;            
            LineObject.transform.localPosition = pos; 
            LineObject.transform.localEulerAngles = angles;
            Debug.Log("mode is round");          
        }        
    } 
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
    private bool turning_line;
    private void Work_of_Line(){
        var angles = LineObject.transform.localEulerAngles;
        if(round_mode == true) angles.z += LineRotationSpeed * Time.deltaTime;         
        else{
            if(turning_line == true)angles.z += LineRotationSpeed * Time.deltaTime;            
            else angles.z -= LineRotationSpeed * Time.deltaTime;            
            if(angles.z > 135) turning_line = true;            
            else if(angles.z < 45)turning_line =false;   
        }
        LineObject.transform.localEulerAngles = angles;
    }
    public static  P_71 Instance_IKO;
    private void Awake() => Instance_IKO = this;

    void Start(){
        round_mode = true;
    }
    void Update(){
        Work_of_Line();
    }
    
    public void Clikc_Button(){
        round_mode =!round_mode;
    }
    public void Click_B(){        
        Brightness_IKO += 0.1f;
        if( Brightness_IKO > 0.9f) Brightness_IKO = 0;
    }

}
