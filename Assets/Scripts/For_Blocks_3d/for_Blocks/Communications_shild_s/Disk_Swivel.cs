using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Disk_Swivel : MonoBehaviour
{  
    [SerializeField] private float min_Angle;
    [SerializeField] private float max_Angle;
    [SerializeField] private float Max_Valum;
    [SerializeField] private GameObject disk;
    [SerializeField] private TextMeshPro text_helper;
    void Start()=> text_helper.gameObject.SetActive(false);
    
    public void Set_Disk_Position(float input_height){        
        // Пропорциональное преобразование значения
        float angleToRotate = Mathf.Lerp(min_Angle, max_Angle, input_height / Max_Valum);        
        // Поворот объекта
        disk.transform.rotation = Quaternion.Euler(0,0, angleToRotate);
        text_helper.text = input_height.ToString();
        text_helper.gameObject.SetActive(true);
        Invoke("HideTextHelper", 4f);        
    }

    private void HideTextHelper()=> text_helper.gameObject.SetActive(false);
}
