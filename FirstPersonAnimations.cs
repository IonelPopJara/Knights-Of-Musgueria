using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonAnimations : MonoBehaviour
{
    [Header("Player")]
    public PlayerManager player;

    [Header("Animators")]
    public Animator gunAnimator;
    public Animator swordAnimator;
    public Animator slideAnimator;
    public Animator cameraAnimator;

    private bool checkSwordAttack;

    private void Update()
    {
        if (player.swordAttack != checkSwordAttack)
        {
            if (!checkSwordAttack)
            {
                swordAnimator.SetTrigger("Sword");
            }
            else if (checkSwordAttack)
            {

            }

            checkSwordAttack = player.swordAttack;
        }

        swordAnimator.SetFloat("Speed", player.velocity);
        swordAnimator.SetBool("Crouching", player.isCrouching);

        gunAnimator.SetBool("Shoot", player.shooting);
        gunAnimator.SetBool("Grappling", player.grappling);

        slideAnimator.SetBool("Crouching", Input.GetKey(KeyCode.LeftControl));
        slideAnimator.SetBool("Grounded", player.grounded);

        cameraAnimator.SetFloat("Speed", player.velocity);
        cameraAnimator.SetBool("Grounded", player.grounded);
    }
}
