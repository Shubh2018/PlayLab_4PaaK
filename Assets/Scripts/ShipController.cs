using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ShipController : MonoBehaviour
{
    [SerializeField] private Island _island;
    [SerializeField] private float _angularSpeed = 180.0f;
    [SerializeField] private InputManager.Team _team;

    public InputManager.Team Team => _team;
    
    private Vector2 _positionOnCircumference;
    private Vector2 _tangentDir;
    private float _angle;

    void Start()
    {
        transform.position = _island.Center;
    }

    private void Update()
    {
        Vector2 center = _island.Center;
        float radius = _island.Radius;

        Vector3 currentPosition = transform.position;

        _angle += (Mathf.Abs(_angularSpeed) * InputManager.GetTeamRotationDirection(_team)) * Time.deltaTime;

        _positionOnCircumference = new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle)) * radius;
        Vector2 offset = center + _positionOnCircumference;

        Vector3 centerDir =  offset - center;

        _tangentDir = Vector3.Cross(centerDir, Vector3.back) *
                      InputManager.GetTeamRotationDirection(_team);

        currentPosition = new Vector3(offset.x, offset.y, 0.0f);

        transform.position = currentPosition;
        transform.up = Vector3.Normalize(_tangentDir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _island.Center - new Vector2(transform.position.x, transform.position.y));
        
        Gizmos.color = Color.green; 
        Gizmos.DrawRay(transform.position, transform.up * 2.0f);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector3.back * 2.0f);
    }
}