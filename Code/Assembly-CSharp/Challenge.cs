using System;

// Token: 0x0200024F RID: 591
public class Challenge
{
	// Token: 0x06001402 RID: 5122 RVA: 0x0005B328 File Offset: 0x00059528
	public Challenge(string name, string description, bool started)
	{
		this.Name = name;
		this.Description = description;
		this.Started = started;
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06001403 RID: 5123 RVA: 0x0005B350 File Offset: 0x00059550
	// (set) Token: 0x06001404 RID: 5124 RVA: 0x0005B358 File Offset: 0x00059558
	public string Name { get; protected set; }

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06001405 RID: 5125 RVA: 0x0005B361 File Offset: 0x00059561
	// (set) Token: 0x06001406 RID: 5126 RVA: 0x0005B369 File Offset: 0x00059569
	public string Description { get; protected set; }

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06001407 RID: 5127 RVA: 0x0005B372 File Offset: 0x00059572
	// (set) Token: 0x06001408 RID: 5128 RVA: 0x0005B37A File Offset: 0x0005957A
	public bool Started { get; protected set; }

	// Token: 0x06001409 RID: 5129 RVA: 0x0005B383 File Offset: 0x00059583
	public override string ToString()
	{
		return this.Name;
	}
}
