using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;


public static class TextDebugger
{





    public static void LogString(string outputString)
    {

        string FilePath = CheckForOutputFile();

        File.AppendAllText(FilePath, outputString + "\n");

    }



    private static string CheckForOutputFile()
    {

        string FileName = "TextDebuggingOutput.txt";

        string FilePath;


        FilePath = Path.Combine(Application.persistentDataPath, FileName);

        if (!File.Exists(FilePath))
        {

            File.AppendAllText(FilePath, "DEBUGGER\n");


        }

        return FilePath;



    }


}
