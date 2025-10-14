using System;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private float _cannonBallSpeed;
    [SerializeField] private float _cannonBallLife = 5.0f;
    private void Start()
    {
        Destroy(this.gameObject, _cannonBallLife);
    }

    private void Update()
    {
        this.transform.Translate(Vector3.up * (_cannonBallSpeed * Time.deltaTime), Space.Self);
    }
}
