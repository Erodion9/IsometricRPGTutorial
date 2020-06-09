using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i).position, waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i).position, GetWaypoint(j).position);
            }
        }

        private int GetNextIndex(int i)
        {
            return (i + 1) % (transform.childCount);
        }

        private Transform GetWaypoint(int i)
        {
            return transform.GetChild(i);
        }
    }
}