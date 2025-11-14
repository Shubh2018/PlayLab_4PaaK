using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SetupEvents : MonoBehaviour
{
    private UIDocument _document;


    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        UIManager.Instance.SetSetupDocumentReferences();
    }

    private void OnDisable()
    {
        UIManager.Instance.UnsetSetupDocument();
    }
}


