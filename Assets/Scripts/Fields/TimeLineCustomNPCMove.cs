using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineCustomNPCMove : PlayableAsset
{
    [SerializeField] private ExposedReference<GameObject> templateGameObject;

    public NPCMoveCustomPlayableBehaviour template = new NPCMoveCustomPlayableBehaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<NPCMoveCustomPlayableBehaviour>.Create(graph, template);

        var behaviour = playable.GetBehaviour();

        behaviour.m_templateGameObject = templateGameObject.Resolve(graph.GetResolver());

        return playable;
    }
}
