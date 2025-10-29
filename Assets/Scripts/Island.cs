using System;
using UnityEngine;
using TMPro;

public class Island : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private KeyboardSequenceController keyboardSequenceController;
    [SerializeField] private char[] keyboardSequenceTeam1;
    [SerializeField] private char[] keyboardSequenceTeam2;
    [SerializeField] private TMP_Text _sequenceTextTeam1;
    [SerializeField] private TMP_Text _sequenceTextTeam2;

    private SpriteRenderer _renderer;

    private ShipController _shipController;
    private ShipController _currentlyControlledBy;

    public ShipController CurrentlyControlledBy => _currentlyControlledBy;

    public Vector2 Center => transform.position;
    public float Radius => _radius;
    public char[] KeyboardSequenceTeam1 => keyboardSequenceTeam1;
    public char[] KeyboardSequenceTeam2 => keyboardSequenceTeam2;
    
    public int ArrayPointer{get; set;} = 0;
    
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        keyboardSequenceController = FindAnyObjectByType<KeyboardSequenceController>();
        keyboardSequenceTeam1 = keyboardSequenceController.GenerateRandomSequence(Constants.Team1KeyboardSequenceOptions);
        keyboardSequenceTeam2 = keyboardSequenceController.GenerateRandomSequence(Constants.Team2KeyboardSequenceOptions);
        
        UpdateSequenceText(_sequenceTextTeam1, keyboardSequenceTeam1);
        UpdateSequenceText(_sequenceTextTeam2, keyboardSequenceTeam2);
    }

    private void UpdateSequenceText(TMP_Text sequenceText, char[] sequence)
    {
        sequenceText.text = String.Empty;
        
        if (sequence.Length == 0) return;

        foreach (char c in sequence)
        {
            sequenceText.text += c + " ";
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
