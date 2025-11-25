using UnityEngine;

public class IslandSpawner : MonoBehaviour
{
    [SerializeField] private Island _island;

    public void SpawnIsland()
    {
        Island island = Instantiate(_island, transform.position, Quaternion.identity);
        Debug.Log($"{island.ShipController}");
        GameManager.Instance.AddIsland(island);
    }
}