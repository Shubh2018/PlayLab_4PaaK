using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument document;
    private Button button;
    private List<Button> menubuttons = new List<Button>();

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        button = document.rootVisualElement.Q("StartGame") as Button;
        button.RegisterCallback<ClickEvent>(OnPlayGameClick);

        menubuttons = document.rootVisualElement.Query<Button>().ToList();
        
        for (int i = 0; i < menubuttons.Count; ++i) 
        {
            menubuttons[i].RegisterCallback<ClickEvent>(AllButtonsClick);
        }

    }

    private void OnDisable()
    {
        button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        
        for (int i = 0; i < menubuttons.Count; ++i)
        {
            menubuttons[i].UnregisterCallback<ClickEvent>(AllButtonsClick);
        }
    }

    private void OnPlayGameClick (ClickEvent evt)
    {
        Debug.Log("Start game was clicked");
    }

    private void AllButtonsClick(ClickEvent evt)
    {
        Debug.Log("A button was clicked");
    }
}


