using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{
    [Header("Stress")]
    [SerializeField] private float _stressLevel;
    [SerializeField] private float _standartChangeSpeed = 0.1f;
    [SerializeField] private float _changeSpeed;
    [SerializeField] private float _minimumStressLevel = 0;
    [SerializeField] private float _targetStress;
    [SerializeField] private StressStatus _status;




    public float TargetStress{
        get { return _targetStress; }
        set { _targetStress = value; }
    }
    public float StressLevel
    {
        get { return _stressLevel; }
        
    }
    public float StressChangeSpeed
    {
        get { return _changeSpeed; }
    }
    public StressStatus StressStatus
    {
        get { return _status; }
    }

    public void ChangeStress(float stress,float speed )
    {
       
        if (_targetStress + stress > 100) {
            _targetStress = 100;
            _changeSpeed = speed;
           
            return;
        } 
        if (_targetStress + stress < 0f)
        {
            _targetStress = 0;
        
            _changeSpeed = speed;
            return;
        } 

        _targetStress += stress;
        _changeSpeed = speed;

        
        
    }

    private void FixedUpdate()
    {
        SmoothChangeStress(_changeSpeed,_minimumStressLevel);
    }
    private void SmoothChangeStress(float changeSpeed,float minimumStress)
    {
        if (_stressLevel == _targetStress) {
            _changeSpeed = _standartChangeSpeed;
            return;
        }
        
        if (_stressLevel > _targetStress || _stressLevel < _targetStress)
        {
            _stressLevel = Mathf.Lerp(_stressLevel, _targetStress, changeSpeed * Time.deltaTime);
            var gate = _targetStress - _stressLevel;
            gate = Mathf.Abs(gate);
            if (gate < 0.4f && _stressLevel != _targetStress) _stressLevel = Mathf.Round(_stressLevel);

        }

        if (_stressLevel <= 10) _status = StressStatus.Ñalmn;
        if(_stressLevel > 10 && _stressLevel < 25) _status = StressStatus.LowStress;
        if (_stressLevel > 25 && _stressLevel < 50) _status = StressStatus.MediumStress;
        if (_stressLevel > 50 && _stressLevel < 80) _status = StressStatus.HighStress;
        if (_stressLevel > 80 && _stressLevel <= 100) _status = StressStatus.Scared;
    }
}

public  enum StressStatus { Ñalmn, LowStress, MediumStress, HighStress, Scared };