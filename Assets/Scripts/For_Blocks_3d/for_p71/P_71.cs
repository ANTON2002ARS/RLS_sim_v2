using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P_71 : Abst_Block
{
    [Header("for Display")]
    [SerializeField] private CanvasGroup Grid;
    [SerializeField] private GameObject Line;

    [Header("for Target")]
    [SerializeField] private Transform folder_target;
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject prs_target;

    [Header("for Interference")]
    [SerializeField] private Transform folder_Interfence;
    [SerializeField] private List<GameObject> passive;
    [SerializeField] private List<GameObject> local;
    [SerializeField] private List<GameObject> nip;
    [SerializeField] private List<GameObject> active_noise;
    [SerializeField] private List<GameObject> respons_answer;

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
    [Header("for Testing")]
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
        // if(Input.GetKeyDown(KeyCode.Return)){
        //     Check_Result();
        //     Debug.Log("is ENTER");
        // }
    }

    public void Span_Interference(int number)
    {
        GameObject interference;
        switch (number)
        {
            case 1:
                interference = Instantiate(passive[Random.Range(0, passive.Count)]);
                break;
            case 2:
                interference = Instantiate(local[Random.Range(0, local.Count)]);
                break;
            case 3:
                interference = Instantiate(nip[Random.Range(0, nip.Count)]);
                break;
            case 4:
                interference = Instantiate(active_noise[Random.Range(0, active_noise.Count)]);
                break;
            case 5:
                interference = Instantiate(respons_answer[Random.Range(0, respons_answer.Count)]);
                break;
            default:
                Debug.LogError("number is out");
                return;
        } 

        interference.transform.SetParent(folder_Interfence);
    }
    
    public void Clikc_Button()=> round_mode =!round_mode;
    
    public void Click_B(){        
        Brightness_IKO += 0.1f;
        if( Brightness_IKO > 0.9f) Brightness_IKO = 0;
    }

    



}
