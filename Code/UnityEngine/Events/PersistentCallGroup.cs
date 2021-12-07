using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000319 RID: 793
	[Serializable]
	internal class PersistentCallGroup
	{
		// Token: 0x0600275C RID: 10076 RVA: 0x000379EC File Offset: 0x00035BEC
		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x00037A00 File Offset: 0x00035C00
		public int Count
		{
			get
			{
				return this.m_Calls.Count;
			}
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00037A10 File Offset: 0x00035C10
		public PersistentCall GetListener(int index)
		{
			return this.m_Calls[index];
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00037A20 File Offset: 0x00035C20
		public IEnumerable<PersistentCall> GetListeners()
		{
			return this.m_Calls;
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00037A28 File Offset: 0x00035C28
		public void AddListener()
		{
			this.m_Calls.Add(new PersistentCall());
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00037A3C File Offset: 0x00035C3C
		public void AddListener(PersistentCall call)
		{
			this.m_Calls.Add(call);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00037A4C File Offset: 0x00035C4C
		public void RemoveListener(int index)
		{
			this.m_Calls.RemoveAt(index);
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00037A5C File Offset: 0x00035C5C
		public void Clear()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00037A6C File Offset: 0x00035C6C
		public void RegisterEventPersistentListener(int index, Object targetObj, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.EventDefined;
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00037A90 File Offset: 0x00035C90
		public void RegisterVoidPersistentListener(int index, Object targetObj, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Void;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00037AB4 File Offset: 0x00035CB4
		public void RegisterObjectPersistentListener(int index, Object targetObj, Object argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Object;
			listener.arguments.unityObjectArgument = argument;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00037AE8 File Offset: 0x00035CE8
		public void RegisterIntPersistentListener(int index, Object targetObj, int argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Int;
			listener.arguments.intArgument = argument;
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00037B1C File Offset: 0x00035D1C
		public void RegisterFloatPersistentListener(int index, Object targetObj, float argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Float;
			listener.arguments.floatArgument = argument;
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00037B50 File Offset: 0x00035D50
		public void RegisterStringPersistentListener(int index, Object targetObj, string argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.String;
			listener.arguments.stringArgument = argument;
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x00037B84 File Offset: 0x00035D84
		public void RegisterBoolPersistentListener(int index, Object targetObj, bool argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Bool;
			listener.arguments.boolArgument = argument;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00037BB8 File Offset: 0x00035DB8
		public void UnregisterPersistentListener(int index)
		{
			PersistentCall listener = this.GetListener(index);
			listener.UnregisterPersistentListener();
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00037BD4 File Offset: 0x00035DD4
		public void RemoveListeners(Object target, string methodName)
		{
			List<PersistentCall> list = new List<PersistentCall>();
			for (int i = 0; i < this.m_Calls.Count; i++)
			{
				if (this.m_Calls[i].target == target && this.m_Calls[i].methodName == methodName)
				{
					list.Add(this.m_Calls[i]);
				}
			}
			this.m_Calls.RemoveAll(new Predicate<PersistentCall>(list.Contains));
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00037C68 File Offset: 0x00035E68
		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall persistentCall in this.m_Calls)
			{
				if (persistentCall.IsValid())
				{
					BaseInvokableCall runtimeCall = persistentCall.GetRuntimeCall(unityEventBase);
					if (runtimeCall != null)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}

		// Token: 0x04000C32 RID: 3122
		[FormerlySerializedAs("m_Listeners")]
		[SerializeField]
		private List<PersistentCall> m_Calls;
	}
}
