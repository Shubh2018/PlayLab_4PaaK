using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private ShipController ship;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
