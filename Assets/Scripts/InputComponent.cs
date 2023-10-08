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
    private DirectionType m_lastType = DirectionType.Up;
    private MoveComponent m_moveComp;
    private FireComponent m_fireComp;

    [SerializeField] private InputAction m_move;
    [SerializeField] private InputAction m_fire;

    public Transform rayPoint;

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

    private void LateUpdate()
    {
        BotProvocation();
    }

    private void BotProvocation() //агр бота
    {
        Vector3 vec = Extensions.ConvertTypeFromDirection(m_lastType);
            RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, new Vector3(vec.x, vec.y), 100f);
            BotComponent bot = hit.collider.gameObject.GetComponent<BotComponent>();
                if (bot != null)
                {
                    Vector3 direction = Extensions.ConvertTypeFromDirection(m_lastType);
                    direction = new Vector3(direction.x * -1f,  direction.y * -1f, 0f);
                    DirectionType directionType = Extensions.ConvertDirectionFromType(direction);
                    bot.isLookAtPlayer = true;
                    bot.BotLogicVision(directionType);
                }
            
    }
    private void OnDestroy()
    {
        m_move.Disable();
        m_fire.Disable();
    }
}
}