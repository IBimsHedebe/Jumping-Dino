using UnityEngine;

public class World_Gen : MonoBehaviour
{
    public int width = 50;
    public int height = 50;
    public GameObject tile;

    private int[,] dungeonGrid;

    void Start()
    {
        GenerateLayout();
        DrawDungeon();
    }

    void GenerateLayout()
    {
        dungeonGrid = new int[width, height];
        // Place rooms, connect with corridors...
    }

    void DrawDungeon()
    {
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            Vector3 pos = new Vector3(x, y, 0);
            Instantiate(tile, pos, Quaternion.identity);
        }
    }
}