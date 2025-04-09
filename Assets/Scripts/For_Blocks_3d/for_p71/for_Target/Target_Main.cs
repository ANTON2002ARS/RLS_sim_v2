using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Main : MonoBehaviour
{
    public bool Use_Group;
    public bool check_group;
    public bool IS_Request_Target;
    public bool Use_Our;
    public bool check_our;
    public bool Is_Helper;
    [SerializeField] private float Radius_IKO = 2.4f;
    [SerializeField] private GameObject Helper;
    [SerializeField] private GameObject Opponent;
    [SerializeField] private GameObject Our;
    public bool flag_move;

    public bool Use_RPS;

    public Vector2 startPoint; // Начальная точка
    public Vector2 endPoint; // Конечная точка

    public float speed = 0.01f; // Скорость перемещения

    private float t = 0.0f; // Параметр интерполяции

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "Line" && flag_move == true)
        {
            flag_move = false;
            if(Use_Our == true){
                P_71.Instance_IKO.Set_Trace_of_Target_on_IKO(Our, this.transform.localPosition, IS_Request_Target, Use_Group);                
            }
            else if(Is_Helper == true){
                P_71.Instance_IKO.Set_Trace_of_Target_on_IKO(Helper, this.transform.localPosition,IS_Request_Target, Use_Group);
            }
            else{
                P_71.Instance_IKO.Set_Trace_of_Target_on_IKO(Opponent, this.transform.localPosition, IS_Request_Target, Use_Group);                
            }
        }
    }

    void Start(){
        if(Use_RPS == true){
            Invoke("Start_PRS", 1f);
        }
        if(IsZeroVector(startPoint) || IsZeroVector(endPoint)){
            startPoint = Generate_Random_Polar_Coordinates(Radius_IKO);
            endPoint = Generate_Random_Polar_Coordinates(Radius_IKO);
            this.transform.localPosition = startPoint;
        }   
    }

    private void Start_PRS(){
        P_71.Instance_IKO.Span_PRS(this.transform.localPosition);
    }

    private bool IsZeroVector(Vector2 vector)
    {
        return vector.x == 0 && vector.y == 0;
    }

    public void Set_Side(bool use_group, bool use_our){
        if(Is_Helper == true){
            Debug.LogError("Цель бедствие не может быть чей то");
        }
        Use_Group = use_group;
        Use_Our = use_our;        
    }

    public void Set_Side(){
        Use_Group = Random.Range(0, 2) == 0;    
        Use_Our = Random.Range(0, 2) == 0;
        Is_Helper = false;        
    }

    public void Set_Helper_Side(){
        Use_Group = false;
        Is_Helper = true;
    }

    public void Set_Point_Target(Vector2 start_Point, Vector2 end_Point){
        startPoint = start_Point;
        endPoint = end_Point;
    }

    public void Set_Point_Target(float radius_start, float angleInDegrees_start, float radius_end, float angleInDegrees_end){
        startPoint = Polar_to_Cartesian_Degrees(radius_start,angleInDegrees_start);
        endPoint = Polar_to_Cartesian_Degrees(radius_end, angleInDegrees_end);
    }

    public void Set_Point_Target(float angleInDegrees_start, float angleInDegrees_end){
        startPoint = Polar_to_Cartesian_Degrees(Radius_IKO,angleInDegrees_start);
        endPoint = Polar_to_Cartesian_Degrees(Radius_IKO, angleInDegrees_end);
    }

    

    void Update()
    {
        t += speed * Time.deltaTime;
        
        if (t > 1)
            t = 1;

        transform.localPosition = new Vector3(
            x: Mathf.Lerp(startPoint.x, endPoint.x, t),
            y: Mathf.Lerp(startPoint.y, endPoint.y, t),
            z: transform.localPosition.z); // Оставляем z неизменным для 2D
        
        if (t == 1){
            Debug.Log("Цель достигнута конца");
            // if(IS_Request_Target == false || !check_group || !check_our ){
            //     Scene_Game.test_instance.Faid_Testing();
            // }
            Destroy(this.gameObject);
        }
            
    }

    

    public static Vector2 Generate_Random_Polar_Coordinates(float maxRadius)
    {
        // Случайный радиус между 0 и maxRadius
        float r = Random.Range(maxRadius *0.8f, maxRadius);
        
        // Случайный угол между 0 и 2π
        float t = Random.Range(0f, Mathf.PI * 2);
        
        // Преобразование полярных координат в декартовы
        return new Vector2(
            x: r * Mathf.Cos(t),
            y: r * Mathf.Sin(t)
        );
    }

    private static Vector2 Polar_to_Cartesian(float r, float t)
    {
        return new Vector2(
            x: r * Mathf.Cos(t),
            y: r * Mathf.Sin(t)
        );
    }

    // Если угол задан в градусах, его необходимо сначала преобразовать в радианы:
    private static Vector2 Polar_to_Cartesian_Degrees(float r, float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        return Polar_to_Cartesian(r, angleInRadians);
    }





}
