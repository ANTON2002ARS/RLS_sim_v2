using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Body_Target : MonoBehaviour
{
    [SerializeField]
    private float radius_spawn;// max 3.5f  
    // номер цели на ико \\
    public int Namber_on_IKO;
    //\\
    public bool Start_Up_PRS;
    // Свой(true) или Чужой(false) \\    
    [HideInInspector]
    public bool is_Our;
    // Груповая(true) или Одиночная(false) \\
    [HideInInspector]
    public bool _is_group;
    // Высота цели над землей\\   
    public int Height;
    // Проверка на выполнение запроса цели \\
    [HideInInspector]
    public bool Check_Request;
    [HideInInspector]
    public bool Check_our;
    [HideInInspector]
    public bool Check_is_Group;
    [HideInInspector]
    public bool Check_line;
    [SerializeField]
    private GameObject Target_Our;      // Свой
    [SerializeField]
    private GameObject Target_Single;   // Чужой

    [SerializeField]
    private GameObject Target_Group_Our;        // Свой Группа
    [SerializeField]
    private GameObject Target_Group_Single;     // Чужой Группа
        
    // Сама реальная цель \\
    private GameObject main_target;
    // Цель отыграла\\
    public bool End_Player;
    // Количество точек перехода от начала к  концу \\
    [SerializeField]
    private int Quantity_point = 30;
    [SerializeField]
    private int Quantity_Trace = 6;
    // Для проверки на выполнение полного оборота \\
    [HideInInspector]
    public bool flag_move;
    // Вектора начало и конца движение цели (R = +-2.4f) \\
    [HideInInspector]
    public Vector2 startPosition;  // max 4.5f
    [HideInInspector]
    public Vector2 endPosition;    // mix 2.4f
    // След триектории\\
    private readonly List<GameObject> trace_trajectories = new List<GameObject>();
    private bool Is_Help_Target;

    private void Generat_vector_circle(float radius)
    {
        // Выбираем сллучайною точку на окружности \\
        //Vector2 randomOffset = Random.insideUnitCircle * radius;
        //Vector2 start_point = new Vector2(randomOffset.x + radius, randomOffset.y + radius);
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;
        Vector2 start_point = new Vector2(x, y);        
        startPosition = start_point;
        // Вычисляем противоположною по какой то из оси\\
        if (Random.Range(0, 2) == 1)
            endPosition = new Vector2(-1 * start_point.x, start_point.y);       
        else    
            endPosition = new Vector2(start_point.x, -1 * start_point.y); 
    }
      

    private void Turn_on_IKO(GameObject gameObject)
    {
        // Поворот цели на ИКО для привильного вида \\
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, this.transform.position) + 180);
        // СМИРНО РАВНЕНИЕ НА ЦЕНТР \\
    }
   
    
    private void Create_Prefab( GameObject target)
    {
        // создаем цель на ико \\            
        //is_Our ? Target_Our : Target_Single
        main_target = Instantiate(target, this.transform, false);
        main_target.transform.SetParent(this.transform, false);
        // не показовать до первого столкновение\\ 
        main_target.SetActive(false);        
    }   

    // номер шага \\
    [SerializeField]
    private int _Namber_Step = 1;
    
    
    private Vector2 Walk_line(int namber_step)
    {               
        Vector2 vector_moving = startPosition + (endPosition - startPosition) * namber_step / Quantity_point;
        return vector_moving;
    }

    private bool is_PRS_one;
    private void Call_PRS()
    {
        if (Namber_on_IKO == 0)
            return;            
        is_PRS_one = true;
        Start_Up_PRS = true;
        IKO_Controll.Instance.Generate_PRS(this.transform.position, Namber_on_IKO);
    }
    
    private void Start()
    {         
        // поднимаем флаг \\
        flag_move = true;
        // Выбиваем точки движение \\
        Generat_vector_circle(radius_spawn);       
        Height = Random.Range(4, 11) * 10;
        // начальная позиция \\
        this.transform.position = startPosition;
        // Turn_on_IKO(this.gameObject);
        // Выбор случайной цели \\
        is_Our = Random.Range(0, 2) == 1;
        _is_group = Random.Range(0, 2) == 1;
        Namber_on_IKO = 0;

        if (is_Our)
        {
            this.tag = "_OUR_";            
            Create_Prefab(_is_group ? Target_Group_Our : Target_Our);
        }            
        else
        {
            this.tag = "_SINGLE_";
            Create_Prefab(_is_group ? Target_Group_Single : Target_Single);
        }         
    }
    
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Line" && flag_move == true)
        {
            // опускаем флаг \\
            flag_move = false;

            // Вызываем ПРС \\ 
            if (Random.Range(0, 5) == 1 && !is_PRS_one)
                Call_PRS();
                        
            // Плавное удаление цели\\
            if (_Namber_Step > Quantity_point)
            {
                End_Player = true;
                trace_trajectories[0].SetActive(true);
                trace_trajectories.Clear();
                if (main_target == null)
                    return;
                // Убираем главною цель\\
                Destroy(main_target);

                // Проверка на запросы целей перед удалением\\
                if (!Check_our || !Check_is_Group || !Check_line)                   
                {
                    Debug.Log("CHECK STATUS TARGET");
                    IKO_Controll iKO_Controll = new();                       
                    iKO_Controll.Mistakes += 3;                    
                }                
                return;
            }
            else
            {
                // Движемся по троектории на шаг \\
                transform.position = Walk_line(_Namber_Step);
                // показовать \\
                main_target.SetActive(true);

                if (!Is_Help_Target)
                {
                    Is_Help_Target = true;
                    IKO_Controll.Instance.Call_Helper("На ИКО появилась ЦЕЛЬ, определить её государственною принадлежность(Нажать (педалька) и потом определить Свой или Чужой), количество (Групповая или Одиночная) и доложить о цели." +
                        "\n (Если цель не запросить ТЕСТ НЕ ПРОЙДЕН, за остальное ОШИБКА)\n" +
                        "Для доклада о цели оператор определяет её координаты и докладывает: \n" +
                        " «00(номер цели) - 000 (азимут) - 000 (дальность)»", true);
                }            

                // Двигоем главною цель \\            
                Turn_on_IKO(main_target);
                main_target.transform .tag = "MAIN";   
                main_target.transform.position = Walk_line(_Namber_Step);
            }
            
            // создаем след на ико \\
            if(_Namber_Step < Quantity_Trace && _Namber_Step > 0)
            {
                GameObject trace_trajectorie;
                if (is_Our)
                {
                    this.tag = "_OUR_";                    
                    trace_trajectorie = Instantiate(_is_group ? Target_Group_Our : Target_Our, this.transform, false);
                }
                else
                {
                    this.tag = "_SINGLE_";                    
                    trace_trajectorie = Instantiate(_is_group ? Target_Group_Single : Target_Single, this.transform, false);
                }                
                Turn_on_IKO(trace_trajectorie);
                trace_trajectories.Insert(0, trace_trajectorie);
            }
                       
            if(trace_trajectories.Count > 1) 
                for (int i = 0; i < trace_trajectories.Count; i++)
                {
                    if (i == 0)
                        trace_trajectories[i].SetActive(false);
                    else
                        trace_trajectories[i].SetActive(true);
                    trace_trajectories[i].transform.position = Walk_line(_Namber_Step - i);
                    trace_trajectories[i].GetComponent<CanvasGroup>().alpha = 0.5f - (0.5f / (Quantity_Trace - Quantity_Trace / 2)) * i;                    
                }

            // Увеличиваем шаг\\
            _Namber_Step++;
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {           
        if(collision.tag == "_SINGLE_" && this.tag == "_OUR_")
        {
            Debug.Log("Враг уничтожен целью номер: " + Namber_on_IKO);
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "_OUR_" && this.tag == "_OUR_")
        {
            IKO_Controll iKO_Controll = new IKO_Controll();
            iKO_Controll.Generate_Target();
            Debug.Log("Вызывоем врага");
            Destroy(gameObject);
        }
        else if(collision.tag == "_SINGLE_" && this.tag == "_SINGLE_")
        {
            IKO_Controll iKO_Controll = new IKO_Controll();
            iKO_Controll.Generate_Target();
            Debug.Log("Вызывоем своего");
            Destroy(gameObject);
        }
    }
}
