using UnityEngine;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class ShipController : MonoBehaviour
{
    [SerializeField] private Island _island;
    [SerializeField] private float _angularSpeed = 180.0f;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private InputManager.Player _player;
    [SerializeField] private SpriteRenderer _shipGFX;
    
    public int CurrentIslandCount { private set; get; } = 0;
    
    public ShipController() { _points = 0; }

    public InputManager.Player Player => _player;

    private Vector2 _positionOnCircumference;
    private Vector2 _tangentDir;
    private float _angle;

    private int _points = 0;
    public int Points => _points;

    [SerializeField] private Color _teamColor;
    public Color TeamColor => _teamColor;

    [SerializeField] private string _playerName;
    public string PlayerName => _playerName;

    private List<Island> _conquredIslands = new List<Island>();
    public List<Island> ConqueredIslands => _conquredIslands;

    public Island TargetIsland => _island;
    
    private Island mostRecentIsland;
    public Island MostRecentIsland => mostRecentIsland;

    private Vector3 _dir;

    void Start()
    {
        transform.position = _island.Center;
        
        SetIsland(_island);
    }

    private void Update()
    {
        //CircleIsland();
        EllipticalMovement();
        LeaveIsland();
    }

    private void CircleIsland()
    {
        if (!_island)
        {
            transform.Translate(transform.up * (_speed * Time.deltaTime), Space.World);
            Vector3 pos = transform.position;

            if (transform.position.x > 10 || transform.position.x < -10)
            {
                pos.x *= -1;
                transform.position = pos;
            }

            if (transform.position.y > 5 || transform.position.y < -5)
            {
                pos.y *= -1;
                transform.position = pos;
            }

            SetIsland(GameManager.Instance.ReturnClosestIsland(this));
                
            return;
        }
        
        Vector2 center = _island.Center;
        float radius = _island.Radius;
        
        float angularSpeed = _angularSpeed * InputManager.GetPlayerRotationDirection(_player);
        
        //_shipGFX.flipX = !(angularSpeed < 0);
        
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

    private void EllipticalMovement()
    {
        if (!_island)
        {
            transform.Translate(Vector3.Normalize(_tangentDir) * (_speed * Time.deltaTime), Space.World);
            Vector3 pos = transform.position;

            if (transform.position.x > 10 || transform.position.x < -10)
            {
                pos.x *= -1;
                transform.position = pos;
            }

            if (transform.position.y > 5 || transform.position.y < -5)
            {
                pos.y *= -1;
                transform.position = pos;
            }

            SetIsland(GameManager.Instance.ReturnClosestIsland(this));
                
            return;
        }
        
        Vector2 center = _island.Center;
        //float radius = _island.Radius;

        Vector2 ellipseAxis = _island.Axes;
        
        float angularSpeed = _angularSpeed * InputManager.GetPlayerRotationDirection(_player);
        
        _angle += angularSpeed * Time.deltaTime;
        //equation of ellipse = ((x - h)2 / a2) + ((y - k)2 / b2) = 1 
        
        if (_angle >= 2 * Mathf.PI)
            _angle = 0;
        
        Vector2 positionOnEllipse = center + new Vector2(ellipseAxis.x * Mathf.Cos(_angle), ellipseAxis.y * Mathf.Sin(_angle));
        transform.position = positionOnEllipse;

        float tangentX = -ellipseAxis.x * Mathf.Sin(_angle);
        float tangentY = ellipseAxis.y * Mathf.Cos(_angle);

        _tangentDir = Vector3.Normalize(new Vector3(tangentX, tangentY, 0.0f) * InputManager.GetPlayerRotationDirection(_player));
        
        float angleInDegrees = _angle * Mathf.Rad2Deg;
        _shipGFX.transform.localScale = new Vector2(FlipShip() * FlipShipByPosition(angleInDegrees), 1f);
    }

    private void LeaveIsland()
    {
        Debug.Log($"{InputManager.GetLaunchPressed(_player) == 0} : {!_conquredIslands.Contains(_island)}");
        if (InputManager.GetLaunchPressed(_player) == 0) return;

        if (!_island) return;
        
        InputManager.SetLaunchPressedFalse(_player);
        _island.UnsetShipController(this);
        _island = null;
    }

    public void SetIsland(Island island)
    {
        if (!island) return;
        
        _island = island;
        
        Vector3 directionVector = transform.position - new Vector3(_island.Center.x, _island.Center.y, 0);
        _angle = Mathf.Atan2(directionVector.y, directionVector.x);
        
        _island.SetShipController(this);

        if (_island is DestinationIsland)
        {
            _island = (DestinationIsland)_island;
            _island.EnableFillImages();
        }

        mostRecentIsland = island;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(Constants.WallTag))
        {
            transform.position = mostRecentIsland.transform.position;
            SetIsland(mostRecentIsland);
        }
    }
    
    public void NextLevel()
    {
        Debug.Log($"{CurrentIslandCount} : {GameManager.Instance.IslandCount}");
        if (CurrentIslandCount >= GameManager.Instance.IslandCount)
            return;
        
        CurrentIslandCount += 1;
    }

    private float FlipShip()
    {
        return InputManager.GetPlayerRotationDirection(_player);
    }

    private float FlipShipByPosition(float angle)
    {
        float scale = 1;
        
        if (angle is >= 0 and < 180.0f)
            scale = -1;
        
        return scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _island.Center - new Vector2(_island.Axes.x / 2, 0) - new Vector2(transform.position.x, transform.position.y));
        Gizmos.DrawRay(transform.position, _island.Center + new Vector2(_island.Axes.x / 2, 0) - new Vector2(transform.position.x, transform.position.y));

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, _tangentDir * 2.0f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector3.back * 2.0f);
    }
}