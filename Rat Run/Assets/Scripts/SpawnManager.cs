using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] path;

    public GameObject currentInstance;

    public Queue<QueueDetails> spawnQueue = new Queue<QueueDetails>();

    public WaveManager waveManager;

    private GameController gameController;

    private void Start()
    {
        gameController = GameController.Instance;
    }

    private void Update()
    {
        //  When there is a spawn instance in the queue, check the Active Enemies list in the GameController to see if the spawner is currently reserved
        if (spawnQueue.Count > 0)
        {
            if (!gameController.CheckForReservedSpawner(this))
            {
                SpawnEnemy();
            }
        }
    }

    public void AddToQueue(GameObject spawnPrefab, bool hasShield, float velocity, float acceleration)
    {
        QueueDetails tempDetails = new QueueDetails();
        tempDetails.spawnPrefab = spawnPrefab;
        tempDetails.hasShield = hasShield;
        tempDetails.velocity = velocity;
        tempDetails.acceleration = acceleration;

        spawnQueue.Enqueue(tempDetails);
        Debug.Log("Spawn Queue length increase: " + spawnQueue.Count);
    }

    public void SpawnEnemy()
    {
        QueueDetails tempDetails = spawnQueue.Peek();
        GameObject spawnInstance = Instantiate(tempDetails.spawnPrefab);

        spawnInstance.GetComponentInChildren<EnemyAI>().hasShield = tempDetails.hasShield;

        //  Have the new enemy use the path attached to the spawn manager, and the velocity from the queued instance
        Pathing pathingScript = spawnInstance.GetComponent<Pathing>();
        pathingScript.path = path;
        pathingScript.targetVelocity = tempDetails.velocity;
        pathingScript.velocity = tempDetails.velocity;
        pathingScript.acceleration = tempDetails.acceleration;


        //  Remove from queue and add enemy and spawner to Active Enemies list in GameController
        spawnQueue.Dequeue();
        gameController.AddActiveEnemy(spawnInstance, this);
    }

    public void setInstance()
    {

    }
}

public class QueueDetails
{
    public GameObject spawnPrefab;
    public bool hasShield;
    public float velocity;
    public float acceleration;
}
