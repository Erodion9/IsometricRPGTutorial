﻿using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
        private Transform target;
        private Mover mover;

        void Start()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            if (target == null) return;

            if (!IsInRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = combatTarget.transform;
        }

        public void Cancel()
        {
            this.target = null;
        }

        //Animation Event
        void Hit()
        {

        }
    }
}