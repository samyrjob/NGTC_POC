using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]   // Run in Edit Mode
public class BenchSouthSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] characterPrefabs; // assign all your 11 prefabs here
    public GameObject[] rescaledPrefabs;  // assign prefabs that need special scaling

    [Header("Seating Setup")]
    public int seatsPerSide = 10;
    public float seatSpacing = 0.5f;
    public Vector3 centerPosition = new Vector3(-5, 0, -62);

    [ContextMenu("Spawn Seats")] // Right-click → Spawn Seats
    void SpawnSeats()
    {
        if (characterPrefabs.Length == 0)
        {
            Debug.LogError("No character prefabs assigned!");
            return;
        }

        // ===== Clear old spawned characters =====
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("StadiumCharacter"))
            {
                DestroyImmediate(child.gameObject);
            }
        }

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

        // Instantiate as child of spawner
        GameObject obj = PrefabUtility.InstantiatePrefab(prefab, transform) as GameObject;
        obj.name = prefab.name;                  // keep prefab name
        obj.tag = "StadiumCharacter";            // tag to identify spawned characters
        obj.transform.position = position;

        // Apply custom scale only if in rescaledPrefabs array
        if (System.Array.Exists(rescaledPrefabs, p => p == prefab))
        {
            obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.5f);
        }
        else
        {
            obj.transform.localScale = prefab.transform.localScale; // default
        }
    }
}
