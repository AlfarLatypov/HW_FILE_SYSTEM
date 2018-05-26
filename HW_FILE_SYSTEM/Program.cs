using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HW_FILE_SYSTEM   //Тема: Взаимодействие с файловой системой.
{
    class Program
    {
        #region Task_1_Zapis_v_file
        public static void Task_1()
        {
            /*С помощью класса StreamWriter записать в текстовый файл свое имя, фамилию и возраст. Каждая запись должна начинаться с новой строки.*/
            //Запрашиваем название файла у пользователя
            Console.Write("\n\t\tВведите название файла, для его создания - ");

            //-----------------------------------Создаем файл----------------------------------------------------------------------
            //оболочка для создания файла
            FileInfo fi = new FileInfo(Console.ReadLine() + ".txt"); //= new FileInfo("File.txt"); 
            FileStream fs = fi.Create();//Команда создать файл, ФайлСтрим поддерживает синхранизацию чтения и записи с файлом
            fs.Close(); //закрыли поток, сохраняет изменения (коммит) 
            //---------------------------------------------------------------------------------------------------------------------


            //-----------------------------------Информация о файле----------------------------------------------------------------
            //1 Можем проверить путь в какой директории создали и записали
            Console.Write("\n\t\tВы создали файл в следующей директории : {0}", fi.DirectoryName);

            //2 Можем проверить путь в какой директории создали и записали
            //Console.Write("\n\t\tВы создали файл в следующей директории : {0}", fi.Directory);  //одно и тоже, что и команда выше fi.DirectoryName

            // Выводим имя файла
            Console.WriteLine("\n\t\tВы создали файл с именем : {0}", fi.Name);

            Console.Write("\n\t\tФайл находится - ");
            string path = Path.Combine(fi.DirectoryName, fi.Name); //ф-ция Combine объеденяет две строки в путь
            Console.WriteLine(path);
            //---------------------------------------------------------------------------------------------------------------------


            //-----------------------------------Запись в файл---------------------------------------------------------------------

            Console.WriteLine("\n\t\t===================================================================");
            using (StreamWriter sw = new StreamWriter(path)) // открыли поток
            {
                Console.Write("\n\t\tВведите имя : ");
                sw.WriteLine(Console.ReadLine());

                Console.Write("\t\tВведите фамилию : ");
                sw.WriteLine(Console.ReadLine());

                Console.Write("\t\tВведите возраст : ");
                sw.WriteLine(Console.ReadLine());
                Console.WriteLine("\n\t\t===================================================================");
            }
            Console.WriteLine("\n\n");
        }
        #endregion

        #region Task_2_Fibonachi
        public static void Task_2_Fibonachi(string path)
        {
            //----------------------СЧИТЫВАЕМ ИЗ ФАЙЛА----------------------------------------
            string file_content;
            Console.Write("\n\t\t****** СЧИТЫВАЕМ ИЗ ФАЙЛА ********\n\t\tЧИСЛА ФИБОНАЧЧИ : ");
            using (StreamReader sr = new StreamReader(path))
            {
                file_content = sr.ReadToEnd(); //Console.WriteLine(""+sr.ReadToEnd());
            }
            Console.Write(file_content);
            //---------------------------------------------------------------------------------
            List<int> list2 = new List<int>();
            string[] numbers = file_content.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Console.Write("\n\t\tРяд из {0} чисел, продолжим ряд еще на {0} чисел [", numbers.Count());
            int count = numbers.Count() - 1;

            foreach (string item in numbers)
                list2.Add(Int32.Parse(item));
            while (count >= 0)
            {
                list2.Add(list2.Last() + (list2[list2.Count() - 2]));
                count--;
            }
            foreach (int item in list2)
                Console.Write(" " + item);
            Console.WriteLine(" ]");
            //---------------------------------------------------------------------------------
            using (StreamWriter sw = new StreamWriter(path)) // открыли поток
            {
                foreach (int item in list2)
                {
                    sw.Write(item + ",");
                }
            }


        }

        #endregion

        //public static void Fibon() //другой способ с Фибоначчи
        //{
        //    Console.WriteLine("До какого числа считать ряд Фибоначчи?");
        //    int number = Convert.ToInt32(Console.ReadLine());
        //    int num = 0;
        //    int num2 = 0;
        //    FileInfo f = new FileInfo(@"C:\Users\Alfar\source\repos\HW_FILE_SYSTEM\HW_FILE_SYSTEM\bin\Debug\Alfar.txt");

        //    using (FileStream fss = f.Open(FileMode.Open, FileAccess.Read))
        //    {
        //        using (StreamReader sr = new StreamReader(fss, System.Text.Encoding.ASCII))
        //        {
        //            string t = sr.ReadLine();
        //            var m = t.Split(',');
        //            num = int.Parse(m[0]);
        //            num2 = int.Parse(m[1]);
        //        }
        //    }


        //    Console.Write("{0} ", num);

        //    Console.Write("{0} ", num2);
        //    int sum = 0;

        //    while (number >= sum)
        //    {
        //        sum = num + num2;

        //        Console.Write("{0} ", sum);

        //        num = num2;
        //        num2 = sum;
        //    }
        //}

        #region Task_3()
        public static void Task_3()
        {
            /* Сложить два целых числа А и В.*/

            //Создам отдельную Директорию, для профилактики
            Directory.CreateDirectory(@"D:\ШАГ\06_C#\014_Занятие_(FileStreame)\Task_3");

            //-----------------------------------Создаем файл----------------------------------------------------------------------
            //оболочка для создания файла  INPUT.TXT
            FileInfo fi = new FileInfo(@"D:\ШАГ\06_C#\014_Занятие_(FileStreame)\Task_3\INPUT.txt");  //открыли поток
            FileStream fs = fi.Create();//Команда создать файл, ФайлСтрим поддерживает синхранизацию чтения и записи с файлом
            fs.Close(); //закрыли поток, сохраняет изменения (коммит) 

            //---------------------------------------------------------------------------------------------------------------------
            /*Заправшиваем необходимые числа у пользователя и записываем в файл*/
            string path1 = Path.Combine(fi.DirectoryName, fi.Name);
            int a, b;
            a = b = 0;
            ///*Записываем туда два числа*/
            using (StreamWriter sw = new StreamWriter(path1))
            {
                Console.Write("\n\t\tВведите число А = ");
                sw.Write((a = Convert.ToInt32(Console.ReadLine())) + " ");
                Console.Write("\t\tВведите число B = ");
                sw.WriteLine(b = Convert.ToInt32(Console.ReadLine()));
            }
            Console.WriteLine("\t\t=================================");
            //---------------------------------------------------------------------------------------------------------------------
            /*Считываем числа из файла и суммируем*/
            string fileInnerText = "";

            using (StreamReader sr = new StreamReader(path1))
            {
                fileInnerText = sr.ReadToEnd();
                var m = fileInnerText.Split(' ');
                a = int.Parse(m[0]);
                b = int.Parse(m[1]);
                Console.Write("\t\tСумма числа {0} + {1} = {2}", a, b, a + b);
            }
            Console.WriteLine("\n\t\t=================================");
            Console.WriteLine("\n\t\tРезультат записываю в файл OUTPUT.TXT");
            Console.WriteLine("\t\t=================================");
            //---------------------------------------------------------------------------------------------------------------------
            /*Результат записываю в файл OUTPUT.TXT*/
            fi = new FileInfo(@"D:\ШАГ\06_C#\014_Занятие_(FileStreame)\Task_3\OUTPUT.txt");  //открыли поток
            fs = fi.Create();//Команда создать файл, ФайлСтрим поддерживает синхранизацию чтения и записи с файлом
            fs.Close(); //закрыли поток, сохраняет изменения (коммит) 
            string path2 = Path.Combine(fi.DirectoryName, fi.Name);

            using (StreamWriter sw = new StreamWriter(path2)) // открыли поток
            {
                sw.Write(a + b);
            }

            Console.Write("\t\t");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.Write("  Записан!\n\n\n\n");

        }
        #endregion

        #region Task_4()
        public static void Task_4()
        {
            //Создам отдельную Директорию, для профилактики
            Directory.CreateDirectory(@"D:\ШАГ\06_C#\014_Занятие_(FileStreame)\Task_4");

            //Создам словарь с чаровым ключем и интовым значением, для подсчета символов
            Dictionary<char, int> countSimbol = new Dictionary<char, int>();

            // указываю путь из какого файла читать
            string path = @"D:\ШАГ\06_C#\014_Занятие_(FileStreame)\Task_4\256.txt";

            //открываю поток, указыаю путь и команду для создания файла при его отсутствии
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] bytes = new byte[stream.Length]; //возвращает длину потока в байтах и создаем массив байтов

                stream.Read(bytes, 0, bytes.Length); //читаем блок байтов с начала и записываем у массив

                string file_content = System.Text.Encoding.Default.GetString(bytes); //Декодируем байты в строку

                Console.WriteLine("\n\t\t\tОригинальный текст\n\t\t=================================================");
                Console.WriteLine("\t\t   " + file_content + "\n\t\t=================================================");


                foreach (char item in file_content)
                {
                    if (countSimbol.ContainsKey(item)) //ContainsKey - Определяет содержится ли указанный ключ в словаре
                        countSimbol[item]++;
                    else
                        countSimbol[item] = 0;
                }

            }
            foreach (KeyValuePair<char, int> item in countSimbol)
                Console.WriteLine("\t\t\t\t" + (int)item.Key + " - " + item.Key + " - " + ((int)item.Value + 1));

            Console.ReadLine();
        }

        #endregion

        static void Main(string[] args)
        {
            Console.Title = "Тема: Взаимодействие с файловой системой - Латыпов Альфар";
            //-------------------------------------------------------------------------------------------------
            /*С помощью класса StreamWriter записать в текстовый файл свое имя, фамилию и возраст. 
             Каждая запись должна начинаться с новой строки.*/

            //Task_1();

            //-------------------------------------------------------------------------------------------------
            /*В файле записана непустая последовательность целых чисел, являющихся числами Фибоначчи. 
             Приписать еще столько же чисел этой последовательности.*/
            //0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711

            //string path = @"C:\Users\Alfar\source\repos\HW_FILE_SYSTEM\HW_FILE_SYSTEM\bin\Debug\Alfar.txt";
            //Task_2_Fibonachi(path);

            //Fibon(); //другой способ с Фибоначчи
            //-------------------------------------------------------------------------------------------------
            /* Сложить два целых числа А и В.
              Входные данные: В единственной строке входного файла INPUT.TXT записано два натуральных числа через пробел.
              Выходные данные: В единственную строку выходного файла OUTPUT.TXT нужно вывести одно целое число — сумму чисел А и В.
           */

            //Task_3();

            //-------------------------------------------------------------------------------------------------
            /*  Написать программу, читающую побайтно заданный файл и подсчитывающую число появлений каждого из 256 возможных знаков.*/

            Task_4();

        }

        #region Theory


        //------------------------------ТЕОРИЯ------------------------------------------------------------------------------------


        /*//Краткая теория
              Фреймворк .NET предоставляет большие возможности по управлению и манипуляции файлами и каталогами, 
              которые по большей части сосредоточены в пространстве имен System.IO. 
              Классы, расположенные в этом пространстве имен (такие как Stream, StreamWriter, FileStream и др.), 
              позволяют управлять файловым вводом-выводом.

            Работа с дисками
            Работу с файловой системой начнем с самого верхнего уровня - дисков. Для представления диска в пространстве имен System.IO имеется класс DriveInfo.
            Этот класс имеет статический метод GetDrives, который возвращает имена всех логических дисков компьютера. Также он предоставляет ряд полезных свойств:
            AvailableFreeSpace: указывает на объем доступного свободного места на диске в байтах
            DriveFormat: получает имя файловой системы
            DriveType: представляет тип диска
            IsReady: готов ли диск (например, DVD-диск может быть не вставлен в дисковод)
            Name: получает имя диска
            TotalFreeSpace: получает общий объем свободного места на диске в байтах
            TotalSize: общий размер диска в байтах
            VolumeLabel: получает или устанавливает метку тома

        Получим имена и свойства всех дисков на компьютере:
          using System;
          using System.Collections.Generic;
          using System.IO;
 
    namespace FileApp
    {   
       class Program
      {        
        static void Main(string[] args)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
             foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("Название: {0}", drive.Name);
                Console.WriteLine("Тип: {0}", drive.DriveType);
                if (drive.IsReady)
                {
                    Console.WriteLine("Объем диска: {0}", drive.TotalSize);
                    Console.WriteLine("Свободное пространство: {0}", drive.TotalFreeSpace);
                    Console.WriteLine("Метка: {0}", drive.VolumeLabel);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}

           // Работа с файлами. Классы File и FileInfo
           Подобно паре Directory/DirectoryInfo для работы с файлами предназначена пара классов File и FileInfo. 
           С их помощью мы можем создавать, удалять, перемещать файлы, получать их свойства и многое другое.
         Некоторые полезные методы и свойства класса FileInfo:
         CopyTo(path): копирует файл в новое место по указанному пути path
         Create(): создает файл
         Delete(): удаляет файл
         MoveTo(destFileName): перемещает файл в новое место
         Свойство Directory: получает родительский каталог в виде объекта DirectoryInfo
         Свойство DirectoryName: получает полный путь к родительскому каталогу
         Свойство Exists: указывает, существует ли файл
         Свойство Length: получает размер файла
         Свойство Extension: получает расширение файла
         Свойство Name: получает имя файла
         Свойство FullName: получает полное имя файла
         Класс File реализует похожую функциональность с помощью статических методов:
         Copy(): копирует файл в новое место
         Create(): создает файл
         Delete(): удаляет файл
         Move: перемещает файл в новое место
         Exists(file): определяет, существует ли файл

        // Получение информации о файле
         string path = @"C:\apache\hta.txt";
        FileInfo fileInf = new FileInfo(path);
        if (fileInf.Exists)
        {
            Console.WriteLine("Имя файла: {0}", fileInf.Name);
            Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
            Console.WriteLine("Размер: {0}", fileInf.Length);
        }
        
        //Удаление файла
        string path = @"C:\apache\hta.txt";
        FileInfo fileInf = new FileInfo(path);
        if (fileInf.Exists)
        {
           fileInf.Delete();
           // альтернатива с помощью класса File
           // File.Delete(path);
        }

        //Перемещение файла
        string path = @"C:\apache\hta.txt";
        string newPath = @"C:\SomeDir\hta.txt";
        FileInfo fileInf = new FileInfo(path);
        if (fileInf.Exists)
        {
           fileInf.MoveTo(newPath);       
           // альтернатива с помощью класса File
           // File.Move(path, newPath);
        }

        //Копирование файла
        string path = @"C:\apache\hta.txt";
        string newPath = @"C:\SomeDir\hta.txt";
        FileInfo fileInf = new FileInfo(path);
        if (fileInf.Exists)
        {
           fileInf.CopyTo(newPath, true);      
           // альтернатива с помощью класса File
           // File.Copy(path, newPath, true);
        }
         //Метод CopyTo класса FileInfo принимает два параметра: 
         //путь, по которому файл будет копироваться, и булевое значение, которое указывает,
         //надо ли при копировании перезаписывать файл (если true, как в случае выше, файл при копировании перезаписывается). 
         //Если же в качестве последнего параметра передать значение false, то если такой файл уже существует, приложение выдаст ошибку.

         // Метод Copy класса File принимает три параметра: 
         путь к исходному файлу, путь, по которому файл будет копироваться, и булевое значение, указывающее, будет ли файл перезаписываться.     
         
        //Чтение и запись файла. Класс FileStream

            Класс FileStream представляет возможности по считыванию из файла и записи в файл. 
            Он позволяет работать как с текстовыми файлами, так и с бинарными.
            Рассмотрим наиболее важные его свойства и методы:
            Свойство Length: возвращает длину потока в байтах
            Свойство Position: возвращает текущую позицию в потоке
            Метод Read: считывает данные из файла в массив байтов. Принимает три параметра: int Read(byte[] array, int offset, int count) 
            и возвращает количество успешно считанных байтов. Здесь используются следующие параметры:
            array - массив байтов, куда будут помещены считываемые из файла данные
            offset представляет смещение в байтах в массиве array, в который считанные байты будут помещены
            count - максимальное число байтов, предназначенных для чтения. Если в файле находится меньшее количество байтов, то все они будут считаны.
            Метод long Seek(long offset, SeekOrigin origin): устанавливает позицию в потоке со смещением на количество байт, указанных в параметре offset.
            Метод Write: записывает в файл данные из массива байтов. Принимает три параметра: Write(byte[] array, int offset, int count)
            array - массив байтов, откуда данные будут записываться в файла
            offset - смещение в байтах в массиве array, откуда начинается запись байтов в поток
            count - максимальное число байтов, предназначенных для записи
            FileStream представляет доступ к файлам на уровне байтов, поэтому, например, если вам надо считать или записать одну или несколько строк 
            в текстовый файл, то массив байтов надо преобразовать в строки, используя специальные методы. Поэтому для работы с текстовыми файлами применяются другие классы.
            В то же время при работе с различными бинарными файлами, имеющими определенную структуру FileStream может быть очень даже полезен 
            для извлечения определенных порций информации и ее обработки.
            Посмотрим на примере считывания-записи в текстовый файл:
            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();
             
            // запись в файл
            using (FileStream fstream = new FileStream(@"C:\SomeDir\noname\note.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }
             
            // чтение из файла
            using (FileStream fstream = File.OpenRead(@"C:\SomeDir\noname\note.txt"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine("Текст из файла: {0}", textFromFile);
            }
             
            Console.ReadLine();
            Разберем этот пример. И при чтении, и при записи используется оператор using. Не надо путать данный оператор с 
            директивой using, которая подключает пространства имен в начале файла кода. Оператор using позволяет создавать 
            объект в блоке кода, по завершению которого вызывается метод Dispose у этого объекта, и, таким образом, объект
            уничтожается. В данном случае в качестве такого объекта служит переменная fstream.
            Объект fstream создается двумя разными способами: через конструктор и через один из статических методов класса File.
            Здесь в конструктор передается два параметра: путь к файлу и перечисление FileMode. Данное перечисление указывает на 
            режим доступа к файлу и может принимать следующие значения:
            Append: если файл существует, то текст добавляется в конец файл. Если файла нет, то он создается. 
            Файл открывается только для записи.
            Create: создается новый файл. Если такой файл уже существует, то он перезаписывается
            CreateNew: создается новый файл. Если такой файл уже существует, то он приложение выбрасывает ошибку
            Open: открывает файл. Если файл не существует, выбрасывается исключение
            OpenOrCreate: если файл существует, он открывается, если нет - создается новый
            Truncate: если файл существует, то он перезаписывается. Файл открывается только для записи.
            Статический метод OpenRead класса File открывает файл для чтения и возвращает объект FileStream.
            Конструктор класса FileStream также имеет ряд перегруженных версий, позволяющий более точно настроить создаваемый объект. 
            Все эти версии можно посмотреть на msdn.
            И при записи, и при чтении применяется объект кодировки Encoding.Default из пространства имен System.Text.
            В данном случае мы используем два его метода: GetBytes для получения массива байтов из строки и GetString для получения строки из массива байтов.
            В итоге введенная нами строка записывается в файл note.txt. По сути это бинарный файл (не текстовый), 
            хотя если мы в него запишем только строку, то сможем посмотреть в удобочитаемом виде этот файл, открыв его в текстовом редакторе. 
            Однако если мы в него запишем случайные байты, например:
            fstream.WriteByte(13);
            fstream.WriteByte(103);
            То у нас могут возникнуть проблемы с его пониманием. Поэтому для работы непосредственно с текстовыми файлами 
            предназначены отдельные классы - StreamReader и StreamWriter.
            Произвольный доступ к файлам
            Нередко бинарные файлы представляют определенную стрктуру. И, зная эту структуру, мы можем взять из файла нужную 
            порцию информации или наоброт записать в определенном месте файла определенный набор байтов. Например, в wav-файлах 
            непосредственно звуковые данные начинаются с 44 байта, а до 44 байта идут различные метаданные - количество каналов 
            аудио, частота дискретизации и т.д.
            С помощью метода Seek() мы можем управлять положением курсора потока, начиная с которого производится считывание
            или запись в файл. Этот метод принимает два параметра: offset (смещение) и позиция в файле. 
            Позиция в файле описывается тремя значениями:
            SeekOrigin.Begin: начало файла
            SeekOrigin.End: конец файла
            SeekOrigin.Current: текущая позиция в файле
            Курсор потока, с которого начинается чтение или запись, смещается вперед на значение offset относительно позиции,
            указанной в качестве второго параметра. Смещение может отрицательным, тогда курсор сдвигается назад, 
            если положительное - то вперед.
            Рассмотрим на примере:
            using System.IO;
            using System.Text;
             
            class Program
            {
                static void Main(string[] args)
                {
                    string text = "hello world";
                         
                    // запись в файл
                    using (FileStream fstream = new FileStream(@"D:\note.dat", FileMode.OpenOrCreate))
                    {
                        // преобразуем строку в байты
                        byte[] input = Encoding.Default.GetBytes(text);
                        // запись массива байтов в файл
                        fstream.Write(input, 0, input.Length);
                        Console.WriteLine("Текст записан в файл");
             
                        // перемещаем указатель в конец файла, до конца файла- пять байт
                        fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока
             
                        // считываем четыре символов с текущей позиции
                        byte[] output = new byte[4];
                        fstream.Read(output, 0, output.Length);
                        // декодируем байты в строку
                        string textFromFile = Encoding.Default.GetString(output);
                        Console.WriteLine("Текст из файла: {0}", textFromFile); // worl
             
                        // заменим в файле слово world на слово house
                        string replaceText = "house";
                        fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока
                        input = Encoding.Default.GetBytes(replaceText);
                        fstream.Write(input, 0, input.Length);
             
                        // считываем весь файл
                        // возвращаем указатель в начало файла
                        fstream.Seek(0, SeekOrigin.Begin);
                        output = new byte[fstream.Length];
                        fstream.Read(output, 0, output.Length);
                        // декодируем байты в строку
                        textFromFile = Encoding.Default.GetString(output);
                        Console.WriteLine("Текст из файла: {0}", textFromFile); // hello house
                    }
                    Console.Read();
                }
            }
            Консольный вывод:
            
            Текст записан в файл
            Текст из файл: worl
            Текст из файла: hello house
            Вызов fstream.Seek(-5, SeekOrigin.End) перемещает курсор потока в конец файлов назад на пять символов:
            
            чтение и запись файлов через FileStream в C#
            То есть после записи в новый файл строки "hello world" курсор будет стоять на позиции символа "w".
            После этого считываем четыре байта начиная с символа "w". В данной кодировке 1 символ будет представлять 1 байт. 
            Поэтому чтение 4 байтов будет эквивалентно чтению четырех сиволов: "worl".
            Затем опять же перемещаемся в конец файла, не доходя до конца пять символов (то есть опять же с позиции символа "w"), 
            и осуществляем запись строки "house". Таким образом, строка "house" заменяет строку "world".
            Закрытие потока
            В примерах выше дл закрытия потока применяется конструкция using. После того как все операторы и выражения в 
            блоке using отработают, объект FileStream уничтожается. Однако мы можем выбрать и другой способ:
            FileStream fstream = null;
            try
            {
                fstream = new FileStream(@"D:\note3.dat", FileMode.OpenOrCreate);
                // операции с потоком
            }
            catch(Exception ex)
            {
             
            }
            finally
            {
                if (fstream != null)
                    fstream.Close();
            }
            Если мы не используем конструкцию using, то нам надо явным образом вызвать метод Close(): fstream.Close()
            
            
            //Чтение и запись текстовых файлов. StreamReader и StreamWriter
            
            Класс FileStream не очень удобно применять для работы с текстовыми файлами. К тому же для этого в пространстве System.IO 
            определены специальные классы: StreamReader и StreamWriter.
            
            Чтение из файла и StreamReader
            Класс StreamReader позволяет нам легко считывать весь текст или отдельные строки из текстового файла. Среди его методов можно выделить следующие:
            Close: закрывает считываемый файл и освобождает все ресурсы
            Peek: возвращает следующий доступный символ, если символов больше нет, то возвращает -1
            Read: считывает и возвращает следующий символ в численном представлении. Имеет перегруженную версию: Read(char[] array, int index, int count), 
            где array - массив, куда считываются символы, index - индекс в массиве array, начиная с которого записываются считываемые символы, 
            и count - максимальное количество считываемых символов
            ReadLine: считывает одну строку в файле
            ReadToEnd: считывает весь текст из файла
            Считаем текст из файла различными способами:
            string path= @"C:\SomeDir\hta.txt";
                        
            try
            {
                Console.WriteLine("******считываем весь файл********");
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
             
                Console.WriteLine();
                Console.WriteLine("******считываем построчно********");
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
             
                Console.WriteLine();
                Console.WriteLine("******считываем блоками********");
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    char[] array = new char[4];
                    // считываем 4 символа
                    sr.Read(array, 0, 4);
             
                    Console.WriteLine(array);           
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Как и в случае с классом FileStream здесь используется конструкция using.
            В первом случае мы разом считываем весь текст с помощью метода ReadToEnd().
            Во втором случае считываем построчно через цикл while: while ((line = sr.ReadLine()) != null) - сначала присваиваем переменной 
            line результат функции sr.ReadLine(), а затем проверяем, не равна ли она null. Когда объект sr дойдет до конца файла и больше 
            строк не останется, то метод sr.ReadLine() будет возвращать null.
            В третьем случае считываем в массив четыре символа.
            Обратите внимание, что в последних двух случаях в конструкторе StreamReader указывалась кодировка System.Text.Encoding.Default. 
            Свойство Default класса Encoding получает кодировку для текущей кодовой страницы ANSI. Также через другие свойства мы можем 
            указать другие кодировки. Если кодировка не указана, то при чтении используется UTF8. Иногда важно указывать кодировку, 
            так как она может отличаться от UTF8, и тогда мы получим некорректный вывод. Например:
            Класс StreamReader в C#
            Запись в файл и StreamWriter
            Для записи в текстовый файл используется класс StreamWriter. Свою функциональность он реализует через следующие методы:
            Close: закрывает записываемый файл и освобождает все ресурсы
            Flush: записывает в файл оставшиеся в буфере данные и очищает буфер.
            Write: записывает в файл данные простейших типов, как int, double, char, string и т.д.
            WriteLine: также записывает данные, только после записи добавляет в файл символ окончания строки
            Рассмотрим запись в файл на примере:
            string readPath= @"C:\SomeDir\hta.txt";
            string writePath = @"C:\SomeDir\ath.txt";
             
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    text=sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
             
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Дозапись");
                    sw.Write(4.5);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Здесь сначала мы считываем файл в переменную text, а затем записываем эту переменную в файл, а затем через объект StreamWriter записываем в новый файл.
            Класс StreamWriter имеет несколько конструкторов. Здесь мы использовали один из них: new StreamWriter(writePath, false, System.Text.Encoding.Default). ,
            В качестве первого параметра передается путь к записываемому файлу. Второй параметр представляет булевую переменную, которая определяет, будет файл 
            дозаписываться или перезаписываться. Если этот параметр равен true, то новые данные добавляются в конце к уже имеющимся данным. Если false, 
            то файл перезаписывается. И если в первом случае файл перезаписывается, то во втором делается дозапись в конец файла.
            Третий параметр указывает кодировку, в которой записывается файл.
          
         */
            #endregion

        #region Классная работа
        /*
        //Классная работа
        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // <= <==  <===   <====

namespace Work_with_file
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Евгений
            //string path2 = @"C:\Users\y.gertsen\Documents\New Text Document.txt";

            //try
            //{
            //    Console.WriteLine("******считываем весь файл********");
            //    using (StreamReader sr = new StreamReader(path2))
            //    {
            //        Console.WriteLine(sr.ReadToEnd());
            //    }

            //    Console.WriteLine();
            //    Console.WriteLine("******считываем построчно********");
            //    using (StreamReader sr = new StreamReader(path2, System.Text.Encoding.Default))
            //    {
            //        string line;
            //        while ((line = sr.ReadLine()) != null)
            //        {
            //            Console.WriteLine(line);
            //        }
            //    }

            //    Console.WriteLine();
            //    Console.WriteLine("******считываем блоками********");
            //    using (StreamReader sr = new StreamReader(path2, System.Text.Encoding.Default))
            //    {
            //        char[] array = new char[4];
            //        // считываем 4 символа
            //        sr.Read(array, 0, 4);

            //        Console.WriteLine(array);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //Console.ReadLine();
            #endregion

            //позволяет работать с файлами -  using System.IO;
            // class Stream - основной класс для работы с файлами
            // class FileStream - для чтение и записи.
           // string path = Path.Combine(@"C:\Users\васильева\Documents\Visual Studio 2015\Projects\Work_with_file", "NewFile3.txt"); // 3


           // NewFile(); // 1
           // NewFile2(); // 2, 4

            //создание директории

            //DirectoryInfo dir1 = new DirectoryInfo(" . "); // привязка к рабочему каталогу
            //dir1.Create();

            DirectoryInfo dir = new DirectoryInfo(@"Dir"); // привязка C:\Users\васильева\Documents
            dir.Create();

            foreach (DirectoryInfo item in dir.GetDirectories())
            {
                Console.WriteLine(item.Name);
                foreach (FileInfo item2 in dir.GetFiles())
                {
                    Console.WriteLine(item2.Name);
                }
            }


        }

        static void NewFile() // 1
        {
            FileInfo file = new FileInfo(@"C:\Users\васильева\Documents\Visual Studio 2015\Projects\Work_with_file\NewFile.txt"); // хранит инфу о файле

            FileStream fs = file.Create(); // создание файла
            fs.Close(); // закрыли поток

        }

        static void NewFile2() // 2
        {
            FileInfo file = new FileInfo(@"C:\Users\васильева\Documents\Visual Studio 2015\Projects\Work_with_file\NewFile2.txt");

            using (FileStream fs = file.Open(FileMode.OpenOrCreate, FileAccess.Write)) // using заменяет открытие и зикрытие скобками
            { // открытие потока

                using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
                {
                    sw.Write("Some text");
                }
            } // закрытие потока

            //метод № 2
            StreamWriter stw = new StreamWriter(@"C:\Users\васильева\Documents\Visual Studio 2015\Projects\Work_with_file\NewFile3.txt");
            stw.Write("Hi");

            CotyTo(file); // 4
            stw.Close();

        }

        static void CotyTo(FileInfo fi) // 4
        {
            fi.CopyTo(@"C:\Users\васильева\Documents\Visual Studio 2015\Projects\Work_with_file\NewFile4.txt"); // копирование
        }

    }
}
        */
        #endregion
    }
}
