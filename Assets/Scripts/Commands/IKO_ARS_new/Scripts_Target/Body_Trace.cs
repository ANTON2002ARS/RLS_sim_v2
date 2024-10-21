using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Trace : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {        
        if (!collision.CompareTag("Line"))
            return;

        if (GetComponentInParent<Body_Target>().End_Player)
        {            
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha -= 0.2f;
            if (canvasGroup.alpha <= 0)
                Destroy(gameObject);           
        }
    }
}
