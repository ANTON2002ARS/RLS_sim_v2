using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RLS_Scene : MonoBehaviour
{
    [SerializeField] private Machine_2 machine_2;
    [SerializeField] private Machine_1 machine_1;
    [SerializeField] private P_71 iko_P71;
    [SerializeField] private O71_block o71_signals;
    [SerializeField] private GameObject Panel_learning;
    [SerializeField] private GameObject Panel_Interfence;
    [SerializeField] private GameObject Panel_Target;
    [SerializeField] private GameObject Panel_Signals;
    [SerializeField] private GameObject Panel_Move;

    [Header("Positions")]
    [SerializeField] private Transform Camera_pos;
    [SerializeField] private Vector3 Position_P71_IKO;
    [SerializeField] private Vector3 Position_O71;

    void Start()
    {
        Stop_Antena(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop_Antena()
    {
        iko_P71.use_round = false;
        machine_2.use_round = false; 
    }

    public void Rotation_Antena(bool is_12)
    {
        iko_P71.use_round = true;
        machine_2.use_round = true; 
        iko_P71.round_mode = is_12;
        machine_2.round_mode = is_12;
    }

    public void Use_Backward(bool is_back){
        iko_P71.use_round = true;
        machine_2.use_round = true; 
        iko_P71.is_backward = is_back;
        machine_2.is_backward = is_back;
    }

    public void Show_Panel_Interfence(){
        Panel_Interfence.SetActive(!Panel_Interfence.activeSelf);
        Panel_Target.SetActive(false);
        Panel_Signals.SetActive(false);
        Panel_Move.SetActive(false);
    }
    public void Show_Panel_Target(){
        Panel_Interfence.SetActive(false);
        Panel_Target.SetActive(!Panel_Target.activeSelf);
        Panel_Signals.SetActive(false);
        Panel_Move.SetActive(false);
    }
    public void Show_Panel_Signals(){
        Panel_Interfence.SetActive(false);
        Panel_Target.SetActive(false);
        Panel_Signals.SetActive(!Panel_Signals.activeSelf);
        Panel_Move.SetActive(false);
    }

    public void Show_Panel_Move(){
        Panel_Interfence.SetActive(false);
        Panel_Target.SetActive(false);
        Panel_Signals.SetActive(false);
        Panel_Move.SetActive(!Panel_Move.activeSelf);
    }

     public void For_Button_Span_Signals(int number){
        o71_signals.Show_Signal(number);
    }    

    public void For_Button_Span_Target(int number){
        Rotation_Antena(false);
        switch(number){
            case 1:
                iko_P71.Span_Target(true,false,false);
            break;
            case 2:
                iko_P71.Span_Target(true,true,false);
            break;
            case 3:
                iko_P71.Span_Target(false,false,false);
            break;
            case 4:
                iko_P71.Span_Target(false,true,false);
            break;
            case 5:
                iko_P71.Span_Target(false,false,true);
            break;
        }
    }
    public void For_Button_Move(int number){
        switch(number){
            case 1:
                Camera_pos.position = Position_P71_IKO;
            break;
            case 2:
                Camera_pos.position = Position_O71;                
            break;            
        }
    }

    public void For_Button_Span_Interfence(int number)
    {
        Rotation_Antena(false);
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
       iko_P71.Span_Interference(number);
    }

    public void Start_RLS_Rise() 
    {
        iko_P71.use_round = false;
        machine_2.use_round = false;
        machine_2.Start_Rise_RLS();        
    } 
}
