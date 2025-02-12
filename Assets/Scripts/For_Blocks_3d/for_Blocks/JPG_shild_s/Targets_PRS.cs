using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets_ : MonoBehaviour
{
    [SerializeField] private GameObject Circle;
    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject Middle;
    [SerializeField] private GameObject line_up;
    [SerializeField] private GameObject linu_down;
    //         \\
    [SerializeField] private int Quantity_point = 30;
    [SerializeField] private int Quantity_Trace = 6;
    //       \\
    [HideInInspector] public bool flag_move;
    //   \\
    [SerializeField] private int _Namber_Step = 1;
    //       (R = +-2.4f) \\
    [HideInInspector] public Vector2 startPosition;  
    [HideInInspector] public Vector2 endPosition;   
    //    \\
    private GameObject main_target; 
    //  \\
    private readonly List<GameObject> trace_trajectories = new List<GameObject>();

    private void Start()
    {
        //Set_off_all_obj();
    }
    private void Set_off_all_obj()
    {
        Circle.SetActive(false);
        Square.SetActive(false);
        line_up.SetActive(false);
        linu_down.SetActive(false);
        Middle.SetActive(true);
    }

    private void Get_Own_Target()
    {
        Square.SetActive(true);
        line_up.SetActive(true);
    }

    private void Get_NOT_Own_Target() => Set_off_all_obj();

    private void Get_Gruaps_Target()
    {
        Circle.SetActive(true);
        line_up.SetActive(true);
    }

    private void Get_of_Passive_Interferense_Target()
    {
        Square.SetActive(true);
        linu_down.SetActive(true);
    }

    private void Get_of_Active_Interferense_Target()
    {
        Circle.SetActive(true);
        linu_down.SetActive(true);
    }

    public void Set_Target(int number, float radius)
    {        
        Set_off_all_obj();

        switch (number)
        {
            case 1:
                Get_Own_Target();
                break;
            case 2:
                Get_NOT_Own_Target();
                break;
            case 3:
                Get_Gruaps_Target();
                break;
            case 4:
                Get_of_Passive_Interferense_Target();
                break;
            case 5:
                Get_of_Active_Interferense_Target();
                break;
            default:
                Debug.LogError("Target is not selected");
                break;
        }
        Generat_vector_circle(radius);
        this.transform.position = startPosition;
        main_target = this.gameObject;
    }

    private void Generat_vector_circle(float radius)
    {
        //      \\
        //Vector2 randomOffset = Random.insideUnitCircle * radius;
        //Vector2 start_point = new Vector2(randomOffset.x + radius, randomOffset.y + radius);
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;
        Vector2 start_point = new Vector2(x, y);
        startPosition = start_point;
        //       \\
        if (Random.Range(0, 2) == 1)
            endPosition = new Vector2(-1 * start_point.x, start_point.y);
        else
            endPosition = new Vector2(start_point.x, -1 * start_point.y);
    }


    private void Turn_on_IKO(GameObject gameObject)
    {
        //        \\
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, this.transform.position) + 180);
        //     \\
    }

    private Vector2 Walk_line(int namber_step)
    {               
        Vector2 vector_moving = startPosition + (endPosition - startPosition) * namber_step / Quantity_point;
        return vector_moving;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Line" && flag_move == true)
        {
            //   \\
            flag_move = false;            
                        
            
            //      \\
            transform.position = Walk_line(_Namber_Step);
            //  \\
            main_target.SetActive(true);
                          

            //    \\            
            Turn_on_IKO(main_target);
            main_target.transform .tag = "MAIN";   
            main_target.transform.position = Walk_line(_Namber_Step);
            
            
            //     \\
            if(_Namber_Step < Quantity_Trace && _Namber_Step > 0)
            {
                GameObject trace_trajectorie;                                  
                trace_trajectorie = Instantiate(main_target, this.transform, false);
                                              
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

            //  \\
            _Namber_Step++;
        }
    }


}
