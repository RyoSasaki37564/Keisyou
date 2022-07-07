using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.855f, 0.8623f, 0.870f)]
[TrackClipType(typeof(NPCMoveTweenClip))]
[TrackBindingType(typeof(GameObject))]
public class TimeLineCustomNPCMove : TrackAsset
{
    [SerializeField] private ExposedReference<GameObject> templateGameObject;

    public NPCMoveTweenClip template = new NPCMoveTweenClip();

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int input)
    {
        var playable = ScriptPlayable<NPCMoveTweenClip>.Create(graph, input);

        var behaviour = playable.GetBehaviour();

        behaviour.m_templateGameObject = templateGameObject.Resolve(graph.GetResolver());

        return playable;
    }

    public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        var comp = director.GetGenericBinding(this) as GameObject;
        if (comp == null)
            return;
        var so = new UnityEditor.SerializedObject(comp);
        var iter = so.GetIterator();
        while (iter.NextVisible(true))
        {
            if (iter.hasVisibleChildren)
                continue;
            driver.AddFromName(comp.gameObject, iter.propertyPath);
        }
#endif
        base.GatherProperties(director, driver);
    }
}
