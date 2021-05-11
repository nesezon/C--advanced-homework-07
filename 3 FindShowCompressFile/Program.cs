using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace FindShowCompressFile {
  class Program {
    static void Main(string[] args) {

      string file2search = "test.txt";
      string destinationFileWithoutExtension = "test";
      Console.WindowWidth = 200;

      // ищу файл в папке 2-го упражнения
      DirectoryInfo directoryInfo = new DirectoryInfo("../../../2 InFileOutFile");
      FileInfo[] fileNames = directoryInfo.GetFiles(file2search, SearchOption.AllDirectories);

      if (fileNames.Length > 0) {
        // найден
        Console.WriteLine($"Файл \"{fileNames[0].FullName}\" найден");

        using (FileStream fstream = File.OpenRead(fileNames[0].FullName)) {
          Console.WriteLine("-[Содержимое файла]-----");
          // Считываем FileStream в байтовый массив
          byte[] array = new byte[fstream.Length];
          fstream.Read(array, 0, array.Length);
          // декодируем байты в строку
          string textFromFile = Encoding.UTF8.GetStringExcludeBOMPreamble(array);
          Console.WriteLine(textFromFile);
          Console.WriteLine("------------------------");

          // компрессия Deflate
          FileStream destination = File.Create($"{destinationFileWithoutExtension}.dfl");
          using (DeflateStream compressor = new DeflateStream(destination, CompressionMode.Compress)) {
            foreach (var theByte in array) {
              compressor.WriteByte(theByte);
            }
          }
          // компрессия Zip
          destination = File.Create($"{destinationFileWithoutExtension}.zip");
          using (GZipStream compressor = new GZipStream(destination, CompressionMode.Compress)) {
            foreach (var theByte in array) {
              compressor.WriteByte(theByte);
            }
          }
        }
      } else {
        // не найден
        Console.WriteLine($"Файл {file2search} не найден в папке {directoryInfo.FullName}");
        Console.WriteLine("(Чтобы он появился, запустите исполняемый файл второго упражнения)");
      }

      // Задержка.
      Console.ReadKey();
    }
  }

  static class ExtensionClass {
    /// <summary>
    /// Избавляемся от BOM если есть
    /// </summary>
    public static string GetStringExcludeBOMPreamble(this Encoding encoding, byte[] bytes) {
      var preamble = encoding.GetPreamble();
      if (preamble?.Length > 0 && bytes.Length >= preamble.Length && bytes.Take(preamble.Length).SequenceEqual(preamble)) {
        return encoding.GetString(bytes, preamble.Length, bytes.Length - preamble.Length);
      } else {
        return encoding.GetString(bytes);
      }
    }
  }
}
