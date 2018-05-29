using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DrunkRally;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController car; // the car controller we want to use
        private WebSocketController wsController;
        [SerializeField] public GameObject wsControl;

        public GameObject noiseGenerator;
        private AlcoholNoiseGenerator alcoGen;

        private void Awake()
        {
            car = GetComponent<CarController>();
            wsController = wsControl.GetComponent<WebSocketController>();
            alcoGen = noiseGenerator.GetComponent<AlcoholNoiseGenerator>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (h == 0) h = alcoGen.ApplyNoise(wsController.axis_y);
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            if (v == 0) v = wsController.axis_x;
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            if (handbrake == 0) handbrake = wsController.handbrake;
            car.Move(h, v, v, handbrake);
        }
    }
}
