using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

public class DeviceCatalog
{
    //Устройства приведены в соответствие с ГОСТ 2.710-81
    //Более полный справочник-https://elektroshema.ru/2009-02-05-22-57-45/ugo-2/58-oboznbukv.html
    private List<string[]> devicess = new List<string[]>{
            new string[]{ "A", "Устройства" },
            /*new string[]{ "AA","Регуляторы тока"},
            new string[]{ "AK","Блоки реле"},
            new string[]{ "AKS","Устройства"},*/
            new string[]{ "B", "Преобразователи" },
            new string[]{ "BA", "Громкоговорители" },
            //new string[]{ "BB", "Датчики Холла" },
            new string[]{ "BB", "Магнитнострикционные элементы" },
            new string[]{ "BC", "Сельсин-датчики" },
            new string[]{ "BD", "Детекторы ионизирующих излучений" },
            new string[]{ "BE", "Сельсин-приемники" },
            new string[]{ "BF", "Телефоны(капсюли)" },
            new string[]{ "BG", "Датчики" },
            new string[]{ "BH", "Датчики Холла" },
            new string[]{ "BK", "Тепловые датчики" },
            new string[]{ "BL", "Фотоэлементы" },
            new string[]{ "BM", "Микрофоны" },
            new string[]{ "BN", "Датчик магнитного поля" },
            new string[]{ "BP", "Дачтики давления" },
            new string[]{ "BQ", "Пьезоэлементы" },
            new string[]{ "BR", "Датчики частоты вращения" },
            new string[]{ "BS", "Звукосниматели" },
            new string[]{ "BV", "Датчик скорости" },
            new string[]{ "C", "Конденсаторы" },
            /*new string[]{ "CB","Батареи конденсаторов силовые"},
            new string[]{ "CG","Блоки конденсаторов зарядные"},*/
            new string[]{ "D", "Схемы интегральные, микросборки" },
            new string[]{ "DA", "Схемы интегральные аналоговые" },
            new string[]{ "DD", "Схемы интегральные, цифровые, логические элементы" },
            new string[]{ "DS", "Устройства хранения информации" },
            new string[]{ "DT", "Устройства задержки" },
            new string[]{ "E", "Элементы разные" },
            new string[]{ "EK", "Нагревательные элементы" },
            new string[]{ "EL", "Лампы осветительные" },
            new string[]{ "ET", "Пиропатроны" },
            new string[]{ "F", "Разрядники, предохранители, устройства  защитные" },
            new string[]{ "FA", "Дискретные элементы защиты по току мгновенного действия" },
            new string[]{ "FP", "Дискретные элементы защиты по току инерционного действия" },
            new string[]{ "FU", "Предохранители плавкие" },
            new string[]{ "FV", "Дискретные элементы защиты по напряжению, разрядники" },
            new string[]{ "G", "Генераторы, источники питания" },
            new string[]{ "GB", "Батареи" },
            /*new string[]{ "GC","Синхронные компенсаторы"},
            new string[]{ "GE","Возбудители генератора"},*/
            new string[]{ "H", "Устройства индикационные и сигнальные" },
            new string[]{ "HA", "Приборы звуковой сигнализации" },
            new string[]{ "HG", "Индикаторы символьные" },
            new string[]{ "HL", "Приборы световой сигнализации" },
            /*new string[]{ "HLA","Табло сигнальные"},
            new string[]{ "HLG","Лампы сигнальные с зеленой линзой"},
            new string[]{ "HLR","Лампы сигнальные с красной линзой"},
            new string[]{ "HLW","Лампы сигнальные с белой линзой"},
            new string[]{ "HV","Индикаторы ионные и полупроводниковые"},*/
            new string[]{ "I", "Устройства"},
            new string[]{ "J", "Джамперы" },
            new string[]{ "K", "Реле, контакторы, пускатели" },
            new string[]{ "KA", "Реле токовые" },
            /*new string[]{ "KCC","Реле команды включения"},
            new string[]{ "KCT","Реле команды отключения"},*/
            new string[]{ "KH", "Реле указательные" },
            new string[]{ "KK", "Реле электротепловые" },
            //new string[]{ "KL","Реле промежуточные"},
            new string[]{ "KM", "Контакторы, магнитные пускатели" },
            new string[]{ "KT", "Реле времени" },
            new string[]{ "KV", "Реле напряжения" },
            new string[]{ "L", "Катушки индуктивности, дроссели" },
            new string[]{ "LL", "Дроссели люминесцентного освещения" },
            //new string[]{ "LM","Обмотки возбуждения электродвигателей"},
            //new string[]{ "LR","Реакторы"},
            new string[]{ "M", "Двигатели" },
            //new string[]{ "MA","Электродвигатели"},
            new string[]{ "P", "Приборы, измерительное оборудование" },
            new string[]{ "PA", "Амперметры" },
            new string[]{ "PC", "Счетчики импульсов" },
            new string[]{ "PF", "Частотометры" },
            new string[]{ "PI", "Счетчики активной энергии" },
            new string[]{ "PK", "Счетчики реактивной энергии" },
            new string[]{ "PR", "Омметры" },
            new string[]{ "PS", "Регистрирующие приборы" },
            new string[]{ "PT", "Часы, измерители времени" },
            new string[]{ "PV", "Вольтметры" },
            new string[]{ "PW", "Ваттметры" },
            new string[]{ "Q", "Выключатели и разъединители в силовых цепях" },
            new string[]{ "QF", "Выключатели автоматические" },
            new string[]{ "QK", "Короткозамыкатели" },
            new string[]{ "QS", "Разъединители" },
            new string[]{ "R", "Резисторы" },
            new string[]{ "RK", "Терморезисторы" },
            //new string[]{ "RN", "Резисторы" },
            new string[]{ "RP", "Потенциометры" },
            //new string[]{ "RR","Реостаты"},
            new string[]{ "RS", "Шунты измерительные" },
            new string[]{ "RU", "Варисторы" },
            new string[]{ "S", "Устройства коммутационные в цепях управления, сигнализации и измерительных" },
            new string[]{ "SA", "Выключатели и переключатели" },
            new string[]{ "SB", "Выключатели кнопочные" },
            new string[]{ "SF", "Выключатели автоматические" },
            new string[]{ "SL", "Выключатели, срабатыающие от уровня" },
            new string[]{ "SP", "Выключатели, срабатыающие от давления" },
            new string[]{ "SQ", "Выключатели, срабатыающие от положения(путевые)" },
            new string[]{ "SR", "Выключатели, срабатыающие от от частоты вращения" },
            new string[]{ "SK", "Выключатели, срабатыающие от температуры" },
            new string[]{ "T", "Трансформаторы, автотрансформаторы" },
            new string[]{ "TA", "Трансформаторы тока" },
            new string[]{ "TS", "Электромагнитные стабилизаторы" },
            new string[]{ "TV", "Трансформаторы напряжения" },
            new string[]{ "U", "Устройства связи" },
            new string[]{ "UB", "Модуляторы" },
            //new string[]{ "UF","Преобразователи частоты"},
            //new string[]{ "UG","Блоки питания"},
            new string[]{ "UI", "Дискриминаторы" },
            new string[]{ "UR", "Демодуляторы" },
            new string[]{ "UZ", "Преобразователи частотные, инверторы, генераторы частоты, выпрямители" },
            new string[]{ "V", "Приборы электровакуумные и полупроводниковые" },
            new string[]{ "VD", "Диоды, стабилитроны" },
            new string[]{ "VL", "Приборы электровакуумные" },
            new string[]{ "VS", "Тиристоры" },
            new string[]{ "VT", "Транзисторы" },
            new string[]{ "W", "Линии и элементы СВЧ, Антенны" },
            new string[]{ "WA", "Антенны" },
            new string[]{ "WE", "Ответвители" },
            new string[]{ "WK", "Короткозамыкатели" },
            new string[]{ "WS", "Вентили" },
            new string[]{ "WT", "Трансформаторы, неоднородности, фазовращатели" },
            new string[]{ "WU", "Аттенюаторы" },
            new string[]{ "X", "Соединения контактные" },
            new string[]{ "XA", "Контакты скользящие, токосъемники" },
            new string[]{ "XP", "Штыри" },
            new string[]{ "XS", "Гнезда" },
            new string[]{ "XT", "Соединения разборные" },
            new string[]{ "XW", "Соединители выскочастотные" },
            new string[]{ "Y", "Устройства механические с электромагнитным приводом" },
            new string[]{ "YA", "Электромагниты" },
            new string[]{ "YB", "Тормоза с электромагнитным приводом" },
            new string[]{ "YC", "Муфты с электромагнитным приводом" },
            new string[]{ "YH", "Электромагнитные патроны или плиты" },
            new string[]{ "Z", "Устройства, оконечные фильтры" },
            //new string[]{ "ZC", "Фильтры" },
            new string[]{ "ZL", "Ограничители" },
            new string[]{ "ZQ", "Фильтры кварцевые" },
            new string[]{ "", " "} };

    //Наименования групп элементов, принятые в ЗАО "ЭЛСИ"
    private List<DesignatorDevice> devices = new List<DesignatorDevice>
        {
            new DesignatorDevice( "A", "Устройства" ),
            new DesignatorDevice( "AA","Регуляторы тока"),
            new DesignatorDevice( "AK","Блоки реле"),
            new DesignatorDevice( "AKS","Устройства"),
            new DesignatorDevice( "B", "Преобразователи" ),
            new DesignatorDevice( "BA", "Громкоговорители" ),
            new DesignatorDevice( "BB", "Датчики Холла" ),
            new DesignatorDevice( "BC", "Сельсин-датчики" ),
            new DesignatorDevice( "BD", "Детекторы ионизирующих излучений" ),
            new DesignatorDevice( "BE", "Сельсин-приемники" ),
            new DesignatorDevice( "BF", "Телефоны" ),
            new DesignatorDevice( "BG", "Датчики" ),
            new DesignatorDevice( "BH", "Датчики Холла" ),
            new DesignatorDevice( "BK", "Тепловые датчики" ),
            new DesignatorDevice( "BL", "Фотоэлементы" ),
            new DesignatorDevice( "BM", "Микрофоны" ),
            new DesignatorDevice( "BN", "Датчик магнитного поля" ),
            new DesignatorDevice( "BP", "Дачтики давления" ),
            new DesignatorDevice( "BQ", "Кварцевые резонаторы, пьезоэлементы" ),
            new DesignatorDevice( "BR", "Датчики частоты вращения" ),
            new DesignatorDevice( "BS", "Звукосниматели" ),
            new DesignatorDevice( "BV", "Датчик скорости" ),
            new DesignatorDevice( "C", "Конденсаторы" ),
            new DesignatorDevice( "CB","Батареи конденсаторов силовые"),
            new DesignatorDevice( "CG","Блоки конденсаторов зарядные"),
            new DesignatorDevice( "D", "Микросхемы" ),
            new DesignatorDevice( "DA", "Микросхемы" ),
            new DesignatorDevice( "DD", "Микросхемы" ),
            new DesignatorDevice( "DS", "Микросхемы" ),
            new DesignatorDevice( "DT", "Микросхемы" ),
            new DesignatorDevice( "E", "Элементы разные" ),
            new DesignatorDevice( "EK", "Нагревательные элементы" ),
            new DesignatorDevice( "EL", "Лампы осветительные" ),
            new DesignatorDevice( "ET", "Пиропатроны" ),
            new DesignatorDevice( "F", "Разрядники, предохранители, устройства  защитные" ),
            new DesignatorDevice( "FA", "Элементы защиты по току" ),
            new DesignatorDevice( "FP", "Элементы защиты по току инерционного действия" ),
            new DesignatorDevice( "FU", "Предохранители плавкие" ),
            new DesignatorDevice( "FV", "Элементы защиты по напряжению" ),
            new DesignatorDevice( "G", "Генераторы" ),
            new DesignatorDevice( "GB", "Батареи" ),
            new DesignatorDevice( "GC","Синхронные компенсаторы"),
            new DesignatorDevice( "GE","Возбудители генератора"),
            new DesignatorDevice( "H", "Устройства индикации" ),
            new DesignatorDevice( "HA", "Приборы звуковой сигнализации" ),
            new DesignatorDevice( "HG", "Индикаторы символьные" ),
            new DesignatorDevice( "HL", "Приборы световой сигнализации" ),
            new DesignatorDevice( "HLA","Табло сигнальные"),
            new DesignatorDevice( "HLG","Лампы сигнальные с зеленой линзой"),
            new DesignatorDevice( "HLR","Лампы сигнальные с красной линзой"),
            new DesignatorDevice( "HLW","Лампы сигнальные с белой линзой"),
            new DesignatorDevice( "HV","Индикаторы ионные и полупроводниковые"),
            new DesignatorDevice( "I", "Устройства"),
            new DesignatorDevice( "J", "Джамперы" ),
            new DesignatorDevice( "K", "Реле" ),
            new DesignatorDevice( "KA", "Реле токовые" ),
            new DesignatorDevice( "KCC","Реле команды включения"),
            new DesignatorDevice( "KCT","Реле команды отключения"),
            new DesignatorDevice( "KH", "Реле указательные" ),
            new DesignatorDevice( "KK", "Реле электротепловые" ),
            new DesignatorDevice( "KL","Реле промежуточные"),
            new DesignatorDevice( "KM", "Контакторы, магнитные пускатели" ),
            new DesignatorDevice( "KT", "Реле времени" ),
            new DesignatorDevice( "KV", "Реле напряжения" ),
            new DesignatorDevice( "L", "Катушки индуктивности" ),
            new DesignatorDevice( "LL", "Дроссели люминесцентного освещения" ),
            new DesignatorDevice( "LM","Обмотки возбуждения электродвигателей"),
            new DesignatorDevice( "LR","Реакторы"),
            new DesignatorDevice( "M", "Двигатели" ),
            new DesignatorDevice( "MA","Электродвигатели"),
            new DesignatorDevice( "P", "Приборы измерительные" ),
            new DesignatorDevice( "PA", "Дроссели люминесцентного освещения" ),
            new DesignatorDevice( "PC", "Счетчики импульсов" ),
            new DesignatorDevice( "PF", "Частотометры" ),
            new DesignatorDevice( "PI", "Счетчики активной энергии" ),
            new DesignatorDevice( "PK", "Счетчики реактивной энергии" ),
            new DesignatorDevice( "PR", "Омметры" ),
            new DesignatorDevice( "PS", "Регистрирующие приборы" ),
            new DesignatorDevice( "PT", "Измерители времени" ),
            new DesignatorDevice( "PV", "Вольтметры" ),
            new DesignatorDevice( "PW", "Ваттметры" ),
            new DesignatorDevice( "Q", "Выключатели и разъединители в силовых цепях" ),
            new DesignatorDevice( "QF", "Выключатели автоматические" ),
            new DesignatorDevice( "QK", "Короткозамыкатели" ),
            new DesignatorDevice( "QS", "Разъединители" ),
            new DesignatorDevice( "R", "Резисторы" ),
            new DesignatorDevice( "RK", "Терморезисторы" ),
            new DesignatorDevice( "RN", "Резисторы" ),
            new DesignatorDevice( "RP", "Потенциометры" ),
            new DesignatorDevice( "RR","Реостаты"),
            new DesignatorDevice( "RS", "Шунты измерительные" ),
            new DesignatorDevice( "RU", "Варисторы" ),
            new DesignatorDevice( "S", "Переключатели" ),
            new DesignatorDevice( "SA", "Переключатели" ),
            new DesignatorDevice( "SB", "Выключатели кнопочные" ),
            new DesignatorDevice( "SF", "Выключатели кнопочные" ),
            new DesignatorDevice( "SL", "Выключатели, срабатыающие от уровня" ),
            new DesignatorDevice( "SP", "Выключатели, срабатыающие от давления" ),
            new DesignatorDevice( "SQ", "Выключатели, срабатыающие от положения" ),
            new DesignatorDevice( "SR", "Выключатели, срабатыающие от от частоты вращения" ),
            new DesignatorDevice( "SK", "Выключатели, срабатыающие от температуры" ),
            new DesignatorDevice( "T", "Трансформаторы" ),
            new DesignatorDevice( "TA", "Трансформаторы тока" ),
            new DesignatorDevice( "TS", "Электромагнитные стабилизаторы" ),
            new DesignatorDevice( "TV", "Трансформаторы напряжения" ),
            new DesignatorDevice( "U", "Модуляторы" ),
            new DesignatorDevice( "UB", "Модуляторы" ),
            new DesignatorDevice( "UF","Преобразователи частоты"),
            new DesignatorDevice( "UG","Блоки питания"),
            new DesignatorDevice( "UR", "Демодуляторы" ),
            new DesignatorDevice( "UI", "Дискриминаторы" ),
            new DesignatorDevice( "UZ", "Выпрямители, генераторы частоты, инверторы, преобразователи частотные" ),
            new DesignatorDevice( "V", "Приборы электровакуумные и полупроводниковые" ),
            new DesignatorDevice( "VD", "Диоды" ),
            new DesignatorDevice( "VL", "Приборы электровакуумные" ),
            new DesignatorDevice( "VS", "Тиристоры" ),
            new DesignatorDevice( "VT", "Транзисторы" ),
            new DesignatorDevice( "W", "Линии и элементы СВЧ, Антенны" ),
            new DesignatorDevice( "WA", "Антенны" ),
            new DesignatorDevice( "WE", "Осветители" ),
            new DesignatorDevice( "WK", "Короткозамыкатели" ),
            new DesignatorDevice( "WS", "Вентили" ),
            new DesignatorDevice( "WT", "Трансформаторы, неоднородности, фазовращатели" ),
            new DesignatorDevice( "WU", "Аттенюаторы" ),
            new DesignatorDevice( "X", "Соединения контактные" ),
            new DesignatorDevice( "XA", "Контакты скользящие, токосъемники" ),
            new DesignatorDevice( "XP", "Вилки" ),
            new DesignatorDevice( "XS", "Розетки" ),
            new DesignatorDevice( "XT", "Соединения разборные" ),
            new DesignatorDevice( "XW", "Соединители выскочастотные" ),
            new DesignatorDevice( "Y", "Устройства механические с электромагнитным приводом" ),
            new DesignatorDevice( "YA", "Электромагниты" ),
            new DesignatorDevice( "YB", "Тормоза с электромагнитным приводом" ),
            new DesignatorDevice( "YC", "Муфты с электромагнитным приводом" ),
            new DesignatorDevice( "YH", "Электромагнитные патроны" ),
            new DesignatorDevice( "Z", "Фильтры, ограничители" ),
            new DesignatorDevice( "ZC", "Фильтры" ),
            new DesignatorDevice( "ZL", "Фильтры" ),
            new DesignatorDevice( "ZQ", "Фильтры" ),
            new DesignatorDevice( "", " " )};

    public string[] Designators
    { get { return this.designators(); } }

    public string FilePath
    { get; private set; }

    private string[] designators()
    {
        List<string> design = new List<string>();
        this.devices.ForEach(x => design.Add(x.Name));
        return design.ToArray();
    }

    public void Sort()
    {
        //Сортировка и удаление итемов будет производиться по одним и тем же условиям
        int[] comparer_des = new int[] { 0 };

        //Удалить дубликаты по дезигнаторам
        this.devices = this.devices.Distinct().ToList();
        //Просортировать по дезигнатору
        this.devices.Sort();

        //Пустой дезигнатор - частный случай, его нужно перенести в конец
        DesignatorDevice empty_device = this.devices[0];
        //Удаляю его из основной коллекции
        this.devices.RemoveAt(0);
        //Добавляю заново
        this.devices.Add(empty_device);
    }

    public void AddElement(string designator, string device)
    {
        this.devices.Add(new DesignatorDevice(designator, device));
    }

    public string GetDeviceByDesinator(string designator)
    {
        //Коллекция временных устройств
        List<DesignatorDevice> temp_device = this.devices.Where(x => x.Name.Equals(designator)).ToList();

        //Если соответствие девайса и дезигнатора найдено, то вернуть девайс
        if (temp_device.Count > 0)
            return temp_device[0].Value;

        //В случае, если дезигнатор не найден, но нужно будет вернуть что-то, это будет сам дезигнатор
        return designator;
    }

    private void WriteFile()
    {
        //Предварительная сортировка справочиника
        this.devices.Sort();
        this.devices.RemoveAt(0);

        //Запись XML в файл
        XElement xElem = new XElement("Device", this.devices.Select(x => new XElement(x.Name, x.Value)));
        xElem.Save(this.FilePath);
    }

    private void ReadFile()
    {
        this.devices.Clear();

        //Извлечение из файла всех нод с соответствующими значениями
        XElement xElem = XElement.Load(new FileStream(this.FilePath, FileMode.Open, FileAccess.Read));
        xElem.Elements().ToList().ForEach(x => this.devices.Add(new DesignatorDevice(x.Name.LocalName, x.Value)));
        this.devices.Distinct();
        this.devices.Sort();
        this.devices.Add(new DesignatorDevice("", " "));
    }

    private void Initialization()
    {
        this.FilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\" + @"DesignatorDevice.xml";
    }

    public DeviceCatalog()
    {
        this.Initialization();
        //Десериализация объекта из ресурсов
        if (!File.Exists(this.FilePath))
            this.WriteFile();
        this.ReadFile();
        //
        //this.ReadFile();
        //Сериализация
        /*XElement xElem= new XElement("DesignatorDevice", this.devices.Select(x => new XElement(string.IsNullOrEmpty(x.Name)?" ":x.Name, x.Value)));
        xElem.Save(@"DesignatorDevice.xml");*/
    }
}
