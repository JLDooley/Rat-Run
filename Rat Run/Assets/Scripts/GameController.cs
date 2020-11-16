using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<ActiveEnemyList> activeEnemies = new List<ActiveEnemyList>();

    public Transform playerStartPosition;

    private PlayerController player;

    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Multiple GameControllers instances in scene, duplicate '" + gameObject.name + "' removed.");
        }

        player = PlayerController.Instance;

        if (playerStartPosition == null)
        {
            Debug.Log(gameObject.name + ": No start position assigned for player, assigning to world origin.");
            playerStartPosition.position = Vector3.zero;
            playerStartPosition.rotation = Quaternion.identity;
            playerStartPosition.localScale = Vector3.one;
        }


        player.transform.position = playerStartPosition.position;
        player.transform.rotation = playerStartPosition.rotation;
    }

    



    public void AddActiveEnemy(GameObject enemy, SpawnManager spawner)
    {
        ActiveEnemyList tempDetails = new ActiveEnemyList();
        tempDetails.enemyInstance = enemy;
        tempDetails.spawnerInstance = spawner;

        activeEnemies.Add(tempDetails);
    }

    public void RemoveActiveEnemy(GameObject enemy)
    {
        
        activeEnemies.Remove(activeEnemies.Find(x => x.enemyInstance == enemy));
    }

    public bool CheckForReservedSpawner(SpawnManager spawner)
    {
        return activeEnemies.Exists(x => x.spawnerInstance == spawner);
    }

}

public class ActiveEnemyList
{
    public GameObject enemyInstance;
    public SpawnManager spawnerInstance;
}
