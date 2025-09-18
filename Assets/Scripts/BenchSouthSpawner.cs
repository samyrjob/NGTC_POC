using UnityEngine;

public class BenchSouthSpawner : MonoBehaviour
{
    public GameObject characterPrefab; // Drag your character prefab here

    public float startX = -7.7f;       // Starting X coordinate
    public float y = -0.15f;           // Constant Y coordinate
    public float z = -61.931f;         // Constant Z coordinate

    public float spacing = 0.5f;       // Distance between characters
    public int count = 1000;           // Number of characters in each direction

    void Start()
    {
        // Spawn characters decrementing X
        for (int i = 1; i <= count; i++)
        {
            float xPos = startX - i * spacing;
            Vector3 pos = new Vector3(xPos, y, z);
            Instantiate(characterPrefab, pos, Quaternion.identity);
        }

        // Spawn characters incrementing X
        for (int i = 1; i <= count; i++)
        {
            float xPos = startX + i * spacing;
            Vector3 pos = new Vector3(xPos, y, z);
            Instantiate(characterPrefab, pos, Quaternion.identity);
        }

        // Optional: spawn one at the start position
        Instantiate(characterPrefab, new Vector3(startX, y, z), Quaternion.identity);
    }
}
