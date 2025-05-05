using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter_valum : MonoBehaviour
{
    public int counter = 0;
    public TMPro.TextMeshPro counterText; 
    public int resetValue = 9999; 
    private bool isCounting = false;

    void Start()
    {
        StartCoroutine(IncrementCounterCoroutine());
    }

    IEnumerator IncrementCounterCoroutine()
    {
        while (isCounting)
        {
            yield return new WaitForSeconds(3f);
            counter++;
            if (counter > resetValue)
            {
                counter = 0;
            }
            counterText.text = counter.ToString("D4"); // 0000
        }
    }

    public void STOP_COUNTING()=> isCounting = false;
    public void START_COUNTING()=> isCounting = true;

    
}
