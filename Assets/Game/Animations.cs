using UnityEngine;

public class Animations : MonoBehaviour
{
    public Movement movement; //Drag the wantes script into here
    [SerializeField] private Animator animator;
    void Update()
    {
        _Turning();
        _Jumping();
        _Walking();
    }

    void _Turning()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flips horizontally   
        }
    }
    void _Jumping()
    {
        if (!movement.isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        if (movement.isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    void _Walking()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (moveInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
