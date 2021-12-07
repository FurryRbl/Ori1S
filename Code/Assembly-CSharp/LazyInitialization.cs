using System;

// Token: 0x0200093D RID: 2365
public class LazyInitialization<T> where T : class
{
	// Token: 0x0600343D RID: 13373 RVA: 0x000DBA6A File Offset: 0x000D9C6A
	public LazyInitialization(Func<T> initializer)
	{
		this.m_initializer = initializer;
	}

	// Token: 0x1700083C RID: 2108
	// (get) Token: 0x0600343E RID: 13374 RVA: 0x000DBA7C File Offset: 0x000D9C7C
	public T Value
	{
		get
		{
			T result;
			if ((result = this.m_value) == null)
			{
				result = (this.m_value = this.m_initializer());
			}
			return result;
		}
	}

	// Token: 0x0600343F RID: 13375 RVA: 0x000DBAAA File Offset: 0x000D9CAA
	public void Refresh()
	{
		this.m_value = this.m_initializer();
	}

	// Token: 0x04002F2E RID: 12078
	private T m_value;

	// Token: 0x04002F2F RID: 12079
	private readonly Func<T> m_initializer;
}
