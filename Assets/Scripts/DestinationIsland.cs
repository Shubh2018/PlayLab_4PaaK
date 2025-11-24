using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DestinationIsland : Island
{
    [SerializeField] private Image[] _fillImages = new Image[4];

    private Image[] _fill;
    
    [SerializeField] private float fillPerTap = 0.1f;
    [SerializeField] private float decreasePerSec = 0.01f;

    protected override void Awake()
    {
        base.Awake();
        
        _fill = new Image[_fillImages.Length];
        
        for (int i = 0; i < _fillImages.Length; i++)
        {
            _fillImages[i].fillAmount = 0;
            _fill[i] = _fillImages[i].transform.GetChild(0).GetComponent<Image>();
            _fill[i].fillAmount = 0;
            _fillImages[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        CheckTaps();
    }

    private void CheckTaps()
    {
        if (_controllersNearIsland.Count <= 0) return;
        
        foreach (ShipController controller in _controllersNearIsland)
        {
            if (InputManager.GetTapPressed(controller.Player))
            {
                _fill[(int)controller.Player - 1].fillAmount += fillPerTap;
                InputManager.SetTapToFalse(controller.Player);
            }

            else
            {
                if (_fill[(int)controller.Player - 1].fillAmount <= 0.0f)
                    continue;
            
                _fill[(int)controller.Player - 1].fillAmount -= fillPerTap * Time.deltaTime;
            }
        }
    }

    public override void EnableFillImages()
    {
        Debug.Log($"EnableFillImages Called before NullCheck!");
        
        if (_controllersNearIsland.Count <= 0) return;

        Debug.Log($"EnableFillImages Called!");
        
        foreach (ShipController controller in _controllersNearIsland)
        {
            switch (controller.Player)
            {
                case InputManager.Player.Player1: _fillImages[(int)controller.Player - 1].gameObject.SetActive(true); break;
                case InputManager.Player.Player2: _fillImages[(int)controller.Player - 1].gameObject.SetActive(true); break;
                case InputManager.Player.Player3: _fillImages[(int)controller.Player - 1].gameObject.SetActive(true); break;
                case InputManager.Player.Player4: _fillImages[(int)controller.Player - 1].gameObject.SetActive(true); break;
                default: break;
            }
        }
    }
}