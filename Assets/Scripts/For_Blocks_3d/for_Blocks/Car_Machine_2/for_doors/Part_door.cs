using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part_door : MonoBehaviour
{
    [SerializeField] private Main_Door main_door;
    private void OnMouseUpAsButton()
    {
        main_door.Change_State();
    }

}
