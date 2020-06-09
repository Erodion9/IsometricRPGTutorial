using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        private Health target;
        private Mover mover;
        private float timeSinceLastAttack = Mathf.Infinity;

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
                TriggerAttack();
                timeSinceLastAttack = 0;

            }
        }

        private void TriggerAttack()
        {
            Animator animator = GetComponent<Animator>();
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
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

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            return (combatTarget != null) && !combatTarget.GetComponent<Health>().IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            UntriggerAttack();
            this.target = null;
        }

        private void UntriggerAttack()
        {
            Animator animator = GetComponent<Animator>();
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("stopAttack");
        }
    }
}