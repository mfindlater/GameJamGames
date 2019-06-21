using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Diagnostics;

public class Build
{
    [MenuItem("Tools/itch.io/Windows Deploy")]
    public static void ItchDeploy()
    {
       var directory = EditorUtility.SaveFolderPanel("Choose Location of Bomb-Bardment executable.", "", "");


        string[] levels = new string[] { "Assets/Scenes/Title.unity", "Assets/Scenes/Main.unity" };

        BuildPipeline.BuildPlayer(levels, Path.Combine(directory, "Bomb-Bardment.exe"), BuildTarget.StandaloneWindows, BuildOptions.None);


        string tomlFile = Path.Combine(directory, ".itch.toml");

        if(File.Exists(tomlFile))
        {
            File.Delete(tomlFile);      
        }

        FileUtil.CopyFileOrDirectory("Assets/Output/itch.txt", tomlFile);


        Process process = new Process();

        process.StartInfo.FileName = "butler";
        process.StartInfo.Arguments = string.Format("push {0} mfindlater/bombardment:windows", directory);

        process.Start();

    }

}
