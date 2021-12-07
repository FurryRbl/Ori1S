using System;

// Token: 0x02000894 RID: 2196
public class RepeatInvoker
{
	// Token: 0x0600314E RID: 12622 RVA: 0x000D1E8C File Offset: 0x000D008C
	public RepeatInvoker(float duration, Action method = null)
	{
		this.m_method = method;
		this.m_duration = duration;
	}

	// Token: 0x0600314F RID: 12623 RVA: 0x000D1EA4 File Offset: 0x000D00A4
	public bool Update(float dt)
	{
		this.m_time += dt;
		if (this.m_time > this.m_duration)
		{
			this.m_time -= this.m_duration;
			if (this.m_method != null)
			{
				this.m_method();
			}
			return true;
		}
		return false;
	}

	// Token: 0x04002C9D RID: 11421
	private float m_time;

	// Token: 0x04002C9E RID: 11422
	private readonly float m_duration;

	// Token: 0x04002C9F RID: 11423
	private readonly Action m_method;
}
