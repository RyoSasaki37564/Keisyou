using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0f, 0.4f, 0.4f)]
[TrackClipType(typeof(NPCMoveTweenClip))]
[TrackBindingType(typeof(GameObject))]
public class TimeLineCustomNPCMove : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int input)
    {
        return ScriptPlayable<NPCMoveTweenMixerBehaviour>.Create(graph, input);
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
