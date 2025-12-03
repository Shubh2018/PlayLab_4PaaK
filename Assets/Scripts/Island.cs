using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Island : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private KeyboardSequenceController keyboardSequenceController;
    [SerializeField] private char[] keyboardSequence;
    [SerializeField] private TMP_Text _sequenceText;
    [SerializeField] private Transform _border;

    [SerializeField] private SpriteRenderer _borderRenderer;
    [SerializeField] private Vector2 _rotationAxis;

    private ShipController _shipController;
    public ShipController ShipController => _shipController;

    private ShipController _currentlyControlledBy;

    public ShipController CurrentlyControlledBy => _currentlyControlledBy;

    public Vector2 Center => transform.position;
    public float Radius => _radius;
    public Vector2 Axes => _rotationAxis;
    public char[] KeyboardSequence => keyboardSequence;
    public int ArrayPointer { get; set; } = 0;

    protected List<ShipController> _controllersNearIsland;
 
    protected virtual void Start()
    {
        //_borderRenderer = GetComponent<SpriteRenderer>();
        
        _controllersNearIsland = new List<ShipController>();

        keyboardSequenceController = FindAnyObjectByType<KeyboardSequenceController>();
        keyboardSequence = keyboardSequenceController.GenerateRandomSequence();

        keyboardSequence = Array.Empty<char>();
        UpdateBorderSize();

        StartCoroutine(SetSequence());
    }

    void Update()
    {
        //UpdateSequenceText(KeyboardSequence);
    }

    public IEnumerator SetSequence()
    {
        yield return new WaitWhile(() => !GameManager.Instance.StartGame);
        
        UpdateSequenceText(keyboardSequence);
    }

    private void UpdateSequenceText(char[] sequence)
    {
        GameManager.Instance.SetSeqeunce(_shipController, sequence);
        
        // _sequenceText.text = String.Empty;
        //
        // if (sequence.Length == 0) return;
        //
        // foreach (char c in sequence)
        // {
        //     _sequenceText.text += $"{c} ";
        // }
    }

    public virtual void ConquerIsland()
    {
        if (!_shipController) return;

        _shipController.AddConquredIslandToList();
        ChangeAllegiance();

        _currentlyControlledBy = _shipController;
        _borderRenderer.color = _currentlyControlledBy.TeamColor;

        keyboardSequence = Array.Empty<char>();
        UpdateSequenceText(keyboardSequence);
        
        ShipController.NextLevel();

        Debug.Log($"{this.name} Conquered by {_currentlyControlledBy.name}");

        //UpdateSequenceText(Array.Empty<char>());
    }

    public void SequenceKeyPressed()
    {
        UpdateSequenceText(keyboardSequence);
    }
    
    public void UpdateBorderSize()
    {
        Vector3 scale = Vector3.zero;
        scale = (_rotationAxis * 2) + new Vector2(1, 1);
        
        _border.localScale = scale / transform.localScale.x;
    }

    public void SetShipController(ShipController shipController)
    {
        //AddControllerToList(shipController);
        _shipController = shipController;
        keyboardSequence = keyboardSequenceController.GenerateRandomSequence();
        UpdateSequenceText(keyboardSequence);
        Debug.Log($"ShipCount: {_controllersNearIsland.Count}");
    }

    public void AddControllerToList(ShipController shipController)
    {
        _controllersNearIsland.Add(shipController);
        CanToggleSequence();
    }

    public void RemoveControllerFromList(ShipController shipController)
    {
        _controllersNearIsland.Remove(shipController);
        CanToggleSequence();
    }

    public void CanToggleSequence()
    {
        Debug.Log("CanToggleSequence Called!");
        
        if (_controllersNearIsland.Count == 1)
        {
            if(_currentlyControlledBy == _controllersNearIsland[0])
                keyboardSequence = Array.Empty<char>();
            else
                keyboardSequence = keyboardSequenceController.GenerateRandomSequence();
            
            Debug.Log($"CanToggleSequence Called In IF! : {keyboardSequence.Length}");
        }
        else
            keyboardSequence = Array.Empty<char>();
        
        UpdateSequenceText(keyboardSequence);
    }

    public void UnsetShipController(ShipController shipController)
    {
        if (!shipController) return;
        
        RemoveControllerFromList(shipController);

        if (_controllersNearIsland.Count <= 0)
        {
            _shipController = null;
            return;
        }
        
        _shipController = _controllersNearIsland[0];
    }

    private void ChangeAllegiance()
    {
        if (!_currentlyControlledBy) return;
        _currentlyControlledBy.RemoveConqueredIslandFromList(this);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag(Constants.Player))
    //     {
    //         //keyboardSequence = keyboardSequenceController.GenerateRandomSequence();
    //         _shipController = other.GetComponent<ShipController>();
    //         //UpdateSequenceText(keyboardSequence);
    //         _shipController.SetIsland(this);
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.CompareTag(Constants.Player))
        {
            keyboardSequence = Array.Empty<char>();
            UpdateSequenceText(keyboardSequence);
            return;
        }*/
    }

    public virtual void EnableFillImages()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}