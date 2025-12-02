using System;
using UnityEngine;
using TMPro;

public class Sequence : MonoBehaviour
{
    [SerializeField] private TMP_Text _sequenceText;
    [SerializeField] private InputManager.Player _player;
    public InputManager.Player Player => _player;

    public void SetText(char[] sequence)
    {
        _sequenceText.text = String.Empty;

        if (sequence.Length == 0) return;

        foreach (char c in sequence)
        {
            _sequenceText.text += $"{c} ";
        }
    }
}