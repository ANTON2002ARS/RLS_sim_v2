using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Body_Target : MonoBehaviour
{
    [SerializeField]
    private float radius_spawn;// max 3.5f  
    // ����� ���� �� ��� \\
    public int Namber_on_IKO;
    //\\
    public bool Start_Up_PRS;
    // ����(true) ��� �����(false) \\    
    [HideInInspector]
    public bool is_Our;
    // ��������(true) ��� ���������(false) \\
    [HideInInspector]
    public bool _is_group;
    // ������ ���� ��� ������\\   
    public int Height;
    // �������� �� ���������� ������� ���� \\
    [HideInInspector]
    public bool Check_Request;
    [HideInInspector]
    public bool Check_our;
    [HideInInspector]
    public bool Check_is_Group;
    [HideInInspector]
    public bool Check_line;
    [SerializeField]
    private GameObject Target_Our;      // ����
    [SerializeField]
    private GameObject Target_Single;   // �����

    [SerializeField]
    private GameObject Target_Group_Our;        // ���� ������
    [SerializeField]
    private GameObject Target_Group_Single;     // ����� ������
        
    // ���� �������� ���� \\
    private GameObject main_target;
    // ���� ��������\\
    public bool End_Player;
    // ���������� ����� �������� �� ������ �  ����� \\
    [SerializeField]
    private int Quantity_point = 30;
    [SerializeField]
    private int Quantity_Trace = 6;
    // ��� �������� �� ���������� ������� ������� \\
    [HideInInspector]
    public bool flag_move;
    // ������� ������ � ����� �������� ���� (R = +-2.4f) \\
    [HideInInspector]
    public Vector2 startPosition;  // max 4.5f
    [HideInInspector]
    public Vector2 endPosition;    // mix 2.4f
    // ���� ����������\\
    private readonly List<GameObject> trace_trajectories = new List<GameObject>();
    private bool Is_Help_Target;

    private void Generat_vector_circle(float radius)
    {
        // �������� ���������� ����� �� ���������� \\
        //Vector2 randomOffset = Random.insideUnitCircle * radius;
        //Vector2 start_point = new Vector2(randomOffset.x + radius, randomOffset.y + radius);
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;
        Vector2 start_point = new Vector2(x, y);        
        startPosition = start_point;
        // ��������� ��������������� �� ����� �� �� ���\\
        if (Random.Range(0, 2) == 1)
            endPosition = new Vector2(-1 * start_point.x, start_point.y);       
        else    
            endPosition = new Vector2(start_point.x, -1 * start_point.y); 
    }
      

    private void Turn_on_IKO(GameObject gameObject)
    {
        // ������� ���� �� ��� ��� ����������� ���� \\
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, this.transform.position) + 180);
        // ������ �������� �� ����� \\
    }
   
    
    private void Create_Prefab( GameObject target)
    {
        // ������� ���� �� ��� \\            
        //is_Our ? Target_Our : Target_Single
        main_target = Instantiate(target, this.transform, false);
        main_target.transform.SetParent(this.transform, false);
        // �� ���������� �� ������� ������������\\ 
        main_target.SetActive(false);        
    }   

    // ����� ���� \\
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
        // ��������� ���� \\
        flag_move = true;
        // �������� ����� �������� \\
        Generat_vector_circle(radius_spawn);       
        Height = Random.Range(4, 11) * 10;
        // ��������� ������� \\
        this.transform.position = startPosition;
        // Turn_on_IKO(this.gameObject);
        // ����� ��������� ���� \\
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
            // �������� ���� \\
            flag_move = false;

            // �������� ��� \\ 
            if (Random.Range(0, 5) == 1 && !is_PRS_one)
                Call_PRS();
                        
            // ������� �������� ����\\
            if (_Namber_Step > Quantity_point)
            {
                End_Player = true;
                trace_trajectories[0].SetActive(true);
                trace_trajectories.Clear();
                if (main_target == null)
                    return;
                // ������� ������� ����\\
                Destroy(main_target);

                // �������� �� ������� ����� ����� ���������\\
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
                // �������� �� ���������� �� ��� \\
                transform.position = Walk_line(_Namber_Step);
                // ���������� \\
                main_target.SetActive(true);

                if (!Is_Help_Target)
                {
                    Is_Help_Target = true;
                    IKO_Controll.Instance.Call_Helper("�� ��� ��������� ����, ���������� � ��������������� ��������������(������ (��������) � ����� ���������� ���� ��� �����), ���������� (��������� ��� ���������) � �������� � ����." +
                        "\n (���� ���� �� ��������� ���� �� �������, �� ��������� ������)\n" +
                        "��� ������� � ���� �������� ���������� � ���������� � �����������: \n" +
                        " �00(����� ����) - 000 (������) - 000 (���������)�", true);
                }            

                // ������� ������� ���� \\            
                Turn_on_IKO(main_target);
                main_target.transform .tag = "MAIN";   
                main_target.transform.position = Walk_line(_Namber_Step);
            }
            
            // ������� ���� �� ��� \\
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

            // ����������� ���\\
            _Namber_Step++;
        }
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {           
        if(collision.tag == "_SINGLE_" && this.tag == "_OUR_")
        {
            Debug.Log("���� ��������� ����� �����: " + Namber_on_IKO);
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "_OUR_" && this.tag == "_OUR_")
        {
            IKO_Controll iKO_Controll = new IKO_Controll();
            iKO_Controll.Generate_Target();
            Debug.Log("�������� �����");
            Destroy(gameObject);
        }
        else if(collision.tag == "_SINGLE_" && this.tag == "_SINGLE_")
        {
            IKO_Controll iKO_Controll = new IKO_Controll();
            iKO_Controll.Generate_Target();
            Debug.Log("�������� ������");
            Destroy(gameObject);
        }
    }
}
