using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
public class FireComponent : MonoBehaviour
{
    [SerializeField,Range(0.1f, 1f)] private float m_delayFire = 0.25f;
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private SideType m_side;
    private bool m_canFire = true;

    public SideType GetSide => m_side;

    public void OnFire()
    {
        if (!m_canFire) return;
        m_canFire = false;
        var bullet = Instantiate(m_prefab, transform.position, transform.rotation);
       //bullet.Se(Extensions.ConvertDirectionFromType(transform.eulerAngles),m_side);
       bullet.GetComponent<ProjectileComponent>().SetParams(Extensions.ConvertDirectionFromType(transform.eulerAngles),m_side);
        StartCoroutine(СooldownFire());
    }

    IEnumerator СooldownFire()
    {
        m_canFire = false;
        yield return new WaitForSeconds(m_delayFire);
        m_canFire = true;

    }
}
}