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
        StartCoroutine(WaitForPathAndSpawn());
    }

    //V�rification n�cessaire sinon probl�me de crash cela v�rifie le chemin est fait avant de faire spawn
    private IEnumerator WaitForPathAndSpawn()
    {
        while (cheminManager.cheminPositions == null || cheminManager.cheminPositions.Count == 0)
        {
            yield return null; 
        }

        Debug.Log("Le Chemin est correctement cr�er spawn d'enemy d�buter");
        StartCoroutine(DelaiSpawn());
    }

    //Un intervalle entre chaque ennemie
    private IEnumerator DelaiSpawn()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnIntervalle);
        }
    }

    /// <summary>
    /// Fonctiuon qui permet de faire spawn les ennemies a la bonne place et donne les positions du chemin
    /// </summary>
    private void SpawnEnemy()
    {
        if (cheminManager.cheminPositions.Count > 0)
        {
            Vector3 spawnPosition = cheminManager.cheminPositions[0];
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
                enemyController.Initialize(cheminManager.cheminPositions);
            else
                Debug.LogError("Probleme aussi fr�rot :(");
        }
        else
        {
            Debug.LogError("Aucun chemin thats bad");
        }
    }
}
