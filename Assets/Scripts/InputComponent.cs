using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks
{
    [RequireComponent(typeof(MoveComponent),typeof(FireComponent) )]
public class InputComponent : MonoBehaviour
{
    private DirectionType m_lastType;
    private MoveComponent m_moveComp;
    private FireComponent m_fireComp;

    [SerializeField] private InputAction m_move;
    [SerializeField] private InputAction m_fire;

    private void Start()
    {
        m_moveComp = GetComponent<MoveComponent>();
        m_fireComp = GetComponent<FireComponent>();
        
        m_move.Enable();
        m_fire.Enable();
    }

    private void Update()
    {
        var fire = m_fire.ReadValue<float>();
        if (fire == 1f) m_fireComp.OnFire();
        
        var direction = m_move.ReadValue<Vector2>();
        DirectionType type;
        
        if (direction.x != 0f && direction.y != 0f)
        {
            type = m_lastType;
        }
        else if (direction.x == 0f && direction.y == 0f) return;
        else type = m_lastType = Extensions.ConvertDirectionFromType(direction);
        
        m_moveComp.OnMove(type);
    }

    private void OnDestroy()
    {
        m_move.Disable();
        m_fire.Disable();
    }
}
}