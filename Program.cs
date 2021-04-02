
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public interface IComparer
{
    int Compare(object o1, object o2);
}
class CompareMe : IComparer < Document >
    {
    public int Compare(Document p1, Document p2)
    {
        if (p1.Pages > p2.Pages) return 1;
        else if (p1.Pages < p2.Pages) return -1;
        else return 0;
    }
}

class CompareMyName : IComparer<Document>
{
    public int Compare(Document p1, Document p2)
    {
        if (p1.Name[0] > p2.Name[0]) return 1;
        else if (p1.Name[0] < p2.Name[0]) return -1;
        else return 0;
    }
}
//class Comparer:IComparable<Document>
// { 
//    public int CompareTo(Document p1, Document p2)
//    {
//        if (p1.Pages > p2.Pages) return 1;
//        else if (p1.Pages < p2.Pages) return -1;
//        else return 0;
//    }

//    public int CompareTo([AllowNull] Document other)
//    {
//        throw new NotImplementedException();
//    }
//}
abstract class Document//потому что только учет НПА и литературы
{
    public string name;
    private string date;
    private int pages;
    public string author;
    public string kind;
    public string Name
    {
        get => name;
        set => name=value;
    }
    public string Date
    {
        get => date;
        set => date=value;
    }
    public int Pages
    {
        get => pages;
        set => pages = value;
    }

    public virtual string Author 
    {
        get => author;
        set => author = value;
    }




    sealed public class Magazine : Document
    {
        public int Num { get; set; }
        public override string Author
        {
            get => base.author;
            set
            {
               
             base.author = "Unknown";
                
            }
        }
        public int GetNum()
        {
            return Num;
        }
    }

    sealed public class Book : Document
    {

        private string genre;
        public string Genre
        {
            get => genre;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    genre = value;
                }
                else
                {
                    genre = "Unknown";
                }
            }
        }
    }

    public class Act : Document
    {
        public string Type { get; set; } // Конституция;федеральные законы конституционные;федеральные законы;законы субъектов федерации.
        

        public class Law : Act
        {
            public string DurationFrom { get; set; } //срок действия
            public string DurationTo { get; set; }
        }

      public  class International : Act
        {
            //договор, соглашение, протокол
           
            public string Country { get; set; }


        }
    }

  
}






namespace Lab3
{
    class Program
    {
        static bool IsDay(int d)
        {
            return d >0 && d<32;
        }
        static bool IsMonth(int m)
        {
            return m > 0 && m < 13;
        }
        static bool IsYear(int y)
        {
            return y > 0 && y < 2021;
        }
        static bool IsPages(int p)
        {
            int n;
            return p > 0 && int.TryParse(p.ToString(), out n) ;
        }
        static bool IsName(string str)
        {
            str = str.ToLower();
            string letters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбёю";
            bool t=false;int i = 0;
            while(t==false && i < letters.Length)
            {
                t = str.Contains(letters[i]);
                i++;
            }
            return t;
        }
        static class DocHelp
        {
            public static string MakeDate(int day, int month, int year)
            {
                string date = day.ToString() + '/' + month.ToString() + '/' + year.ToString();
                return date;
            }
        }
      
        static void Main(string[] args)
        {
            List <Document> docs = new List<Document>();
            var doc1 = new Document.Act.Law { kind = "Закон РФ", Author = "Путин", Date = "12/05/2006", DurationFrom = "12/05/2006", DurationTo = "31/06/2006", Name = "О бюджетной классификации РФ", Pages = 20, Type = "Федеральные закон" };
            var doc2 = new Document.Act.Law { kind = "Закон РФ", Author = "Путин", Date = "12/12/1993", DurationFrom = "12/12/1993", DurationTo = "31/06/2020", Name = "Конституция", Pages = 83, Type = "Конституция" };
            var doc3 = new Document.Act.International { kind = "Международный закон", Author = "Правительство РФ", Date = "12/01/2005", Name = "О заключении межд-ных договоров", Pages = 53, Type = "Соглашение" };
            var doc4 = new Document.Book { kind = "Книга", Author = "Линдгрен", Date = "01/01/1986",  Name = "Малыш и Карлсон", Pages = 326, Genre = "Повесть" };
            var doc5 = new Document.Book { kind = "Книга", Author = "Линдгрен", Date = "01/01/1981", Name = "Рони, дочь разбойника", Pages = 546, Genre = "Повесть" };
            var doc6 = new Document.Magazine { kind = "Журнал", Author = "", Date = "10/01/2005", Name = "GEOлёнок", Pages = 45, Num = 2};
            var doc7 = new Document.Magazine { kind = "Журнал", Author = "", Date = "10/02/2005", Name = "GEOлёнок", Pages = 45, Num = 3 };
            var doc8 = new Document.Magazine { kind = "Журнал", Author = "", Date = "10/03/2005", Name = "GEOлёнок", Pages = 45, Num = 4 };
            var doc9 = new Document.Magazine { kind = "Журнал", Author = "", Date = "10/04/2005", Name = "GEOлёнок", Pages = 45, Num = 5 };
            docs.Add(doc1); docs.Add(doc2); docs.Add(doc3); docs.Add(doc4); docs.Add(doc5); docs.Add(doc6); docs.Add(doc7); docs.Add(doc8); docs.Add(doc9);
            bool fl1 = true; bool fl2 = true; bool fl3 = true; bool fl4;
            string str = ""; int n; bool f = true;
            string day = ""; string month = ""; string year = "";
            string sort_info = "Выберите поле, по которому будет вестись сортировка:\n1-Имя документа\n2-Год издания\n3-Автор\n4-Количество страниц\n5-Тип документа(книга/журнал/закон РФ/международный закон)\n0-Вернуться назад";
            string repeat_str = "Некорректное значение! Повторите ввод";
            string hello = "С помощью этого приложения можно вести учет НПА и литературы.";
            string agregat = "1-Максимальные и минимальные значения\n2-Суммарные значения\n3-Средние значения\n0-Вернуться назад";
            string find_me = "1-Поиск по имени\n2-Поиск по автору\n0-Вернуться назад";
            string sort_for_each= "Выберите тип документа, по которому будет производиться расчет\n1-Книга\n2-Закон РФ\n3-Международный закон\n4-Журнал\n0-Вернуться назад";
            string info1 = "Выберите функцию:\n1-Добавить новый объект\n2-Удалить объект\n3-Подсчет количества объектов\n4-Поиск и сортировка\n5-Агрегированные характеристики\n0-Выход из программы";
            string search_info = "1-Сортировка по полю\n2-Поиск по условию\n3-Поиск по полю\n0-Вернуться назад";
            string searsh_if = "1-Найти все документы, изданные раньше указанного года\n2-Найти все документы, изданные позже указанного года\n3-Найти все документы, с меньшим количеством страниц, чем указано";
            string info_object = "\nВы можете добавить документ к общей базе. \n\nИерархия базы выглядит так:\n1.НПА\n   1.1. Законы\n   1.2. Международные законы\n2. Книги\n3. Журналы\nШаг 1: \nВыберите уровень:\n1 - НПА\n2 - Книги\n3 - Журналы\n0-Вернуться назад ";
            Console.WriteLine(hello);
            while (fl1)
            {
                Console.WriteLine("".PadRight(30, '_'));
                Console.WriteLine(info1);
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': 
                        fl2 = true;
                        while (fl2)
                        {
                            Console.WriteLine("".PadRight(30, '_'));
                            Console.WriteLine(info_object);
                            fl3 = true;
                            switch (char.ToLower(Console.ReadKey(true).KeyChar))
                            {
                                
                                case '1': //НПА
                                    Console.WriteLine("Шаг 2:\nВыберите:\n1 - Закон\n2 - Международный закон\n0 - Назад");
                                    while (fl3) {
                                        switch (char.ToLower(Console.ReadKey(true).KeyChar))
                                        {
                                            case '1':
                                                var doc = new Document.Act.Law();
                                                doc.kind = "Закон РФ";
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите имя документа:");
                                                    if ((str = Console.ReadLine()) != null) { doc.Name = str; fl4 = false; }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");

                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите тип закона:\n1-Конституция\n2-Федеральный конституционный закон\n3-Федеральные закон\n4-Закон субъекта федерации");
                                                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                                                    {
                                                        case '1': doc.Type = "Конституция"; fl4 = false;  break;
                                                        case '2': doc.Type = "Федеральный конституционный закон"; fl4 = false; break;
                                                        case '3': doc.Type = "Федеральные закон"; fl4 = false; break;
                                                        case '4': doc.Type = "Закон субъекта федерации"; fl4 = false; break;
                                                        default: Console.WriteLine("Некорректный ввод! Повторите попытку"); break;
                                                    }
                                                }
                                                fl4 = true; 
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите дату издания \nДень XX:");
                                                    day = Console.ReadLine();
                                                    Console.WriteLine("Месяц (число от 1 до 12):");
                                                    month = Console.ReadLine();
                                                    Console.WriteLine("Год XXXX :");
                                                    year = Console.ReadLine(); 
                                                    if (day!= null&& int.TryParse(day, out n)&& month != null&&
                                                        int.TryParse(month, out n) && year != null&& (day[0] == '0' || (day[0] != '0' && day.Length > 1)) &&
                                                        (month[0] == '0' || (month[0] != '0' && month.Length > 1)) &&
                                                        int.TryParse(year, out n) && IsDay(Int32.Parse(day))&&
                                                        IsMonth(Int32.Parse(month))&&
                                                        IsYear(Int32.Parse(year)))
                                                    { doc.Date = DocHelp.MakeDate(Int32.Parse(day), Int32.Parse(month), Int32.Parse(year)); fl4 = false; }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите количество страниц в документе:");
                                                    if ((str = Console.ReadLine()) != null&&IsPages(Int32.Parse(str))) 
                                                    { doc.Pages = Int32.Parse(str); fl4 = false; }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите фамилию автора документа:");
                                                    if ((str = Console.ReadLine()) != null && IsName(str))
                                                    { doc.Author = str.ToUpper(); fl4 = false;}
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
           
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите срок действия закона \nНачало действия:");
                                                    Console.WriteLine("День XX:");
                                                    day = Console.ReadLine();
                                                    Console.WriteLine("Месяц (число от 1 до 12):");
                                                    month = Console.ReadLine();
                                                    Console.WriteLine("Год XXXX :");
                                                    year = Console.ReadLine();
                                                    if (day != null && int.TryParse(day, out n) && month != null &&
                                                           int.TryParse(month, out n) && year != null &&
                                                           (day[0] == '0' || (day[0] != '0' && day.Length > 1)) &&
                                                           (month[0] == '0' || (month[0] != '0' && month.Length > 1)) &&
                                                           int.TryParse(year, out n) && IsDay(Int32.Parse(day)) &&
                                                           IsMonth(Int32.Parse(month)) &&
                                                           IsYear(Int32.Parse(year)))
                                                    {
                                                        doc.DurationFrom = DocHelp.MakeDate(Int32.Parse(day), Int32.Parse(month), Int32.Parse(year)); fl4 = false;
                                                    }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите конец действия:");
                                                    Console.WriteLine("День XX:");
                                                    day = Console.ReadLine();
                                                    Console.WriteLine("Месяц (число от 1 до 12):");
                                                    month = Console.ReadLine();
                                                    Console.WriteLine("Год XXXX :");                                                   
                                                    year = Console.ReadLine(); 
                                                   
                                                    if (day != null && int.TryParse(day, out n) && month != null &&
                                                                                                  int.TryParse(month, out n) && year != null && (day[0] == '0' || (day[0] != '0' && day.Length > 1)) && (month[0] == '0' || (month[0] != '0' && month.Length > 1)) &&
                                                                                                  int.TryParse(year, out n) && IsDay(Int32.Parse(day)) &&
                                                                                                  IsMonth(Int32.Parse(month)) &&
                                                                                                  IsYear(Int32.Parse(year)))
                                                    {
                                                        doc.DurationTo = DocHelp.MakeDate(Int32.Parse(day), Int32.Parse(month), Int32.Parse(year)); fl4 = false;
                                                    }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                docs.Add(doc);
                                                fl3 = false;
                                                break;
                                            case '2':
                                                var _doc = new Document.Act.International();
                                                _doc.kind = "Международный закон";
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите имя документа:");
                                                    if ((str = Console.ReadLine()) != null) { _doc.Name = str; fl4 = false; }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");

                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите тип закона:\n1-Договор\n2-Соглашение\n3-Протокол");
                                                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                                                    {
                                                        case '1': _doc.Type = "Договор"; fl4 = false; break;
                                                        case '2': _doc.Type = "Соглашение"; fl4 = false; break;
                                                        case '3': _doc.Type = "Протокол"; fl4 = false; break;
                                                        default: Console.WriteLine("Некорректный ввод! Повторите попытку"); break;
                                                    }
                                                }
                                                fl4 = true; 
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите дату издания \nДень XX:");
                                                    day = Console.ReadLine();
                                                    Console.WriteLine("Месяц (число от 1 до 12):");
                                                    month = Console.ReadLine();
                                                    Console.WriteLine("Год XXXX :");
                                                    year = Console.ReadLine();
                                                    if (day != null && int.TryParse(day, out n) && month != null &&
                                                        int.TryParse(month, out n) && year != null && (day[0] == '0' || (day[0] != '0' && day.Length > 1)) &&
                                                        (month[0] == '0' || (month[0] != '0' && month.Length > 1)) &&
                                                        int.TryParse(year, out n) && IsDay(Int32.Parse(day)) &&
                                                        IsMonth(Int32.Parse(month)) &&
                                                        IsYear(Int32.Parse(year)))
                                                    { _doc.Date = DocHelp.MakeDate(Int32.Parse(day), Int32.Parse(month), Int32.Parse(year)); fl4 = false; }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите количество страниц в документе:");
                                                    if ((str = Console.ReadLine()) != null && IsPages(Int32.Parse(str)))
                                                    { _doc.Pages = Int32.Parse(str); fl4 = false; }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите страну-участника:");
                                                    if ((str = Console.ReadLine()) != null) { _doc.Country = str; fl4 = false;}
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                fl4 = true;
                                                while (fl4)
                                                {
                                                    Console.WriteLine("Введите фамилию автора документа:");
                                                    if ((str = Console.ReadLine()) != null && IsName(str))
                                                    { _doc.Author = str.ToUpper(); fl4 = false; }
                                                    else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                                }
                                                docs.Add(_doc);
                                                fl3 = false;
                                                break;
                                            case '0': fl3 = false; break;
                                            default: Console.WriteLine(repeat_str); break;
                                        }
                                        
                                    }
                                    
                                    break;
                                case '2': //Книги
                                    var doc_ = new Document.Book();
                                    doc_.kind = "Книга";
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите название книги:");
                                        if ((str = Console.ReadLine()) != null) { doc_.Name = str; fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");

                                    }                    
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите дату издания \nДень XX:");
                                        day = Console.ReadLine();
                                        Console.WriteLine("Месяц (число от 1 до 12):");
                                        month = Console.ReadLine();
                                        Console.WriteLine("Год XXXX :");
                                        year = Console.ReadLine();
                                        if (day != null && int.TryParse(day, out n) && month != null &&
                                            int.TryParse(month, out n) && year != null && (day[0] == '0' || (day[0] != '0' && day.Length > 1)) &&
                                            (month[0] == '0' || (month[0] != '0' && month.Length > 1)) &&
                                            int.TryParse(year, out n) && IsDay(Int32.Parse(day)) &&
                                            IsMonth(Int32.Parse(month)) &&
                                            IsYear(Int32.Parse(year)))
                                        { doc_.Date = DocHelp.MakeDate(Int32.Parse(day), Int32.Parse(month), Int32.Parse(year)); fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                    }
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите количество страниц в документе:");
                                        if ((str = Console.ReadLine()) != null && IsPages(Int32.Parse(str)))
                                        { doc_.Pages = Int32.Parse(str); fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                    }
                               
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите фамилию автора:");
                                        if ((str = Console.ReadLine()) != null && IsName(str))
                                        { doc_.Author = str.ToUpper(); fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                    }
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите жанр произведения (если его нет, оставьте строку пустой):");
                                        if (IsName(str))
                                        { doc_.Genre = str.ToUpper(); fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                    }
                                
                                    docs.Add(doc_);
                                    fl3 = false;
                                    break; 
                                case '3': //Журналы
                                    var doc_j = new Document.Magazine();
                                    doc_j.kind = "Журнал";
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите название журнала:");
                                        if ((str = Console.ReadLine()) != null) { doc_j.Name = str; fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");

                                    }
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите номер выпуска:");
                                        if ((str = Console.ReadLine()) != null && int.TryParse(str, out n) && IsPages(int.Parse(str)))
                                        { doc_j.Author = str.ToUpper(); doc_j.Num = int.Parse(str); fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                    }
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите дату издания \nДень XX:");
                                        day = Console.ReadLine();
                                        Console.WriteLine("Месяц (число от 1 до 12):");
                                        month = Console.ReadLine();
                                        Console.WriteLine("Год XXXX :");
                                        year = Console.ReadLine();
                                        if (day != null && int.TryParse(day, out n) && month != null &&
                                            int.TryParse(month, out n) && year != null && (day[0] == '0' || (day[0] != '0' && day.Length > 1)) &&
                                            (month[0] == '0' || (month[0] != '0' && month.Length > 1)) &&
                                            int.TryParse(year, out n) && IsDay(Int32.Parse(day)) &&
                                            IsMonth(Int32.Parse(month)) &&
                                            IsYear(Int32.Parse(year)))
                                        { doc_j.Date = DocHelp.MakeDate(Int32.Parse(day), Int32.Parse(month), Int32.Parse(year)); fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                    }
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("Введите количество страниц в документе:");
                                        if ((str = Console.ReadLine()) != null && IsPages(Int32.Parse(str)))
                                        { doc_j.Pages = Int32.Parse(str); fl4 = false; }
                                        else Console.WriteLine("Некорректный ввод! Повторите попытку");
                                    }

                                                            
                                    docs.Add(doc_j);
                                    fl3 = false;
                                    break;

                                case '0': fl2 = false; break;
                                default: Console.WriteLine(repeat_str); break;

                            }
                        }
                        break;
                    case '2':
                        int i = 0; int n1=1; int n2;
                        Console.WriteLine("".PadRight(30, '_'));
                        Console.WriteLine("Данные в базе:");
                        Console.WriteLine("\nЖурналы:");
                        n2 = n1;
                        var doc_help = new Document.Magazine(); 
                        for (i = 0; i < docs.Count; i++)
                        {
                            if (docs[i].kind == "Журнал")
                            {
                                doc_help = (Document.Magazine)docs[i];
                                Console.WriteLine(n1.ToString() + '.' + ' ' + docs[i].Name.ToUpper().PadRight(25, ' ') + " №" + doc_help.Num);
                                n1++;
                            }
                        }
                       
                        if (n2==n1) Console.WriteLine("Нет позиций"); n2 = n1;
                        Console.WriteLine("\nЗаконы РФ:");
                        for (i = 0; i < docs.Count; i++)
                        {
                            if (docs[i].kind == "Закон РФ")
                            {
                                Console.WriteLine(n1.ToString() + '.' + ' ' + docs[i].Name.ToUpper().PadRight(25, ' ') + " Принял: " + docs[i].Author.ToUpper());
                                n1++;
                            }
                        }
                        if (n2 == n1) Console.WriteLine("Нет позиций"); n2 = n1;
                        Console.WriteLine("\nКниги:");
                      
                        for (i = 0; i < docs.Count; i++)
                        {
                            if (docs[i].kind == "Книга")
                            {
                                Console.WriteLine(n1.ToString() + '.' + ' ' + docs[i].Name.ToUpper().PadRight(25, ' ') + " Автор: " + docs[i].Author.ToUpper());
                                n1++;
                            }
                        }
                        
                        if (n2 == n1) Console.WriteLine("Нет позиций"); n2 = n1;
                        Console.WriteLine("\nМеждународные законы:");
                        for (i = 0; i < docs.Count; i++)
                        {
                            if (docs[i].kind == "Международный закон")
                            {
                                Console.WriteLine(n1.ToString() + '.' + ' ' + docs[i].Name.ToUpper().PadRight(25, ' ') + " Принял: " + docs[i].Author.ToUpper());
                                n1++;
                            }
                        }
                        if (n2 == n1) Console.WriteLine("Нет позиций"); n2 = n1;

                        
                        Console.WriteLine("\nВведите номер удаляемой позиции:");
                        fl3 = true; char c; int j1 = 0; int j2 = 0; int j3 = 0; int j4 = 0; int k = 0; int delete_i = 0;
                        while (fl3)
                        {
                           c = char.ToLower(Console.ReadKey(true).KeyChar);
                            Console.WriteLine(c);
                           n2 = int.Parse(c.ToString());
                            if (n2>0&&n2<n1)
                            {

                                for (int j=0; j < n1-1; j++)
                                {
                                    if (docs[j].kind == "Журнал") j1++;
                                    if (docs[j].kind == "Закон РФ") j2++;
                                    if (docs[j].kind == "Книга") j3++;
                                    if (docs[j].kind == "Международный закон") j4++;
                                }

                                if (n2 < j1 + 1) //журнал
                                {
                                    k = -1;                                                                      
                                        for (i = 0; i < docs.Count; i++)
                                        {
                                            if (docs[i].kind == "Журнал") k++;
                                            if (k == n2 -1) { delete_i = i; break; }
                                        }
                                        Console.WriteLine("\nЭлемент  " + docs[delete_i].kind + "  с названием '" + docs[delete_i].Name + "' был успешно удален.");
                                        docs.Remove(docs[delete_i]); fl3 = false;
                                }
                               else
                                {
                                    k = -1;
                                    if (n2>j1&&n2<j1+j2+1) //Закон РФ
                                    {
                                        for (i=0; i < docs.Count; i++)
                                        {
                                            if (docs[i].kind == "Закон РФ") k++;
                                            if (k == j1 + j2 - n2+1) { delete_i = i; break; }
                                        }
                                        Console.WriteLine("\nЭлемент  " + docs[delete_i].kind + "  с названием '" + docs[delete_i].Name + "' был успешно удален.");
                                        docs.Remove(docs[delete_i]); fl3 = false;
                                    }
                                    if(n2> j1 + j2 && n2< j1 + j2 + j3+1) //Книга
                                    {
                                        for (i = 0; i < docs.Count; i++)
                                        {
                                            if (docs[i].kind == "Книга") k++;
                                            if (k == j1 + j2 +j3 - n2 + 1) { delete_i = i; break; }
                                        }
                                        Console.WriteLine("\nЭлемент  " + docs[delete_i].kind + "  с названием '" + docs[delete_i].Name + "' был успешно удален.");
                                        docs.Remove(docs[delete_i]); fl3 = false;
                                    }
                                    if (n2 > j1 + j2 + j3) // международный закон
                                    {
                                        for (i = 0; i < docs.Count; i++)
                                        {
                                            if (docs[i].kind == "Международный закон") k++;
                                            if (k == j1 + j2 + j3 - n2 + 1) { delete_i = i; break; }
                                        }
                                        Console.WriteLine("\nЭлемент  " + docs[delete_i].kind + "  с названием '" + docs[delete_i].Name + "' был успешно удален.");
                                        docs.Remove(docs[delete_i]); fl3 = false;
                                    }

                                }
                               
                            }
                            else Console.WriteLine("Некорректный ввод! Повторите попытку");
                        }
                        break;
                    case '3':
                        Console.WriteLine("\nПодсчет количества объектов каждого типа");
                        Console.WriteLine("".PadRight(30, '_'));
                        j1 = 0; j2 = 0; j3 = 0; j4 = 0;
                        for (int j = 0; j < docs.Count; j++)
                        {
                            if (docs[j].kind == "Журнал") j1++;
                            if (docs[j].kind == "Закон РФ") j2++;
                            if (docs[j].kind == "Книга") j3++;
                            if (docs[j].kind == "Международный закон") j4++;
                        }
                        Console.WriteLine("\nВсего документов:".PadRight(25, ' ') + docs.Count);
                        Console.WriteLine("\nЖурналы: ".PadRight(25, ' ') + j1);
                        Console.WriteLine("\rКниги: ".PadRight(25, ' ') + j3);
                        Console.WriteLine("\nВсего законов: ".PadRight(25, ' ') + (j2 +j4));
                        Console.WriteLine("из них");
                        Console.WriteLine("\rЗаконы РФ: ".PadRight(25, ' ') + j2);            
                        Console.WriteLine("\rМеждународные законы: ".PadRight(25, ' ') + j4 + "\n");
                        break;
                    case '4':
                       
                        fl3 = true; 
                        while (fl3)
                        {
                            Console.WriteLine("".PadRight(30, '_'));
                            Console.WriteLine(search_info);
                            switch (char.ToLower(Console.ReadKey(true).KeyChar)) 
                            {
                                case '1':  //1-Сортировка по полю
                                    
                                    fl4 = true;
                                    while (fl4)
                                    {
                                        Console.WriteLine("".PadRight(30, '_'));
                                        Console.WriteLine(sort_info); 
                                        k = 1;
                                        switch (char.ToLower(Console.ReadKey(true).KeyChar))
                                        {
                                            case '1': //Имя
                                                Console.WriteLine("".PadRight(30, '_'));
                                                /*var sortedDocs1 = from u in docs
                                                                 orderby u.Name
                                                                 select u;*/
                                                Document[] docar1 = docs.ToArray();
                                                Array.Sort(docar1, new CompareMyName());
                                                foreach (Document u in docar1)
                                                    Console.WriteLine((k++) + ".  " + u.Name.PadRight(35, ' ') +   u.Date.PadRight(12, ' ') + u.Author.PadRight(20, ' ') + u.Pages.ToString().PadRight(7, ' ') + u.kind);
                                                fl4 = false;
                                                break;
                                            case '2': //Дата
                                                Console.WriteLine("".PadRight(30, '_'));
                                                var sortedDocs2 = from u in docs
                                                                 orderby u.Date.Substring(6)
                                                                  select u;
                                                foreach (Document u in sortedDocs2)
                                                    Console.WriteLine((k++) + ".  " + u.Name.PadRight(35, ' ') + u.Date.PadRight(12, ' ') + u.Author.PadRight(20, ' ') + u.Pages.ToString().PadRight(7, ' ') + u.kind);
                                                fl4 = false;
                                                break;
                                            case '3': //Автор
                                                Console.WriteLine("".PadRight(30, '_'));
                                                var sortedDocs3 = from u in docs
                                                                  orderby u.Author
                                                                  select u;
                                                foreach (Document u in sortedDocs3)
                                                    Console.WriteLine((k++) + ".  " + u.Name.PadRight(35, ' ') + u.Date.PadRight(12, ' ') + u.Author.PadRight(20, ' ') + u.Pages.ToString().PadRight(7, ' ') + u.kind);
                                                fl4 = false;
                                                break;
                                            case '4': //Кол-во стр
                                                Console.WriteLine("".PadRight(30, '_'));
                                                /* var sortedDocs4 = from u in docs
                                                                   orderby u.Pages
                                                                   select u;
                                                 foreach (Document u in sortedDocs4)
                                                 Console.WriteLine((k++) + ".  " + u.Name.PadRight(35, ' ') + u.Date.PadRight(12, ' ') + u.Author.PadRight(20, ' ') + u.Pages.ToString().PadRight(7, ' ') + u.kind);*/
                                                Document[] docar = docs.ToArray();
                                                Array.Sort(docar, new CompareMe());
                                                foreach (Document u in docar)
                                                Console.WriteLine((k++) + ".  " + u.Name.PadRight(35, ' ') + u.Date.PadRight(12, ' ') + u.Author.PadRight(20, ' ') + u.Pages.ToString().PadRight(7, ' ') + u.kind); 
                                                fl4 = false;
                                                break;
                                            case '5': //Тип дока
                                                Console.WriteLine("".PadRight(30, '_'));
                                                var sortedDocs5 = from u in docs
                                                                  orderby u.kind
                                                                   select u;
                                                    foreach (Document u in sortedDocs5)
                                                    Console.WriteLine((k++) + ".  " + u.Name.PadRight(35, ' ') + u.Date.PadRight(12, ' ') + u.Author.PadRight(20, ' ') + u.Pages.ToString().PadRight(7, ' ') + u.kind);
                                                fl4 = false;
                                                break;
                                            case '0':fl4 = false; break;
                                            default: Console.WriteLine(repeat_str); break; 
                                        }
                                    }

                                    break;
                                case '2':  //2-Поиск по условию
                                    Console.WriteLine("".PadRight(30, '_'));
                                    Console.WriteLine(searsh_if);
                                    k = 1;
                                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                                    {
                                        case '1': //Раньше введенного года
                                            Console.WriteLine("".PadRight(30, '_'));
                                            Console.WriteLine("Введите год:");
                                            if ((str = Console.ReadLine()) != null && int.TryParse(str, out n)) {
                                                var docFind = from u in docs where int.Parse(u.Date.Substring(6)) < int.Parse(str) select u;
                                                foreach (Document u in docFind) Console.WriteLine(u.kind.PadRight(25, ' ') + u.Name.PadRight(40, ' ') + u.Date);
                                            }
                                            else Console.WriteLine(repeat_str);
                                            break;
                                        case '2': //Позже введенного года
                                            Console.WriteLine("".PadRight(30, '_'));
                                            Console.WriteLine("Введите год:");
                                            if ((str = Console.ReadLine()) != null && int.TryParse(str, out n))
                                            {
                                                var docFind1 = from u in docs where int.Parse(u.Date.Substring(6)) > int.Parse(str) select u;
                                                foreach (Document u in docFind1) Console.WriteLine(u.kind.PadRight(25, ' ') + u.Name.PadRight(40, ' ') + u.Date);
                                            }
                                            else Console.WriteLine(repeat_str);

                                            break;
                                        case '3': //С меньшим введенного количеством страниц
                                            Console.WriteLine("".PadRight(30, '_'));
                                            Console.WriteLine("Введите количество страниц:");
                                            if ((str = Console.ReadLine()) != null && int.TryParse(str, out n))
                                            {
                                                var docFind = from u in docs where u.Pages < int.Parse(str) select u;
                                                foreach (Document u in docFind) Console.WriteLine(u.kind.PadRight(25, ' ') + u.Name.PadRight(40, ' ') + u.Pages);
                                            }
                                            else Console.WriteLine(repeat_str);
                                            break;
                                        case '0': fl4 = false; break;
                                        default: Console.WriteLine(repeat_str); break;
                                    }                          
                            break;
                                case '3':  //3-Поиск по полю
                                    Console.WriteLine("".PadRight(30, '_'));
                                    Console.WriteLine(find_me);
                                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                                    {
                                        case '1':
                                            Console.WriteLine("Введите имя документа:");
                                            if ((str = Console.ReadLine()) != null)
                                            {
                                                var docFind = from u in docs where u.name == str select u;
                                                if (docFind.Count() != 0)
                                                { foreach (Document u in docFind) Console.WriteLine(u.kind.PadRight(25, ' ') + u.Name.PadRight(40, ' ') + u.Author); }
                                                else
                                                {
                                                    Console.WriteLine("Нет записей");
                                                }
                                            }
                                            else Console.WriteLine(repeat_str);
                                            break;
                                        case '2':
                                            Console.WriteLine("Введите автора:");
                                            if ((str = Console.ReadLine()) != null)
                                            {
                                                var docFind = from u in docs where u.author == str select u;
                                                if (docFind.Count() != 0)
                                                { foreach (Document u in docFind) Console.WriteLine(u.kind.PadRight(25, ' ') + u.Name.PadRight(40, ' ') + u.Author); }
                                                else Console.WriteLine("Нет записей");
                                            }
                                            else Console.WriteLine(repeat_str);

                                            break;
                                        case '0':

                                            break;
                                        default: Console.WriteLine(repeat_str); break;
                                    }

                                    break;
                           
                                case '0': fl3 = false; break;
                                default: Console.WriteLine(repeat_str); break;
                            }
                        }
                        break;
                    case '5':
                        fl3 = true;
                        var years = new List<int>(docs.Count);
                        for (int j = 0; j < docs.Count; j++)
                        {
                            years.Add(int.Parse(docs[j].Date.Substring(6)));
                        }
                        var pages = new List<int>(docs.Count);
                        for (int j = 0; j < docs.Count; j++)
                        {
                            pages.Add(docs[j].Pages);
                        }                  
                            j1 = 0; j2 = 0; j3 = 0; j4 = 0;
                            for (int j = 0; j < docs.Count; j++)
                            {
                                if (docs[j].kind == "Журнал") j1++;
                                if (docs[j].kind == "Закон РФ") j2++;
                                if (docs[j].kind == "Книга") j3++;
                                if (docs[j].kind == "Международный закон") j4++;
                            }
                        var count_types = new List<int>(4);
                        count_types.Add(j1); count_types.Add(j2); count_types.Add(j3); count_types.Add(j4);
                        f = true;
                        Console.WriteLine("".PadRight(30, '_'));
                        Console.WriteLine(sort_for_each);
                        while (f)
                        {

                            switch (char.ToLower(Console.ReadKey(true).KeyChar))
                            {

                                case '1':
                                    years.Clear();
                                    pages.Clear();
                                    var cy = (from d in docs where d.kind == "Книга" select int.Parse(d.Date.Substring(6)));
                                    years = cy.ToList();
                                    var cp = (from d in docs where d.kind == "Книга" select d.Pages);
                                    pages = cp.ToList();
                                    f = false;
                                    break;
                                case '2':
                                    years.Clear();
                                    pages.Clear();
                                    var cy2 = (from d in docs where d.kind == "Закон РФ" select int.Parse(d.Date.Substring(6)));
                                    years = cy2.ToList();
                                    var cp2 = (from d in docs where d.kind == "Закон РФ" select d.Pages);
                                    pages = cp2.ToList();
                                    f = false;
                                    break;
                                case '3':
                                    years.Clear();
                                    pages.Clear();
                                    var cy3 = (from d in docs where d.kind == "Международный закон" select int.Parse(d.Date.Substring(6)));
                                    years = cy3.ToList();
                                    var cp3 = (from d in docs where d.kind == "Международный закон" select d.Pages);
                                    pages = cp3.ToList();
                                    f = false;
                                    break;
                                case '4':
                                    years.Clear();
                                    pages.Clear();
                                    var cy4 = (from d in docs where d.kind == "Журнал" select int.Parse(d.Date.Substring(6)));
                                    years = cy4.ToList();
                                    var cp4 = (from d in docs where d.kind == "Журнал" select d.Pages);
                                    pages = cp4.ToList();
                                    f = false;
                                    break;
                                case '0':
                                    break;
                                default: Console.WriteLine(repeat_str); break;
                            }
                        }
                            Console.WriteLine("".PadRight(30, '_'));
                            Console.WriteLine(agregat); n = -1;
                        fl3 = true;
                            while (fl3)
                            {
                                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                                {
                                    case '1':  //min max

                                        Console.WriteLine("Минимальный год издания: " + years.Min());
                                        Console.WriteLine("Максимальный год издания: " + years.Max());
                                        Console.WriteLine("Минимальное количество страниц: " + pages.Min());
                                        Console.WriteLine("Максимальное количество страниц: " + pages.Max());

                                        break;
                                    case '2':  //суммарные
                                        Console.WriteLine("Суммарное количество страниц: " + pages.Sum());
                                        break;
                                    case '3':  //средние
                                        Console.WriteLine("Среднее количество документов разных типов (журналы/книги/законы РФ/международные законы): " + count_types.Average());
                                        Console.WriteLine("Средний год издания: " + years.Average());
                                        Console.WriteLine("Среднее количество страниц: " + pages.Average());
                                        break;
                                    case '0':
                                        fl3 = false;
                                        break;
                                    default: Console.WriteLine(repeat_str); break;
                                }
                            }
                        
                        
                        
                        
                        break;
                    case '0': fl1 = false; break;
                    default: Console.WriteLine(repeat_str); break;
                }


            }
        }
    }
}
