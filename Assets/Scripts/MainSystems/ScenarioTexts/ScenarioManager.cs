using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ScenarioManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
             CustomPlayableBehaviour.m_director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
        }
    }
}
