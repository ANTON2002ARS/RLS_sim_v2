using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Main : MonoBehaviour
{
    [SerializeField] private GameObject Helper;
    [SerializeField] private GameObject Opponent;
    [SerializeField] private GameObject Our;
    public bool flag_move;

    public Vector2 startPoint; // Начальная точка
    public Vector2 endPoint; // Конечная точка

    public float speed = 1.0f; // Скорость перемещения

    private float t = 0.0f; // Параметр интерполяции

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Line" && flag_move == true)
        {
            flag_move = false;
            P_71.Instance_IKO.Set_Trace_on_IKO(Our, this.transform.position);
            Debug.Log("Marge line");
        }
    }

    void Start(){
        startPoint = Generate_Random_Polar_Coordinates(2.4f);
        endPoint = Generate_Random_Polar_Coordinates(2.4f);

    }

    void Update()
    {
        t += speed * Time.deltaTime;
        
        if (t > 1)
            t = 1;

        transform.position = new Vector3(
            x: Mathf.Lerp(startPoint.x, endPoint.x, t),
            y: Mathf.Lerp(startPoint.y, endPoint.y, t),
            z: transform.position.z); // Оставляем z неизменным для 2D
        
        if (t == 1){
            Debug.Log("Цель достигнута конца");
        }
            
    }

    

    public static Vector2 Generate_Random_Polar_Coordinates(float maxRadius)
    {
        // Случайный радиус между 0 и maxRadius
        float r = Random.Range(0f, maxRadius);
        
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
