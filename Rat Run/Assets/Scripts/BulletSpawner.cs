using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR;
using Valve.VR.InteractionSystem;
using Valve.VR;

namespace Valve.VR.InteractionSystem.Sample
{



    public class BulletSpawner : MonoBehaviour
    {
        public SteamVR_Action_Single inputAction;

        public SteamVR_Input_Sources source;

        public float inputValue;
        public float threshold = 0.75f;

        public GameObject spawner;

        public GameObject prefab;

        public Interactable interactable;

        private float timer = 0f;

        [Tooltip("Cooldown period (in seconds) between shots")]
        public float rateOfFire = 1f;


/*        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (inputAction == null)
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No plant action assigned", this);
                return;
            }

            

            inputAction.AddOnChangeListener(OnInputActionChange, hand.handType);
        }

        private void OnInputActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                Plant();
            }
        }

        private void OnDisable()
        {
            if (inputAction != null)
                inputAction.RemoveOnChangeListener(OnInputActionChange, hand.handType);
        }
*/
        void Start()
        {
            interactable = GetComponent<Interactable>();

        }

        void Update()
        {
            if (interactable.attachedToHand)
            {
                Debug.Log("Hand Detected");
                source = interactable.attachedToHand.handType;
                inputValue = inputAction.GetAxis(source);

                if (inputValue > threshold)
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
                Instantiate(prefab, spawner.transform);
                timer = rateOfFire;
            }

        }
    }
}
