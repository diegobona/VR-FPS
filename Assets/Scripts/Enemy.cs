using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health and Damage")]
    public float enemyHealth = 120f; 
    public float presentHealth;

    public float giveDamage = 5f;
    public float enemySpeed;



    [Header("Enemy Things")]
    public NavMeshAgent enemyAgent;
    public Transform playerBody;
    public LayerMask playerLayer;
    public Transform lookPoint;
    public GameObject shootingRaycastArea;


    [Header("Enemy Shooting Var")]
    public float timebtwShoot;
    bool previouslyShoot;

    [Header("Enemy Animation and Spark effect")] 
    public Animator anim;


    [Header("Enemy States")]
    public float visionRadius;
    public float shootingRadius;
    public bool playerInvisionRadius;
    public bool playerInshootingRadius;
    public bool isPlayer = false;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        presentHealth = enemyHealth;
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInshootingRadius = Physics.CheckSphere(transform.position, shootingRadius, playerLayer);

        if (playerInvisionRadius && !playerInshootingRadius) PursuePlayer();
        if (playerInvisionRadius && playerInshootingRadius) ShootPlayer();
    }

    private void PursuePlayer()
    {
        //if (enemyAgent.SetDestination(playerBody.position))
        //{
        //    //animation
        //    anim.SetBool("Running", true);
        //    anim.SetBool("Shooting", false);
        //} else
        //{
        //    anim.SetBool("Running", false);
        //    anim.SetBool("Shooting", false);
        //}
    }


    private void ShootPlayer()
    {

        enemyAgent.SetDestination(transform.position);//transform指当前脚本所挂载的对象的transform？
        transform.LookAt(lookPoint);

        if (!previouslyShoot)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward, out hit,shootingRadius))
            {
                Debug.DrawRay(shootingRaycastArea.transform.position, shootingRaycastArea.transform.forward * hit.distance, Color.red);
                Debug.Log("Shooting" + hit.transform.name);

                PlayerScript playerBody=hit.transform.GetComponent<PlayerScript>(); //获取击中的player的playerscript组件（脚本也属于组件）

                if(playerBody != null)
                {
                    playerBody.PlayerHitDamage(giveDamage);
                }
                
            }
            anim.SetBool("Running", false);
            anim.SetBool("Shooting", true);
        }

        previouslyShoot = true;
        Invoke(nameof(ActiveShooting), timebtwShoot);


    }

    private void ActiveShooting()
    {
        previouslyShoot = false;
    }

    public void EnemyHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        if (presentHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        anim.SetBool("Die", true);
    }



}
