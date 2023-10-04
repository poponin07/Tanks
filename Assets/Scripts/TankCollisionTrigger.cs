using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class TankCollisionTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {

                if (col.transform?.GetComponent<CellComponent>()?.DestroyCell == false ||
                    col.transform?.GetComponent<BotComponent>() != null)
                {
                    transform.parent.GetComponent<BotComponent>().Verrt(DirectionType.Up);
                }

        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform?.GetComponent<CellComponent>()?.DestroyCell == false ||
                collision.transform?.GetComponent<BotComponent>() != null)
                {
                    transform.parent.GetComponent<BotComponent>().Verrt(DirectionType.Up);
                }
        }
    }
}