using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TimeLineTextCustomCon : PlayableAsset
{
    [SerializeField] private ExposedReference<GameObject> templateGameObject;

    public TextCustomPlayableBehaviour template = new TextCustomPlayableBehaviour();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<TextCustomPlayableBehaviour>.Create(graph, template);

        var behaviour = playable.GetBehaviour();

        behaviour.m_templateGameObject = templateGameObject.Resolve(graph.GetResolver());

        return playable;
    }
}
