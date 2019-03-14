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

    public void SpawnProjectile(FlyController flyController)
    {
        GameObject newProjectileGO = Instantiate(projectilePrefab, flyController.transform.position, flyController.transform.rotation);
        Projectile newProjectile = newProjectileGO.GetComponent<Projectile>();

        if (newProjectile)
        {
            projectiles.Add(newProjectile);
            newProjectile.Launch(flyController);

            newProjectile.OnDestroyProjectile += RemoveProjectile;
        }
    }

    public void RemoveProjectile(Projectile projectile)
    {
        projectiles.Remove(projectile);
        Destroy(projectile.gameObject);
    }
}
