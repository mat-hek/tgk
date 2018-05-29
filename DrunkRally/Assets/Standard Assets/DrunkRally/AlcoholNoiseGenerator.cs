using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholNoiseGenerator : MonoBehaviour {
        public float alcohol_level = 0.5f;
        private const float ALCO_DOSE = 0.3f;
        private float last_bottle;
        private const float CYCLE = 2*Mathf.PI;

        private void Awake() {
            last_bottle = Time.time;
        }

        public float ApplyNoise(float input_y) {
            // assuming input_y is between -1 and 1
            float fraction_of_second = Time.time / 2f % 1.0f;
            float noise = Mathf.Sin(CYCLE * fraction_of_second) * alcohol_level;
            return Mathf.Clamp(input_y + noise, -1f, 1f);
        }

        public void DrinkAlcohol() {
            alcohol_level += ALCO_DOSE;
            this.Invoke("SoberUp", 15);
        }
        public void SoberUp() {
            alcohol_level -= ALCO_DOSE;
        }
}
