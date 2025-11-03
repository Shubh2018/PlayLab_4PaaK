using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private Island[] _islands;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        
        InputManager.EnableInput();
    }

    private void Start()
    {
        _islands = FindObjectsByType<Island>(FindObjectsSortMode.None);
    }

    public Island ReturnClosestIsland(ShipController ship)
    {
        if(ship.TargetIsland) return ship.TargetIsland;

        Island newTarget = null;
        
        foreach (Island island in _islands)
        {
            float distance = Mathf.Abs(Vector3.Distance(island.Center, ship.transform.position));

            if (distance <= island.Radius)
            {
                newTarget = island;
                break;
            }
        }
        
        return newTarget;
    }

    private void OnDestroy()
    {
        InputManager.DisableInput();
    }
}
