using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



public class EnemyAI : MonoBehaviour
{
    public bool hasShield = false;

    public bool isHostile = false;

    public bool isBerserk = false;

    public float berserkMultiplier = 3f;

    public bool isFacing = false;

    public bool trackPlayer = false;

    public float facingTolerance = 10f;

    public PlayerController player;

    public GameController gameController;

    public Pathing pathing;

    public WeaponAim[] weapons;

    public GameObject shield;

    [Header("Berserk effect AOE damage:")]
    public float AOEDamageDuration = 5f;

    public float damagePerTick = 5f;

    public float tickInterval = 0.5f;

    public LayerMask AOELayerMask = 1;

    public float damageRadius = 3f;

    void Start()
    {
        player = PlayerController.Instance;
        gameController = GameController.Instance;

        if (pathing == null)
        {
            pathing = GetComponent<Pathing>();
        }

        shield.SetActive(hasShield);
    }


    
    void Update()
    {

        FacingCheck();

    }





    void FacingCheck()
    {
        //What is the x-z direction to the player (VR headset)
        Vector3 directionToPlayer = player.playerPosition.position - transform.position;
        directionToPlayer = Vector3.ProjectOnPlane(directionToPlayer, Vector3.up);

        //What is the current orientation of this gameobject
        Vector3 currentFacing = transform.forward;
        currentFacing = Vector3.ProjectOnPlane(currentFacing, Vector3.up);

        //The angle between the two directions
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


    public void Berserk()
    {
        if (!isBerserk)
        {
            //Set to Berserk, so that it won't be triggered multiple times.
            isBerserk = true;

            //Increase ROF
            foreach (var weapon in weapons)
            {
                weapon.BerserkBuff();
            }

            //Make Hostile
            trackPlayer = true;
            isHostile = true;

            //Damage nearby enemies over time
            StartCoroutine(AOEDamageOverTime());
        }
        
    }

    IEnumerator AOEDamageOverTime()
    {
        for (int currentTick = 0; currentTick <= Mathf.RoundToInt(AOEDamageDuration/tickInterval); currentTick++)
        {
            //Find enemies in range, apply damage
            Vector3 centre = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(centre, damageRadius, AOELayerMask);

            foreach (var hitCollider in hitColliders)
            {
                GameObject hitObject = hitCollider.gameObject;

                if (!hitObject.CompareTag("Player") && hitObject.GetComponent<Target>().targetController != this)
                {
                    Debug.Log(hitObject.name + ": Berserk AOE damage recieved.");
                    hitObject.GetComponent<Target>().TakeDamage(damagePerTick, Gun.damageTypeProperty.Physical);
                }
            }

            yield return new WaitForSeconds(tickInterval);
        }
    }
}
