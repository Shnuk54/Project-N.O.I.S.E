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


    [ContextMenu("ChangeStress")]
    private void ChangeStress()
    {
        ChangeStress(Random.Range(-5,5),Random.Range(0.1f,1f));
    }
   private void ChangeStress(float stress,float speed )
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
    }
}

public  enum StressStatus { Ñalmn, LowStress, MediumStress, HighStress, Scared };