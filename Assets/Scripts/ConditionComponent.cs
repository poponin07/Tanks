using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public  class ConditionComponent : MonoBehaviour
    {
        [SerializeField, Range(1, 500)] protected int m_health = 10;
    public virtual void SetDamage(int damage)
    {
        m_health -= damage;

        if (m_health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
}