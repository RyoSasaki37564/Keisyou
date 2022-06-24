using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TimeLineCustomCon : PlayableAsset
{
    [SerializeField] private ExposedReference<GameObject> templateGameObject;

    public CustomPlayableBehaviour template = new CustomPlayableBehaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<CustomPlayableBehaviour>.Create(graph, template);

        var behaviour = playable.GetBehaviour();

        behaviour.m_templateGameObject = templateGameObject.Resolve(graph.GetResolver());

        return playable;
    }
}
