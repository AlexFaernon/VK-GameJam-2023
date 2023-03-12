using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemy : MonoBehaviour
{
    public static int Charge;
    private const float ControlRange = 5;
    private bool _isMouseOver;
    private bool _inPlayerRange;
    private bool _controlledByPlayer;
    private SpriteRenderer _spriteRenderer;
    private CameraFollow _cameraFollow;
    private Transform _player;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cameraFollow = Camera.main.GetComponent<CameraFollow>();
        _player = _cameraFollow.objectToFollow;
    }

    private void Update()
    {
        var playerDistance = (_player.transform.position - transform.position).magnitude;
        _inPlayerRange = playerDistance <= ControlRange;

        if (_controlledByPlayer)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ReleaseControl();
            }
            return;
        }
        
        if (_isMouseOver && _inPlayerRange && Charge >= 4)
        {
            _spriteRenderer.color = Color.yellow;
            if (Input.GetMouseButtonDown(1))
            {
                TakeControl();
            }
        }
        else
        {
            _spriteRenderer.color = Color.white;
        }
    }

    private void TakeControl()
    {
        Charge = 0;
        gameObject.SendMessage("PlayerControl", true);
        _controlledByPlayer = true;
        _player.gameObject.SetActive(false);
        _cameraFollow.objectToFollow = transform;
    }

    private void ReleaseControl()
    {
        gameObject.SendMessage("PlayerControl", false);
        _controlledByPlayer = false;
        _cameraFollow.objectToFollow = _player;
        _player.gameObject.SetActive(true);
    }

    private void OnMouseEnter()
    {
        _isMouseOver = true;
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
    }

    private void OnDestroy()
    {
        if (_controlledByPlayer)
        {
            _cameraFollow.objectToFollow = _player;
            _player.gameObject.SetActive(true);
        }
    }
}
