using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleCircleLayoutGroup : UIBehaviour, ILayoutGroup
{
	public float m_radius = 100; //デカさ
	public float m_offsetAngle; //そのボタンのいる角度

	Vector2 m_mouseEntPos; //クリック地点

	[SerializeField] float m_speedRate = 1.5f;

	[SerializeField] List<GameObject> m_openAndCloseGOList = new List<GameObject>();


	//ここんとところ、Editorでしか機能しないしビルド時コンパイルエラー吐くぞ。プリプロっとけば安心☆
#if UNITY_EDITOR
	protected override void OnValidate()
	{
		base.OnValidate();
		Arrange();
	}
#endif

	// 要素数が変わると自動的に呼ばれるコールバック
	#region ILayoutController implementation
	public void SetLayoutHorizontal() { }
	public void SetLayoutVertical()
	{
		Arrange();
	}
	#endregion

	void Arrange()
	{
		if(transform.childCount == 0) { return; }

		float splitAngle = 360 / transform.childCount;

		for (int elementId = 0; elementId < transform.childCount; elementId++)
		{
			var child = transform.GetChild(elementId) as RectTransform;
			//要素数とインデックス、三角関数で対応する角度に子を設置
			float currentAngle = splitAngle * elementId + m_offsetAngle;
			child.anchoredPosition = new Vector2(
				Mathf.Cos(currentAngle * Mathf.Deg2Rad),
				Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * m_radius;
		}
	}

    protected override void Start()
    {
		TouchManager.Began += (info) =>
		{
			m_mouseEntPos = Input.mousePosition;
		};

		TouchManager.Moved += (info) =>
		{
			//入力位置から移動位置の差で加速
			if(gameObject.activeSelf)
			{
				m_offsetAngle -= (m_mouseEntPos.y - Input.mousePosition.y) / (m_radius / m_speedRate);
				if (m_offsetAngle > 360 || m_offsetAngle < -360)
				{
					m_offsetAngle /= 360;
				}
				Arrange();
			}
		};
	}

	public void OpenCloseSunAndMoon()
    {
		foreach(var g in m_openAndCloseGOList)
		{
			g.SetActive(!g.activeSelf);
		}
    }
}