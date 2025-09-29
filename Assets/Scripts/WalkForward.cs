//using UnityEngine;

//public class WalkForward : MonoBehaviour
//{
//    public float speed = 1.0f; // units per second

//    void Update()
//    {
//        transform.Translate(Vector3.forward * speed * Time.deltaTime);
//    }
//}


using UnityEngine;

public class WalkForward : MonoBehaviour
{
    public Animator animator;          // assign in Inspector
    public string walkAnimationName = "Armature|Armature|Armature|Armature|walking_man|baselayer";   // name of the walk animation state
    public float unitsPerCycle = 1f;   // how far forward per cycle

    private float cycleDuration;       // seconds per cycle
    private float speed;               // units per second

    void Start()
    {
        // Find the animation clip duration
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            foreach (var clip in animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == walkAnimationName)
                {
                    cycleDuration = clip.length;
                    break;
                }
            }
        }

        if (cycleDuration > 0f)
        {
            speed = unitsPerCycle / cycleDuration; // 1 unit per cycle
        }
        else
        {
            Debug.LogWarning("Walk animation not found or duration is zero!");
            speed = 0f;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurnTrigger"))
        {
            // Rotate 180 degrees around Y axis
            transform.Rotate(0f, 180f, 0f);
            // Print to console
            Debug.Log(gameObject.name + " bumped into a TurnTrigger at " + Time.time + "s");
        }
    }
}

