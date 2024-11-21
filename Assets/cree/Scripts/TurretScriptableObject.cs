using UnityEngine;

[CreateAssetMenu(fileName = "TurretScriptableObject", menuName = "Scriptable Objects/TurretScriptableObject")]
public class TurretScriptableObject : ScriptableObject
{
    public string turretNom;
    public int cout;
    public int damage;
    public float distanceTir;
    public float frequenceTir;
    public GameObject prefab;
}
