using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPG_block : Abst_Block
{
    [Header("for Target")]
    [SerializeField] private Transform folder_target_main;
    [SerializeField] private GameObject Target_of_IKO;
    public List<GameObject> List_Target_On_IKO;
    [SerializeField] private float targetDetectionRadius = 0.5f; // радиус обнаружения цели

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

    public void Span_Target()
    {
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main, false);
        List_Target_On_IKO.Add(target);
        if (Random.Range(0, 5) == 0)
        {
            target.GetComponent<Target_Main>().Set_Helper_Side();
        }
        else
        {
            target.GetComponent<Target_Main>().Set_Side();
        }
    }

    public void Span_Target(Vector2 start_Point, Vector2 end_Point)
    {
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main, false);
        target.GetComponent<Target_Main>().Set_Point_Target(start_Point, end_Point);
        List_Target_On_IKO.Add(target);
        target.GetComponent<Target_Main>().Set_Side();
    }

    public void Span_Target(float radius_start, float angleInDegrees_start, float radius_end, float angleInDegrees_end)
    {
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main, false);
        target.GetComponent<Target_Main>().Set_Point_Target(radius_start, angleInDegrees_start, radius_end, angleInDegrees_end);
        List_Target_On_IKO.Add(target);
        target.GetComponent<Target_Main>().Set_Side();
    }

    public void Span_Target(float angleInDegrees_start, float angleInDegrees_end)
    {
        GameObject target = Instantiate(Target_of_IKO);
        target.transform.SetParent(folder_target_main, false);
        target.GetComponent<Target_Main>().Set_Point_Target(angleInDegrees_start, angleInDegrees_end);
        List_Target_On_IKO.Add(target);
        target.GetComponent<Target_Main>().Set_Side();
    }

    private GameObject Get_last_Target()
    {
        if (List_Target_On_IKO.Count == 0)
        {
            Debug.LogError("Целей нет на ико или закончились");
            Scene_Game.test_instance.Faid_Testing();
            return null;
        }
        GameObject target;
        if (List_Target_On_IKO.Count == 1)
        {
            target = List_Target_On_IKO[0];
        }
        else
        {
            target = List_Target_On_IKO[List_Target_On_IKO.Count - 1];
        }
        return target;
    }

    public bool IsTargetAtPosition(Vector2 position)
    {
        foreach (GameObject target in List_Target_On_IKO)
        {
            if (target != null)
            {
                Vector2 targetPosition = target.transform.localPosition;
                float distance = Vector2.Distance(targetPosition, position);
                
                if (distance <= targetDetectionRadius)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public GameObject GetTargetAtPosition(Vector2 position)
    {
        GameObject closestTarget = null;
        float closestDistance = targetDetectionRadius;

        foreach (GameObject target in List_Target_On_IKO)
        {
            if (target != null)
            {
                Vector2 targetPosition = target.transform.localPosition;
                float distance = Vector2.Distance(targetPosition, position);
                
                if (distance <= closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }
        }
        
        return closestTarget;
    }

    public bool RemoveTargetAtPosition(Vector2 position)
    {
        GameObject targetToRemove = GetTargetAtPosition(position);
        
        if (targetToRemove != null)
        {
            List_Target_On_IKO.Remove(targetToRemove);
            Destroy(targetToRemove);
            return true;
        }
        
        return false;
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

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            Check_Result();
            Debug.Log("is ENTER");
        }
    } 
            
}
