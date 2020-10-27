using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;

    public Transform playerPosition;

    public static PlayerController Instance
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
            Debug.LogError("Multiple PlayerControllers instances in scene, duplicate '" + gameObject.name + "' removed.");
        }
    }



}
