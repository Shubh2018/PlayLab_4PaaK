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

    private bool _disappear = false;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _wallGFX;

    private void Start()
    {
        startPosition = _wallGFX.transform.position;
        StartCoroutine(Tremble());
    }

    void Update()
    {
        UpdatePlayerConquerCount();
    }

    void UpdatePlayerConquerCount()
    {
        if (ship.CurrentIslandCount >= GameManager.Instance.IslandCount)
        {
            _disappear = true;
        }
    }

    private IEnumerator Tremble()
    {
        yield return new WaitUntil(() => _disappear);
        
        _collider.gameObject.SetActive(false);

        float t = 0;

        while (t <= 1)
        {
            t += Time.deltaTime;
            
            float alpha = Mathf.Lerp(1, 0, t);
            _wallGFX.transform.position = startPosition + direction * (Mathf.Sin(speed * Time.time) * amount);
            
            yield return new WaitForEndOfFrame();
            
            Color color = _wallGFX.color;
            _wallGFX.color = new Color(color.r, color.g, color.b, alpha);
        }
        
        if (!_particle.isPlaying)
            _particle.Play();
        
        yield return new WaitForSeconds(2f);
        
        gameObject.SetActive(false);
    }
}
