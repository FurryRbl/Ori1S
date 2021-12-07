using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class ObjectBasedMotionBlur : MonoBehaviour
{
	// Token: 0x06000A10 RID: 2576 RVA: 0x0002BB84 File Offset: 0x00029D84
	private void Start()
	{
		this.m_oldPosition = base.transform.position;
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x0002BB98 File Offset: 0x00029D98
	private void FixedUpdate()
	{
		this.CleanupGeneratedObjects();
		this.GenerateBlurObjects();
		this.m_oldPosition = base.transform.position;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0002BBC4 File Offset: 0x00029DC4
	private void GenerateBlurObjects()
	{
		Vector3 a = base.transform.position - this.m_oldPosition;
		a *= this.MotionBlurAmount;
		Vector3 a2 = base.transform.position - 0.5f * a;
		for (int i = 0; i < 10; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(base.gameObject);
			gameObject.transform.position = a2 + a * ((float)i / 9f);
			this.m_generatedGameObjects.Add(gameObject);
			Color color = gameObject.GetComponent<Renderer>().material.color;
			color.a = 0.09090909f;
			gameObject.GetComponent<Renderer>().material.color = color;
		}
		Color color2 = base.GetComponent<Renderer>().material.color;
		color2.a = 0.09090909f;
		base.GetComponent<Renderer>().material.color = color2;
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0002BCC0 File Offset: 0x00029EC0
	private void CleanupGeneratedObjects()
	{
		foreach (GameObject gameObject in this.m_generatedGameObjects)
		{
			InstantiateUtility.Destroy(gameObject);
		}
		this.m_generatedGameObjects.Clear();
	}

	// Token: 0x04000846 RID: 2118
	private List<GameObject> m_generatedGameObjects = new List<GameObject>();

	// Token: 0x04000847 RID: 2119
	private Vector3 m_oldPosition;

	// Token: 0x04000848 RID: 2120
	public float MotionBlurAmount = 1f;
}
