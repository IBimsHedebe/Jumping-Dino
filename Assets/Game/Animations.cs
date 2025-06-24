using UnityEngine;

public class Animations : MonoBehaviour
{
    public Movement movement; //Drag the wantes script into here
    [SerializeField] private Animator animator;
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetBool("isWalking", true);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flips horizontally
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetButtonDown("Jump") && !movement.isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        if (movement.isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }
}