using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PallyParticleSettings : MonoBehaviour
{
    [SerializeField] ParticleSystem m_thisPaticle = default;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_thisPaticle.Simulate(Time.unscaledDeltaTime, false, false);

    }

    private void OnEnable()
    {
        StartCoroutine(Stopper());
    }

    IEnumerator Stopper()
    {
        yield return new WaitForSeconds(2.0f);

        this.gameObject.SetActive(false);
    }
}
