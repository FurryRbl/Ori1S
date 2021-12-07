using System;
using System.Runtime.InteropServices;
using System.Threading;

// Token: 0x0200097A RID: 2426
internal class HighPerformanceCounter
{
	// Token: 0x0600352E RID: 13614 RVA: 0x000DEF34 File Offset: 0x000DD134
	public HighPerformanceCounter()
	{
		this.startTime = 0L;
		this.stopTime = 0L;
		if (!HighPerformanceCounter.QueryPerformanceFrequency(out this.freq))
		{
			throw new ArgumentException();
		}
	}

	// Token: 0x0600352F RID: 13615
	[DllImport("Kernel32.dll")]
	private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

	// Token: 0x06003530 RID: 13616
	[DllImport("Kernel32.dll")]
	private static extern bool QueryPerformanceFrequency(out long lpFrequency);

	// Token: 0x06003531 RID: 13617 RVA: 0x000DEF6D File Offset: 0x000DD16D
	public void Start()
	{
		Thread.Sleep(0);
		HighPerformanceCounter.QueryPerformanceCounter(out this.startTime);
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x000DEF81 File Offset: 0x000DD181
	public void Stop()
	{
		HighPerformanceCounter.QueryPerformanceCounter(out this.stopTime);
	}

	// Token: 0x1700085B RID: 2139
	// (get) Token: 0x06003533 RID: 13619 RVA: 0x000DEF8F File Offset: 0x000DD18F
	public double Duration
	{
		get
		{
			return (double)(this.stopTime - this.startTime) / (double)this.freq;
		}
	}

	// Token: 0x04002FD0 RID: 12240
	private long startTime;

	// Token: 0x04002FD1 RID: 12241
	private long stopTime;

	// Token: 0x04002FD2 RID: 12242
	private long freq;
}
