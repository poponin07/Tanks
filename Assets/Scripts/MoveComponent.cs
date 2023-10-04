using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private float m_speed = 1f;

        public void OnMove(DirectionType type)
        {
            transform.position += Extensions.ConvertTypeFromDirection(type) * (m_speed * Time.deltaTime);
            transform.eulerAngles = Extensions.ConvertTypeFromDRotation(type);
        }
        
        
    }
}