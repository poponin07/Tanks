using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Tanks
{
    public class BotComponent : MonoBehaviour
    {
        [SerializeField, Range(11, 100)] private int m_maxTimeForMove = 25;
        private int m_timeForMove;
        private MoveComponent m_moveComponent;
        private FireComponent m_fireComponent;
        private DirectionType m_direction;
        private Collider2D curHit; //текущий хит рейкаста
        public bool isLookAtPlayer; 
        public Transform rayPoint; //начальная точка луча
        

        private void Start()
        {
            isLookAtPlayer = false;
            m_moveComponent = GetComponent<MoveComponent>();
            m_fireComponent = GetComponent<FireComponent>();
            m_direction = Extensions.GetRandomDirecrion();
            SetRandomTime();
            StartCoroutine(ApplyDirection());
        }

        private void FixedUpdate()
        {
            BotVision();
        }


        public void BotLogicVision(DirectionType playerDirecrionType)
        {
            m_direction = Extensions.GetRandomDirecrion();
            StopAllCoroutines();
            if (isLookAtPlayer)
            {
                m_direction = playerDirecrionType;
            }
            SetRandomTime();
            StartCoroutine(ApplyDirection());
        } //основные шаги зрения бота
        
      
        private void SetRandomTime()
        {
            Random random = new Random();
            m_timeForMove = random.Next(10,m_maxTimeForMove) / 10;
        } //установка рандомного время

        private void BotVision()
        {
            Vector3 vec = Extensions.ConvertTypeFromDirection(m_direction);
            RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, new Vector3(vec.x, vec.y), 0.1f);
            if (hit.transform.GetComponent<CellComponent>().DestroyCell == false || hit.transform.GetComponent<BotComponent>() == true)
            {
                BotLogicVision(DirectionType.Up);
            }
        } //зрение бота

        IEnumerator ApplyDirection() //логика  движения бота
        {
            transform.eulerAngles = Extensions.ConvertTypeFromDRotation(m_direction);
            float time = m_timeForMove;
            while (time > 0f)
            {
                time -= Time.deltaTime;
                m_moveComponent.OnMove(m_direction);
                yield return null;
            }
            isLookAtPlayer = false;
            BotLogicVision(DirectionType.Up);
        }
    }
}