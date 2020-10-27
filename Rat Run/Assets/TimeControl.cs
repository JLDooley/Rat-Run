using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float timeSpeed = 0.1f;
    void Start()
    {
        Time.timeScale = timeSpeed;
    }


}
