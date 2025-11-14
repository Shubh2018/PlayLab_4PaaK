using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        UIManager.Instance.SetMainMenuDocReferences();
    }

    private void OnDisable()
    {
        UIManager.Instance.UnsetMainMenuDoc();
    }
}


