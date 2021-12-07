using System;
using System.Collections.Generic;

// Token: 0x02000040 RID: 64
public class AllContainer<T>
{
	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000BBB2 File Offset: 0x00009DB2
	public int Count
	{
		get
		{
			if (this.m_objects == null)
			{
				return 0;
			}
			return this.m_objects.Count;
		}
	}

	// Token: 0x170000B6 RID: 182
	public T this[int index]
	{
		get
		{
			if (this.m_objects == null)
			{
				return default(T);
			}
			return this.m_objects[index];
		}
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0000BBFC File Offset: 0x00009DFC
	public void Add(T item)
	{
		if (this.m_objects == null)
		{
			this.m_objects = new List<T>(16);
		}
		this.m_objects.Add(item);
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0000BC2D File Offset: 0x00009E2D
	public void Remove(T item)
	{
		if (this.m_objects != null)
		{
			this.m_objects.RemoveUnordered(item);
		}
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000BC47 File Offset: 0x00009E47
	public void Clear()
	{
		if (this.m_objects != null)
		{
			this.m_objects.Clear();
		}
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0000BC5F File Offset: 0x00009E5F
	public bool Contains(T item)
	{
		return this.m_objects != null && this.m_objects.Contains(item);
	}

	// Token: 0x04000208 RID: 520
	private List<T> m_objects;
}
