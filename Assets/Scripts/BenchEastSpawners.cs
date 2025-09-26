using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BenchEastSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] characterPrefabs; // all 12
    public GameObject[] rescaledPrefabs;  // only prefabs needing custom scale

    [Header("Seating Setup")]
    public int seatsPerSide = 60;
    public float seatSpacing = 0.5f;
    public Vector3 centerPosition = new Vector3(50f, 3.56f, -25f);

    // Track all spawned characters
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    [ContextMenu("Spawn Stadium")]
    void SpawnStadium()
    {
        if (characterPrefabs.Length == 0)
        {
            Debug.LogError("No character prefabs assigned!");
            return;
        }

#if UNITY_EDITOR
        Undo.RecordObject(this, "Spawn Stadium");
#endif

        // ===== Clear previously spawned characters =====
        for (int i = spawnedCharacters.Count - 1; i >= 0; i--)
        {
            if (spawnedCharacters[i] != null)
            {
#if UNITY_EDITOR
                Undo.DestroyObjectImmediate(spawnedCharacters[i]);
#else
                DestroyImmediate(spawnedCharacters[i]);
#endif
            }
        }
        spawnedCharacters.Clear();

        // ===== CENTER ROW =====
        SpawnRow(centerPosition);

        // ===== ROWS ABOVE CENTER =====
        for (int row = 1; row <= 14; row++)
        {
            Vector3 rowPos = new Vector3(
                centerPosition.x,
                centerPosition.y + 0.5f * row,
                centerPosition.z - 0.8f * row
            );
            SpawnRow(rowPos);
        }

        // ===== ROWS BELOW CENTER =====
        for (int row = 1; row <= 14; row++)
        {
            Vector3 rowPos = new Vector3(
                centerPosition.x,
                centerPosition.y - 0.5f * row,
                centerPosition.z + 0.8f * row
            );
            SpawnRow(rowPos);
        }

#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    void SpawnRow(Vector3 centerRowPos)
    {
        // LEFT SIDE
        for (int i = 0; i < seatsPerSide; i++)
        {
            Vector3 spawnPos = centerRowPos + new Vector3(-seatSpacing * (i + 1), 0, 0);
            SpawnRandomCharacter(spawnPos);
        }

        // RIGHT SIDE
        for (int i = 0; i < seatsPerSide; i++)
        {
            Vector3 spawnPos = centerRowPos + new Vector3(seatSpacing * (i + 1), 0, 0);
            SpawnRandomCharacter(spawnPos);
        }
    }

    void SpawnRandomCharacter(Vector3 position)
    {
        int randomIndex = Random.Range(0, characterPrefabs.Length);
        GameObject prefab = characterPrefabs[randomIndex];

#if UNITY_EDITOR
        Undo.RegisterCreatedObjectUndo(prefab, "Spawn Character");
#endif

        GameObject obj = PrefabUtility.InstantiatePrefab(prefab, transform) as GameObject;
        obj.name = prefab.name;
        obj.transform.position = position;

        // Apply rotation around Y axis
        obj.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

        // Apply custom scale if in rescaledPrefabs
        if (System.Array.Exists(rescaledPrefabs, p => p == prefab))
        {
            obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.5f);
        }
        else
        {
            obj.transform.localScale = prefab.transform.localScale;
        }

        spawnedCharacters.Add(obj);

#if UNITY_EDITOR
        EditorUtility.SetDirty(obj);
#endif
    }
}
