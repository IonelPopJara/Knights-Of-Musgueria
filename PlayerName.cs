using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public PlayerManager player;
    public TextMeshProUGUI playerTag;

    private Transform cameraTransform;

    private void Start()
    {
        playerTag.text = player.username;
        cameraTransform = GameObject.Find("Camera").transform;
    }

    private void Update()
    {
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }


}
