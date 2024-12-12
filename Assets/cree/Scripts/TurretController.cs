using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private TurretScriptableObject turretScriptableObject;
    [SerializeField]
    private Transform turretRotationPart;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform firePoint;

    private float frequqenceTir;

    private void Update()
    {
        if (frequqenceTir > 0)
        {
            frequqenceTir -= Time.deltaTime;
        }

        Transform closestEnemy = TrouverEnemyPlusProche();

        if (closestEnemy != null)
        {
            RotateTurret(closestEnemy);
            if (frequqenceTir <= 0f)
            {
                TirTurret(closestEnemy);
                frequqenceTir = 1f / turretScriptableObject.frequenceTir;
            }
        }
    }

    /// <summary>
    /// Permet de trouver lennemie le plus proche
    /// </summary>
    /// <returns></returns>
    private Transform TrouverEnemyPlusProche()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, turretScriptableObject.distanceTir);
        Transform closestEnemy = null;
        //Infinity qui est le nombre maximum positif d'un float Source: https://discussions.unity.com/t/what-is-the-real-value-of-mathf-infinity/2971/5
        float shortestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);

                // Et on utilise l'infinity pour comparer
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }

    private void RotateTurret(Transform enemy)
    {
        Vector3 direction = enemy.position - turretRotationPart.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = Quaternion.Lerp(turretRotationPart.rotation, lookRotation, Time.deltaTime * 20f).eulerAngles;
        turretRotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void TirTurret(Transform enemy)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            if (projectileController != null)
            {
                projectileController.Init(enemy, 10f, 10);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (turretScriptableObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, turretScriptableObject.distanceTir); 
        }
    }
}
