using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    [RequireComponent( typeof(SpriteRenderer))]
    public class PlayerConditionComponent : ConditionComponent
    {
        private bool m_isImmortal;
        private Vector3 m_startPoint;
        private SpriteRenderer m_render;
        [SerializeField, Range(0.1f, 5)] private float m_timeImmortal = 3f;
        private float m_immortalSwitchVisual = 0.2f;
        private void Start()
        {
            m_startPoint = transform.position;
            m_render = GetComponent<SpriteRenderer>();
        }

        public override void SetDamage(int damage)
        {
            if (m_isImmortal) return;
            
            m_health -= damage;
            transform.position = m_startPoint; 
            Coroutine immortalCor = StartCoroutine(OnImmortal());
            
            if (m_health <= 0f)
            {
                StopCoroutine(immortalCor);
                Destroy(gameObject);
            }
        }

        private IEnumerator OnImmortal()
        {
            m_isImmortal = true;
            var time = m_timeImmortal;
           while (time > 0)
           {
               time -= Time.deltaTime;
               m_render.enabled = !m_render.enabled;
               yield return new WaitForSeconds(m_immortalSwitchVisual);
               
           }
           m_isImmortal = false;
           m_render.enabled = true;
        }
        }
}