using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatSounds : MonoBehaviour
{
    [Header("Heart sounds")]
    [SerializeField] private List<AudioClip> _leftVentricleSounds;
    [SerializeField] private List<AudioClip> _rightVentricleSounds;
    [SerializeField] private AudioSource _source;
   


   
    [Header("Pulse settings")]
    [SerializeField] private float _pulse;
    [Range(1, 200)]
    [SerializeField] private float _targetPulse;
    [SerializeField] private float _pulseChangeSpeed;

  
    [SerializeField] private float _delay;

   
    

    private void Start()
    {
        _targetPulse = _pulse;
        _delay = 60 / _pulse;
        PlayHeartBeat();
    }

    public float Pulse
    {
        get { return _pulse; }
        set {
            _targetPulse = value;
        }
    }
    private void FixedUpdate()
    {
        if(PlayerStateHandler.instance.PlayerState.isDead == false) SmoothChangeHeartBeat();
    }
    
    private void SmoothChangeHeartBeat()
    {
        if (_pulse > _targetPulse || _pulse < _targetPulse)
        {
            _pulse = Mathf.Lerp(_pulse, _targetPulse, _pulseChangeSpeed * Time.deltaTime);
            _delay = 60f / _pulse;
            
        }
    }
   
    private void PlayHeartBeat()
    {
        StartCoroutine("HeartBeat");
    }
    private IEnumerator HeartBeat()
    {
        while (PlayerStateHandler.instance.PlayerState.isDead == false)
        {
            _source.clip = _leftVentricleSounds[Random.Range(0, _leftVentricleSounds.Count - 1)];
            _source.Play();
            yield return new WaitForSeconds(_delay); 
            _source.clip = _rightVentricleSounds[Random.Range(0, _rightVentricleSounds.Count - 1)];
            _source.Play();
            yield return new WaitForSeconds(0.2f); 
        }
    }
}
