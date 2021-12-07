using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020004C4 RID: 1220
public class UberShaderDetector : MonoBehaviour
{
	// Token: 0x1700059B RID: 1435
	// (get) Token: 0x0600211A RID: 8474 RVA: 0x00091223 File Offset: 0x0008F423
	// (set) Token: 0x0600211B RID: 8475 RVA: 0x00091230 File Offset: 0x0008F430
	public static bool Enabled
	{
		get
		{
			return UberShaderDetector.m_instance != null;
		}
		set
		{
			if (UberShaderDetector.Enabled != value)
			{
				if (value)
				{
					UberShaderDetector.m_instance = new GameObject("uberShaderCatcher");
					UberShaderDetector.m_instance.AddComponent<UberShaderDetector>();
				}
				else
				{
					UnityEngine.Object.Destroy(UberShaderDetector.m_instance);
					UberShaderDetector.m_instance = null;
				}
			}
		}
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x00091280 File Offset: 0x0008F480
	public string FullPath(Transform target)
	{
		Transform transform = target;
		string text = string.Empty;
		while (transform != null)
		{
			text = transform.name + "\\" + text;
			transform = transform.parent;
		}
		return text;
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x000912C0 File Offset: 0x0008F4C0
	public void Update()
	{
		if (Time.frameCount % 20 == 0)
		{
			foreach (UberShaderComponent uberShaderComponent in UnityEngine.Object.FindObjectsOfType<UberShaderComponent>())
			{
				string text = this.FullPath(uberShaderComponent.transform);
				if (!this.m_found.Contains(text))
				{
					this.m_found.Add(text);
					Debug.LogError("Found an uber shader: " + text);
				}
			}
		}
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x00091334 File Offset: 0x0008F534
	public void OnDisable()
	{
		string path = Path.Combine(OutputFolder.BuildOutputPath, "uberShaderCatcher.txt");
		StreamWriter streamWriter = new StreamWriter(new FileStream(path, FileMode.Append));
		using (StreamWriter streamWriter2 = streamWriter)
		{
			foreach (string value in this.m_found)
			{
				streamWriter2.WriteLine(value);
			}
		}
	}

	// Token: 0x04001C02 RID: 7170
	private HashSet<string> m_found = new HashSet<string>();

	// Token: 0x04001C03 RID: 7171
	private static GameObject m_instance;
}
