using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation : MonoBehaviour
{

    Animator _animator;
    public PlayerMove playerMove;
    Vector3 move;

    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        move = playerMove.rb.velocity;

        //Animating
        float velocityZ = Vector3.Dot(move.normalized, transform.forward);
        float velocityX = Vector3.Dot(move.normalized, transform.right);

        _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
    }
}
