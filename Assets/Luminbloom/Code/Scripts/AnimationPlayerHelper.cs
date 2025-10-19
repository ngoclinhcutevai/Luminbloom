using UnityEngine;

public class AnimationPlayerHelper : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    // This is the function you will call from the Animation Event
    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
    }
}