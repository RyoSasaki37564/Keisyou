using UnityEditor;
using UnityEngine;

public class ActorMoveGizmoEditor
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(Enemy))]
    private static void DrawGizmo(GizmoLineRendererWedge startWedge, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(startWedge.transform.position, startWedge.m_linkedWedge.transform.position);
    }
}