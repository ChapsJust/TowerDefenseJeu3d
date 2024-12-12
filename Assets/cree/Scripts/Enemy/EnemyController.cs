using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private List<Vector3> cheminPositions;
    private int cheminIndex = 0;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    public int vie;
    [SerializeField]
    private EnemyScriptableObject enemyScriptableObject;
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject player;
    private JoueurFpsControlleur joueurFpsControlleur;

    /// <summary>
    /// Intialize l'ennemi
    /// </summary>
    /// <param name="chemin"></param>
    public void Initialize(List<Vector3> chemin)
    {
        vie = enemyScriptableObject.vie;
        cheminPositions = new List<Vector3>(chemin);
        joueurFpsControlleur = player.GetComponent<JoueurFpsControlleur>();

    }

    private void Update()
    {
        if(cheminPositions == null || cheminPositions.Count == 0)
            return;
        if(slider != null ) 
            slider.value = vie;
        SuivreChemin();
    }

    /// <summary>
    /// Fait en sorte que l'ennemie suive le bon chemin
    /// </summary>
    private void SuivreChemin()
    {
        Vector3 target = cheminPositions[cheminIndex];
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, target) < 0.1f)
        {
            cheminIndex++;
            if(cheminIndex >= cheminPositions.Count)
            {
                joueurFpsControlleur.Vie--;
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Fait en sorte que L'ennemie recoit des dommages
    /// </summary>
    /// <param name="domage"></param>
    public void PrendreDegat(int domage)
    {
        vie -= domage;

        if(vie < 0)
        {
            Destroy(gameObject);
        }
    }
}
