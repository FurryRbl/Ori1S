using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200031A RID: 794
	internal class InvokableCallList
	{
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x00037D28 File Offset: 0x00035F28
		public int Count
		{
			get
			{
				return this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count;
			}
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00037D44 File Offset: 0x00035F44
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x00037D5C File Offset: 0x00035F5C
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00037D74 File Offset: 0x00035F74
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> list = new List<BaseInvokableCall>();
			for (int i = 0; i < this.m_RuntimeCalls.Count; i++)
			{
				if (this.m_RuntimeCalls[i].Find(targetObj, method))
				{
					list.Add(this.m_RuntimeCalls[i]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(list.Contains));
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00037DF0 File Offset: 0x00035FF0
		public void Clear()
		{
			this.m_RuntimeCalls.Clear();
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00037E04 File Offset: 0x00036004
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x00037E18 File Offset: 0x00036018
		public void Invoke(object[] parameters)
		{
			if (this.m_NeedsUpdate)
			{
				this.m_ExecutingCalls.Clear();
				this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
				this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
				this.m_NeedsUpdate = false;
			}
			for (int i = 0; i < this.m_ExecutingCalls.Count; i++)
			{
				this.m_ExecutingCalls[i].Invoke(parameters);
			}
		}

		// Token: 0x04000C33 RID: 3123
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x04000C34 RID: 3124
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x04000C35 RID: 3125
		private readonly List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();

		// Token: 0x04000C36 RID: 3126
		private bool m_NeedsUpdate = true;
	}
}
