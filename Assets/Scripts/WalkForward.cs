// SCRIPT WITH TRIGGER ASSOCIATED

using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class WalkForward : MonoBehaviour
{
    public Animator animator;            // optional, assign if you want speed synced to clip
    public float unitsPerCycle = 1f;     // how many units per walking cycle

    private Rigidbody rb;
    private float speed = 0f;            // units per second

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Helpful defaults for a walking NPC
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        // Calculate speed from current playing clip (safe checks)
        float clipLength = 0f;
        if (animator != null && animator.runtimeAnimatorController != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            foreach (var clip in animator.runtimeAnimatorController.animationClips)
            {
                if (stateInfo.IsName(clip.name))
                {
                    clipLength = clip.length;
                    break;
                }
            }
        }

        // If we found a clip, use it; otherwise you can set speed manually (unitsPerCycle / clipLength)
        if (clipLength > 0f)
            speed = unitsPerCycle / clipLength;

        // Move using physics (MovePosition) so triggers/collisions work reliably
        Vector3 delta = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + delta);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurnTrigger"))
        {
            Debug.Log($"{name} bumped into TurnTrigger '{other.name}' at t={Time.time:F2}s");
            TurnAround();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Useful fallback to see if a non-trigger collision happened
        Debug.Log($"{name} OnCollisionEnter with '{collision.gameObject.name}' at t={Time.time:F2}s");
    }

    // public so other scripts (e.g. a trigger script) can call it
    public void TurnAround()
    {
        Quaternion target = transform.rotation * Quaternion.Euler(0f, 180f, 0f);
        rb.MoveRotation(target);
        Debug.Log($"{name} rotated 180° at t={Time.time:F2}s");
    }
}
