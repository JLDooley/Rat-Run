using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Controls aiming and shooting of weapons attached to an enemy character
public class WeaponAim : MonoBehaviour
{
    public EnemyAI characterAI;

    public float rotationSpeed = 90f;

    public Transform spawner;

    public GameObject prefab;

    [Tooltip("Cooldown period (in seconds) between shots")]
    public float rateOfFire = 1f;

    public float aimingTolerance = 10f;

    public bool isAimed = false;

    public bool shouldAttack = false;

    private float timer = 0f;

    void Start()
    {
        if (characterAI == null)
        {
            characterAI = GetComponentInParent<EnemyAI>();
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (characterAI.trackPlayer && characterAI.isHostile && characterAI.isFacing)
        {
            //Get direction to player
            Vector3 aimVec = characterAI.player.playerPosition.position - transform.position;
            Quaternion aimQuat = Quaternion.LookRotation(aimVec);

            //Should shoot at the current target
            shouldAttack = true;

            //Begin aiming at player
            AimWeapon(aimQuat);
        }
        else
        {
            //Target is not the player, don't shoot
            shouldAttack = false;
            
            //Return to default orientation
            AimWeapon(characterAI.transform.rotation * Quaternion.identity);
        }

        if (characterAI.isHostile && isAimed && shouldAttack)
        {
            Shoot();
        }


    }


    public void AimWeapon(Quaternion target)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, target) <= aimingTolerance)
        {
            isAimed = true;
        }
        else
        {
            isAimed = false;
        }
    }

    void Shoot()
    {
        //Debug.Log("Shoot() Run");


        if (timer <= 0f)
        {
            Instantiate(prefab, spawner.position, spawner.rotation);
            timer = 1 / rateOfFire;
        }

    }

    public void BerserkBuff()
    {
        rateOfFire *= characterAI.berserkMultiplier;
    }
}
