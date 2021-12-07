using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200063A RID: 1594
public class SceneLayers : MonoBehaviour
{
	// Token: 0x0600271A RID: 10010 RVA: 0x000AAD98 File Offset: 0x000A8F98
	public static bool IsLocked(GameObject go)
	{
		string tag = go.tag;
		Transform parent = go.transform.parent;
		while (parent)
		{
			if (parent.name == "center" && SceneLayers.Current.Center.Locked)
			{
				return true;
			}
			if (parent.name == "bg" && SceneLayers.Current.Background.Locked)
			{
				return true;
			}
			if (parent.name == "fg" && SceneLayers.Current.Foreground.Locked)
			{
				return true;
			}
			parent = parent.parent;
		}
		foreach (SceneLayers.Layer layer in SceneLayers.Current.Layers)
		{
			if (layer.Name == tag && layer.Locked)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x1700062B RID: 1579
	// (get) Token: 0x0600271B RID: 10011 RVA: 0x000AAE98 File Offset: 0x000A9098
	public static SceneLayers Current
	{
		get
		{
			if (SceneLayers.m_current == null)
			{
				SceneLayers.m_current = (SceneLayers)UnityEngine.Object.FindObjectOfType(typeof(SceneLayers));
			}
			if (SceneLayers.m_current == null)
			{
				SceneLayers.m_current = new GameObject("sceneLayers")
				{
					tag = "EditorOnly",
					hideFlags = HideFlags.HideInHierarchy
				}.AddComponent<SceneLayers>();
			}
			return SceneLayers.m_current;
		}
	}

	// Token: 0x040021B2 RID: 8626
	private static SceneLayers m_current;

	// Token: 0x040021B3 RID: 8627
	public SceneLayers.Layer[] Layers = new SceneLayers.Layer[]
	{
		new SceneLayers.Layer("Transparent"),
		new SceneLayers.Layer("Dynamic Objects"),
		new SceneLayers.Layer("Particles"),
		new SceneLayers.Layer("Collider"),
		new SceneLayers.Layer("Utility")
	};

	// Token: 0x040021B4 RID: 8628
	public SceneLayers.Layer Center = new SceneLayers.Layer("Center");

	// Token: 0x040021B5 RID: 8629
	public SceneLayers.Layer Foreground = new SceneLayers.Layer("Foreground");

	// Token: 0x040021B6 RID: 8630
	public SceneLayers.Layer Background = new SceneLayers.Layer("Background");

	// Token: 0x040021B7 RID: 8631
	public List<Renderer> HiddenRenders = new List<Renderer>();

	// Token: 0x0200063B RID: 1595
	[Serializable]
	public class Layer
	{
		// Token: 0x0600271C RID: 10012 RVA: 0x000AAF0B File Offset: 0x000A910B
		public Layer(string name)
		{
			this.Name = name;
		}

		// Token: 0x040021B8 RID: 8632
		public string Name;

		// Token: 0x040021B9 RID: 8633
		public bool Visible = true;

		// Token: 0x040021BA RID: 8634
		public bool Locked;
	}
}
