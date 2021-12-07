using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000486 RID: 1158
public class HasComponentGameObjectFilter : GameObjectFilter
{
	// Token: 0x06001F9D RID: 8093 RVA: 0x0008B198 File Offset: 0x00089398
	public void Start()
	{
		foreach (string typeName in this.AffectingClasses)
		{
			Type type = Type.GetType(typeName);
			if (type != null)
			{
				this.m_affectingClasses.Add(type);
			}
		}
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x0008B204 File Offset: 0x00089404
	public override bool Valid(GameObject gameObject)
	{
		return base.Valid(gameObject) && this.m_affectingClasses.Any((Type affectingClass) => gameObject.GetComponent(affectingClass));
	}

	// Token: 0x04001B33 RID: 6963
	public List<string> AffectingClasses;

	// Token: 0x04001B34 RID: 6964
	private readonly List<Type> m_affectingClasses = new List<Type>();
}
