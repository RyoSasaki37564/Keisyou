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

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
		{
			m_mouseEntPos = Input.mousePosition;
			Debug.Log(m_mouseEntPos);
		}
		else if(Input.GetMouseButton(0))
		{
			m_offsetAngle -= (m_mouseEntPos.y - Input.mousePosition.y) / (m_radius / m_speedRate);
			Arrange();
		}
    }
}