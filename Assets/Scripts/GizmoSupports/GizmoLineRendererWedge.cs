using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoLineRendererWedge : MonoBehaviour
{
    [SerializeField] public GizmoLineRendererWedge m_linkedWedge;

    private void OnDrawGizmos()
    {
        if(m_linkedWedge)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, m_linkedWedge.transform.position);
        }
    }
}
