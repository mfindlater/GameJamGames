using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace JustAttack
{
    public class Game : MonoBehaviour
    {



        public static string DefaultSaveFolder = Application.persistentDataPath;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {

        }


        public static void Save<T>(T data, string filename) where T : class
        {
            try
            {
                using (var fs = new FileStream(Path.Combine(DefaultSaveFolder,filename), FileMode.OpenOrCreate))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fs, data);
                }
            }
            catch(Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public static T Load<T>(string filename) where T : class
        {
            T data = default(T);

            try
            {
                if (File.Exists(Path.Combine(Application.persistentDataPath, filename)))
                {
                    using (var fs = new FileStream(Path.Combine(DefaultSaveFolder, filename), FileMode.Open))
                    {
                        var formatter = new BinaryFormatter();
                        data = (T)formatter.Deserialize(fs);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            return data;
        }

        
    }   
}
