using UnityEngine;


public class ManUnitedFanNPCController  : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // When NPC touches a trigger zone
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StairTrigger"))
        {
            animator.SetTrigger("StandUp");
        }
    }
}
