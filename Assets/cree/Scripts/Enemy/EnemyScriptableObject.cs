using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/EnemyScriptableObject")]
public class EnemyScriptableObject : ScriptableObject
{
    public string enemyNom;
    public int vie;
    public float speed;
    public int valeurPiece;
    public GameObject prefabEnnemy;
}
