using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace Tanks
{
public class FireComponent : MonoBehaviour
{
    [SerializeField,Range(0.1f, 1f)] private float m_delayFire = 0.25f;
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private SideType m_side;
    private bool m_canFire = true;
    private bool m_isBot;

    public SideType GetSide => m_side;

    private void Start()
    {
        if (GetComponent<BotComponent>() == true)
        {
            m_isBot = true;
            OnFire();
        }
    }

    public void OnFire() //стрельба
    {
        if (!m_canFire) return;
        m_canFire = false;
        var bullet = Instantiate(m_prefab, transform.position, transform.rotation);
        bullet.GetComponent<ProjectileComponent>().SetParams(Extensions.ConvertRotationFromType(transform.eulerAngles),m_side);
        StartCoroutine(СooldownFire());
    }

    IEnumerator СooldownFire() //перезарядка стрельбы
    {
        m_canFire = false;
        yield return new WaitForSeconds(m_delayFire);
        m_canFire = true;
        if (m_isBot)  OnFire();
    }
}
}