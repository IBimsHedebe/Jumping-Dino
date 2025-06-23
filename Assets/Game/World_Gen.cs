using UnityEngine;

public class World_Gen : MonoBehaviour
{
    public GameObject[] platformPrefabs; // Asigning all Platform Prefabs
    public int numberOfPlatforms = 60;
    public float levelWidth = 10f;
    public float minY = 1f;
    public float maxY = 3f;
    public GameObject[] perkPrefabs; // Asigning all Perk Prefabs
    public float perkProp = 0.3f;

    private Vector3 spawnPosition = Vector3.zero;

    void Start()
    {
        Generator();
    }

    void Generator()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            // Generate Platforms
            GameObject platform = Instantiate(
                platformPrefabs[Random.Range(0, platformPrefabs.Length)],
                spawnPosition,
                Quaternion.identity
            );

            platform.transform.parent = transform; //Parenting it, to keep the Hirachy cleaner

            //Move to the next spawn location
            spawnPosition.x += Random.Range(1f, levelWidth);
            spawnPosition.y += Random.Range(minY, maxY);

            //Generate Perks
            float randomValue = Random.Range(0f, 1f);

            if (randomValue < perkProp)
            {
                GameObject perks = Instantiate(
                    perkPrefabs[Random.Range(0, perkPrefabs.Length)], // Proüabillizys are 1/3 for a perk to spawn
                    new Vector3(spawnPosition.x, spawnPosition.y + 4),
                    Quaternion.identity
                );
                perks.transform.parent = transform; //Parneting it, to keep the Hirachy cleaner
            }
        }
    }
}
