using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class Sigment_Display : MonoBehaviour
{

    [SerializeField] private One_Siigment first_sigment;
    [SerializeField] private One_Siigment second_sigment;
    [SerializeField] private One_Siigment third_sigment;

    public void Set_Number(float number)
    {
        var resulf = ParseFloat(number);
        //  �� �������  \\


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

            if (hasDecimal && i == 3) // �������� ������� ������� (����� 3-� �����)
                decimalPosition = i;
        }

        return (digits, hasDecimal, decimalPosition);
    }





}
