using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Game : MonoBehaviour
{
    [Header("Use of Task from Menu")]
    public Abst_Task Active_Task;
    [SerializeField] private Task Use_Simple_Test;
    //[SerializeField] private War_Interference war_interference;
    //[SerializeField] private War_targets war_targets;
    [Header("UI")]
    [SerializeField] private GameObject SureCheck;
    [SerializeField] private GameObject CheckResult;
    [SerializeField] private GameObject Pass_Test;
    [SerializeField] private GameObject Faid_Test;
    [SerializeField] private int index_block;
    [SerializeField] private GameObject Button_Show_IKO;
    [SerializeField] private GameObject Button_Kill_Interference;
    [Header("list blocks")]
    [SerializeField] private Transform folder_blocks;
    [SerializeField] private GameObject pedalka;
    [SerializeField] private GameObject block_pos_72;
    [SerializeField] private GameObject pov_71;
    [Header("Text of Target")]
    [SerializeField] private GameObject Panel_Blocks;
    [SerializeField] private InputField Input_Height;
    [SerializeField] private GameObject Panel_Target;
    [SerializeField] private GameObject Panel_Interfence;
    [SerializeField] private GameObject Buttons_Target_Test;
    [SerializeField] private GameObject Buttons_Target_Test_Report;
    [SerializeField] private List<Button> Buttons_Report;
    [SerializeField] public Dropdown Choice_target;

    [SerializeField] private Text Report_Text_UP;
    [SerializeField] private Text report_text;
    [SerializeField] private GameObject Panel_learning;
    [SerializeField] private GameObject Panel_learning_Start;
    [SerializeField] private Text Panel_Text_Start;

    private int choice_target;

    [Header("for IKO")]
    [SerializeField] private P_71 IKO;
    [Header("Search in thr sector")]
    //для Проверки в тесте на положение. Радиус 2.5f, -2.5< Y,X <2.5
    [SerializeField] private float line_X_border;
    [SerializeField] private float line_Y_border;

    public static Scene_Game test_instance { get; private set; }
    private void Awake() => test_instance = this;

    void Start()
    {   
        Hide_All_Elements();        

        this.Active_Task = MenuManager.Active_Task;
        //Abst_Task task = Active_Task;

        // Задачи теста
        if (Active_Task is Task)
        {
            Debug.Log("Задание простого теста");
            Use_Simple_Test = Active_Task as Task;
            if (Active_Task == null)
            {
                Debug.LogError("Task obj not set");
                return;
            }
            Show_Block(index_block);
            if (MenuManager.Menu_Instance.Use_Learning == true)
            {
                if(Use_Simple_Test.Text_Learnihg_All != "")
                {                    
                    Panel_Text_Start.gameObject.SetActive(true);
                    Panel_Text_Start.text = Use_Simple_Test.Text_Learnihg_All;
                }
                
            }
        }
        else if (Active_Task is War_Interference)
        {
            Start_Test_Interference(Active_Task);
        }
        else if (Active_Task is War_targets)
        {
            Debug.Log("Задачи слежение за целями");
            War_targets war_targets = new War_targets();
            war_targets = Active_Task as War_targets;
            if (war_targets.use_sector)
            {
                Start_Test_Sector_Review();                
            }
            else if (war_targets.use_reguest_target)
            {
                Start_Test_Reguest();                
            }
            else if (war_targets.reguest_height)
            {
                Start_Test_Reguest_Height();                
            }
            else if(war_targets.use_war_with_PRS){
                Start_Test_war_with_PRS();       
            } 
            else{
                Debug.LogError("War task is not active");
                return;
            }  

            if (MenuManager.Menu_Instance.Use_Learning == true)
            {
                Panel_learning.transform.GetChild(0).gameObject.GetComponent<Text>().text = war_targets.Text_Learning;
            } 
        }
        else
        {
            Debug.LogError("Неизвестный тип данных в Abst_Task");
        }
    }

    private float skipCooldown = 1f;
    private float lastSkipTime = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.O))
        {
            Pass_Testing();
        }
        else if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.V) && Input.GetKey(KeyCode.O))
        {
            if (Time.time - lastSkipTime >= skipCooldown)
            {
                index_block++;
                lastSkipTime = Time.time;

                if (index_block >= Use_Simple_Test.block_need.Count)
                {
                    Pass_Testing();
                    index_block = 0;
                }
                else
                {
                    report_text.gameObject.SetActive(true);
                    report_text.text = "ТЕСТ НА БЛОКЕ ВЫПОЛНЕН ПРАВИЛЬНО, ПОЯВЛЕНИЕ СЛЕДУЮЩЕГО БЛОКА";
                    Invoke("Off_Report_Text", 2f);
                    Show_Block(index_block);
                    Debug.Log("next block texting");
                }
            }
        }
    }


    private void Hide_All_Elements()
    {
        Show_SureChecker(false);
        CheckResult.SetActive(false);
        Button_Show_IKO.SetActive(false);
        Button_Kill_Interference.SetActive(false);
        Panel_Blocks.SetActive(false);
        Input_Height.gameObject.SetActive(false);
        Panel_Target.SetActive(false);
        Panel_Interfence.SetActive(false);
        Buttons_Target_Test_Report.SetActive(false);
        report_text.gameObject.SetActive(false);
        Report_Text_UP.gameObject.SetActive(false);
        IKO.gameObject.SetActive(false);
        folder_blocks.gameObject.SetActive(false);
        Panel_learning_Start.SetActive(false);
        Panel_learning.SetActive(MenuManager.Menu_Instance.Use_Learning);
    }

    private void Start_Test_Interference(Abst_Task task)
    {
        Debug.Log("Задачи избавление от помех");
        War_Interference war_interference = new War_Interference();
        war_interference = task as War_Interference;
        Use_Simple_Test = new Task();
        Use_Simple_Test.block_need = war_interference.block_need;

        if (war_interference.use_passive)
        {
            IKO.Span_Interference(1);
        }
        else if (war_interference.use_local)
        {
            IKO.Span_Interference(2);
        }
        else if (war_interference.use_nip)
        {
            IKO.Span_Interference(3);
        }
        else if (war_interference.use_active_noise)
        {
            IKO.Span_Interference(4);
        }
        else if (war_interference.use_respons_answer)
        {
            IKO.Span_Interference(5);
        }
        else if (war_interference.Visualize_Interfence)
        {
            IKO.gameObject.SetActive(true);
            Panel_Interfence.SetActive(true);
            IKO.Span_Target();
            return;
        }
        else
        {
            Debug.Log("Task Interference defined");
            return;
        }
        //else if(war.use_passive || war.use_local || 
        // war.use_nip || war.use_active_noise || 
        // war.use_respons_answer){// Debug.Log("ALL ERROR");}
        // Нужные элемены для теста
        IKO.Span_Target();
        IKO.Span_Target();
        IKO.Span_Target();
        Button_Show_IKO.SetActive(true);
        Button_Kill_Interference.SetActive(true);
        IKO.gameObject.SetActive(true);
        folder_blocks.gameObject.SetActive(false);
    }

    public void For_Button_Span_Interfence(int number)
    {
        Panel_learning.SetActive(true);
        switch(number){
            case 1:
                //ПАССИВНАЯ помеха
                Panel_learning.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "Пассивная помеха на радиолокационных станциях (РЛС) — это тип помех, возникающий при отражении зондирующего сигнала РЛС от различных объектов, таких как земная или водная поверхность, облака, осадки, или специально созданных отражателей (например, дипольных или уголковых отражателей). "+
                "Эти отражения могут маскировать или подавлять сигналы, отраженные от реальных целей, что затрудняет их обнаружение и сопровождение. \n"+
                "Принцип действия: \n"+
                "1. Отражение сигнала: Зондирующий сигнал РЛС отражается от мешающих объектов. \n"+
                "2. Воздействие на РЛС: Отраженные сигналы возвращаются к РЛС и могут создавать ложные отметки или шумовой фон на экране. \n"+
                "3. Последствия: Это приводит к трудностям в обнаружении и сопровождении реальных целей, а также может перегружать системы автоматической обработки данных. \n"+
                "Методы борьбы с пассивными помехами включают: \n"+
                "Селекцию движущихся целей (СДЦ) для различия сигналов от движущихся целей и неподвижных отражателей. \n"+
                "Уменьшение боковых лепестков антенны для снижения количества отражений от окружающих объектов. \n"+
                "Использование специальных фильтров для подавления частотных составляющих помех. \n";

            break;
            case 2:
                //от МЕСТНЫХ предметов
                Panel_learning.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "Пассивные помехи от местных предметов на радиолокационных станциях (РЛС) возникают при отражении зондирующих сигналов РЛС от различных объектов, таких как здания, деревья, рельеф местности и другие неподвижные или медленно движущиеся предметы. "+
                "Эти отражения могут создавать ложные отметки или шумовой фон на экране РЛС, что затрудняет обнаружение и сопровождение реальных целей. \n"+
                "Принцип действия: \n"+
                "1. Отражение сигнала: Зондирующий сигнал РЛС отражается от местных предметов. \n"+
                "2. Воздействие на РЛС: Отраженные сигналы возвращаются к РЛС и могут создавать ложные отметки или шум. \n"+
                "3. Последствия: Это приводит к трудностям в обнаружении и сопровождении реальных целей, а также может перегружать системы автоматической обработки данных. \n"+
                "Методы борьбы включают: \n"+
                "Селекцию движущихся целей (СДЦ) для различия сигналов от движущихся целей и неподвижных отражателей. \n"+
                "Картографирование местности для учёта стационарных помех. \n"+
                "Использование допплеровского сдвига для фильтрации сигналов от неподвижных объектов. \n";                
            break;
            case 3:
                // Нелинейная ИМПУЛЬСНАЯ помеха 
                Panel_learning.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                "Нелинейная импульсная помеха на радиолокационных станциях (РЛС) — это тип помех, который возникает из-за нелинейных эффектов в радиолокационных системах или окружающей среде. "+
                "Однако в контексте радиолокации термин \"нелинейная\" чаще используется для описания методов повышения помехоустойчивости и разрешающей способности сигналов, а не для описания типа помех. \n"+
                "Нелинейные методы обработки сигналов в РЛС используются для повышения помехоустойчивости и разрешающей способности. Например, в нелинейной радиолокации применяются методы, такие как модифицированное вейвлет-преобразование, для улучшения качества сигналов и снижения влияния помех. \n"+
                "Если говорить об импульсных помехах в целом, то они представляют собой кратковременные, высокоинтенсивные электромагнитные излучения, которые могут исказить или полностью заглушить сигналы РЛС. Для борьбы с такими помехами используются различные методы обработки сигналов и технические решения, такие как селекция движущихся целей или использование нелинейных компенсаторов. ";                
            break;
            case 4:
                // АКТИВНО-ШУМОВАЯ помеха
                Panel_learning.transform.GetChild(0).gameObject.GetComponent<Text>().text = 
                "Активно-шумовая помеха на радиолокационных станциях (РЛС) — это тип помех, создаваемый специальными генераторами для нарушения работы РЛС. \n"+
                "Эти помехи представляют собой шумовые сигналы, которые добавляются к внутренним шумам приемника РЛС, ухудшая отношение сигнал-шум и снижая вероятность обнаружения целей. \n"+
                "Виды шумовых помех: \n"+
                "Заградительные: Широкополосные помехи, используемые когда неизвестны параметры РЛС. \n"+
                "Прицельные: Узкополосные помехи, настроенные на частоту конкретной РЛС. \n"+
                "Последствия: \n"+
                "Снижение дальности и высоты обнаружения. \n"+
                "Создание ложных отметок или шумового фона на экране РЛС. \n"+
                "Перегрузка систем автоматической обработки данных. \n"+
                "Методы защиты: \n"+
                "Использование автокомпенсаторов и устройств защиты от помех. \n"+
                "Применение пространственной, частотной и поляризационной селекции сигналов. ";                
            break;
            case 5:
                // ответно импульсная помеха 
                Panel_learning.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                 "Ответно-импульсная помеха на радиолокационные станции (РЛС) — это тип радиоэлектронных помех, который работает следующим образом:\n" +
                 "1. Принятие сигнала РЛС: Передатчик помех принимает зондирующий сигнал от РЛС.\n"+
                 "2. Формирование ответного сигнала: В ответ на этот сигнал формируется один или несколько импульсов помехи, которые по форме, длительности и мощности идентичны сигналу РЛС, но могут иметь задержку во времени.\n"+
                 "3. Излучение помехи: Эти импульсы усиливаются и излучаются в направлении РЛС.\n"+
                 "4. Создание ложных отметок: На экране РЛС появляются ложные отметки, которые могут быть приняты за реальные цели. Это приводит к дезориентации оператора и перегрузке системы обработки информации.\n"+
                 "Ответно-импульсные помехи бывают многократными и однократными. "+
                 "Многократные помехи создают несколько ложных отметок, в то время как однократные — одну, но более длительную. "+
                 "Помехи также могут быть уводящими, когда ложная информация вводится в сигнал, чтобы сбить с толку системы сопровождения РЛС. ";
            break;
        }
       IKO.Span_Interference(number);
    }

    public void Kill_Test_Interference()
    {
        IKO.gameObject.SetActive(false);
        folder_blocks.gameObject.SetActive(true);
        Show_Block(index_block);
        Button_Kill_Interference.SetActive(false);
    }

    private void Start_Test_Sector_Review()
    {
        Button_Show_IKO.SetActive(true);
        IKO.gameObject.SetActive(true);
        Report_Text_UP.gameObject.SetActive(true);
        int use_sector = Random.Range(1, 5);
        switch (use_sector)
        {
            case 1:
                line_X_border = -1f;
                line_Y_border = -1f;
                Report_Text_UP.text = "Поиск в секторе 030-060";
                IKO.Span_Target(0f, 90f);
                break;
            case 2:
                line_X_border = -1f;
                line_Y_border = 1f;
                Report_Text_UP.text = "Поиск в секторе 120-150";
                IKO.Span_Target(90f, 180f);
                break;
            case 3:
                line_X_border = 1f;
                line_Y_border = 1f;
                Report_Text_UP.text = "Поиск в секторе 210-240";
                IKO.Span_Target(180f, 240f);
                break;
            case 4:
                line_X_border = 1f;
                line_Y_border = -1f;
                Report_Text_UP.text = "Поиск в секторе 300-330";
                IKO.Span_Target(240f, 0f);
                break;
        }
        IKO.Span_Target();
        IKO.Span_Target();
    }

    public void Proverka_Test_Sector_Review(Vector2 Display_IKO_position)
    {
        if (line_X_border == 0 && line_Y_border == 0)
            return;

        Vector2 position_display = Display_IKO_position;
        bool x_line_pass = false;
        bool y_line_pass = false;

        if (Check_Position(line_X_border, position_display.x))
        {
            x_line_pass = true;
            if (Check_Position(line_X_border * (-1), position_display.x))
            {
                // End_Test_Sector_Review(false);
                Debug.Log("Сетка в противоположном направлении по X");
            }
        }

        if (Check_Position(line_Y_border, position_display.y))
        {
            y_line_pass = true;
            if (Check_Position(line_Y_border * (-1), position_display.y))
            {
                // End_Test_Sector_Review(false);
                Debug.Log("Сетка в противоположном направлении по Y");
            }
        }

        if (x_line_pass && y_line_pass)
        {
            End_Test_Sector_Review(true);
            Debug.Log("ТЕСТ ПРОЕДЕН");
        }
    }

    private bool Check_Position(float line, float position)
    {
        return (line > 0) ? (position > line) : (position < line);
    }

    private void End_Test_Sector_Review(bool is_pass)
    {
        if (is_pass == true)
            Pass_Testing();
        else
            Faid_Testing();
    }

    // Переключение на ИКО
    public void Show_IKO()
    {
        bool is_active = !IKO.gameObject.activeSelf;
        IKO.gameObject.SetActive(is_active);
        folder_blocks.gameObject.SetActive(!is_active);
    }

    public void Show_Block(int index)
    {
        folder_blocks.gameObject.SetActive(true);
        if (folder_blocks.childCount > 0)
        {
            GameObject block = folder_blocks.GetChild(0).gameObject;
            Destroy(block);
        }
        GameObject b = Instantiate(Use_Simple_Test.block_need[index].of_Blocks);
        b.transform.SetParent(folder_blocks);
        b.GetComponent<Abst_Block>().Need_Condition = Use_Simple_Test.block_need[index].Command_need;
        if (MenuManager.Menu_Instance.Use_Learning == true)
        {
            if (Use_Simple_Test.Text_Learnihg_All != "")
            {
                Panel_Text_Start.gameObject.SetActive(true);
                Panel_learning_Start.transform.GetChild(0).gameObject.GetComponent<Text>().text = Use_Simple_Test.Text_Learnihg_All;
            }               
            else
                Panel_learning_Start.SetActive(false);

            Panel_learning.transform.GetChild(0).gameObject.GetComponent<Text>().text = Use_Simple_Test.block_need[index].Text_Learnihg_for_Block;
        }

            

    }

    public void Calling_Completion_Block(bool is_pass)
    {
        if (!is_pass)
            Faid_Testing();
        else
        {
            index_block++;

            if (index_block >= Use_Simple_Test.block_need.Count)
            {
                Pass_Testing();
                index_block = 0;
                //GameObject block = folder_blocks.GetChild(0).gameObject;
                //Destroy(block);
            }
            else
            {
                report_text.gameObject.SetActive(true);
                report_text.text = "ТЕСТ НА БЛОКЕ ВЫПОЛНЕН ПРАВИЛЬНО, ПОЯВЛЕНИЕ СЛЕДУЮЩЕГО БЛОКА";
                Invoke("Off_Report_Text", 2f);
                Show_Block(index_block);
                Debug.Log("next block texting");
            }
        }

        Debug.Log("отработка блока, статус: " + is_pass);
    }

    // задание слежение за целями
    private void Start_Test_Reguest()
    {
        Button_Show_IKO.SetActive(true);
        Panel_Blocks.SetActive(true);
        IKO.gameObject.SetActive(true);
        IKO.Span_Target();
        Report_Text_UP.gameObject.SetActive(true);
        Report_Text_UP.text = "Определить государственную принадлежность и кол-во целей";
        Panel_Target.SetActive(true);
    }
    // Запрос цели
    public void Show_Pedalka()
    {
        folder_blocks.gameObject.SetActive(true);
        IKO.gameObject.SetActive(false);
        if (folder_blocks.childCount > 0)
        {
            Destroy(folder_blocks.GetChild(0).gameObject);
        }
        GameObject block_pedalka = Instantiate(pedalka);
        block_pedalka.transform.SetParent(folder_blocks);
        block_pedalka.GetComponent<Block_Pedalka>().jump_foot.AddListener(Ckick_Pedalka);
    }
    public void Ckick_Pedalka()
    {
        IKO.gameObject.SetActive(true);
        folder_blocks.gameObject.SetActive(false);
        IKO.Request_Target_last();
    }

    public void Show_POV_71()
    {
        folder_blocks.gameObject.SetActive(true);
        IKO.gameObject.SetActive(false);
        if (folder_blocks.childCount > 0)
        {
            Destroy(folder_blocks.GetChild(0).gameObject);
        }
        GameObject block_pov71 = Instantiate(pov_71);
        block_pov71.transform.SetParent(folder_blocks);
        folder_blocks.gameObject.SetActive(true);
    }

    public void Show_POS_71()
    {
        folder_blocks.gameObject.SetActive(true);
        IKO.gameObject.SetActive(false);
        if (folder_blocks.childCount > 0)
        {
            Destroy(folder_blocks.GetChild(0).gameObject);
        }
        GameObject block_pov71 = Instantiate(block_pos_72);
        block_pov71.transform.SetParent(folder_blocks);
        folder_blocks.gameObject.SetActive(true);
    }

    private bool is_answer_group;
    private bool is_answer_our;

    public void Click_Button_Target_Group(int number)
    {
        is_answer_group = true;
        switch (number)
        {
            case 1:
                // Одиночная
                if (IKO.Last_Target_is_Group())
                    Faid_Testing();
                report_text.gameObject.SetActive(true);
                report_text.text = "ПРАВИЛЬНО, Цель одиночная";
                Invoke("Off_Report_Text", 1f);
                break;
            case 2:
                // Групповая
                if (!IKO.Last_Target_is_Group())
                    Faid_Testing();
                report_text.gameObject.SetActive(true);
                report_text.text = "ПРАВИЛЬНО, Цель групповая";
                Invoke("Off_Report_Text", 1f);
                break;
        }

    }

    public void Click_Button_Target_Our(int number)
    {
        is_answer_our = true;
        switch (number)
        {
            case 1:
                // Цель 00 нет ответа
                if (IKO.Last_Target_is_Our())
                    Faid_Testing();
                report_text.gameObject.SetActive(true);
                report_text.text = "ПРАВИЛЬНО, Цель чужая";
                Invoke("Off_Report_Text", 1f);
                break;
            case 2:
                // Цель 00 свой
                if (!IKO.Last_Target_is_Our())
                    Faid_Testing();
                report_text.gameObject.SetActive(true);
                report_text.text = "ПРАВИЛЬНО, Цель своя";
                Invoke("Off_Report_Text", 1f);
                break;
            case 3:
                // Цель 00 сигнал бедствие
                if (!IKO.Last_Target_is_Helper())
                    Faid_Testing();
                report_text.gameObject.SetActive(true);
                report_text.text = "ПРАВИЛЬНО, Цель с сигналом бедствия";
                Invoke("Off_Report_Text", 1f);
                break;
        }
        Buttons_Target_Test_Report.SetActive(true);
        Set_Report_last_Target();
    }

    private int Find_Azimuth(Vector2 of_Target)
    {
        float angle = Vector3.SignedAngle(Vector2.up, of_Target, Vector3.forward);
        angle = -1 * angle;
        if (angle < 0)
            angle += 360f;
        //Debug.Log("????: " + angle + " ???????? ?? ??????? ???????");
        return (int)angle;
    }

    private int Find_Ring_of_Target(Vector2 of_target)
    {
        float ring_target = (float)(of_target.magnitude * 15 / 4);
        return (int)ring_target;
    }

    private float Get_Deviated_Random_Value(float initial)
    {
        float deviation_Percentage = 0.7f;
        float deviation = initial * deviation_Percentage;
        float minRange = initial - deviation;
        float maxRange = initial + deviation;
        return Random.Range(minRange, maxRange);
    }

    private string Answer_Report;

    public void Set_Report_last_Target()
    {
        Vector2 of_target = IKO.Last_Target_position();
        int ring_target = Find_Ring_of_Target(of_target) * 10;
        int azimuth_target = Find_Azimuth(of_target);
        Answer_Report = "Цель 00 " + azimuth_target + " " + ring_target;
        Debug.Log(Answer_Report);
        foreach (var button in Buttons_Report)
        {
            string variant = "Цель 00 " + (int)Get_Deviated_Random_Value(azimuth_target) + " " + (int)Get_Deviated_Random_Value(ring_target);
            button.GetComponentInChildren<Text>().text = variant;
            button.onClick.AddListener(() => End_Test_Report_Targets(button));
        }
        Button button_right = Buttons_Report[Random.Range(0, Buttons_Report.Count)];
        button_right.GetComponentInChildren<Text>().text = Answer_Report;
    }

    public void End_Test_Report_Targets(Button button)
    {
        if (button.GetComponentInChildren<Text>().text != Answer_Report)
            Faid_Testing();
        if (is_answer_group && is_answer_our)
        {
            Faid_Testing();
        }
        Pass_Testing();
    }


    private void Start_Test_Reguest_Height()
    {
        Button_Show_IKO.SetActive(true);
        Panel_Blocks.SetActive(true);
        Input_Height.gameObject.SetActive(true);
        IKO.gameObject.SetActive(true);
        IKO.Span_Target();
        Report_Text_UP.gameObject.SetActive(true);
        Report_Text_UP.text = "Определить высоту цели на ИКО";
        pov_71.GetComponent<POV72_block>().Use_Height = Random.Range(0, 31);
    }
    public void Check_Height()
    {
        string answer_str = Input_Height.text;
        int answer;
        if (!int.TryParse(answer_str, out answer))
        {
            Debug.Log("Ошибка ввода: " + answer_str);
            Input_Height.text = "не то что нужно";
        }
        if (answer == pov_71.GetComponent<POV72_block>().Use_Height)
        {
            End_Test_Reguest_Height();
        }
        else
            Faid_Testing();
    }

    private void End_Test_Reguest_Height() => Pass_Testing();

    public void Start_Test_war_with_PRS(){
        IKO.gameObject.SetActive(true);
        Panel_Blocks.SetActive(true);
        Button_Show_IKO.SetActive(true);
        IKO.Span_Target_with_PRS();
    }

    public void End_Text_war_with_PRS(bool is_pass){
        if(is_pass == true){
            Pass_Testing();
        }
        else{
            Faid_Testing();
        }
    }

    public void Remove_Option(int optionIndex)
    {
        List<Dropdown.OptionData> options = Choice_target.options;

        options.RemoveAt(optionIndex);

        Choice_target.options = options;
        Choice_target.value = 0;
    }

    public int Find_Free_Number(List<int> numbers)
    {
        numbers.Sort();
        int lastNumber = -1;
        foreach (int number in numbers)
        {
            if (number - lastNumber > 1)
                return lastNumber + 1;
            lastNumber = number;
        }
        return lastNumber + 1;
    }

    public void Request_Targets()
    {
        int namber = 0;
        Choice_target.SetValueWithoutNotify(namber);
        Choice_target.GetComponentInChildren<Text>().text = Choice_target.options[namber].text;
        //Choice_target.options.Add(new Dropdown.OptionData() { text = "???? 0" + Targets.Count });

    }


    public void Close_Panel_learning_Start(bool is_active) => Panel_learning_Start.SetActive(is_active);

    private void Off_Report_Text() => report_text.gameObject.SetActive(false);

    public void Show_SureChecker(bool is_active) => SureCheck.SetActive(is_active);

    public void Pass_Testing()
    {
        CheckResult.SetActive(true);
        Pass_Test.SetActive(true);
        Faid_Test.SetActive(false);
    }
    public void Faid_Testing()
    {
        CheckResult.SetActive(true);
        Pass_Test.SetActive(false);
        Faid_Test.SetActive(true);
    }

    public void Open_Scene_Test_RLS() => SceneManager.LoadScene("Scene_Task"); 

    public void Exit_Scene()
    {
        SceneManager.LoadScene("Menu_Scene");
        //MenuManager.Menu_Instance.Active_Task = null;
    }
}
