using System;
using System.IO;


class Program
{
    static void Main(string[] args)
    {
        string binaryFilePath = "D:\\Загрузки"; 
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string studentsPath = Path.Combine(desktopPath, "Students"); if (!Directory.Exists(studentsPath))
            Directory.CreateDirectory(studentsPath);

        if (File.Exists(binaryFilePath))
        {
            using (FileStream binaryFile = new FileStream(binaryFilePath, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader(binaryFile))
                {
                    while (binaryFile.Position < binaryFile.Length)
                    {
                        string name = binaryReader.ReadString();
                        string group = binaryReader.ReadString();
                        DateTime dateOfBirth = new DateTime(binaryReader.ReadInt64());

                        string groupPath = Path.Combine(studentsPath, group + ".txt");
                        if (!File.Exists(groupPath))
                            File.Create(groupPath).Close();

                        using (StreamWriter sw = new StreamWriter(groupPath, true))
                        {
                            sw.WriteLine(name + ", " + dateOfBirth.ToShortDateString());
                        }
                    }
                }
            }
            Console.WriteLine("Successfully loaded data from binary file to text files in the Students folder on the desktop.");
        }
        else
        {
            Console.WriteLine("The specified binary file was not found.");
        }
    }
}
