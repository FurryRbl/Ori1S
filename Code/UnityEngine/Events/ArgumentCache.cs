using System;
using System.Text.RegularExpressions;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200030F RID: 783
	[Serializable]
	internal class ArgumentCache : ISerializationCallbackReceiver
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x00036FE4 File Offset: 0x000351E4
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x00036FEC File Offset: 0x000351EC
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
			set
			{
				this.m_ObjectArgument = value;
				this.m_ObjectArgumentAssemblyTypeName = ((!(value != null)) ? string.Empty : value.GetType().AssemblyQualifiedName);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x00037028 File Offset: 0x00035228
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x00037030 File Offset: 0x00035230
		// (set) Token: 0x0600271F RID: 10015 RVA: 0x00037038 File Offset: 0x00035238
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
			set
			{
				this.m_IntArgument = value;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x00037044 File Offset: 0x00035244
		// (set) Token: 0x06002721 RID: 10017 RVA: 0x0003704C File Offset: 0x0003524C
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
			set
			{
				this.m_FloatArgument = value;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002722 RID: 10018 RVA: 0x00037058 File Offset: 0x00035258
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x00037060 File Offset: 0x00035260
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
			set
			{
				this.m_StringArgument = value;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x0003706C File Offset: 0x0003526C
		// (set) Token: 0x06002725 RID: 10021 RVA: 0x00037074 File Offset: 0x00035274
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
			set
			{
				this.m_BoolArgument = value;
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00037080 File Offset: 0x00035280
		private void TidyAssemblyTypeName()
		{
			if (string.IsNullOrEmpty(this.m_ObjectArgumentAssemblyTypeName))
			{
				return;
			}
			this.m_ObjectArgumentAssemblyTypeName = Regex.Replace(this.m_ObjectArgumentAssemblyTypeName, ", Version=\\d+.\\d+.\\d+.\\d+", string.Empty);
			this.m_ObjectArgumentAssemblyTypeName = Regex.Replace(this.m_ObjectArgumentAssemblyTypeName, ", Culture=\\w+", string.Empty);
			this.m_ObjectArgumentAssemblyTypeName = Regex.Replace(this.m_ObjectArgumentAssemblyTypeName, ", PublicKeyToken=\\w+", string.Empty);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x000370F0 File Offset: 0x000352F0
		public void OnBeforeSerialize()
		{
			this.TidyAssemblyTypeName();
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x000370F8 File Offset: 0x000352F8
		public void OnAfterDeserialize()
		{
			this.TidyAssemblyTypeName();
		}

		// Token: 0x04000C1A RID: 3098
		private const string kVersionString = ", Version=\\d+.\\d+.\\d+.\\d+";

		// Token: 0x04000C1B RID: 3099
		private const string kCultureString = ", Culture=\\w+";

		// Token: 0x04000C1C RID: 3100
		private const string kTokenString = ", PublicKeyToken=\\w+";

		// Token: 0x04000C1D RID: 3101
		[FormerlySerializedAs("objectArgument")]
		[SerializeField]
		private Object m_ObjectArgument;

		// Token: 0x04000C1E RID: 3102
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x04000C1F RID: 3103
		[FormerlySerializedAs("intArgument")]
		[SerializeField]
		private int m_IntArgument;

		// Token: 0x04000C20 RID: 3104
		[SerializeField]
		[FormerlySerializedAs("floatArgument")]
		private float m_FloatArgument;

		// Token: 0x04000C21 RID: 3105
		[FormerlySerializedAs("stringArgument")]
		[SerializeField]
		private string m_StringArgument;

		// Token: 0x04000C22 RID: 3106
		[SerializeField]
		private bool m_BoolArgument;
	}
}
