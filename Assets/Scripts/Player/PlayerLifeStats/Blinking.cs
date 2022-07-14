using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    [SerializeField] private AnimationCurve _blinkDuration;
    [SerializeField] private bool _eyesClosed = false;
    [SerializeField] public bool startBlinking = false;
    [SerializeField] private Image _image;
    
    private float _time;
    private float _pastTime;

    private void Start()
    {
        _time = _blinkDuration.keys[_blinkDuration.keys.Length - 1].time;
    }

    private void OnEnable()
    {
        Events.onPlayerStartBlink += Blink;
    }
    private void OnDisable()
    {
        Events.onPlayerStartBlink -= Blink;
    }
    private void Blink()
    {
        StartCoroutine("MakeBlink");
    }

    private IEnumerator MakeBlink()
    {
        startBlinking = true;
        while (_pastTime < _time)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, _blinkDuration.Evaluate(_pastTime));
            _pastTime += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
           
            if (_image.color.a >= 0.8f && _eyesClosed == false)
            {
                _eyesClosed = true;
                Events.instance.OnPlayerBlink();
            } 
        }
        _pastTime = 0;
        _eyesClosed = false;
        startBlinking = false;
    }
}
