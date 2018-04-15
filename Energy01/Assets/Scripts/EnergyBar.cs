using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Energy
{
    public static class RemapExtension
    {
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
    public class EnergyBar : MonoBehaviour
    {

        public Image energybarUI;
        public bool increaseOverTime;
        //[Range(0, 10)]
        public float currentEnergy;


        private float maxEnergy;
        private float minEnergy;

        private void Start()
        {
            maxEnergy = 50;
            minEnergy = 0;
        }


        void Update()
        {
            
            if (increaseOverTime)
            {
                energybarUI.fillAmount = (currentEnergy.Remap(minEnergy, maxEnergy, 0, 100));
                Debug.Log(currentEnergy.Remap(minEnergy, maxEnergy, 0, 1) + "current fill");
            }
        }
    }
}
