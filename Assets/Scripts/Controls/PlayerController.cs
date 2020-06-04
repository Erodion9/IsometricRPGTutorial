using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            EvaluateInput();
        }

        private void EvaluateInput()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool didHit = Physics.Raycast(ray, out RaycastHit raycastHit);

            if (didHit)
            {
                GetComponent<Mover>().MoveTo(raycastHit.point);
            }
        }
    }
}
