using System;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private float _angularSpeed;
    [SerializeField] private Transform _cannonBallPrefab;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private Transform _bulletSpawn;

    private ShipController _shipController;
    private float _nextTimeToFire = 0;

    private void Start()
    {
        _shipController = GetComponentInParent<ShipController>();
    }

    private void Update()
    {
        float angle = InputManager.GetTeamCannonDirection(_shipController.Team);

        Vector3 eulerAngles = transform.localEulerAngles;
        eulerAngles.z += angle * Time.deltaTime * _angularSpeed;
        transform.localEulerAngles = eulerAngles;

        Shoot();
    }

    private void Shoot()
    {
        if (InputManager.GetShootPressed(_shipController.Team) == 0) return;

        if (_nextTimeToFire < Time.time)
        {
            Fire();
            _nextTimeToFire = Time.time + _fireRate;
        }
    }

    private void Fire()
    {
        Instantiate(_cannonBallPrefab, _bulletSpawn.position, _bulletSpawn.rotation);
    }
}