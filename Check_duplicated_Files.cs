/**
** Copyright (c) 2022 Felix Adamietz / VolleInspiration
**/
using System;
using System.Collections.Generic;
using System.IO;

namespace Check_duplicated_Files
{
    class Program
    {
        static string dirA = "", dirB = "", fileEnding = "";
        static List<string> dirAList = new List<string>();
        static List<string> dirBList = new List<string>();

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
                inputAndCheckDirs(false);
            else
            {
                Console.WriteLine("Hier gibts nichts zu sehen!");
                Console.ReadLine();
            }
        }

        static void inputAndCheckDirs(bool hasArgs)
        {
            if (!hasArgs)
            {
                do
                {
                    Console.Write("Gib den Pfad des Ordners der neuen Dateien ein:");
                    dirA = Console.ReadLine();

                    if (dirA == "")
                        break;

                    if (!isValidDir(dirA))
                        continue;

                    Console.Write("Gib den Pfad des Ordners des Zielordners ein:");
                    dirB = Console.ReadLine();
                } while (!isValidDir(dirB));
            }
            Console.Write("Gib bitte das Dateiformat ein, nachdem zu suchen möchtest:");
            fileEnding = Console.ReadLine();

            if (isValidDir(dirB))
            {
                addFilesInList();
            }
        }

        static bool isValidDir(string dir)
        {
            if(!Directory.Exists(dir))
                return false;
            else
                return true;   
        }

        static void addFilesInList()
        {
            int fileCountDirA = Directory.GetFiles(dirA, "*." + fileEnding, SearchOption.AllDirectories).Length;
            int fileCountDirB = Directory.GetFiles(dirB, "*." + fileEnding, SearchOption.AllDirectories).Length;
            
            string[] fileDirsA = Directory.GetFiles(dirA, "*." + fileEnding, SearchOption.AllDirectories);
            for(int i = 0; i < fileDirsA.Length; i++)
            {
                string str = fileDirsA[i];
                string[] arr = str.Split("\\");
                dirAList.Add(arr[arr.Length-1]);
            }
            
            string[] fileDirsB = Directory.GetFiles(dirB, "*." + fileEnding, SearchOption.AllDirectories);
            for (int i = 0; i < fileDirsB.Length; i++)
            {
                string str = fileDirsB[i];
                string[] arr = str.Split("\\");
                dirBList.Add(arr[arr.Length - 1]);
            }

            foreach (var item in dirAList)
            {
                for(int i = 0; i < fileCountDirB; i++)
                {
                    if(item.ToString() == dirBList[i])
                    {
                        filePath(fileDirsA, fileDirsB, item.ToString());
                        Console.WriteLine(item.ToString());
                        Console.WriteLine("");
                    }
                }
            }
            Console.WriteLine("Fertig...");
            Console.ReadLine();
        }

        static void filePath(string[] arrA, string[] arrB, string str)
        {
            for(int i = 0; i < arrB.Length; i++)
            {
                if(arrB[i].Contains(str))
                {
                    Console.WriteLine("Speicherort:");
                    Console.WriteLine(arrB[i]);
                }
            }

            for (int i = 0; i < arrA.Length; i++)
            {
                if (arrA[i].Contains(str))
                {
                    Console.WriteLine(arrA[i]);
                    try
                    {
                        string fName="";
                        for (int j = 0; j < arrA[i].Length; j++)
                        {
                            string fileName = arrA[i];
                            string[] arr = str.Split("\\");
                            fName = arr[arr.Length - 1];
                        }

                        string dirToCreate = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\testTrash\\";
                        if (!Directory.Exists(dirToCreate))
                            Directory.CreateDirectory(dirToCreate);
                        File.Move(arrA[i], dirToCreate + fName);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }
}
