using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController _charController;

    [Range(0.01f,0.1f)]
    [SerializeField] float _speed;

    [Range(0.02f, 0.1f)]
    [SerializeField] float _sprintSpeed;

    [Range(0.5f,10)]
    [SerializeField] float _mouseSens;

    [Range(-10,10)]
    [SerializeField] float _gravity = -5;

    [SerializeField] float _maxXRot;
    [SerializeField] float _minXRot;
    private float _xRot;
    private float _yRot;
    private Transform _transform;
   
   

   
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
       
    }
    
    public void Move(){
        var verInput = InputHandler.instance.verInput;
        var horInput = InputHandler.instance.horInput;
        var forward = _transform.forward.normalized;
        var right = _transform.right.normalized;
        forward.y = 0;
        right.y = 0;

        Vector3 dir;
        dir =  forward*verInput + right*horInput;
        if(InputHandler.instance.sprinting == false)dir = dir * _speed;
        if (InputHandler.instance.sprinting) dir = dir * _sprintSpeed;
        dir.y = _gravity;
   
        _charController.Move(dir);
    }
     public void Rotation(){
            _yRot += InputHandler.instance.mouseX * _mouseSens;
            _xRot -= InputHandler.instance.mouseY * _mouseSens;
            _xRot = Mathf.Clamp(_xRot, _minXRot, _maxXRot);
            _transform.localRotation = Quaternion.Euler(_xRot, _yRot, 0);
    }
}
