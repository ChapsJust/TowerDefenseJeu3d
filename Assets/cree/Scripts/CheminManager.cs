using System.Collections.Generic;
using UnityEngine;

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

    private GameObject[,] grid;

    public List<Vector3> cheminPositions = new List<Vector3>();

    private void Start()
    {
        GenerationGrid();
        GenerationChemin();
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
        int x = 0;
        int y = Random.Range(0, hauteurGrid);
        int ancienY = y;

        while(x < largeurGrid)
        {
            int direction = 0;

            if(x < largeurGrid - 1)
            {
                direction = Random.Range(0, 3);

                if (direction == 1 && y < hauteurGrid - 1)
                    y++;
                else if (direction == 2 && y > 0)
                    y--;
                else
                    direction = 0;
            }
            RemplacerSolParChemin(x, y, ancienY);
            ancienY = y;
            x++;
        }
    }

    private void RemplacerSolParChemin(int x, int y, int ancienY)
    {
        if(grid[x, y] != null)
        {
            Destroy(grid[x, y]);
            Vector3 position = new Vector3(x * prefabsSize, 0, y * prefabsSize);
            grid[x, y] = Instantiate(cheminPrefab, position, Quaternion.identity, transform);

            cheminPositions.Add(position);
        }

        if(ancienY != y && x - 1 >= 0)
        {
           int yStart = Mathf.Min(ancienY, y);
            int yFin = Mathf.Max(ancienY, y);

            for(int i = yStart; i <= yFin; i++)
            {
                if (grid[x - 1, i] != null)
                {
                    Destroy(grid[x - 1, i]);
                    Vector3 position = new Vector3((x -1) * prefabsSize, 0, i * prefabsSize);
                    grid[x - 1, i] = Instantiate(cheminPrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }
}
