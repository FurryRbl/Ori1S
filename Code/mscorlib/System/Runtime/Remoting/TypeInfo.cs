using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000435 RID: 1077
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x06002DCC RID: 11724 RVA: 0x000988E8 File Offset: 0x00096AE8
		public TypeInfo(Type type)
		{
			if (type.IsInterface)
			{
				this.serverType = typeof(MarshalByRefObject).AssemblyQualifiedName;
				this.serverHierarchy = new string[0];
				this.interfacesImplemented = new string[]
				{
					type.AssemblyQualifiedName
				};
			}
			else
			{
				this.serverType = type.AssemblyQualifiedName;
				int num = 0;
				Type baseType = type.BaseType;
				while (baseType != typeof(MarshalByRefObject) && baseType != typeof(object))
				{
					baseType = baseType.BaseType;
					num++;
				}
				this.serverHierarchy = new string[num];
				baseType = type.BaseType;
				for (int i = 0; i < num; i++)
				{
					this.serverHierarchy[i] = baseType.AssemblyQualifiedName;
					baseType = baseType.BaseType;
				}
				Type[] interfaces = type.GetInterfaces();
				this.interfacesImplemented = new string[interfaces.Length];
				for (int j = 0; j < interfaces.Length; j++)
				{
					this.interfacesImplemented[j] = interfaces[j].AssemblyQualifiedName;
				}
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x00098A00 File Offset: 0x00096C00
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x00098A08 File Offset: 0x00096C08
		public string TypeName
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x00098A14 File Offset: 0x00096C14
		public bool CanCastTo(Type fromType, object o)
		{
			if (fromType == typeof(object))
			{
				return true;
			}
			if (fromType == typeof(MarshalByRefObject))
			{
				return true;
			}
			string text = fromType.AssemblyQualifiedName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				num = text.IndexOf(',', num + 1);
			}
			if (num != -1)
			{
				text = text.Substring(0, num + 1);
			}
			else
			{
				text += ",";
			}
			if ((this.serverType + ",").StartsWith(text))
			{
				return true;
			}
			if (this.serverHierarchy != null)
			{
				foreach (string str in this.serverHierarchy)
				{
					if ((str + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			if (this.interfacesImplemented != null)
			{
				foreach (string str2 in this.interfacesImplemented)
				{
					if ((str2 + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040013AD RID: 5037
		private string serverType;

		// Token: 0x040013AE RID: 5038
		private string[] serverHierarchy;

		// Token: 0x040013AF RID: 5039
		private string[] interfacesImplemented;
	}
}
