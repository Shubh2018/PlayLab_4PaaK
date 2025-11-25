using UnityEngine;

public class CheckpointIsland : Island
{
    [SerializeField] private IslandSpawner[] _islandSpawner;
    
    protected override void Awake()
    {
        base.Awake();
    }

    public override void ConquerIsland()
    {
        base.ConquerIsland();

        if (_islandSpawner.Length <= 0)
            return;

        foreach (var spawner in _islandSpawner)
        {
            spawner.SpawnIsland();
        }
    }
}
