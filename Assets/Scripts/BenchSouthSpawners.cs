using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]   // 👈 makes this run even when not playing
public class BenSouthSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] characterPrefabs; // assign your 11 prefabs

    [Header("Seating Setup")]
    public int seatsPerSide = 100;
    public float seatSpacing = 0.5f;
    public Vector3 centerPosition = new Vector3(-5, 0, -62);

    [ContextMenu("Spawn Seats")] // 👈 adds a right-click menu option
    void SpawnSeats()
    {
        if (characterPrefabs.Length == 0)
        {
            Debug.LogError("No character prefabs assigned!");
            return;
        }

        // Clear old children (so you don’t get duplicates every time)
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        // LEFT SIDE
        for (int i = 0; i < seatsPerSide; i++)
        {
            Vector3 spawnPos = centerPosition + new Vector3(-seatSpacing * (i + 1), 0, 0);
            SpawnRandomCharacter(spawnPos);
        }

        // RIGHT SIDE
        for (int i = 0; i < seatsPerSide; i++)
        {
            Vector3 spawnPos = centerPosition + new Vector3(seatSpacing * (i + 1), 0, 0);
            SpawnRandomCharacter(spawnPos);
        }
    }

    void SpawnRandomCharacter(Vector3 position)
    {
        int randomIndex = Random.Range(0, characterPrefabs.Length);
        GameObject prefab = characterPrefabs[randomIndex];
        GameObject obj = PrefabUtility.InstantiatePrefab(prefab, transform) as GameObject;
        obj.transform.position = position;
    }
}
