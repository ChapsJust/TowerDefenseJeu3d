using UnityEngine;

public class TurretPlaceHolder : MonoBehaviour
{
    [SerializeField]
    private TurretScriptableObject turretScriptableObject;
    private bool isJoueurInRange = false;
    private GameObject currentTurret;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isJoueurInRange = true;
            Debug.Log("Fait E");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isJoueurInRange = false;
        }
    }

    private void Update()
    {
        if (isJoueurInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (currentTurret == null)
            {
                PlacerTurret();
            }
            else
            {
                UpgradeTurret();
            }
        }
    }

    private void PlacerTurret()
    {
        currentTurret = Instantiate(turretScriptableObject.prefab, transform.position, Quaternion.identity);
    }

    private void UpgradeTurret()
    {
        if (turretScriptableObject.nextLevel != null)
        {
            turretScriptableObject = turretScriptableObject.nextLevel;
            Destroy(currentTurret);
            currentTurret = Instantiate(turretScriptableObject.prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Aucun Upgrade");
        }
    }
}
