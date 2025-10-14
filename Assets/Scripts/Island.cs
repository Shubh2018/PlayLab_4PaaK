using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private float _radius;
    
    public Vector2 Center => transform.position;
    public float Radius => _radius;
}
