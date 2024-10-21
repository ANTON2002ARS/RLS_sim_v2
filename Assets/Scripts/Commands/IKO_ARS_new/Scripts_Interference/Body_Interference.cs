using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Interference : MonoBehaviour
{
    // проверка на избавление от помехи \\
    [SerializeField]
    public bool Check_work;
    [SerializeField]
    public bool Check_Test;
    [SerializeField]
    public string Tag_;
        

    private readonly float max_Radius = 2.5f;
    private readonly float min_Radius = 0.5f;

    public void Delete() => Destroy(this.gameObject);

    private void Random_Rotation()
    {
        float randomAngle = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);
    }


    private void Random_Movement()
    {
        Vector2 randomOffset = Random.insideUnitCircle * max_Radius;        
        transform.position = new Vector2(randomOffset.x + min_Radius, randomOffset.y + min_Radius);
    }
    

    void Start()
    {
        switch (this.tag)
        {
            case "RESPONSE":
                Random_Rotation();
                break;            
            case "NIP":
                Random_Rotation();
                break;
            case "ACTIVE_NOISE":
                Random_Rotation();
                break;
            case "FROM_LOCAL":
                Random_Rotation();
                break;
            case "PASSIVE":
                Random_Movement();
                Random_Rotation();
                break;
            default:
                Debug.Log("TAG not find");
                break;           
        }   
        //Debug.Log("position Interference: " + this.transform.position);   
    }

        
}
