using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P_71 : Abst_Block
{  
    
    [Header("for Target")]
    [SerializeField] private Transform folder_target_main;
    [SerializeField] private Transform folder_for_trace;
    [SerializeField] private GameObject Target_of_IKO;
    [SerializeField] private GameObject PRS_of_IKO;
    public List<GameObject> List_Target_On_IKO;
    [HideInInspector] public GameObject PRS;
    public bool ON_MODE_M;

    [Header("for Interference")]
    [SerializeField] private Transform folder_Interfence;
    [SerializeField] private List<GameObject> passive;
    [SerializeField] private List<GameObject> local;
    [SerializeField] private List<GameObject> nip;
    [SerializeField] private List<GameObject> active_noise;
    [SerializeField] private List<GameObject> respons_answer; 
    [Header("for Display")]
    [SerializeField] private SpriteRenderer Grid;
    [SerializeField] private GameObject Line;
    [SerializeField] private GameObject Display_IKO;
    [SerializeField] private float step_position =0.4f;

    [Header("for Line")]
    [SerializeField] private GameObject IKO;
    [SerializeField]  private GameObject LineObject;    
    [SerializeField] private float LineRotationSpeed_6rpm = -36f;
    [SerializeField] private float LineRotationSpeed_12rpm = -72f;

    private bool use_sector_review_IKO;
    private float _brightness;
    public float Brightness_IKO{
        private get{return _brightness;}
        set{            
            _brightness = value;
            Color color = Grid.color;
            if(_brightness > 1) color.a =1;
            else if(_brightness < 0) color.a =0;
            else color.a= value;
            Grid.color = color;
        }  
    }
    
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
                    return;
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
    
    private void Work_of_Line(){        
        if(use_round == false)
            return;
        var angles = LineObject.transform.localEulerAngles;
        if(is_backward){
            angles.z -= LineRotationSpeed * Time.deltaTime;
        }
        else{
            angles.z += LineRotationSpeed * Time.deltaTime;
        } 
        LineObject.transform.localEulerAngles = angles;
    }


    public static  P_71 Instance_IKO;
    private void Awake() => Instance_IKO = this;

    void Start(){        
        if (SceneManager.GetActiveScene().name == "RLS_Scene")
            return;
        
        round_mode = true;
        use_round = true;
    }
    void Update(){
        Work_of_Line();
        // if(Input.GetKeyDown(KeyCode.Return)){
        //     Check_Result();
        //     Debug.Log("is ENTER");
        // }
    }

    public void Span_PRS(Vector2 position){
        PRS = Instantiate(PRS_of_IKO);
        PRS.transform.SetParent(folder_target_main,false); 
        PRS.transform.localPosition = position;
    }

    public void PRS_End_Trace(){
        Scene_Game.test_instance.End_Text_war_with_PRS(ON_MODE_M);        
        Destroy(PRS);
    }

    public void Span_Target_with_PRS(){
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main,false); 
        List_Target_On_IKO.Add(target);
        //target.GetComponent<Target_Main>().Set_Side(); 
        target.GetComponent<Target_Main>().Use_Group = Random.Range(0, 2) == 0; 
        target.GetComponent<Target_Main>().Use_Our = false;     
        target.GetComponent<Target_Main>().Use_RPS = true;
    }

    public void Span_Target(){
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main,false); 
        List_Target_On_IKO.Add(target);
        if(Random.Range(0, 5) == 0){
            target.GetComponent<Target_Main>().Set_Helper_Side();            
        }
        else{
            target.GetComponent<Target_Main>().Set_Side();
        }        
    }
    
    public void Span_Target(Vector2 start_Point, Vector2 end_Point){
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main,false); 
        target.GetComponent<Target_Main>().Set_Point_Target(start_Point,end_Point);
        List_Target_On_IKO.Add(target);
        target.GetComponent<Target_Main>().Set_Side();
    }

    public void Span_Target(float radius_start, float angleInDegrees_start, float radius_end, float angleInDegrees_end){
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main,false); 
        target.GetComponent<Target_Main>().Set_Point_Target(radius_start,  angleInDegrees_start, radius_end, angleInDegrees_end);
        List_Target_On_IKO.Add(target);
        target.GetComponent<Target_Main>().Set_Side();
    }

    public void Span_Target(float angleInDegrees_start, float angleInDegrees_end){
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main,false); 
        target.GetComponent<Target_Main>().Set_Point_Target( angleInDegrees_start, angleInDegrees_end);
        List_Target_On_IKO.Add(target);
        target.GetComponent<Target_Main>().Set_Side();
    }

    public void Span_Target(bool use_our, bool use_group, bool is_helper){
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main,false); 
        List_Target_On_IKO.Add(target);
        if(is_helper){
            target.GetComponent<Target_Main>().Set_Helper_Side();  
            return;
        }
        else{
            target.GetComponent<Target_Main>().Set_Side(use_group, use_our);
            target.GetComponent<Target_Main>().IS_Request_Target = true; 
        }
    }

    public void Request_Target_last(){
        GameObject target = Get_last_Target();
        target.GetComponent<Target_Main>().IS_Request_Target = true;
    }

    public bool Last_Target_is_Group(){
        Target_Main target = Get_last_Target().GetComponent<Target_Main>();
        target.check_group = true;
        return target.Use_Group;
    }

    public bool Last_Target_is_Our(){
        Target_Main target = Get_last_Target().GetComponent<Target_Main>();
        target.check_group = true;
        return target.Use_Our;
    }
    public bool Last_Target_is_Helper(){
        Target_Main target = Get_last_Target().GetComponent<Target_Main>();
        target.check_group = true;
        target.IS_Request_Target = true;
        return target.Is_Helper;
    }

    public Vector2 Last_Target_position(){
        return Get_last_Target().GetComponent<Transform>().localPosition;
    }


    private GameObject Get_last_Target(){
        if(List_Target_On_IKO.Count == 0){
            Debug.LogError("Целей нет на ико или закончились");
            Scene_Game.test_instance.Faid_Testing();
            return null;
        }
        GameObject target;
        if(List_Target_On_IKO.Count == 1){
            target = List_Target_On_IKO[0];
        }
        else{
            target = List_Target_On_IKO[List_Target_On_IKO.Count -1];
        }
        return target;
    }




    public void Set_Trace_of_Target_on_IKO(GameObject trace_of_target, Vector2 position, bool is_request_target,bool use_group){
        GameObject trace = Instantiate(trace_of_target);
        trace.transform.SetParent(folder_for_trace,false);
        trace.transform.localPosition = position;
        if(is_request_target == true){
            trace.GetComponent<Trace_of_Target>().Show_tail();
        }
        if(use_group == true){
            trace.GetComponent<Trace_of_Target>().Use_Group();
        }
    }

    public void Set_Trace_of_PRS_on_IKO(Vector2 position, GameObject trace_of_PRS){
        GameObject trace = Instantiate(trace_of_PRS);
        trace.transform.SetParent(folder_for_trace,false);
        trace.transform.localPosition = position;
    }



    public void Remove_All_Trace()
    {
        foreach (Transform child in folder_for_trace)
            Destroy(child.gameObject);   
    }   

    public void Span_Interference(int number)
    {        
        if (folder_Interfence.childCount > 0)
        {
            GameObject block = folder_Interfence.GetChild(0).gameObject;
            Destroy(block);
        }

        GameObject interference;
        switch (number)
        {
            case 1:
                //interference = Instantiate(passive[Random.Range(0, passive.Count)]);
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

         interference.transform.SetParent(folder_Interfence, false);
         interference.transform.localScale = Vector3.one;
         interference.transform.localPosition = Vector3.zero;
        // interference.transform.eulerAngles = Vector3.zero;
        // interference.transform.rotation = Quaternion.Euler(Vector3.zero);
        // RectTransform trans = interference.GetComponent<RectTransform>();
        // trans.rotation = Quaternion.Euler(22, 0, 0);
        //interference.transform.localRotation = Quaternion.identity; 
    }
    
    public void Clikc_Button()=> round_mode =!round_mode;
    
    // Move to Sevtor 
    // min scale 1.2 max scale 2 (step=0.0033)
    // min position -240 max position 240
    public void Move_Horizontal_X(bool to_left){
        if(use_sector_review_IKO == false)
            return;
        float old_x_position = Display_IKO.transform.localPosition.x;
        float old_y_position = Display_IKO.transform.localPosition.y;

        if(to_left){
            Display_IKO.transform.localPosition = new Vector3(old_x_position - step_position, old_y_position,0);            
        }
        else{
            Display_IKO.transform.localPosition = new Vector3(old_x_position + step_position, old_y_position,0);
        }

        // Вычисляем относительное положение объекта в диапазоне от 0 до 1
        float normalizedX = Mathf.InverseLerp(0f, 2.5f, Mathf.Abs(Display_IKO.transform.localPosition.x));
        // Интерполируем масштаб в зависимости от положения объекта
        float newScale = Mathf.Lerp(1f, 2f, normalizedX);        
        // Применяем новый масштаб к объекту по оси X
        Display_IKO.transform.localScale = new Vector3(newScale, Display_IKO.transform.localScale.y, Display_IKO.transform.localScale.z);

        //проверка теста
        Scene_Game.test_instance.Proverka_Test_Sector_Review(Display_IKO.transform.localPosition);
    }
    public void Move_Vertical_Y(bool to_left){
        if(use_sector_review_IKO == false)
            return;
        float old_x_position = Display_IKO.transform.localPosition.x;
        float old_y_position = Display_IKO.transform.localPosition.y;

        if(to_left){            
            Display_IKO.transform.localPosition = new Vector3(old_x_position , old_y_position - step_position,0);
        }
        else{            
            Display_IKO.transform.localPosition = new Vector3(old_x_position , old_y_position + step_position,0);
        }

        // Вычисляем относительное положение объекта в диапазоне от 0 до 1
        float normalizedY = Mathf.InverseLerp(0f, 2.5f, Mathf.Abs(Display_IKO.transform.localPosition.y));
        // Интерполируем масштаб в зависимости от положения объекта
        float newScale = Mathf.Lerp(1f, 2f, normalizedY);        
        // Применяем новый масштаб к объекту по оси X
        Display_IKO.transform.localScale = new Vector3(Display_IKO.transform.localScale.x, newScale , Display_IKO.transform.localScale.z);

        //проверка теста
        Scene_Game.test_instance.Proverka_Test_Sector_Review(Display_IKO.transform.localPosition);
    }
    public void Use_Sector_Review(bool is_sector){
        if(is_sector == true){
            use_sector_review_IKO = true;  
        }
        else{
            use_sector_review_IKO = false;  
            Display_IKO.transform.localPosition = new Vector3(0,0,0);
            Display_IKO.transform.localScale = new Vector3(1f,1f,1);
        }
    }



    public void Brightness_Down(){        
        Brightness_IKO = Brightness_IKO - 0.2f;        
    }

     public void Brightness_UP(){        
        Brightness_IKO = Brightness_IKO + 0.2f;        
    }

    



}
