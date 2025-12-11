using System;
using GameDevUtils.Runtime.Extensions;
using UnityEngine;

namespace PanzerHero.Runtime.LevelDesign
{
    public class LevelEndTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.IsSameMask("Player"))
            {
                var statement = GameStatement.GetInstance;
                statement.FinishGame();
            }
        }
    }
}