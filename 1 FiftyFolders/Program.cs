using System;
using System.IO;

namespace FiftyFolders {
  class Program {
    static void Main(string[] args) {
      // создаю 50 папок (с 1 по 50 а не с 0 по 50 как в задании, т.к. это будет уже 51 папка)
      for (int i = 1; i <= 50; i++) {
        string dir = $"Folder_{i}";
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
      }

      DirectoryInfo di = new DirectoryInfo(".");
      DirectoryInfo[] diArr = di.GetDirectories("Folder_*", SearchOption.TopDirectoryOnly);

      // список папок на экран + удаление
      foreach (var dir in diArr) {
        Console.WriteLine(dir.Name);
        try {
          Directory.Delete(dir.FullName, true);
        }
        catch (Exception e) {
          Console.WriteLine(e.Message);
        }
      }
      
      Console.ReadKey();
    }
  }
}
