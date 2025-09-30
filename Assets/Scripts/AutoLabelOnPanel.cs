using UnityEngine;
using TMPro;

//[ExecuteAlways] // optional: updates in Edit mode
public class AutoLabelOnPanel : MonoBehaviour
{
    [Header("Label Settings")]
    public string labelText = "V-14";
    public Color textColor = Color.black;
    public float fontSize = 1f;

    private TextMeshPro tmp;

    void Start()
    {
        // Check if panel exists
        if (gameObject == null)
        {
            Debug.LogWarning("No panel attached!");
            return;
        }

        // Create TMP object
        GameObject textObj = new GameObject("PanelLabel_TMP");
        textObj.transform.SetParent(transform);

        // Reset local position and rotation
        textObj.transform.localPosition = Vector3.zero;
        textObj.transform.localRotation = Quaternion.identity;
        textObj.transform.localScale = Vector3.one;

        // Align with panel's up axis
        textObj.transform.up = transform.up;

        // Add TextMeshPro component
        tmp = textObj.AddComponent<TextMeshPro>();
        tmp.text = labelText;
        tmp.fontSize = fontSize;
        tmp.color = textColor;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.rectTransform.sizeDelta = new Vector2(1f, 1f); // you can adjust width/height if needed

        // Optional: make text always face forward (panel's local Z)
        tmp.transform.localRotation = Quaternion.LookRotation(transform.forward, transform.up);
    }

    // For editor changes
#if UNITY_EDITOR
    void OnValidate()
    {
        if (tmp != null)
        {
            tmp.text = labelText;
            tmp.color = textColor;
            tmp.fontSize = fontSize;
        }
    }
#endif
}
