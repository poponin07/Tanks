using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
public class FireComponent : MonoBehaviour
{
    [SerializeField,Range(0.1f, 1f)] private float m_delayFire = 0.25f;
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private SideType m_syde;
    private bool m_canFire = true;
    
    
}
}