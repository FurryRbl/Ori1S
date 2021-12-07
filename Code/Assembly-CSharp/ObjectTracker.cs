using System;
using System.Collections.Generic;

// Token: 0x0200048C RID: 1164
public class ObjectTracker<T> where T : class
{
	// Token: 0x06001FAD RID: 8109 RVA: 0x0008B358 File Offset: 0x00089558
	public int Add(T obj)
	{
		if (this.m_freePositions == null)
		{
			this.m_freePositions = new Queue<int>();
		}
		else if (this.m_freePositions.Count > 0)
		{
			int num = this.m_freePositions.Dequeue();
			this.m_objects[num] = obj;
			return num;
		}
		if (this.m_objects == null)
		{
			this.m_objects = new T[4];
			this.m_objects[0] = obj;
			return 0;
		}
		int num2 = this.m_objects.Length;
		Array.Resize<T>(ref this.m_objects, num2 * 2);
		this.m_objects[num2] = obj;
		return num2;
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x0008B3F7 File Offset: 0x000895F7
	public void Remove(int id)
	{
		this.m_objects[id] = (T)((object)null);
	}

	// Token: 0x04001B38 RID: 6968
	private T[] m_objects;

	// Token: 0x04001B39 RID: 6969
	private Queue<int> m_freePositions;
}
