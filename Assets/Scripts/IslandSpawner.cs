using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    [SerializeField] private Island _island;
    [SerializeField] private Island _checkpointIsland;

    public void SpawnIsland(int currentLevel)
    {
        Island island = null;
        
        if(currentLevel < GameManager.Instance.IslandCount)
            island = _checkpointIsland;
        else
            island = _island;
        
        Island spawnedIsland = Instantiate(island, transform.position, Quaternion.identity);
        Debug.Log($"{island.ShipController}");
        GameManager.Instance.AddIsland(spawnedIsland);
    }
}