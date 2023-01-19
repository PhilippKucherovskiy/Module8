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
                
                DateTime currentTime = DateTime.Now;
                long initialSize = GetDirectorySize(path);
                Console.WriteLine("The initial size of the folder is: " + initialSize + " bytes");
                int deletedFiles = 0;
                long deletedSize = 0;
                
                foreach (FileInfo file in directory.GetFiles())
                {
                    
                    TimeSpan timeDifference = currentTime - file.LastAccessTime;

                    
                    if (timeDifference.TotalMinutes > 30)
                    {
                        
                        deletedSize += file.Length;
                        file.Delete();
                        deletedFiles++;
                    }
                }

                
                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                {
                    
                    TimeSpan timeDifference = currentTime - subDirectory.LastAccessTime;
                    
                    if (timeDifference.TotalMinutes > 30)
                    {
                        
                        deletedSize += GetDirectorySize(subDirectory.FullName);
                        subDirectory.Delete(true);
                    }
                }

                Console.WriteLine("Successfully cleaned the folder.");
                Console.WriteLine("Number of deleted files: " + deletedFiles);
                Console.WriteLine("Deleted size: " + deletedSize + " bytes");
                long finalSize = GetDirectorySize(path);
                Console.WriteLine("The final size of the folder is: " + finalSize + " bytes");
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
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("The specified folder path is not valid.");
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

        
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
        {
            
            FileInfo info = new FileInfo(file);
            size += info.Length;
        }    
        string[] subDirectories = Directory.GetDirectories(path);
        foreach (string subDirectory in subDirectories)
        {
            try
            {
                
                size += GetDirectorySize(subDirectory);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("You do not have the necessary permissions to access the subdirectory: " + subDirectory);
                Console.WriteLine("Error message: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to access the subdirectory: " + subDirectory);
                Console.WriteLine("Error message: " + ex.Message);
            }
        }

        return size;
    }
}

