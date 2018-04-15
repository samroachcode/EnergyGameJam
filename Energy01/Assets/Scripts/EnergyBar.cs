using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Energy
{
    public class EnergyBar : MonoBehaviour
    {
        public Image energyBar;
        public float energyAmount;
        public float fillAmount;
        void Update()
        {

            energyBar.fillAmount = fillAmount ;/* += 1.0f / energyAmount * Time.deltaTime;*/

        }
    }
}
