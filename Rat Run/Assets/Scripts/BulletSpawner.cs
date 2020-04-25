using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

//namespace Valve.VR.InteractionSystem.Sample
//{



    public class BulletSpawner : MonoBehaviour
    {
        public SteamVR_Action_Boolean inputAction;

        public SteamVR_Input_Sources source;

        public Transform spawner;

        public GameObject prefab;

        public Interactable interactable;

        private float timer = 0f;

        [Tooltip("Cooldown period (in seconds) between shots")]
        public float rateOfFire = 1f;



        void Start()
        {
            interactable = GetComponent<Interactable>();


                
        }

        void Update()
        {
            timer -= Time.deltaTime;

            if (interactable.attachedToHand)
            {
                Debug.Log("Hand Detected");
                source = interactable.attachedToHand.handType;
                

                if (inputAction.GetState(source))
                {
                    Debug.Log("Shooting");
                    Shoot();
                }
            }


        }

        void Shoot()
        {
            Debug.Log("Shoot() Run");
            
            
            if (timer <= 0f)
            {
                Instantiate(prefab, spawner.position, Quaternion.identity);
                timer = rateOfFire;
            }

        }
    }
//}
