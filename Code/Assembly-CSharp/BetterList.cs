using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200047E RID: 1150
public class BetterList<T>
{
	// Token: 0x06001F73 RID: 8051 RVA: 0x0008A861 File Offset: 0x00088A61
	public BetterList(int capacity)
	{
		this.Buffer = new T[capacity];
	}

	// Token: 0x17000579 RID: 1401
	// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0008A875 File Offset: 0x00088A75
	public int Count
	{
		get
		{
			return this.m_size;
		}
	}

	// Token: 0x06001F75 RID: 8053 RVA: 0x0008A880 File Offset: 0x00088A80
	public IEnumerator<T> GetEnumerator()
	{
		if (this.Buffer != null)
		{
			for (int i = 0; i < this.Count; i++)
			{
				yield return this.Buffer[i];
			}
		}
		yield break;
	}

	// Token: 0x1700057A RID: 1402
	[DebuggerHidden]
	public T this[int i]
	{
		get
		{
			return this.Buffer[i];
		}
		set
		{
			this.Buffer[i] = value;
		}
	}

	// Token: 0x06001F78 RID: 8056 RVA: 0x0008A8B8 File Offset: 0x00088AB8
	private void AllocateMore()
	{
		T[] array = (this.Buffer == null) ? new T[8] : new T[Mathf.Max(this.Buffer.Length << 1, 8)];
		if (this.Buffer != null && this.Count > 0)
		{
			this.Buffer.CopyTo(array, 0);
		}
		this.Buffer = array;
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x0008A91C File Offset: 0x00088B1C
	private void Trim()
	{
		if (this.Count > 0)
		{
			if (this.Count < this.Buffer.Length)
			{
				T[] array = new T[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = this.Buffer[i];
				}
				this.Buffer = array;
			}
		}
		else
		{
			this.Buffer = null;
		}
	}

	// Token: 0x06001F7A RID: 8058 RVA: 0x0008A991 File Offset: 0x00088B91
	public void Clear()
	{
		this.m_size = 0;
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x0008A99A File Offset: 0x00088B9A
	public void Release()
	{
		this.m_size = 0;
		this.Buffer = null;
	}

	// Token: 0x06001F7C RID: 8060 RVA: 0x0008A9AC File Offset: 0x00088BAC
	public void Add(T item)
	{
		if (this.Buffer == null || this.Count == this.Buffer.Length)
		{
			this.AllocateMore();
		}
		this.Buffer[this.m_size++] = item;
	}

	// Token: 0x06001F7D RID: 8061 RVA: 0x0008A9FC File Offset: 0x00088BFC
	public void Insert(int index, T item)
	{
		if (this.Buffer == null || this.Count == this.Buffer.Length)
		{
			this.AllocateMore();
		}
		if (index < this.Count)
		{
			for (int i = this.Count; i > index; i--)
			{
				this.Buffer[i] = this.Buffer[i - 1];
			}
			this.Buffer[index] = item;
			this.m_size++;
		}
		else
		{
			this.Add(item);
		}
	}

	// Token: 0x06001F7E RID: 8062 RVA: 0x0008AA94 File Offset: 0x00088C94
	public bool Contains(T item)
	{
		if (this.Buffer == null)
		{
			return false;
		}
		for (int i = 0; i < this.Count; i++)
		{
			if (this.Buffer[i].Equals(item))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001F7F RID: 8063 RVA: 0x0008AAEC File Offset: 0x00088CEC
	public bool Remove(T item)
	{
		if (this.Buffer != null)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < this.Count; i++)
			{
				if (@default.Equals(this.Buffer[i], item))
				{
					this.m_size--;
					for (int j = i; j < this.Count; j++)
					{
						this.Buffer[j] = this.Buffer[j + 1];
					}
					this.Buffer[this.Count] = default(T);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001F80 RID: 8064 RVA: 0x0008AB98 File Offset: 0x00088D98
	public void RemoveAt(int index)
	{
		if (this.Buffer != null && index < this.Count)
		{
			this.m_size--;
			this.Buffer[index] = default(T);
			for (int i = index; i < this.Count; i++)
			{
				this.Buffer[i] = this.Buffer[i + 1];
			}
			this.Buffer[this.Count] = default(T);
		}
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x0008AC2C File Offset: 0x00088E2C
	public T Pop()
	{
		if (this.Buffer != null && this.Count != 0)
		{
			T result = this.Buffer[--this.m_size];
			this.Buffer[this.Count] = default(T);
			return result;
		}
		return default(T);
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x0008AC91 File Offset: 0x00088E91
	public T[] ToArray()
	{
		this.Trim();
		return this.Buffer;
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x0008ACA0 File Offset: 0x00088EA0
	[DebuggerHidden]
	[DebuggerStepThrough]
	public void Sort(BetterList<T>.CompareFunc comparer)
	{
		int num = 0;
		int num2 = this.Count - 1;
		bool flag = true;
		while (flag)
		{
			flag = false;
			for (int i = num; i < num2; i++)
			{
				if (comparer(this.Buffer[i], this.Buffer[i + 1]) > 0)
				{
					T t = this.Buffer[i];
					this.Buffer[i] = this.Buffer[i + 1];
					this.Buffer[i + 1] = t;
					flag = true;
				}
				else if (!flag)
				{
					num = ((i != 0) ? (i - 1) : 0);
				}
			}
		}
	}

	// Token: 0x04001B25 RID: 6949
	public T[] Buffer;

	// Token: 0x04001B26 RID: 6950
	private int m_size;

	// Token: 0x0200047F RID: 1151
	// (Invoke) Token: 0x06001F85 RID: 8069
	public delegate int CompareFunc(T left, T right);
}
