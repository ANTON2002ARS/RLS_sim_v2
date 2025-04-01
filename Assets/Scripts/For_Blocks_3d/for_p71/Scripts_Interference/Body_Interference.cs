using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Interference : MonoBehaviour
{    
    [SerializeField] public bool End_Work;
    [SerializeField] public string Tag_;
        

    [SerializeField] private float max_Radius = 2.4f;
    [SerializeField] private float min_Radius = 0.5f;

    public void Delete() => Destroy(this.gameObject);

    private void Random_Rotation()
    {
        float randomAngle = Random.Range(0f, 180f);
        this.transform.localRotation = Quaternion.Euler(0f, 0f, randomAngle);
    }


    private void Random_Movement()
    {
        Vector2 randomOffset = Random.insideUnitCircle * max_Radius;        
        this.transform.localPosition = new Vector2(randomOffset.x + min_Radius, randomOffset.y + min_Radius);
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
        Debug.Log("position Interference: " +this.tag + "  pos: "+ this.transform.position + " angle: " + this.transform.rotation);   
    }

        
}
