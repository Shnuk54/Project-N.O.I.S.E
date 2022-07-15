using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeStatsController : MonoBehaviour
{
    [Header("Eyes")]
    [SerializeField] private float _eyeEndurance;
    [SerializeField][Range(0.1f,5)] private float _eyeTiredSpeed;
    [SerializeField] private float _maxEyeEndurance;
    private Blinking _eye;

    [Header("Health")]
    [SerializeField] private float _health;
    [SerializeField][Range(1, 100)] private float _maxHealth;

    [Header("Endurance")]
    [SerializeField] private float _endurance;
    [SerializeField][Range(1, 100)] private float _maxEndurance;

    [Header("Stress")]
    [SerializeField] private float _stress;
    [SerializeField][Range(1, 100)] private float _maxStress;

    [Header("Heart")]
    [SerializeField] private float _heartBeat;
    private HeartBeatSounds _heart;

    private void Start()
    {
        _eye = FindObjectOfType<Blinking>().GetComponent<Blinking>();    
        _heart = FindObjectOfType<HeartBeatSounds>().GetComponent<HeartBeatSounds>();
    }
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
        if (_eyeEndurance <= 0 && _eye.startBlinking == false ) {
            Events.instance.OnPlayerStartBlink();
            return;
        }
        _eyeEndurance -= _eyeTiredSpeed * Time.deltaTime;
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
