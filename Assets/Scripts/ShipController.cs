using UnityEngine;
using System.Collections.Generic;

public class ShipController : MonoBehaviour
{
    [SerializeField] private Island _island;
    [SerializeField] private float _angularSpeed = 180.0f;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private InputManager.Player _player;

    public InputManager.Player Player => _player;

    private Vector2 _positionOnCircumference;
    private Vector2 _tangentDir;
    private float _angle;

    private int _points = 0;
    public int Points => _points;

    [SerializeField] private Color _teamColor;
    public Color TeamColor => _teamColor;

    [SerializeField] private CannonController _cannon;

    private List<Island> _conquredIslands = new List<Island>();
    public List<Island> ConqueredIslands => _conquredIslands;

    public Island TargetIsland => _island;

    void Start()
    {
        transform.position = _island.Center;
        
        SetIsland(_island);
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
            transform.Translate(transform.up * (_speed * Time.deltaTime), Space.World);

            if ((transform.position.x >= 10 || transform.position.x <= -10) || (transform.position.y >= 5 || transform.position.y <= -5))
            {
                Vector3 pos = transform.position;
                pos *= -1;
                transform.position = pos;
            }

            SetIsland(GameManager.Instance.ReturnClosestIsland(this));
                
            return;
        }
        
        Vector2 center = _island.Center;
        float radius = _island.Radius;

        float angularSpeed = _angularSpeed * InputManager.GetPlayerRotationDirection(_player);
        
        _angle += angularSpeed * Time.deltaTime;

        _positionOnCircumference = new Vector2(Mathf.Cos(_angle), Mathf.Sin(_angle)) * radius;
        Vector2 offset = center + _positionOnCircumference;

        Vector3 centerDir = offset - center;

        _tangentDir = Vector3.Cross(centerDir, Vector3.back) *
                      angularSpeed;

        Vector3 currentPosition = new Vector3(offset.x, offset.y, 0.0f);
        
        transform.position = currentPosition;
        transform.up = Vector3.Normalize(_tangentDir);
    }

    private void LeaveIsland()
    {
        if (InputManager.GetLaunchPressed((_player)) == 0) return;

        if (!_island) return;
        
        _island.UnsetShipController();
        _island = null;
    }

    public void SetIsland(Island island)
    {
        if (!island) return;
        
        _island = island;
        Vector3 directionVector = transform.position - new Vector3(_island.Center.x, _island.Center.y, 0);
        _angle = Mathf.Atan2(directionVector.y, directionVector.x);
        
        _island.SetShipController(this);
    }

    public void AddConquredIslandToList()
    {
        if (!_island) return;

        InputManager.Player controllingPlayer = InputManager.Player.None;
        
        if (_island.CurrentlyControlledBy)
            controllingPlayer = _island.CurrentlyControlledBy.Player;
            
        if (controllingPlayer == _player)
            return;
        
        _conquredIslands.Add(_island);

        UpdatePoints(3);
    }

    public void RemoveConqueredIslandFromList(Island island)
    {
        if (!_conquredIslands.Contains(island)) return;
        
        _conquredIslands.Remove(island);
    }

    public void UpdatePoints(int addedPoints)
    {
        _points += addedPoints;
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