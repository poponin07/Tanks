using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class SpawnPointComponent : MonoBehaviour
    {
        [SerializeField] private bool canSpawn = true;
        public bool GetCanSpawn => canSpawn;
        
        [SerializeField] private bool isPlayerSpawnPoint;
        [SerializeField] private PlayerConditionComponent m_playerConditionComponent;

        public bool GetIsPlayerSpawnPoint => isPlayerSpawnPoint;
        private void Start()
        {
            if (m_playerConditionComponent)
            {
                m_playerConditionComponent.StartPoint = transform; 
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.CompareTag("Character")) canSpawn = false ;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Character")) canSpawn = true;
        }
    }
}