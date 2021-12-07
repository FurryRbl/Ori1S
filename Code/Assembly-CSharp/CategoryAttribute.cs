using System;

// Token: 0x020000E6 RID: 230
internal class CategoryAttribute : Attribute
{
	// Token: 0x0600092B RID: 2347 RVA: 0x000278CD File Offset: 0x00025ACD
	public CategoryAttribute()
	{
		this.Category = string.Empty;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x000278E0 File Offset: 0x00025AE0
	public CategoryAttribute(string category)
	{
		this.Category = category;
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x0600092D RID: 2349 RVA: 0x000278EF File Offset: 0x00025AEF
	// (set) Token: 0x0600092E RID: 2350 RVA: 0x000278F7 File Offset: 0x00025AF7
	private string Category { get; set; }
}
