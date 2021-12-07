using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

// Token: 0x020004C1 RID: 1217
public class FramePerformanceMonitor : MonoBehaviour
{
	// Token: 0x17000597 RID: 1431
	// (get) Token: 0x060020F6 RID: 8438 RVA: 0x000907AD File Offset: 0x0008E9AD
	// (set) Token: 0x060020F7 RID: 8439 RVA: 0x000907BC File Offset: 0x0008E9BC
	public static bool Enabled
	{
		get
		{
			return FramePerformanceMonitor.m_instance != null;
		}
		set
		{
			if (FramePerformanceMonitor.m_instance && !value)
			{
				InstantiateUtility.Destroy(FramePerformanceMonitor.m_instance.gameObject);
			}
			if (FramePerformanceMonitor.m_instance == null && value)
			{
				GameObject gameObject = new GameObject("framePerformanceMonitor");
				gameObject.AddComponent<FramePerformanceMonitor>();
			}
		}
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x00090818 File Offset: 0x0008EA18
	[Conditional("NOT_FINAL_BUILD")]
	public static void BeginSample(string label)
	{
		if (FramePerformanceMonitor.m_instance && FramePerformanceMonitor.m_instance.m_activeEntries.Count > 0)
		{
			FramePerformanceMonitor.PerformanceEntry performanceEntry = new FramePerformanceMonitor.PerformanceEntry(label);
			FramePerformanceMonitor.m_instance.m_activeEntries.Peek().Children.Add(performanceEntry);
			FramePerformanceMonitor.m_instance.m_activeEntries.Push(performanceEntry);
		}
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x0009087C File Offset: 0x0008EA7C
	[Conditional("NOT_FINAL_BUILD")]
	public static void EndSample()
	{
		if (FramePerformanceMonitor.m_instance && FramePerformanceMonitor.m_instance.m_activeEntries.Count > 0)
		{
			FramePerformanceMonitor.PerformanceEntry performanceEntry = FramePerformanceMonitor.m_instance.m_activeEntries.Pop();
			performanceEntry.Duration = Time.realtimeSinceStartup - performanceEntry.StartTime;
		}
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x000908CF File Offset: 0x0008EACF
	public void Awake()
	{
		FramePerformanceMonitor.m_instance = this;
		this.m_activeEntries.Push(this.m_root);
	}

	// Token: 0x060020FB RID: 8443 RVA: 0x000908E8 File Offset: 0x0008EAE8
	public void OnEnable()
	{
		this.m_root.StartTime = Time.realtimeSinceStartup;
		this.m_lastGarbageCollectionTime = Time.time;
		new GarbageCollectionDetector();
	}

	// Token: 0x060020FC RID: 8444 RVA: 0x0009090C File Offset: 0x0008EB0C
	public void OnDestroy()
	{
		this.Flush();
		this.CloseWriter();
		if (FramePerformanceMonitor.m_instance == this)
		{
			FramePerformanceMonitor.m_instance = null;
		}
	}

	// Token: 0x060020FD RID: 8445 RVA: 0x0009093C File Offset: 0x0008EB3C
	public void Update()
	{
		this.m_root.Duration = Time.realtimeSinceStartup - this.m_root.StartTime;
		bool garbageCollectionFlag = FramePerformanceMonitor.GarbageCollectionFlag;
		float lastGarbageCollectionTime = 0f;
		if (garbageCollectionFlag)
		{
			FramePerformanceMonitor.GarbageCollectionFlag = false;
			new GarbageCollectionDetector();
			lastGarbageCollectionTime = Time.time - this.m_lastGarbageCollectionTime;
			this.m_lastGarbageCollectionTime = Time.time;
		}
		if (this.m_root.Duration > 0.017f || garbageCollectionFlag)
		{
			this.m_recordedFrames.Add(new FramePerformanceMonitor.PerformanceFrameData(this.m_root, garbageCollectionFlag, lastGarbageCollectionTime));
		}
		this.m_root.StartTime = Time.realtimeSinceStartup;
		this.m_root.Children.Clear();
		if (this.m_recordedFrames.Count > 10)
		{
			this.Flush();
		}
	}

	// Token: 0x060020FE RID: 8446 RVA: 0x00090A08 File Offset: 0x0008EC08
	public void Flush()
	{
		if (this.m_streamWriter == null)
		{
			this.m_streamWriter = new StreamWriter(new FileStream(Path.Combine(OutputFolder.BuildOutputPath, "framePerformance.txt"), FileMode.Create));
		}
		foreach (FramePerformanceMonitor.PerformanceFrameData performanceFrameData in this.m_recordedFrames)
		{
			performanceFrameData.Write(this.m_streamWriter);
		}
		this.m_recordedFrames.Clear();
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x00090AA0 File Offset: 0x0008ECA0
	public void CloseWriter()
	{
		if (this.m_streamWriter != null)
		{
			((IDisposable)this.m_streamWriter).Dispose();
			this.m_streamWriter = null;
		}
	}

	// Token: 0x04001BF0 RID: 7152
	public static bool GarbageCollectionFlag;

	// Token: 0x04001BF1 RID: 7153
	private static FramePerformanceMonitor m_instance;

	// Token: 0x04001BF2 RID: 7154
	private float m_lastGarbageCollectionTime;

	// Token: 0x04001BF3 RID: 7155
	private readonly Stack<FramePerformanceMonitor.PerformanceEntry> m_activeEntries = new Stack<FramePerformanceMonitor.PerformanceEntry>();

	// Token: 0x04001BF4 RID: 7156
	private readonly FramePerformanceMonitor.PerformanceEntry m_root = new FramePerformanceMonitor.PerformanceEntry("root");

	// Token: 0x04001BF5 RID: 7157
	private readonly List<FramePerformanceMonitor.PerformanceFrameData> m_recordedFrames = new List<FramePerformanceMonitor.PerformanceFrameData>();

	// Token: 0x04001BF6 RID: 7158
	private StreamWriter m_streamWriter;

	// Token: 0x02000749 RID: 1865
	private struct PerformanceFrameData
	{
		// Token: 0x06002BC0 RID: 11200 RVA: 0x000BB684 File Offset: 0x000B9884
		public PerformanceFrameData(FramePerformanceMonitor.PerformanceEntry root, bool garbageCollection, float lastGarbageCollectionTime)
		{
			this.m_duration = root.Duration;
			this.m_frame = Time.renderedFrameCount;
			this.m_entries = new List<FramePerformanceMonitor.PerformanceEntry>(root.Children);
			this.m_entries.Sort((FramePerformanceMonitor.PerformanceEntry a, FramePerformanceMonitor.PerformanceEntry b) => a.Duration.CompareTo(b.Duration));
			this.m_garbageCollection = garbageCollection;
			this.m_lastGarbageCollectionTime = lastGarbageCollectionTime;
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000BB6F0 File Offset: 0x000B98F0
		public void Write(StreamWriter writer)
		{
			writer.WriteLine("-------------------------");
			writer.WriteLine(string.Concat(new object[]
			{
				"Frame: ",
				this.m_frame,
				"\t",
				this.m_duration * 1000f
			}));
			if (this.m_garbageCollection)
			{
				writer.WriteLine("GC Spike occured. Time since last GC:\t" + this.m_lastGarbageCollectionTime * 1000f);
			}
			foreach (FramePerformanceMonitor.PerformanceEntry performanceEntry in this.m_entries)
			{
				performanceEntry.Write(writer, 0, this.m_duration);
			}
		}

		// Token: 0x0400276A RID: 10090
		private readonly int m_frame;

		// Token: 0x0400276B RID: 10091
		private readonly float m_duration;

		// Token: 0x0400276C RID: 10092
		private readonly List<FramePerformanceMonitor.PerformanceEntry> m_entries;

		// Token: 0x0400276D RID: 10093
		private readonly bool m_garbageCollection;

		// Token: 0x0400276E RID: 10094
		private readonly float m_lastGarbageCollectionTime;
	}

	// Token: 0x0200074A RID: 1866
	private class PerformanceEntry
	{
		// Token: 0x06002BC3 RID: 11203 RVA: 0x000BB7DF File Offset: 0x000B99DF
		public PerformanceEntry(string label)
		{
			this.m_label = label;
			this.StartTime = Time.realtimeSinceStartup;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000BB804 File Offset: 0x000B9A04
		public void Write(StreamWriter writer, int depth, float totalDuration)
		{
			writer.WriteLine(string.Concat(new object[]
			{
				new string(' ', depth * 4),
				this.m_label,
				"\t",
				this.Duration * 1000f,
				"\t",
				this.Duration * 100f / totalDuration,
				"%"
			}));
			this.Children.Sort((FramePerformanceMonitor.PerformanceEntry a, FramePerformanceMonitor.PerformanceEntry b) => a.Duration.CompareTo(b.Duration));
			foreach (FramePerformanceMonitor.PerformanceEntry performanceEntry in this.Children)
			{
				performanceEntry.Write(writer, depth + 1, totalDuration);
			}
		}

		// Token: 0x04002770 RID: 10096
		public readonly List<FramePerformanceMonitor.PerformanceEntry> Children = new List<FramePerformanceMonitor.PerformanceEntry>();

		// Token: 0x04002771 RID: 10097
		public float StartTime;

		// Token: 0x04002772 RID: 10098
		private readonly string m_label;

		// Token: 0x04002773 RID: 10099
		public float Duration;
	}
}
