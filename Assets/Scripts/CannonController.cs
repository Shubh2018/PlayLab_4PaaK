using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private float _angularSpeed;
    [SerializeField] private Transform _cannonBallPrefab;

    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private Transform _bulletSpawn;

    [SerializeField] private ShipController _shipController;
    private float _nextTimeToFire = 0;
    
    //private float currentAngle = 0;

    private void Update()
    {
        transform.position = _shipController.transform.position;
        RotateCannon();
        Shoot();
    }

    private void RotateCannon()
    {
        float angle = InputManager.GetTeamCannonDirection(_shipController.Team);

        transform.Rotate(Vector3.forward * (angle * _angularSpeed * Time.deltaTime));
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