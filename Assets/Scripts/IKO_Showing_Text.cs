using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IKO_Showing_Text : MonoBehaviour
{
    [TextArea(3, 10)] public string text_passive;
    [TextArea(3, 10)] public string text_local;
    [TextArea(3, 10)] public string text_nip;
    [TextArea(3, 10)] public string text_active_noise;
    [TextArea(3, 10)] public string text_respons_answer;
    [SerializeField] private List<GameObject> passive;
    [SerializeField] private List<GameObject> local;
    [SerializeField] private List<GameObject> nip;
    [SerializeField] private List<GameObject> active_noise;
    [SerializeField] private List<GameObject> respons_answer;

    [TextArea(3, 10)] public string text_target;
    [SerializeField] private GameObject target;

    [Header("UI")]
    [SerializeField] private Transform folder_Interfence;
    [SerializeField] private Text text_learning;

    [SerializeField] private GameObject Active_Interfence;
    [SerializeField] private GameObject Active_Target;

    [Header("for Display")]
    [SerializeField] private CanvasGroup Grid;    
    [SerializeField] private GameObject LineObject;
    [SerializeField] private float LineRotationSpeed_6rpm = -36f;
    [SerializeField] private float LineRotationSpeed_12rpm = -72f;

    private float _brightness;
    public float Brightness_IKO
    {
        private get { return _brightness; }
        set
        {
            _brightness = value;
            if (_brightness > 1) Grid.alpha = 1;
            else if (_brightness < 0) Grid.alpha = 0;
            else Grid.alpha = value;
        }
    }

    private bool _mode;
    public bool round_mode
    {
        get => _mode;
        set
        {
            _mode = value;
            var pos = LineObject.transform.localPosition;
            var angles = LineObject.transform.localEulerAngles;
            if (value == true) pos.y = 0;
            else pos.y = -250;
            angles.z = 135;
            LineObject.transform.localPosition = pos;
            LineObject.transform.localEulerAngles = angles;
            Debug.Log("mode is round");
        }
    }
    private float LineRotationSpeed
    {
        get
        {
            switch (round_mode)
            {
                case true:
                    return LineRotationSpeed_12rpm;
                default:
                    return LineRotationSpeed_6rpm;
            }
        }
    }

    void Start()
    {
        round_mode = true;
    }

    void Update() => Work_of_Line();
         

    private bool turning_line;
    private void Work_of_Line()
    {
        var angles = LineObject.transform.localEulerAngles;
        if (round_mode == true) angles.z += LineRotationSpeed * Time.deltaTime;
        else
        {
            if (turning_line == true) angles.z += LineRotationSpeed * Time.deltaTime;
            else angles.z -= LineRotationSpeed * Time.deltaTime;
            if (angles.z > 135) turning_line = true;
            else if (angles.z < 45) turning_line = false;
        }
        LineObject.transform.localEulerAngles = angles;
    }

    public void Span_Interference(int number)
    {       
        switch (number)
        {
            case 1:                
                Active_Interfence = Instantiate(passive[Random.Range(0, passive.Count)]);
                text_learning.text = text_passive;
                break;
            case 2:
                Active_Interfence = Instantiate(local[Random.Range(0, local.Count)]);
                text_learning.text = text_local;
                break;
            case 3:
                Active_Interfence = Instantiate(nip[Random.Range(0, nip.Count)]);
                text_learning.text = text_nip;
                break;
            case 4:
                Active_Interfence = Instantiate(active_noise[Random.Range(0, active_noise.Count)]);
                text_learning.text = text_active_noise;
                break;
            case 5:
                Active_Interfence = Instantiate(respons_answer[Random.Range(0, respons_answer.Count)]);
                text_learning.text = text_respons_answer;
                break;
            default:
                Debug.LogError("number is out");
                return;
        }

        if (folder_Interfence.childCount > 0)
        {
            GameObject block = folder_Interfence.GetChild(0).gameObject;
            Destroy(block);
        }

        Active_Interfence.transform.SetParent(folder_Interfence, false);
        Invoke("Delete_Interference", 4f);

    }

    private void Delete_Interference()
    {
        Active_Interfence.GetComponent<Body_Interference>().Check_work = true;
    }


    public void Span_Target(int number)
    {
        switch (number)
        {
            case 1:
                
                break;
            case 2:
                
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            case 5:
                
                break;
            default:
                Debug.LogError("number is out");
                return;
        }

        if (folder_Interfence.childCount > 0)
        {
            GameObject block = folder_Interfence.GetChild(0).gameObject;
            Destroy(block);
        }

        Active_Interfence.transform.SetParent(folder_Interfence, false);
    }

    public void Delete_Target()
    {

    }

}