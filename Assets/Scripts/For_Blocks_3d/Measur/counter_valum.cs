using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter_valum : MonoBehaviour
{
    public int counter = 0;
    public TMPro.TextMeshPro counterText; // ��� TMPro.TMP_Text ��� TextMeshPro
    public int resetValue = 2524; // �������� ��� ������
    private bool isCounting = true;

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
            counterText.text = counter.ToString("D4"); // �������������� � 0000
        }
    }

    public void StopCounting()
    {
        isCounting = false;
    }
}
