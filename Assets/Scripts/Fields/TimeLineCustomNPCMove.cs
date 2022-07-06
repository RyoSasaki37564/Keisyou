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
        var playable = ScriptPlayable<NPCMoveTweenClip>.Create(graph, template);

        var behaviour = playable.GetBehaviour();

        behaviour.m_templateGameObject = templateGameObject.Resolve(graph.GetResolver());

        return playable;
    }
}
