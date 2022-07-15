using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{
    [Header("Stress")]
    [SerializeField] private float _stressLevel;
    [SerializeField] private StressStatus _status;

   

}

public  enum StressStatus { Ñalmn, LowStress, MediumStress, HighStress, Scared };