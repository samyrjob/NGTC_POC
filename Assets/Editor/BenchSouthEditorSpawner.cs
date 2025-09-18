using UnityEngine;
using UnityEditor;

public class BenchSouthEditorSpawner : EditorWindow
{
    public GameObject characterPrefab;
    public float startX = -7.7f;
    public float y = -0.15f;
    public float z = -61.931f;
    public float spacing = 0.5f;
    public int count = 1000;

    [MenuItem("Tools/Bench South Spawner")]
    public static void ShowWindow()
    {
        GetWindow<BenchSouthEditorSpawner>("Bench South Spawner");
    }

    void OnGUI()
    {
        characterPrefab = (GameObject)EditorGUILayout.ObjectField("Character Prefab", characterPrefab, typeof(GameObject), false);
        startX = EditorGUILayout.FloatField("Start X", startX);
        y = EditorGUILayout.FloatField("Y", y);
        z = EditorGUILayout.FloatField("Z", z);
        spacing = EditorGUILayout.FloatField("Spacing", spacing);
        count = EditorGUILayout.IntField("Count Each Direction", count);

        if (GUILayout.Button("Spawn Bench"))
        {
            SpawnBench();
        }
    }

    void SpawnBench()
    {
        if (characterPrefab == null)
        {
            Debug.LogError("Character Prefab is not assigned!");
            return;
        }

        GameObject parent = new GameObject("BenchSouthRow");

        // Spawn characters decrementing X
        for (int i = 1; i <= count; i++)
        {
            Vector3 pos = new Vector3(startX - i * spacing, y, z);
            GameObject obj = PrefabUtility.InstantiatePrefab(characterPrefab) as GameObject;
            obj.transform.position = pos;
            obj.transform.parent = parent.transform;
        }

        // Spawn characters incrementing X
        for (int i = 1; i <= count; i++)
        {
            Vector3 pos = new Vector3(startX + i * spacing, y, z);
            GameObject obj = PrefabUtility.InstantiatePrefab(characterPrefab) as GameObject;
            obj.transform.position = pos;
            obj.transform.parent = parent.transform;
        }

        // Spawn one at the start position
        GameObject centerObj = PrefabUtility.InstantiatePrefab(characterPrefab) as GameObject;
        centerObj.transform.position = new Vector3(startX, y, z);
        centerObj.transform.parent = parent.transform;

        Debug.Log("Bench South spawned in Scene!");
    }
}
