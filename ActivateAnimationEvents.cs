using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimationEvents : MonoBehaviour
{
    public PlayerManager player;

    public void ActivateCollider()
    {
        FindObjectOfType<AudioManager>().Play("Sword");
        if (player == null)
            return;
        player.ActivateSwordCollider();
    }

    public void DeactivateCollider()
    {
        if (player == null)
            return;
        player.DeactivateSwordCollider();
    }

    public void DashSound()
    {
        FindObjectOfType<AudioManager>().Play("Dash");
    }
}
