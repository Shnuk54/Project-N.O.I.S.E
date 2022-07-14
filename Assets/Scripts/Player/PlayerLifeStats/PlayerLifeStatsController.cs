using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeStatsController : MonoBehaviour
{
    [Header("Eyes")]
    [SerializeField] private float _eyeEndurance;
    [SerializeField] private float _eyeTiredSpeed;
    [SerializeField] private float _maxEyeEndurance;

    [Header("Health")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;

    [Header("Endurance")]
    [SerializeField] private float _endurance;
    [SerializeField] private float _maxEndurance;

    [Header("Stress")]
    [SerializeField] private float _stress;
    [SerializeField] private float _maxStress;

    [Header("Heart")]
    [SerializeField] private float _heartBeat;


    private void OnEnable()
    {
        Events.onPlayerBlink += RestoreEyeEndurance;
    }
    private void OnDisable()
    {
        Events.onPlayerBlink -= RestoreEyeEndurance;
    }
    private void LostEyeEndurance()
    {
        if (_eyeEndurance <= 0 && FindObjectOfType<Blinking>().startBlinking == false ) {
            Events.instance.OnPlayerStartBlink();
            return;
        }
        _eyeEndurance -= 1*Time.deltaTime;
    }
    private void RestoreEyeEndurance()
    {
        _eyeEndurance = _maxEyeEndurance;
    }

    private void FixedUpdate()
    {
        LostEyeEndurance();
    }
}
