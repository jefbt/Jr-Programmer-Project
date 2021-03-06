using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        MainManager.SetTeamColor(color);
    }
    
    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;

        if (ColorPicker.ContainsColor(MainManager.GetTeamColor()))
        {
            ColorPicker.SelectColor(MainManager.GetTeamColor());
        }
        else
        {
            ColorPicker.SelectColor(ColorPicker.AvailableColors[0]);
        }
        
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        MainManager.StaticSaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveColorClicked()
    {
        MainManager.StaticSaveColor();
    }
    public void LoadColorClicked()
    {
        MainManager.StaticLoadColor();
        ColorPicker.SelectColor(MainManager.GetTeamColor());
    }
}
