using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detector_Position : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision" + collision.name);
    }
}
