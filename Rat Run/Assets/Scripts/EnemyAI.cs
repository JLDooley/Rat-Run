using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(Pathing))]
public class EnemyAI : MonoBehaviour
{
    [Range(0,3)]
    public int aggression = 2;

    public bool isHostile = false;
    [Tooltip("Cooldown period (in seconds) between shots")]
    public float rateOfFire = 1f;


    public bool isFacing = false;

    public bool trackPlayer = false;
    public float facingTolerance = 10f;

    public PlayerController player;

    public Pathing pathing;

    private float timer = 0f;


    public Transform spawner;

    public GameObject prefab;



    void Start()
    {
        player = PlayerController.Instance;

        if (pathing == null)
        {
            pathing = GetComponent<Pathing>();
        }
    }


    
    void Update()
    {
        timer -= Time.deltaTime;

        FacingCheck();
        
        if (trackPlayer && isHostile && isFacing)
        {
            Shoot();
        }
    

    }





    void FacingCheck()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer = Vector3.ProjectOnPlane(directionToPlayer, Vector3.up);

        Vector3 currentFacing = transform.forward;
        currentFacing = Vector3.ProjectOnPlane(currentFacing, Vector3.up);

        float facingAngle = Vector3.Angle(directionToPlayer, currentFacing);

        if (facingAngle <= facingTolerance)
        {
            isFacing = true;
        }
        else
        {
            isFacing = false;
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot() Run");


        if (timer <= 0f)
        {
            Instantiate(prefab, spawner.position, spawner.rotation);
            timer = rateOfFire;
        }

    }


}
