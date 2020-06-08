using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        private Health target;
        private Mover mover;
        private float timeSinceLastAttack = 0;

        void Start()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;

            if (!IsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //This will trigger the Hit() event.
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;

            }
        }

        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            return (combatTarget != null) && !combatTarget.GetComponent<Health>().IsDead();
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            this.target = null;
        }


    }
}