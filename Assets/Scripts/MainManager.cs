using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // auto-implemented property (using get; private set;)
    public static MainManager instance { get; private set; }

    private Color teamColor;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadColor();
    }

    public static void SetTeamColor(Color color)
    {
        if (instance != null) instance.teamColor = color;
    }

    public static Color GetTeamColor()
    {
        if (instance != null)
        {
            return instance.teamColor;
        }
        return Color.white;
    }

    public static bool HasInstance()
    {
        return instance != null;
    }

    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public static void StaticSaveColor()
    {
        if (instance != null) instance.SaveColor();
    }

    public static void StaticLoadColor()
    {
        if (instance != null) instance.LoadColor();
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.teamColor = MainManager.GetTeamColor();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            MainManager.SetTeamColor(data.teamColor);
        }
    }
}
