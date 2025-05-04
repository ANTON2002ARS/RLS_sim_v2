using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class Sigment_Display : MonoBehaviour
{

    [SerializeField] private One_Siigment first_sigment;
    [SerializeField] private One_Siigment second_sigment;
    [SerializeField] private One_Siigment third_sigment;

    private void Start()
    {
        int point = Random.Range(1, 4);
        first_sigment.Show_Number(Random.Range(0, 10),point == 1 ? true : false);
        second_sigment.Show_Number(Random.Range(0,10), point == 2 ? true : false);
        third_sigment.Show_Number(Random.Range(0, 10), point == 3 ? true : false);
    }

    public void Set_Number(float number)
    {
        var resulf = ParseFloat(number);
        //  не доделал  \\


    }


    private (int[] digits, bool hasDecimal, int decimalPosition) ParseFloat(float number)
    {
        string numStr = number.ToString("F3", CultureInfo.InvariantCulture);
        bool hasDecimal = numStr.Contains('.');
        string cleanStr = numStr.Replace(".", "");

        int[] digits = new int[cleanStr.Length];
        int decimalPosition = -1;

        for (int i = 0; i < cleanStr.Length; i++)
        {
            digits[i] = int.Parse(cleanStr[i].ToString());

            if (hasDecimal && i == 3) // Проверка позиции запятой (после 3-й цифры)
                decimalPosition = i;
        }

        return (digits, hasDecimal, decimalPosition);
    }





}
