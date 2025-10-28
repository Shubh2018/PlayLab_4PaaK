using System;
using UnityEngine;
using TMPro;

public class Island : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private KeyboardSequenceController keyboardSequenceController;
    [SerializeField] private char[] keyboardSequence;
    [SerializeField] private TMP_Text _sequenceText;

    private SpriteRenderer _renderer;

    private ShipController _shipController;
    private ShipController _currentlyControlledBy;

    public ShipController CurrentlyControlledBy => _currentlyControlledBy;

    public Vector2 Center => transform.position;
    public float Radius => _radius;
    public char[] KeyboardSequence => keyboardSequence;
    public int ArrayPointer{get; set;} = 0;
    
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        keyboardSequenceController = FindAnyObjectByType<KeyboardSequenceController>();
        keyboardSequence = keyboardSequenceController.GenerateRandomSequence();
        
        UpdateSequenceText(keyboardSequence);
    }

    private void UpdateSequenceText(char[] sequence)
    {
        _sequenceText.text = String.Empty;
        
        if (sequence.Length == 0) return;

        foreach (char c in sequence)
        {
            _sequenceText.text += c + " ";
        }
    }

    public void ConquerIsland()
    {
        if (!_shipController) return;
        
        _shipController.AddConquredIslandToList();
        ChangeAllegiance();
        
        _currentlyControlledBy = _shipController;
        _renderer.color = _currentlyControlledBy.TeamColor;
        
        Debug.Log($"{this.name} Conquered");
        
        //UpdateSequenceText(Array.Empty<char>());
    }

    private void ChangeAllegiance()
    {
        if (!_currentlyControlledBy) return;
            
        _currentlyControlledBy.RemoveConqueredIslandFromList(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Player))
        {
            //keyboardSequence = keyboardSequenceController.GenerateRandomSequence();
            _shipController = other.GetComponent<ShipController>();
            //UpdateSequenceText(keyboardSequence);
            _shipController.SetIsland(this);
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.CompareTag(Constants.Player))
        {
            keyboardSequence = Array.Empty<char>();
            UpdateSequenceText(keyboardSequence);
            return;
        }*/
    }
}
