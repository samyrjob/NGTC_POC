using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

[ExecuteInEditMode]
public class SimpleFloorGenerator : MonoBehaviour
{
    public GameObject tilePrefab;   // Assign your tile prefab (suelo)
    public int width = 3;           // Number of tiles along X
    public int length = 50;         // Number of tiles along Z
    public float tileStep = 1f;     // Distance between tiles (use 1 if prefab scale = 1)
    public bool clearExisting = true;

    [ContextMenu("Generate Floor")]
    public void GenerateFloor()
    {
#if UNITY_EDITOR
        if (tilePrefab == null)
        {
            Debug.LogError("No prefab assigned.");
            return;
        }

        if (clearExisting)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Undo.DestroyObjectImmediate(transform.GetChild(i).gameObject);
        }

        Vector3 startPos = transform.position;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                Vector3 pos = startPos + new Vector3(x * tileStep, 0, z * tileStep);
                GameObject tile = (GameObject)PrefabUtility.InstantiatePrefab(tilePrefab, transform);
                tile.transform.position = pos;
                tile.transform.rotation = Quaternion.identity;
                tile.transform.localScale = Vector3.one; // keep prefab’s scale
                Undo.RegisterCreatedObjectUndo(tile, "Create Tile");
            }
        }

        EditorSceneManager.MarkSceneDirty(gameObject.scene);
#endif
    }
}
