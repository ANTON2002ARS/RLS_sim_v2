using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show_Objects_on_IKO : MonoBehaviour
{
    [Header("for Interference")]
    [SerializeField] private Transform folder_Interfence;
    [SerializeField] private List<GameObject> passive;
    [SerializeField] private List<GameObject> local;
    [SerializeField] private List<GameObject> nip;
    [SerializeField] private List<GameObject> active_noise;
    [SerializeField] private List<GameObject> respons_answer;

    public void Span_Interference(int number)
    {
        GameObject interference;
        switch (number)
        {
            case 1:
                //interference = Instantiate(passive[Random.Range(0, passive.Count)]);
                interference = Instantiate(passive[Random.Range(0, passive.Count)], folder_Interfence);
                break;
            case 2:
                interference = Instantiate(local[Random.Range(0, local.Count)], folder_Interfence);
                break;
            case 3:
                interference = Instantiate(nip[Random.Range(0, nip.Count)], folder_Interfence);
                break;
            case 4:
                interference = Instantiate(active_noise[Random.Range(0, active_noise.Count)], folder_Interfence);
                break;
            case 5:
                interference = Instantiate(respons_answer[Random.Range(0, respons_answer.Count)], folder_Interfence);
                break;
            default:
                Debug.LogError("number is out");
                return;
        }

        interference.transform.SetParent(folder_Interfence, true);
        interference.transform.localScale = Vector3.one;
        interference.transform.eulerAngles = Vector3.zero;
        //interference.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
