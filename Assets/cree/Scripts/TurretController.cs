using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    private TurretScriptableObject turretScriptableObject;

    private float frequqenceTir;

    private void Update()
    {
        if (frequqenceTir > 0)
            frequqenceTir -= Time.deltaTime;

        Collider[] hits = Physics.OverlapSphere(transform.position, turretScriptableObject.distanceTir);
        foreach (Collider hit in hits )
        {
            if(hit.CompareTag("Enemy") && turretScriptableObject.frequenceTir <= 0)
            {
                //TirTarget();
                turretScriptableObject.frequenceTir = 1f / turretScriptableObject.frequenceTir;
                break;
            }
        }
    }
}
