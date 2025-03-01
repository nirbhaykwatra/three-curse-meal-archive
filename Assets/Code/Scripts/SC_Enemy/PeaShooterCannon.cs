using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooterCannon : MonoBehaviour
{
    [SerializeField] private float shootCooldown = .2f;
    [SerializeField] private KeyCode input = KeyCode.Mouse0;
    [SerializeField] protected GameObject bulletPrefab;
    private float nextShootTime = 0;
    protected virtual void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
        bullet.SpawnBullet(this.gameObject);
        if (bulletPrefab == null) return;
    }
}
