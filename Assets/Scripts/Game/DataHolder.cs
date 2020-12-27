using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// That script save data between two scenes
/// </summary>
public class DataHolder 
{
    public static DataHolder instance {
        get {
            if (_instance == null) _instance = new DataHolder();
            return _instance;
        }
    }
    private static DataHolder _instance;
    public int score;
}
