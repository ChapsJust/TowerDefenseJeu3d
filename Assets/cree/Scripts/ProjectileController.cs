using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform target;
    private float speed;
    private int damage;

    public void Init(Transform enemyTarget, float projectileSpeed, int projectileDomage)
    {
        target = enemyTarget;
        speed = projectileSpeed;
        damage = projectileDomage;
    }

    private void Update()
    {
        if (target == null) 
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            EnemyController enemyController = target.GetComponentInParent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.PrendreDegat(damage);
                Destroy(gameObject); 
            }
            else
            {
                Debug.LogWarning($"WHAT ::::: {target.name}");
            }
        }
    }
}

