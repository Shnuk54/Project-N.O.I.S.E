using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeStatsController : MonoBehaviour
{
    [Header("Eyes")]
    [SerializeField] private float _eyeEndurance;
    [SerializeField][Range(0.1f,5)] private float _eyeTiredSpeed;
    [SerializeField] private float _maxEyeEndurance;
    private Blinking _eyeHandler;

    [Header("Health")]
    [SerializeField] private float _health;
    [SerializeField][Range(1, 100)] private float _maxHealth;

    [Header("Endurance")]
    [SerializeField] private float _endurance;
    [SerializeField][Range(1, 100)] private float _maxEndurance;
    [SerializeField] private float _enduranceChangeSpeed;

    [Header("Stress")]
    [SerializeField] private float _stress;
    [SerializeField][Range(1, 100)] private float _maxStress;
    [SerializeField] private float _targetStress;
    [SerializeField] private float _stressChangeSpeed;
    private Stress _stressHandler;

    [Header("Heart")]
    [SerializeField] private float _heartBeat;
    [SerializeField] private float _heartBeatChangeSpeed;
    [SerializeField] private float _targetHeartBeat;
    private HeartBeatSounds _heartHandler;

    private void Start()
    {
        _eyeHandler = FindObjectOfType<Blinking>().GetComponent<Blinking>();    
        _heartHandler = FindObjectOfType<HeartBeatSounds>().GetComponent<HeartBeatSounds>();
        _stressHandler = FindObjectOfType<Stress>().GetComponent<Stress>();
    }
   
 

   
    private void HandleLifeStats()
    {
        _targetHeartBeat = _heartHandler.TargetPulse;
        _heartBeat = _heartHandler.Pulse;
        _heartBeatChangeSpeed = _heartHandler.PulseChangeSpeed;

        _targetStress = _stressHandler.TargetStress;
        _stress = _stressHandler.StressLevel;
        _stressChangeSpeed = _stressHandler.StressChangeSpeed;

        _eyeTiredSpeed = _eyeHandler.EyeTiredSpeed;
        _eyeEndurance = _eyeHandler.EyeEndurance;
        _maxEyeEndurance = _eyeHandler.MaxEyeEndurace;

    }
    private void FixedUpdate()
    {

        HandleLifeStats();
    }
    [ContextMenu("ChangeLifeStats")]
    private void ChangeLifeStats()
    {
        if(_stressHandler.StressStatus == StressStatus.Ñalmn)
        {
            _heartHandler.TargetPulse = Random.Range(45, 65);
            _eyeHandler.EyeTiredSpeed = 0.1f;
            
        }
        if(_stressHandler.StressStatus == StressStatus.LowStress)
        {
            _heartHandler.TargetPulse = Random.Range(65, 90);
            _eyeHandler.EyeTiredSpeed = 0.3f;
        }
        if(_stressHandler.StressStatus == StressStatus.MediumStress)
        {
            _heartHandler.TargetPulse = Random.Range(90, 125);
            _eyeHandler.EyeTiredSpeed = 0.6f;
        }
        if (_stressHandler.StressStatus == StressStatus.HighStress)
        {
            _heartHandler.TargetPulse = Random.Range(125, 150);
            _eyeHandler.EyeTiredSpeed = 0.9f;
        }
        if (_stressHandler.StressStatus == StressStatus.Scared)
        {
            _heartHandler.TargetPulse = Random.Range(150, 200);
            _eyeHandler.EyeTiredSpeed = 1.5f;
        }
    }
}
