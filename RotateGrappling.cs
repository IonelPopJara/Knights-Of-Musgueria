using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGrappling : MonoBehaviour
{
    public PlayerManager player;

    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    private void Update()
    {
        //If is not grappling
        if(player.lr.positionCount == 0)
        {
            desiredRotation = transform.parent.rotation;
        }
        else if(player.lr.positionCount != 0)
        {
            desiredRotation = Quaternion.LookRotation(player.grapplePoint - transform.position);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
