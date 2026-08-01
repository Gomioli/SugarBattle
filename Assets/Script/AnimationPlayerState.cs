using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerState : MonoBehaviour
{

    [SerializeField]Animator animator;
    [SerializeField] PlayerController playerController;

    private void Start()
    {

    }

    void Update()
    {
        if (playerController.isAttacking)
        {
            animator.SetBool("isAttacking", true);
        }
    }
}
