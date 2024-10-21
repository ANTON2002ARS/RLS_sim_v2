using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckResult : MonoBehaviour
{
    public GameObject PassMessage;
    public GameObject FailMessage;
    public Text Report_text;
    public UnityEvent OnCloseToMenu;
    public UnityEvent OnCloseRestart;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
        PassMessage.SetActive(false);
        FailMessage.SetActive(false);
    }

    public void ShowPassMessage()
    {
        gameObject.SetActive(true);
        PassMessage.SetActive(true);

        if (IKO_Controll.Instance == null)
            return;
        Report_text.text = IKO_Controll.Instance.Str_Mistakes + " Ошибок: " + IKO_Controll.Instance.Mistakes;
    }

    public void ShowFailMessage()
    {
        gameObject.SetActive(true);
        FailMessage.SetActive(true);

        if (IKO_Controll.Instance == null)
            return;
        Report_text.text = IKO_Controll.Instance.Str_Mistakes + " Ошибок: " + IKO_Controll.Instance.Mistakes;

    }

    public void Close()
    {
        gameObject.SetActive(false);
        PassMessage.SetActive(false);
        FailMessage.SetActive(false);
        OnCloseToMenu?.Invoke();
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        PassMessage.SetActive(false);
        FailMessage.SetActive(false);
        OnCloseRestart?.Invoke();
    }
}
