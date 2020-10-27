using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /*
     * Aca puedo cambiar las teclas, recordar que tengo que asignar el slide en FirstPersonAnimations
     */
    public Transform camTransform;
    public Camera ThirdPersonCamera;
    public Camera ThirdPersonCamera2;
    public PlayerManager player;
    public Slider healthBar;

    public GameObject damagePanel;

    public float playerHealth;

    public TextMeshProUGUI shootingText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI slainText;

    private bool shooting;

    private float ctmCoolDown = 2.5f;
    public float ctmTime;

    private void Start()
    {
        ThirdPersonCamera.enabled = false;
        ThirdPersonCamera2.enabled = false;
        healthBar = GameObject.Find("Health").GetComponent<Slider>();
        damagePanel = GameObject.Find("Death Panel");
        healthBar.maxValue = player.maxHealth;

        shootingText = GameObject.Find("Shooting Text").GetComponent<TextMeshProUGUI>();
        ammoText = GameObject.Find("Ammo Text").GetComponent<TextMeshProUGUI>();
        slainText = GameObject.Find("Slain Text").GetComponent<TextMeshProUGUI>();

        damagePanel.SetActive(false);

        slainText.text = null;
    }

    private void Update()
    {
        ammoText.text = $"{player.itemCount}/20";
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            FindObjectOfType<AudioManager>().Play("Weapon Shift");
            shooting = !shooting;
            if(shooting)
            {
                if(player.grappling)
                {
                    ClientSend.PlayerStopGrappling();
                }
                shootingText.text = "Blaster";
            }
            else if(!shooting)
            {
                shootingText.text = "Grappling Gun";
            }
        }

        playerHealth = player.health;

        healthBar.value = playerHealth;
        
        /*
        if(Input.GetKeyDown(KeyCode.R))
        {
            ClientSend.PlayerStartGrappling(camTransform.forward);
            FindObjectOfType<AudioManager>().Play("Hook");
        }

        if(Input.GetKeyUp(KeyCode.R))
        {
            ClientSend.PlayerStopGrappling();
        }*/

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(shooting)
            {
                ClientSend.PlayerShoot(camTransform.forward);
            }
            else if(!shooting)
            {
                ClientSend.PlayerStartGrappling(camTransform.forward);
                FindObjectOfType<AudioManager>().Play("Hook");
            }
            //ClientSend.PlayerShoot(camTransform.forward);
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(shooting)
            {
                ClientSend.PlayerStopShooting();
            }
            if (!shooting)
            {
                ClientSend.PlayerStopGrappling();
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            ClientSend.PlayerStartSword(camTransform.forward);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            ClientSend.PlayerStopSword();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ClientSend.PlayerStartTPose();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            ClientSend.PlayerStopTPose();
        }

        if(Input.GetKeyDown(KeyCode.F5))
        {
            ThirdPersonCamera.enabled = !ThirdPersonCamera.enabled;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            ThirdPersonCamera2.enabled = !ThirdPersonCamera2.enabled;
        }

        if(ctmTime > 0)
        {
            ctmTime -= Time.deltaTime;
        }
        else if(ctmTime <= 0)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                ctmTime = ctmCoolDown;
                ClientSend.PlayerConchetumare();
            }
        }
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space),
            Input.GetKey(KeyCode.LeftControl)
        };

        ClientSend.PlayerMovement(_inputs);
    }

    public void DamageTaken()
    {
        //Camera Shake and Red Panel
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(3f,2f));
        damagePanel.SetActive(true);
        StartCoroutine(DamagePanel());
    }

    private IEnumerator DamagePanel()
    {
        yield return new WaitForSeconds(0.1f);
        damagePanel.SetActive(false);
    }

    public IEnumerator SetSlainText(string playerKilledYou)
    {
        if (playerKilledYou != "")
            slainText.text = $"{playerKilledYou} te mato, pao qlo";
        else
            slainText.text = "Te caiste malo qlo";

        yield return new WaitForSeconds(1f);
        slainText.text = null;
    }
}