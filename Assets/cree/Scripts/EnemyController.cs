using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Vector3> cheminPositions;
    private int cheminIndex = 0;
    [SerializeField]
    private float speed = 2f;

    public void Initialize(List<Vector3> chemin)
    {
        cheminPositions = new List<Vector3>(chemin);
    }

    private void Update()
    {
        if(cheminPositions == null || cheminPositions.Count == 0)
            return;
        SuivreChemin();
    }

    private void SuivreChemin()
    {
        Vector3 target = cheminPositions[cheminIndex];
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, target) < 0.1f)
        {
            cheminIndex++;
            if(cheminIndex >= cheminPositions.Count)
                Destroy(gameObject);
        }
    }
}
