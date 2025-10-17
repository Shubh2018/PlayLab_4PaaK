using System;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private float _radius;
    
    public Vector2 Center => transform.position;
    public float Radius => _radius;

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
