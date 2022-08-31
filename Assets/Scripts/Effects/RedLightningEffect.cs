using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedLightningEffect : MonoBehaviour
{
    [SerializeField] bool m_isUIObject;

    [SerializeField] Sprite[] m_sprites = new Sprite[6];
    [SerializeField] SpriteRenderer m_sr;
    [SerializeField] Image m_img;

    [SerializeField] float m_movingLenge = 17f;

    float m_colorAlfaDef;

    [SerializeField] bool m_isEndless;

    bool m_isStop;

    private void Start()
    {
        m_isStop = false;
        if(m_isUIObject)
        {
            m_img = GetComponent<Image>();
            m_colorAlfaDef = m_img.color.a;
        }
        else
        {
            m_sr = GetComponent<SpriteRenderer>();
            m_colorAlfaDef = m_sr.color.a;
        }
        StartCoroutine(CountDown());
        StartCoroutine(LightningAnimationCol(0.1f));
    }

    private void OnEnable()
    {
        m_isStop = false;
        StartCoroutine(CountDown());
        StartCoroutine(LightningAnimationCol(0.1f));
    }

    IEnumerator CountDown()
    {
        if(!m_isEndless)
        {
            yield return new WaitForSeconds(2f);
            m_isStop = true;
        }
    }

    IEnumerator LightningAnimationCol(float time)
    {
        yield return new WaitForSeconds(time);
        if(m_isStop)
        {
            if(m_isUIObject)
            {
                m_img.color = new Color(m_img.color.r, m_img.color.g, m_img.color.b, m_colorAlfaDef);
                gameObject.SetActive(false);
            }
            else
            {
                m_sr.color = new Color(m_sr.color.r, m_sr.color.g, m_sr.color.b, m_colorAlfaDef);
                if (transform.parent.gameObject.activeSelf)
                {
                    transform.parent.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (m_isUIObject)
            {
                float size = Random.Range(0.2f, 1.7f);
                RectTransform rect = GetComponent<RectTransform>();
                float x = Random.Range(-m_movingLenge, m_movingLenge);
                float y = Random.Range(-m_movingLenge, m_movingLenge);
                rect.anchoredPosition = new Vector2(x, y);
                rect.localScale = new Vector3(size, size, 1);
                size = Random.Range(0, m_sprites.Length - 1);
                m_img.sprite = m_sprites[(int)size];
                //m_img.color = new Color(m_img.color.r, m_img.color.g, m_img.color.b, m_img.color.a - m_img.color.a / 75);
            }
            else
            {
                float size = Random.Range(0.2f, 5f);
                transform.localScale = new Vector3(size, size, 1);
                float x = Random.Range(-m_movingLenge, m_movingLenge);
                float y = Random.Range(-m_movingLenge, m_movingLenge);
                transform.localPosition = new Vector2(x, y);
                size = Random.Range(0, m_sprites.Length - 1);
                m_sr.sprite = m_sprites[(int)size];
            }
            float nextTime = Random.Range(0.01f, 0.07f);
            StartCoroutine(LightningAnimationCol(nextTime));
        }
    }
}
