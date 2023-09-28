using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class CellComponent : MonoBehaviour
    {
        [SerializeField] private bool m_destroyProjectile;
        [SerializeField] private bool m_destroyCell;

        public bool DestroyProjectile => m_destroyProjectile;
        public bool DestroyCell => m_destroyCell;
    }
}