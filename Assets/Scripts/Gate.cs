using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private ShipController ship;
    
    void Update()
    {
        UpdatePlayerConquerCount();
    }

    void UpdatePlayerConquerCount()
    {
        if (ship.CurrentIslandCount >= GameManager.Instance.IslandCount)
        {
            gameObject.SetActive(false);
        }
    }
}
