using System;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private KeyboardSequenceController keyboardSequenceController;
    [SerializeField] public char[] keyboardSequence;

    public Vector2 Center => transform.position;
    public float Radius => _radius;
    public char[] KeyboardSequence => keyboardSequence;

    public int ArrayPointer{get; set;} = 0;
    void Start()
    {
        keyboardSequenceController = FindAnyObjectByType<KeyboardSequenceController>();
        keyboardSequence = keyboardSequenceController.GenerateRandomSequence();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Player))
        {
            ShipController controller = other.GetComponent<ShipController>();
            controller.SetIsland(this);
            return;
        }
    }
}
