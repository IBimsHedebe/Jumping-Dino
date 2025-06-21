using UnityEngine;

public class World_Gen : MonoBehaviour
{
    public GameObject[] platformPrefabs; // Asigning all Prefabs
    public int numberOfPlatforms = 60;
    public float levelWidth = 10f;
    public float minY = 1f;
    public float maxY = 3f;

    private Vector3 spawnPosition = Vector3.zero;

    void Start()
    {
        GenerateWorld();
    }

    void GenerateWorld()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            GameObject platform = Instantiate(
                platformPrefabs[Random.Range(0, platformPrefabs.Length)],
                spawnPosition,
                Quaternion.identity
            );

            platform.transform.parent = transform; //Parenting it, to keep the Hirachy cleaner

            //Move to the next spawn location
            spawnPosition.x += Random.Range(1f, levelWidth);
            spawnPosition.y += Random.Range(minY, maxY);
        }
    }
}
