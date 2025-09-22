using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BenchSouthSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] characterPrefabs; // all 11
    public GameObject[] rescaledPrefabs;  // only prefabs needing custom scale

    [Header("Seating Setup")]
    public int seatsPerSide = 10;
    public float seatSpacing = 0.5f;
    public Vector3 centerPosition = new Vector3(-5, 0, -62);

    // Track all spawned characters
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    [ContextMenu("Spawn Seats")]
    void SpawnSeats()
    {
        if (characterPrefabs.Length == 0)
        {
            Debug.LogError("No character prefabs assigned!");
            return;
        }

        // ===== Clear previously spawned characters =====
        for (int i = spawnedCharacters.Count - 1; i >= 0; i--)
        {
            if (spawnedCharacters[i] != null)
                DestroyImmediate(spawnedCharacters[i]);
        }
        spawnedCharacters.Clear();

        // ===== Spawn LEFT side =====
        for (int i = 0; i < seatsPerSide; i++)
        {
            Vector3 spawnPos = centerPosition + new Vector3(-seatSpacing * (i + 1), 0, 0);
            SpawnRandomCharacter(spawnPos);
        }

        // ===== Spawn RIGHT side =====
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

        // Instantiate as child
        GameObject obj = PrefabUtility.InstantiatePrefab(prefab, transform) as GameObject;
        obj.name = prefab.name;
        obj.transform.position = position;

        // Apply custom scale if in rescaledPrefabs
        if (System.Array.Exists(rescaledPrefabs, p => p == prefab))
        {
            obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.5f);
        }
        else
        {
            obj.transform.localScale = prefab.transform.localScale;
        }

        // Track this object for safe clearing next time
        spawnedCharacters.Add(obj);
    }
}
