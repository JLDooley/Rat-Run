using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject mainPrefab;

    public bool useMainVelocity = false;
    public float mainVelocity;

    public bool useMainAcceleration = false;
    public float mainAcceleration;    
    
    public ArrayVariables[] waveSequence;


    

    void Start()
    {
        StartCoroutine(RunWaveSequence());
    }

    IEnumerator RunWaveSequence()
    {
        for (int i = 0; i < waveSequence.Length; i++)
        {
            // Check for Overrides
            if (waveSequence[i].spawnPrefab == null)
            {
                waveSequence[i].spawnPrefab = mainPrefab;
            }
            if (useMainVelocity)
            {
                waveSequence[i].velocity = mainVelocity;
            }
            if (useMainAcceleration)
            {
                waveSequence[i].acceleration = mainAcceleration;
            }

            
            waveSequence[i].spawner.AddToQueue(waveSequence[i].spawnPrefab, waveSequence[i].spawnShield, waveSequence[i].velocity, waveSequence[i].acceleration);
            yield return new WaitForSeconds(waveSequence[i].interval);
        }
        
    }


}

[System.Serializable]
public class ArrayVariables
{
    public float interval = 0f;
    public SpawnManager spawner;
    public GameObject spawnPrefab;
    public float velocity;
    public float acceleration;
    public bool spawnShield;
}

