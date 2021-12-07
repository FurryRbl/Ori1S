using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000498 RID: 1176
	[Serializable]
	internal class ErrorMessage : IMessage, IMethodCallMessage, IMethodMessage
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x0009DE48 File Offset: 0x0009C048
		public int ArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x0009DE4C File Offset: 0x0009C04C
		public object[] Args
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x0009DE50 File Offset: 0x0009C050
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x0009DE54 File Offset: 0x0009C054
		public MethodBase MethodBase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x0009DE58 File Offset: 0x0009C058
		public string MethodName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x0009DE60 File Offset: 0x0009C060
		public object MethodSignature
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x0009DE64 File Offset: 0x0009C064
		public virtual IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x0009DE68 File Offset: 0x0009C068
		public string TypeName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x0009DE70 File Offset: 0x0009C070
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x0009DE78 File Offset: 0x0009C078
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0009DE84 File Offset: 0x0009C084
		public object GetArg(int arg_num)
		{
			return null;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0009DE88 File Offset: 0x0009C088
		public string GetArgName(int arg_num)
		{
			return "unknown";
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x0009DE90 File Offset: 0x0009C090
		public int InArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x0009DE94 File Offset: 0x0009C094
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x0009DE98 File Offset: 0x0009C098
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x0009DE9C File Offset: 0x0009C09C
		public object[] InArgs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x0009DEA0 File Offset: 0x0009C0A0
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400145C RID: 5212
		private string _uri = "Exception";
	}
}
