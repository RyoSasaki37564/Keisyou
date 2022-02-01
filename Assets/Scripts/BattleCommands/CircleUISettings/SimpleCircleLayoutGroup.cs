using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleCircleLayoutGroup : UIBehaviour, ILayoutGroup
{
	public float m_radius = 100;
	public float m_offsetAngle;

	Vector2 m_mouseEntPos; //クリック地点

	[SerializeField] float m_speedRate = 1.5f;

	protected override void OnValidate()
	{
		base.OnValidate();
		Arrange();
	}

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
		float splitAngle = 360 / transform.childCount;
		//var rect = transform as RectTransform;

		for (int elementId = 0; elementId < transform.childCount; elementId++)
		{
			var child = transform.GetChild(elementId) as RectTransform;
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
		}; //Beganはタッチマネージャーのインスタンス生成も持ってる

		TouchManager.Moved += (info) =>
		{
			m_offsetAngle -= (m_mouseEntPos.y - Input.mousePosition.y) / (m_radius / m_speedRate);
			Arrange();
		};

		TouchManager.Ended += (info) =>
		{

		};
	}
}