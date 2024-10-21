using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Domain : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private CanvasGroup Canvas;
    // Скрываем для обноружении\\
    void Start() => image.SetActive(false);    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Line")
            return;  
        // Показываем часть помехи \\
        image.SetActive(true);
        // Плавное удаление помехи \\
        if(GetComponentInParent<Body_Interference>() != null && GetComponentInParent<Body_Interference>().Check_work == true)
        {
            Canvas.alpha -= 0.2f;
            // Удаляем всю помеху при видимоть \\
            if(Canvas.alpha < 0.01f)
                GetComponentInParent<Body_Interference>().Delete();     
        }
        // Для стробирование не применяется\\
        if(this.tag == "PASSIVE" && Body_Passive._is_strobing == true)
            Canvas.alpha -= 0.4f; 
    }   
}
