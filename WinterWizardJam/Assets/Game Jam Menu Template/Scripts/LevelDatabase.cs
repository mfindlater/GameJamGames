using UnityEngine;
using System;

[Serializable]
public class Level
{
    public string levelName;
    public string sceneName;
}

[CreateAssetMenu]
public class LevelDatabase : ScriptableObject
{
    public Level[] levels;
}