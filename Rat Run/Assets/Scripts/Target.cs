using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


//  Target.cs controls health, hit reactions (damage, explosions, buffs/debuffs), and death for the parent entity and its AI
public class Target : MonoBehaviour
{
    [Tooltip("Dictates how the target reacts to damage types.")]
    public targetTypeProperty targetType = targetTypeProperty.Default;
    
    public float health = 30f;

    [Tooltip("The root object of the hierarchy to be destroyed on death. Defaults to this object if blank.")]
    public GameObject parentEntity;

    [Tooltip("Should be applied for the 'Enemy' target type.")]
    public EnemyAI targetController;

    [Header("Explosion Settings", order = 0), Space (-10, order = 1), Header("Only applicable to the 'Shield' target type.", order = 2)]

    public float explosionRadius;

    public LayerMask explosionLayer = 1;

    public GameObject explosionPrefab;

     
    public enum targetTypeProperty
    {
        Default,
        Enemy,
        Shield
    };

    private void Start()
    {
        if (parentEntity == null)
        {
            parentEntity = gameObject;
            Debug.Log(gameObject.name + ": No parent assigned, assigning this gameObject");
        }


        if (targetController == null && targetType == targetTypeProperty.Enemy)
        {
            targetController = parentEntity.GetComponentInChildren<EnemyAI>();

            if (targetController == null)
            {
                Debug.LogWarning(gameObject.name + ": No AI found for enemy, converting to default type.");
                targetType = targetTypeProperty.Default;
            }
        }
        


    }

    public void TakeDamage (float amount, Gun.damageTypeProperty damageType)
    {
        //Debug.Log("Damage Recieved");
        switch (targetType)
        {
            case targetTypeProperty.Enemy:
                
                //Debug.Log("Damage Recieved: " + "Enemy, " + damageType );
                
                if (damageType == Gun.damageTypeProperty.Physical)
                {
                    health -= amount;
                    targetController.trackPlayer = true;
                    targetController.isHostile = true;
                }
                else if (damageType == Gun.damageTypeProperty.Energy)
                {
                    targetController.Berserk();
                }
                else
                {
                    health -= amount;
                }    
                break;

            case targetTypeProperty.Shield:
                
                //Debug.Log("Damage Recieved: " + "Shield, " + damageType);
                
                if (damageType == Gun.damageTypeProperty.Physical)
                {
                    health -= amount;
                }
                else if (damageType == Gun.damageTypeProperty.Energy)
                {
                    Explode();
                }
                else
                {
                    health -= amount;
                }
                break;

            default:
                {
                    //Debug.Log("Default State Case");
                    health -= amount;
                }
                break;

        }


        
        if (health <= 0f)
        {
            Die();
        }
    }

    void Explode()
    {
        Explosion explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<Explosion>();

        explosion.Explode(explosionRadius, explosionLayer);

    }

    public void Die ()
    {
        if (targetController != null && targetController.gameController != null)
        {
            targetController.gameController.RemoveActiveEnemy(parentEntity);
        }
        
        Destroy(parentEntity);
    }
}
