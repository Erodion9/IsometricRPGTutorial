using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 4f;
        private Transform target;
        private Mover mover;

        void Start()
        {
            mover = GetComponent<Mover>();
        }

        void Update()
        {
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;
            if (target != null && !isInRange)
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Stop();
            }
        }
        public void Attack(CombatTarget combatTarget)
        {
            this.target = combatTarget.transform;
        }
    }
}