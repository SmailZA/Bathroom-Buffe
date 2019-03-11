using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem : MonoBehaviour
{
    public GameObject projectilePrefab;

    [SerializeField] List<Projectile> projectiles = new List<Projectile>();

    public void Tick()
    {
        foreach(Projectile p in projectiles)
        {
            p?.Tick();
        }
    }

    public void SpawnProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject newProjectileGO = Instantiate(projectilePrefab, position, rotation);
        Projectile newProjectile = newProjectileGO.GetComponent<Projectile>();

        if (newProjectile)
        {
            projectiles.Add(newProjectile);
            newProjectile.Launch();
        }
    }

    public void RemoveProjectile()
    {

    }
}
