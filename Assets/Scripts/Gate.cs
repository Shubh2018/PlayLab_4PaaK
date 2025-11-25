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
        if (ship.ConqueredIslands.Count == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
