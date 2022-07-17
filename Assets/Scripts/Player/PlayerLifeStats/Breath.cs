using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private List<AudioClip> _inhaleSounds;
    [SerializeField] private List<AudioClip> _exhaleSounds;

    [Header("Breath Settings")]
    [SerializeField] private float _delay;
    [SerializeField] private float _breathingSpeed;

    [Range(0,2f)]
    [SerializeField] private float _breathChangeSpeed;
    [Range(0, 60)]
    [SerializeField] private int _targetBreathSpeed;

    
    private AudioSource _source;

    private void Start()
    {
        _targetBreathSpeed = Mathf.RoundToInt(_breathingSpeed);
        _delay = 60f / _breathingSpeed;
        _source = GetComponent<AudioSource>();
        StartBreathing();
    }
    public float BreathSpeed
    {
        get { return _breathingSpeed; }
        set
        {
            _targetBreathSpeed = Mathf.RoundToInt(value);
        }
    }
    private void FixedUpdate()
    {
        if (PlayerStateHandler.instance.PlayerState.isDead == false) SmoothChangeBreathSpeed();
    }
    private void SmoothChangeBreathSpeed()
    {
        if (_breathingSpeed == _targetBreathSpeed) return;
        if (_breathingSpeed > _targetBreathSpeed || _breathingSpeed < _targetBreathSpeed)
        {
            _breathingSpeed = Mathf.Lerp(_breathingSpeed, _targetBreathSpeed, _breathChangeSpeed * Time.deltaTime);
            var gate = _targetBreathSpeed - _breathingSpeed;
            gate = Mathf.Abs(gate);
            if (gate < 0.4f && _breathingSpeed != _targetBreathSpeed) _breathingSpeed = Mathf.Round(_breathingSpeed);
            _delay = 60f / _breathingSpeed;

        }
    }
    private void StartBreathing()
    {
        StartCoroutine("Breathing");
    }
    private IEnumerator Breathing()
    {
        while (PlayerStateHandler.instance.PlayerState.isDead == false)
        {
            _source.clip = _inhaleSounds[Random.Range(0,_inhaleSounds.Count-1)];
            _source.Play();
            yield return new WaitForSeconds(_delay/2);
            _source.clip = _exhaleSounds[Random.Range(0, _exhaleSounds.Count - 1)];
            _source.Play();
            yield return new WaitForSeconds(_delay / 2);
        }
        
    }
}
