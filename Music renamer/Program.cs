using System;
using System.IO;

namespace Music_Renamer
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter path to folder...");
            string folderPath = Console.ReadLine();
            Console.WriteLine();

            var filesNames = Directory.GetFiles(folderPath);
            for (int i = 0; i < filesNames.Length; i++)
            {
                Console.WriteLine(filesNames[i]);

                string[] trackTags = GetTags(filesNames[i]);
                Console.WriteLine("Title: " + trackTags[0]);
                Console.WriteLine("Performer: " + trackTags[1]);

                ChangeFileName(folderPath, filesNames[i], trackTags);

                Console.WriteLine("--------------------------");
            }

            Console.ReadKey();
        }

        static string[] GetTags(string path)
        {
            string[] trackTags = new string[] { "", "" };

            var tfile = TagLib.File.Create(path);
            trackTags[0] = tfile.Tag.Title;                 //Title
            trackTags[1] = tfile.Tag.FirstPerformer;        //Performer

            return trackTags;
        }

        static void ChangeFileName(string folderPath, string filePath, string[] fileTags)
        {
            if (fileTags[0] == null)
                fileTags[0] = "EMPTY TITLE";
            if (fileTags[1] == null)
                fileTags[1] = "EMPTY PERFORMER";
            try
            {
                File.Move(filePath, folderPath + " new//" + fileTags[0] + " - " + fileTags[1] + ".mp3");
            }
            catch
            {
                Console.WriteLine("Something happened!");
            }
        }
    }
}
