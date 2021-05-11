using System;
using System.IO;
using System.Text;

namespace InFileOutFile {
  class Program {
    static void Main(string[] args) {
      string fileName = "test.txt";

      try {
        // если такой файл есть, удаляю
        if (File.Exists(fileName)) File.Delete(fileName);

        // Создаю новый файл и пишу в него
        using (FileStream fs = File.Create(fileName)) {
          using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8)) {
            sw.WriteLine("Первая строка текста...");
            sw.WriteLine("Вторая строка текста...");
            sw.Write(sw.NewLine);
            sw.WriteLine("Третья строка чисел:");
            for (int i = 0; i < 10; i++) {
              sw.Write($"{i} ");
            }
          }
        }

        // открываю файл на чтение и вывожу содержимое
        using (StreamReader sr = File.OpenText(fileName)) {
          string s = "";
          while ((s = sr.ReadLine()) != null) {
            Console.WriteLine(s);
          }
        }
      } catch (Exception Ex) {
        Console.WriteLine(Ex.ToString());
      }

      // Задержка.
      Console.ReadKey();
    }
  }
}