using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheminManager : MonoBehaviour
{
    [SerializeField]
    private int largeurGrid = 10;
    [SerializeField]
    private int hauteurGrid = 10;
    [SerializeField]
    private GameObject cheminPrefab;
    [SerializeField]
    private GameObject solPrefab;

    [SerializeField]
    private float prefabsSize;
    [SerializeField]
    private GameObject turretPlaceholderPrefab;
    [SerializeField]
    private int turretCount = 5; //Peut ne pas marcher si nombre abuser et map pas asssez grande

    private GameObject[,] grid;
    
    public List<Vector3> turretPositions = new List<Vector3>();
    
    public List<Vector3> cheminPositions = new List<Vector3>();

    private void Start()
    {
        GenerationGrid();
        GenerationChemin();
        PlaceTurretPlaceholders();
    }

    private void GenerationGrid()
    {
        grid = new GameObject[largeurGrid, hauteurGrid];

        for (int x = 0; x < largeurGrid; x++)
        {
            for (int y = 0; y < hauteurGrid; y++)
            {
                Vector3 position = new Vector3(x * prefabsSize, 0, y * prefabsSize);
                grid[x, y] = Instantiate(solPrefab, position, Quaternion.identity, transform);
            }
        }
    }

    private void GenerationChemin()
    {
        int curY = 5;
        int prevY = curY;

        for (int curX = 0;  curX < largeurGrid; curX++) 
        {
            Vector3 position = new Vector3(curX * prefabsSize, 0.02f, curY * prefabsSize);
            if (!cheminPositions.Contains(position))
            {
                cheminPositions.Add(position);
                Instantiate(cheminPrefab, position, Quaternion.identity, transform);
            }

            int direction = Random.Range(0, 3);

            if (direction == 1 && curY > 0) curY--; 
            if (direction == 2 && curY < hauteurGrid - 1) curY++;

            if (curY != prevY)
            {
                Vector3 coinPosition = new Vector3(curX * prefabsSize, 0.02f, curY * prefabsSize);
                if (!cheminPositions.Contains(coinPosition))
                {
                    cheminPositions.Add(coinPosition);
                    Instantiate(cheminPrefab, coinPosition, Quaternion.identity, transform);
                }
            }

            prevY = curY;
        }
    }

    private void PlaceTurretPlaceholders()
    { 
        int spacing = Mathf.Max(1, Mathf.RoundToInt((float)cheminPositions.Count / turretCount)); //Espacement éviter pendant génération tout collé sinon impossible
        int placerTurret = 0;

        for (int i = 0; i < cheminPositions.Count; i += spacing)
        {
            if (placerTurret >= turretCount) break; 

            Vector3 cheminPosition = cheminPositions[i];

            List<Vector3> possibleTurretPositions = new List<Vector3>
        {
            cheminPosition + new Vector3(prefabsSize, 0, 0), // Droit
            cheminPosition + new Vector3(-prefabsSize, 0, 0), // Gauche
            cheminPosition + new Vector3(0, 0, prefabsSize), // Up
            cheminPosition + new Vector3(0, 0, -prefabsSize) // Down
        };

            foreach (var pos in possibleTurretPositions)
            {
                if (PositionValideTurret(pos))
                {
                    turretPositions.Add(pos);
                    Instantiate(turretPlaceholderPrefab, pos, Quaternion.identity, transform);
                    placerTurret++;
                    break; 
                }
            }
        }
    }


    private bool PositionValideTurret(Vector3 position)
    {
        int gridX = Mathf.RoundToInt(position.x / prefabsSize);
        int gridY = Mathf.RoundToInt(position.z / prefabsSize);

        //Vérification si conditions respecter turrets bien placé
        return gridX >= 0 && gridX < largeurGrid &&
               gridY >= 0 && gridY < hauteurGrid &&
               !cheminPositions.Contains(position) &&
               !turretPositions.Contains(position);
    }
}
