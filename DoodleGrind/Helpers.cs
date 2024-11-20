using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoodleGrind
{
    internal static class Helpers
    {
        public static string? ReadFileToString(string relativeFilePath)
        {
            try
            {
                //string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeFilePath);
                string fullPath = Path.GetFullPath(relativeFilePath);
                Console.WriteLine(fullPath);
                string fileContent = File.ReadAllText(fullPath);

                // Remove trailing whitespaces and new lines
                string trimmedContent = fileContent.TrimEnd();

                return trimmedContent;
            }
            catch (FileNotFoundException fnfex)
            {
                Console.Error.WriteLine($"The file was not found, see details: {fnfex.Message}");
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine("You do not have permission to access this file: " + ex.Message);
                return null;
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine("An I/O error occurred while accessing the file: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Something went wrong, details: {ex.Message}");
                return null;
            }
        }
    }
}
