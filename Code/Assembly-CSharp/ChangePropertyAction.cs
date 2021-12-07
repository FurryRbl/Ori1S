using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020002CA RID: 714
[Category("General")]
public class ChangePropertyAction : ActionMethod
{
	// Token: 0x06001637 RID: 5687 RVA: 0x00061F8C File Offset: 0x0006018C
	public override void Perform(IContext context)
	{
		foreach (GameObject gameObject in this.Targets)
		{
			FieldInfo fieldInfo = null;
			PropertyInfo propertyInfo = null;
			Component obj = null;
			foreach (Component component in gameObject.GetComponents(typeof(Component)))
			{
				if (this.ComponentName == component.GetType().Name)
				{
					foreach (PropertyInfo propertyInfo2 in component.GetType().GetProperties())
					{
						if (propertyInfo2.Name == this.Name)
						{
							obj = component;
							propertyInfo = propertyInfo2;
							break;
						}
					}
					foreach (FieldInfo fieldInfo2 in component.GetType().GetFields())
					{
						if (fieldInfo2.Name == this.Name)
						{
							obj = component;
							fieldInfo = fieldInfo2;
							break;
						}
					}
				}
			}
			if (fieldInfo == null && propertyInfo == null)
			{
				break;
			}
			if (fieldInfo != null)
			{
				switch (this.Type)
				{
				case ChangePropertyAction.PropertyTypes.Int:
					fieldInfo.SetValue(obj, this.IntValue);
					break;
				case ChangePropertyAction.PropertyTypes.Float:
					fieldInfo.SetValue(obj, this.FloatValue);
					break;
				case ChangePropertyAction.PropertyTypes.String:
					fieldInfo.SetValue(obj, this.StringValue);
					break;
				case ChangePropertyAction.PropertyTypes.Bool:
					fieldInfo.SetValue(obj, this.BoolValue);
					break;
				case ChangePropertyAction.PropertyTypes.Vector3:
					fieldInfo.SetValue(obj, this.Vector3Value);
					break;
				}
			}
			else if (propertyInfo != null)
			{
				switch (this.Type)
				{
				case ChangePropertyAction.PropertyTypes.Int:
					propertyInfo.SetValue(obj, this.IntValue, null);
					break;
				case ChangePropertyAction.PropertyTypes.Float:
					propertyInfo.SetValue(obj, this.FloatValue, null);
					break;
				case ChangePropertyAction.PropertyTypes.String:
					propertyInfo.SetValue(obj, this.StringValue, null);
					break;
				case ChangePropertyAction.PropertyTypes.Bool:
					propertyInfo.SetValue(obj, this.BoolValue, null);
					break;
				case ChangePropertyAction.PropertyTypes.Vector3:
					propertyInfo.SetValue(obj, this.Vector3Value, null);
					break;
				}
			}
		}
	}

	// Token: 0x0400132A RID: 4906
	public List<GameObject> Targets;

	// Token: 0x0400132B RID: 4907
	public string ComponentName = string.Empty;

	// Token: 0x0400132C RID: 4908
	public string Name = string.Empty;

	// Token: 0x0400132D RID: 4909
	public ChangePropertyAction.PropertyTypes Type = ChangePropertyAction.PropertyTypes.String;

	// Token: 0x0400132E RID: 4910
	public string StringValue = string.Empty;

	// Token: 0x0400132F RID: 4911
	public int IntValue;

	// Token: 0x04001330 RID: 4912
	public float FloatValue;

	// Token: 0x04001331 RID: 4913
	public bool BoolValue = true;

	// Token: 0x04001332 RID: 4914
	public Vector3 Vector3Value = Vector3.zero;

	// Token: 0x020002CB RID: 715
	public enum PropertyTypes
	{
		// Token: 0x04001334 RID: 4916
		Int,
		// Token: 0x04001335 RID: 4917
		Float,
		// Token: 0x04001336 RID: 4918
		String,
		// Token: 0x04001337 RID: 4919
		Bool,
		// Token: 0x04001338 RID: 4920
		Vector3
	}
}
