using RPG.Movement;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;
        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                currentAction.Cancel();
                print("Cancelled: " + currentAction);
            }
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}