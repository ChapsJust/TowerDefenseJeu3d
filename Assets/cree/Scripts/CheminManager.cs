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
        curX = 0;
        curY = Random.Range(0, hauteurGrid);
        exDirection = 0;

        int targetX = largeurGrid;
        int prevDirection = 0;

        while(curX< largeurGrid) 
        {
            Vector3 position = new Vector3(curX * prefabsSize, 0, curY * prefabsSize);
            cheminPositions.Add(position);
            Instantiate(cheminPrefab, position, Quaternion.identity, transform);

            int direction = ProchaineDirection();

            if (direction == 0)
            {
                curX++;
            }
            if (direction == 1 && curY > 0)
            {
                curY--;
            }
            if (direction == 2 && curY < hauteurGrid - 1) 
            {
                curY++;
            }

            if (prevDirection != 0 && direction != prevDirection)
            {
                Vector3 coinPosition = new Vector3(curX * prefabsSize, 0, curY * prefabsSize);
                cheminPositions.Add(coinPosition);
                Instantiate(cheminPrefab, coinPosition, Quaternion.identity, transform);
            }

            prevDirection = direction;
        }
    }

    private int ProchaineDirection()
    {
        List<int> directionsPossibles = new List<int> { 0 };

        if (curY > 0) directionsPossibles.Add(1);

        if (curY < hauteurGrid - 1) directionsPossibles.Add(2);

        if (exDirection == 0) directionsPossibles.Add(0);

        int direction = directionsPossibles[Random.Range(0, directionsPossibles.Count)];
        exDirection = direction;
        return direction;
    }
}
