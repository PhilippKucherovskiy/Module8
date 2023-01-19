using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string path = ""; 

        try
        {
            if (Directory.Exists(path))
            {
                long size = GetDirectorySize(path);
                Console.WriteLine("The size of the folder is: " + size + " bytes");
            }
            else
            {
                Console.WriteLine("The specified folder does not exist.");//обработка исключений
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("You do not have the necessary permissions to access this folder.");
            Console.WriteLine("Error message: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while trying to access the folder.");
            Console.WriteLine("Error message: " + ex.Message);
        }
    }

    static long GetDirectorySize(string path)
    {
        long size = 0;

        // получает все файлы директории
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            // добавляет размер файлов
            FileInfo info = new FileInfo(file);
            size += info.Length;
        }

        // получает все субдиректории
        string[] subDirectories = Directory.GetDirectories(path);
        foreach (string subDirectory in subDirectories)
        {
            // добавляет размер всех директорий в итоговый размер
            size += GetDirectorySize(subDirectory);
        }

        return size;
    }
}
