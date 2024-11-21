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

    private void Start()
    {
        GenerationGrid();
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
}
