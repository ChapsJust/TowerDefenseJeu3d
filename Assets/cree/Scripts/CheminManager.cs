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

    private GameObject[,] grid;
    private int curX;
    private int curY;
    private int exDirection;

    public List<Vector3> cheminPositions = new List<Vector3>();

    private void Start()
    {
        GenerationGrid();
        GenerationChemin();
        Debug.Log(cheminPositions.Count);
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
        int curY = 0;
        int prevY = curY;

        for (int curX = 0;  curX < largeurGrid; curX++) 
        {
            Vector3 position = new Vector3(curX * prefabsSize, 0, curY * prefabsSize);
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
                Vector3 coinPosition = new Vector3(curX * prefabsSize, 0, curY * prefabsSize);
                if (!cheminPositions.Contains(coinPosition))
                {
                    cheminPositions.Add(coinPosition);
                    Instantiate(cheminPrefab, coinPosition, Quaternion.identity, transform);
                }
            }

            prevY = curY;
        }
    }
}
