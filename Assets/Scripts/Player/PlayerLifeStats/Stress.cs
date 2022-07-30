using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoBehaviour
{
    [Header("Stress")]
    [SerializeField] private float _stress;
    [SerializeField] private float _calmingSpeed;
    [SerializeField] private float _changeSpeed;
    [SerializeField] private float _minimumStress = 0;
    [SerializeField] private float _targetStress;
    [SerializeField] private StressStatus _status;

    [SerializeField] private bool _stressedUp;
    [SerializeField] private bool _alreadyStressed;
    [SerializeField] private bool _calmedDown;
    [SerializeField] private float _nextScareDelay;
    [SerializeField] private float _timeBeforeCalming;
    [SerializeField] private float _pastTime;



    public float TargetStress{
        get { return _targetStress; }
        set { _targetStress = value; }
    }
    public float StressValue
    {
        get { return _stress; }
        
    }
    public float StressChangeSpeed
    {
        get { return _changeSpeed; }
    }
    public StressStatus StressStatus
    {
        get { return _status; }
    }

    public void ChangeStress(float stress,float speed)
    {
       if(stress > 0) _stressedUp = true;

        if (_targetStress + stress > 100) {
            _targetStress = 100;
            _changeSpeed = speed;
            if (_alreadyStressed)
            {
                _changeSpeed = speed / 3;
            }
            
            return;
        } 
        if (_targetStress + stress < 0f)
        {
            _targetStress = 0;
            _changeSpeed = _calmingSpeed;
            return;
        } 

        _targetStress += stress;
        _changeSpeed = 0.1f;
    }

    private void FixedUpdate()
    {
        SmoothChangeStress(_changeSpeed,_minimumStress);
       
    }
    private void Update()
    {
        Timer();
    }
    private void Timer()
    {
        if (_stressedUp)
        {
            _calmedDown = false;
            _alreadyStressed = true;
            _pastTime += Time.deltaTime;

            if (_pastTime >= _nextScareDelay) _alreadyStressed = false;

            if (_pastTime >= _timeBeforeCalming && _alreadyStressed == false)
            {
                _pastTime = 0;
                _alreadyStressed = false;
                _stressedUp = false;
                _calmedDown = true;
            }
        }
    }

    private void SmoothChangeStress(float changeSpeed,float minimumStress)
    {
        if (_calmedDown)
        {
            _targetStress = Mathf.Lerp(_targetStress, minimumStress, _calmingSpeed * Time.deltaTime);
            var gate = _targetStress - minimumStress;
            gate = Mathf.Abs(gate);
            if (gate < 0.4f && minimumStress != _targetStress) _targetStress = Mathf.Round(_targetStress);
        }
       

        if (_stress == _targetStress) {
            return;
        }
        
        if (_stress > _targetStress || _stress < _targetStress)
        {
            _stress = Mathf.Lerp(_stress, _targetStress, changeSpeed * Time.deltaTime);
            var gate = _targetStress - _stress;
            gate = Mathf.Abs(gate);
            if (gate < 0.4f && _stress != _targetStress) _stress = Mathf.Round(_stress);

        }

        if (_stress <= 10) _status = StressStatus.Ñalmn;
        if(_stress > 10 && _stress < 25) _status = StressStatus.LowStress;
        if (_stress > 25 && _stress < 50) _status = StressStatus.MediumStress;
        if (_stress > 50 && _stress < 80) _status = StressStatus.HighStress;
        if (_stress > 80 && _stress <= 100) _status = StressStatus.Scared;

        
    }
}

    

public  enum StressStatus { Ñalmn, LowStress, MediumStress, HighStress, Scared };