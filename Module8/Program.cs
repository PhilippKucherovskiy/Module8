using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string path = ""; 

        try
        {
            DirectoryInfo directory = new DirectoryInfo(path);

            if (directory.Exists)
            {
                // время сейчас
                DateTime currentTime = DateTime.Now;

                // перечисляет все файлы в директории
                foreach (FileInfo file in directory.GetFiles())
                {
                    // вычисляет разницу между действительным временем и временем файла
                    TimeSpan timeDifference = currentTime - file.LastAccessTime;

                    // проверяет наличие разницы больше 30 минут
                    if (timeDifference.TotalMinutes > 30)
                    {
                        // удаляет подходящий файл
                        file.Delete();
                    }
                }

                // перечисляет файлы субдиректории в директории
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    // вычисляет разницу между текущим временем и временем доступа к папке
                    TimeSpan timeDifference = currentTime - subDirectory.LastAccessTime;

                    // проверяет наличие разницы больше 30 минут
                    if (timeDifference.TotalMinutes > 30)
                    {
                        // удаляет папку
                        subDirectory.Delete(true);
                    }
                }

                Console.WriteLine("Successfully cleaned the folder.");
            }
            else
            {
                Console.WriteLine("The specified folder does not exist.");
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
}
