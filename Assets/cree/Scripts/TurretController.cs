using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private TurretScriptableObject turretScriptableObject;
    [SerializeField]
    private Transform turretRotation;

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
                frequqenceTir = 1f / turretScriptableObject.frequenceTir;
            }
        }
    }

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
        Vector3 direction = enemy.position - turretRotation.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = Quaternion.Lerp(turretRotation.rotation, lookRotation, Time.deltaTime * 50f).eulerAngles;
        turretRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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
