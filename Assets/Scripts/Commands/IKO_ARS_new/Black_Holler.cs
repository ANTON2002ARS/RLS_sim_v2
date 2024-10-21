using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Holler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Обьекты рядом с РЛС что делать (думать)\\
        switch (collision.tag)
        {
            case "_OUR_":
                Destroy(collision.gameObject);
                break;
            case "_SINGLE_":
                Destroy(collision.gameObject);
                break;
            case "RESPONSE":
                Destroy(collision.gameObject);
                break;
            case "NIP":
                Destroy(collision.gameObject);
                break;
            case "ACTIVE_NOISE":
                Destroy(collision.gameObject);
                break;
            case "FROM_LOCAL":
                Destroy(collision.gameObject);
                break;
            case "PASSIVE":
                Destroy(collision.gameObject);
                break;
            case "_DOMAIN_":
                Destroy(collision.gameObject);
                break;
            case "PRS":
                IKO_Controll.Instance.Check_Flickering(collision.gameObject);
                break;
        }

    }
}
