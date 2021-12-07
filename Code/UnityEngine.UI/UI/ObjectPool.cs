using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x0200009F RID: 159
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x060005A2 RID: 1442 RVA: 0x00018614 File Offset: 0x00016814
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00018638 File Offset: 0x00016838
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x00018640 File Offset: 0x00016840
		public int countAll { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001864C File Offset: 0x0001684C
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001865C File Offset: 0x0001685C
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001866C File Offset: 0x0001686C
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
				this.countAll++;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000186E4 File Offset: 0x000168E4
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && object.ReferenceEquals(this.m_Stack.Peek(), element))
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x040002A7 RID: 679
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040002A8 RID: 680
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x040002A9 RID: 681
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
