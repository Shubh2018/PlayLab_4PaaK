using UnityEngine;
using System.Collections.Generic;

public class ShipController : MonoBehaviour
{
    [SerializeField] private Island _island;
    [SerializeField] private float _angularSpeed = 180.0f;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private InputManager.Team _team;
    [SerializeField] private Transform _shipGFX;

    public InputManager.Team Team => _team;

    private Vector2 _positionOnCircumference;
    private Vector2 _tangentDir;
    private float _angle;

    [SerializeField] private Color _teamColor;
    public Color TeamColor => _teamColor;

    [SerializeField] private CannonController _cannon;

    private List<Island> _conquredIslands = new List<Island>();

    void Start()
    {
        _shipGFX.transform.position = _island.Center;
    }

    private void Update()
    {
        CircleIsland();
        LeaveIsland();
    }

    private void CircleIsland()
    {
        if (!_island)
        {
            _shipGFX.transform.Translate(_shipGFX.transform.up * (_speed * Time.deltaTime), Space.World);

            if (transform.position.x >= 10 || transform.position.x <= -10)
            {
                Vector3 pos = transform.position;
                pos.x *= -1;
                _shipGFX.transform.position = pos;
            }
            
            if (transform.position.y >= 5 || transform.position.y <= -5)
            {
                Vector3 pos = transform.position;
                pos.y *= -1;
                _shipGFX.transform.position = pos;
            }
                
            return;
        }
        
        Vector2 center = _island.Center;
        float radius = _island.Radius;

        _angle += (Mathf.Abs(_angularSpeed) * InputManager.GetTeamRotationDirection(_team)) * Time.deltaTime;

        _positionOnCircumference = new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle)) * radius;
        Vector2 offset = center + _positionOnCircumference;

        Vector3 centerDir = offset - center;

        _tangentDir = Vector3.Cross(centerDir, Vector3.back) *
                      InputManager.GetTeamRotationDirection(_team);

        Vector3 currentPosition = new Vector3(offset.x, offset.y, 0.0f);
        
        _shipGFX.transform.position = currentPosition;
        _shipGFX.transform.up = Vector3.Normalize(_tangentDir);
    }

    private void LeaveIsland()
    {
        if (InputManager.GetLaunchPressed((_team)) == 0) return;
        
        _island = null;
    }

    public void SetIsland(Island island)
    {
        _island = island;
        Vector3 directionVector = transform.position - new Vector3(_island.Center.x, _island.Center.y, 0);
        _angle = Mathf.Atan2(directionVector.y, directionVector.x);
    }

    public void AddConquredIslandToList()
    {
        if (!_island) return;

        InputManager.Team controllingTeam = InputManager.Team.None;
        
        if (_island.CurrentlyControlledBy)
            controllingTeam = _island.CurrentlyControlledBy.Team;
            
        if (controllingTeam == _team)
            return;
        
        _conquredIslands.Add(_island);
    }

    public void RemoveConqueredIslandFromList(Island island)
    {
        if (!_conquredIslands.Contains(island)) return;
        
        _conquredIslands.Remove(island);
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