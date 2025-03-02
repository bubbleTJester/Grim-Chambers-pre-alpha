using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int AnimationState = 0;
    [SerializeField] private Animator animator;

    private void Update()
    {
        switch (AnimationState)
        {
            default: // exception handling
                animator.SetBool("isAttacking", false);
                break;
            case 0: // Moving | Default
                animator.SetBool("isAttacking", false);
                break;
            case 1: // Attacking
                animator.SetBool("isAttacking", true);
                break;
            case 2: // Attacked
                animator.SetBool("isAttacking", false);
                break;
        }
    }




}
