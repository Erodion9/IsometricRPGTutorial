using RPG.Movement;
using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private MonoBehaviour currentAction;
        public void StartAction(MonoBehaviour action)
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                print("Cancelled: " + currentAction);
            }
            currentAction = action;
        } 
    }
}