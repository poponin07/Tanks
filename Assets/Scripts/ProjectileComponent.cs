using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    [RequireComponent(typeof(MoveComponent))]
public class ProjectileComponent : MonoBehaviour
{
    private SideType m_side;
    private DirectionType m_direction;
    private MoveComponent m_moveComp;

    [SerializeField, Range(0,100)] private int m_damage = 1;
    [SerializeField, Range(1, 10)] private float m_lifeTime = 5; 
    private void Start()
    {
        m_moveComp = GetComponent<MoveComponent>();
        
        Destroy(gameObject, m_lifeTime);
    }

    public void SetParams(DirectionType direction, SideType side)
    => (m_direction, m_side) = (direction, side);


    private void Update()
    {
        m_moveComp.OnMove(m_direction);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var fire = col.GetComponent<FireComponent>();
        if (fire != null)
        {
            Debug.Log(fire.GetSide);
            Debug.Log(m_side);
            if (fire.GetSide == m_side) return;

            var condition = fire.GetComponent<ConditionComponent>();
            condition.SetDamage(m_damage);
            Destroy(gameObject);
            return;
        }

        var cell = col.GetComponent<CellComponent>();
         if(cell != null)
        {
            if (cell.DestroyProjectile) Destroy(gameObject);
            if (cell.DestroyCell) Destroy(cell.gameObject);
            return;
        }
    }
}
}
