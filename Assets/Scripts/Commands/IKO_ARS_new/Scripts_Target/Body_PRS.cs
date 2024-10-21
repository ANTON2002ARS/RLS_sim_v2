using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Body_PRS : MonoBehaviour
{
    [SerializeField]
    private GameObject PRS;
    private GameObject main_PRS;

    public int Of_Target;
    public bool Check_Status;
    public bool Mode_Frickering;
    // ���������� ����� �������� �� ������ �  ����� \\
    [SerializeField]
    private int Quantity_point = 7;
    [SerializeField]
    private int Quantity_Trace = 4;
    // ��� �������� �� ���������� ������� ������� \\
    [HideInInspector]
    public bool flag_move;
    // ������� ������ � ����� �������� ���� (R = +-2.4f) \\
    [HideInInspector]
    private Vector2 start_prs;
    public Vector2 Start_PRS 
    {
        set => start_prs = value;
        private get => start_prs;
    }        
    [HideInInspector]
    private Vector2 end_PRS = Vector2.zero;  

    // ���� ����������\\
    private readonly List<GameObject> trace_trajectories = new List<GameObject>();

    private Vector2 Walk_line(int namber_step)
    {
        Vector2 vector_moving = Start_PRS + (end_PRS - Start_PRS) * namber_step / Quantity_point;
        return vector_moving;
    }

    
    void Start()
    {
        // ��������� ���� \\
        flag_move = true;        
        main_PRS = Instantiate(PRS, this.transform, false);
        main_PRS.transform.SetParent(this.transform, false);        
        // ��������� ������� \\
        this.transform.position = Start_PRS;
    }

    // ����� ���� \\
    [SerializeField]
    private int _Namber_Step = 1;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Line" && flag_move == true)
        {
            // �������� ���� \\
            flag_move = false;

            // �������� �� ���������� �� ��� \\
            this.transform.position = Walk_line(_Namber_Step);

           //Debug.Log("start: " + start_PRS + " end: " + end_PRS);
           //Debug.Log("position: " + this.transform.position);

            // ������� ���� �� ��� \\
            if (_Namber_Step < Quantity_Trace && _Namber_Step > 0)
            {                
                GameObject trace_trajectorie = Instantiate(PRS, this.transform, false);                          
                trace_trajectories.Insert(0, trace_trajectorie);
            }

            if (trace_trajectories.Count > 1)
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


}
