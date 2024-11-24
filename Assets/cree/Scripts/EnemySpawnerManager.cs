using System.Collections;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private CheminManager cheminManager;
    [SerializeField]
    private float spawnIntervalle = 2f;

    private void Start()
    {
        
    }

    private IEnumerator DelaiSpawn()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnIntervalle);
        }
    }

    private void SpawnEnemy()
    {
        if (cheminManager.cheminPositions.Count > 0)
        {
            Vector3 spawnPosition = cheminManager.cheminPositions[0];
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Aucun chemin thats bad");
        }
    }
}
