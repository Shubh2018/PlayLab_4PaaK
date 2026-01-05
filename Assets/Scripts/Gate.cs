using UnityEngine;
using System.Collections;

public class Gate : MonoBehaviour
{
    [SerializeField] private ShipController ship;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float speed;
    [SerializeField] private float amount;
    [SerializeField] private float dim;
    private Vector3 startPosition;
    Vector3 direction = Vector3.left;
    private bool particle_started = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        UpdatePlayerConquerCount();
    }

    void UpdatePlayerConquerCount()
    {
        if (true)//(ship.CurrentIslandCount >= GameManager.Instance.IslandCount)
        {
            StartCoroutine("Tremble");
        }
        
    }

    private IEnumerator Tremble()
    {
        if (particle_started == false)
        {
            particle_started = true;
            _particle.Play();
        }
        transform.position = startPosition + direction * Mathf.Sin(speed * Time.time) * amount;
        Color color = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        color.a -= (Time.time) * dim;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
