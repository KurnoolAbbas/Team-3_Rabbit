using UnityEngine;

public class RabbitController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        // Get the Animator component attached to the rabbit GameObject
        animator = GetComponent<Animator>();
    }

    // Method to make the rabbit smile
    public void Smile()
    {
        // Trigger the "Smile" animation state in the Animator
        animator.SetTrigger("Smile");
    }

    // Method to make the rabbit cry
    public void Cry()
    {
        // Trigger the "Cry" animation state in the Animator
        animator.SetTrigger("Cry");
    }
}
