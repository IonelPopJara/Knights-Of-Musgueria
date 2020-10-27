using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Networking")]
    public int id;
    public string username;

    [Header("Health and Stats")]
    public float health;
    public float maxHealth;
    public int itemCount = 0;

    [Header("Movement & Animations")]
    public Animator animator;
    public float velocity;
    public bool grounded;

    [Header("Grappling")]
    public Vector3 grapplePoint;
    public Transform gunTip;
    public LineRenderer lr;
    private int positionCount;
    public bool grappling;

    [Header("Shooting")]
    public bool shooting;

    [Header("Sword Attack")]
    public bool swordAttack;

    [Header("T Pose")]
    public bool tPose;

    [Header("Crouching")]
    public bool isCrouching;
    public Transform playerBody;
    private Vector3 playerScale;
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);

    [Header("Death")]
    public SkinnedMeshRenderer model;
    public GameObject damageParticles;
    public GameObject deathParticles;

    private bool checkSwordAttack;

    private TextMeshProUGUI slainText;

    private void Start()
    {
        playerScale = playerBody.localScale;

        slainText = GameObject.Find("Slain Text").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(swordAttack != checkSwordAttack)
        {
            if(!checkSwordAttack)
            {
                animator.SetTrigger("Sword");
            }
            else if(checkSwordAttack)
            {

            }

            checkSwordAttack = swordAttack;
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        health = maxHealth;
    }

    public void SetHealth(float _health, string _playerDoingDamage)
    {
        health = _health;

        if(health <= 0f)
        {
            //Decir quien chucha te mató
            StartCoroutine(SetSlainText(_playerDoingDamage));

            Die();
        }
    }

    public void SetGrapplePoint(Vector3 _grapplePoint)
    {
        grapplePoint = _grapplePoint;
    }

    public void SetCrouchValue(bool _crouching)
    {
        isCrouching = _crouching;
        animator.SetBool("Crouching", isCrouching);
    }

    public void SetLRPositionCount(int _positionCount)
    {
        positionCount = _positionCount;
        grappling = positionCount != 0;
        animator.SetBool("Grappling", grappling);
    }

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity.sqrMagnitude;
        animator.SetFloat("Speed", velocity);
    }

    public void SetGroundedValue(bool _grounded)
    {
        grounded = _grounded;
        animator.SetBool("Grounded", grounded);
    }

    public void SetShootingValue(bool _shooting)
    {
        shooting = _shooting;
        animator.SetBool("Shooting", shooting);
    }

    public void SetSwordAttackValue(bool _swordAttack)
    {
        swordAttack = _swordAttack;
        //Lo dejé solo por si acaso, pero no hace nada
        animator.SetBool("Sword Attack", swordAttack);
    }

    public void SetTPoseValue(bool _tPose)
    {
        tPose = _tPose;
        animator.SetBool("T Pose", tPose);
    }

    public void DamageTaken(float _health)
    {
        //Hacer alguna wea aca
        PlayerController controller = gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.DamageTaken();
            return;
        }
        print("Pantalla qla muevete");
        //Instantiate sound, points and camera shake
        StartCoroutine(FindObjectOfType<CameraShake>().Shake(.1f, .3f));
        FindObjectOfType<AudioManager>().Play("Hit");

        GameObject particles = Instantiate(damageParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2f);

    }

    public void DrawRope()
    {
        lr.positionCount = positionCount;
        
        if(!grappling)
        {
            return;
        }

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
        //model.enabled = false;
        //Maybe add some animations???Particles???Sounds???
        model.enabled = false;
        GameObject particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(particles, 2f);
        itemCount = 0;
    }

    public void Respawn()
    {
        model.enabled = true;
        //model.enabled = true;
        SetHealth(maxHealth, null);
    }

    public void ActivateSwordCollider()
    {
        ClientSend.PlayerSwordActivateCollider();
    }

    public void DeactivateSwordCollider()
    {
        ClientSend.PlayerSwordDeactivateCollider();
    }

    public IEnumerator SetSlainText(string playerKilledYou)
    {
        if (playerKilledYou != "")
            slainText.text = $"{playerKilledYou} se piteo a {username}";
        else
            slainText.text = $"{username} entero pao, se cayo";

        yield return new WaitForSeconds(5f);
        slainText.text = null;
    }

    public void ConchetumareReceived()
    {
        FindObjectOfType<AudioManager>().Play("CONCHETUMARE");
        //Efectos de Post Processing
    }
}
