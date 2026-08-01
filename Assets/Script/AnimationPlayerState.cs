using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerState : MonoBehaviour
{

    Animator animator;
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerController.isAttacking)
        {
            animator.SetBool("isAttacking", true);
        }
    }
}
