using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class PhysicsLimitTest : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x06000041 RID: 65 RVA: 0x000030B8 File Offset: 0x000012B8
	private void AddTimeSample(float dt)
	{
		this.m_totalFrames++;
		this.m_timeSamples[this.m_timeSampleIndex++] = dt;
		if (this.m_timeSampleIndex >= 500)
		{
			this.m_timeSampleIndex = 0;
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003104 File Offset: 0x00001304
	private int GetFrameCount(float duration)
	{
		int num = 0;
		float num2 = 0f;
		int num3 = this.m_timeSampleIndex;
		while (num2 < duration && num < this.m_totalFrames)
		{
			if (num3 <= 0)
			{
				num3 = 500;
			}
			num2 += this.m_timeSamples[--num3];
			num++;
		}
		return num;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000315C File Offset: 0x0000135C
	public void UpdateMethod(float timeSpan, int minFrames)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_lastRealTime;
		this.m_lastRealTime = realtimeSinceStartup;
		if (num > 0.05f)
		{
			num = 0.016666668f;
		}
		this.AddTimeSample(num);
		int frameCount = this.GetFrameCount(timeSpan);
		if (this.m_totalFrames > 60)
		{
			if (this.m_synced)
			{
				if (frameCount < minFrames)
				{
					this.m_synced = false;
					Time.maximumDeltaTime = 0.033333335f;
				}
			}
			else if (frameCount > minFrames && GameSettings.Instance.Vsync)
			{
				this.m_synced = true;
				Time.maximumDeltaTime = 0.016666668f;
			}
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000031FC File Offset: 0x000013FC
	public void UpdateMethodOld()
	{
		this.m_frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_lastRealTime;
		this.m_lastRealTime = realtimeSinceStartup;
		if (num > 0.05f)
		{
			num = 0.016666668f;
		}
		this.m_time += num;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000324C File Offset: 0x0000144C
	public void Update()
	{
		if (this.m_currentOption == 0)
		{
			this.UpdateMethod(0.5f, 30);
		}
		else if (this.m_currentOption == 3)
		{
			this.UpdateMethod(1f, 55);
		}
		else if (this.m_currentOption == 4 || this.m_currentOption == 5)
		{
			this.UpdateMethodOld();
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x000032B4 File Offset: 0x000014B4
	public void FixedUpdate()
	{
		if (this.m_currentOption != 4 || this.m_currentOption != 5)
		{
			return;
		}
		this.m_fixedUpdates++;
		int num = (this.m_currentOption != 5) ? 25 : 55;
		int num2 = (this.m_currentOption != 5) ? 27 : 57;
		float num3 = (this.m_currentOption != 5) ? 0.5f : 1f;
		if (this.m_time > num3)
		{
			if (this.m_synced)
			{
				if (this.m_fixedUpdates < num)
				{
					this.m_synced = false;
					Time.maximumDeltaTime = 0.033333335f;
				}
			}
			else
			{
				if (this.m_frames > num2)
				{
					this.m_synced = true;
					Time.maximumDeltaTime = 0.016666668f;
				}
				this.ResetValues();
			}
			this.ResetValues();
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003394 File Offset: 0x00001594
	public void ResetValues()
	{
		this.m_frames = 0;
		this.m_fixedUpdates = 0;
		this.m_time = 0f;
		this.m_lastRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000048 RID: 72 RVA: 0x000033C5 File Offset: 0x000015C5
	public string Name
	{
		get
		{
			return "Physics Iteration";
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000049 RID: 73 RVA: 0x000033CC File Offset: 0x000015CC
	public string HelpText
	{
		get
		{
			return "Toggle differnt options";
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600004A RID: 74 RVA: 0x000033D4 File Offset: 0x000015D4
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Physics: Auto (30)",
				"Physics: Synced",
				"Physics: Unsynced",
				"Physics: Auto (60)",
				"Physics: Old (30)",
				"Physics: Old (60)"
			};
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600004B RID: 75 RVA: 0x00003417 File Offset: 0x00001617
	// (set) Token: 0x0600004C RID: 76 RVA: 0x00003420 File Offset: 0x00001620
	public int CurrentToggleOptionId
	{
		get
		{
			return this.m_currentOption;
		}
		set
		{
			this.m_currentOption = value % 3;
			switch (this.m_currentOption)
			{
			case 0:
			case 3:
			case 4:
			case 5:
				this.Active = true;
				Time.maximumDeltaTime = 0.016666668f;
				this.m_synced = true;
				break;
			case 1:
				this.Active = false;
				Time.maximumDeltaTime = 0.016666668f;
				this.m_synced = true;
				break;
			case 2:
				this.Active = false;
				Time.maximumDeltaTime = 0.033333335f;
				this.m_synced = false;
				break;
			}
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x000034B7 File Offset: 0x000016B7
	public void Awake()
	{
		PhysicsLimitTest.Instance = this;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000034BF File Offset: 0x000016BF
	public void OnEnable()
	{
		Time.maximumDeltaTime = 0.016666668f;
		this.m_lastRealTime = Time.realtimeSinceStartup;
		this.m_synced = true;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000034DD File Offset: 0x000016DD
	public void OnDestroy()
	{
		if (PhysicsLimitTest.Instance == this)
		{
			PhysicsLimitTest.Instance = null;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000050 RID: 80 RVA: 0x000034F5 File Offset: 0x000016F5
	// (set) Token: 0x06000051 RID: 81 RVA: 0x000034FD File Offset: 0x000016FD
	public bool Active
	{
		get
		{
			return base.enabled;
		}
		set
		{
			base.enabled = value;
			if (value)
			{
			}
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000052 RID: 82 RVA: 0x0000350C File Offset: 0x0000170C
	public static bool IsSynced
	{
		get
		{
			return !PhysicsLimitTest.Instance || PhysicsLimitTest.Instance.m_synced;
		}
	}

	// Token: 0x04000024 RID: 36
	private const int TIME_SAMPLE_SIZE = 500;

	// Token: 0x04000025 RID: 37
	public static PhysicsLimitTest Instance;

	// Token: 0x04000026 RID: 38
	private int m_currentOption;

	// Token: 0x04000027 RID: 39
	private readonly float[] m_timeSamples = new float[500];

	// Token: 0x04000028 RID: 40
	private int m_timeSampleIndex;

	// Token: 0x04000029 RID: 41
	private int m_totalFrames;

	// Token: 0x0400002A RID: 42
	private int m_fixedUpdates;

	// Token: 0x0400002B RID: 43
	private int m_frames;

	// Token: 0x0400002C RID: 44
	private float m_time;

	// Token: 0x0400002D RID: 45
	private bool m_synced = true;

	// Token: 0x0400002E RID: 46
	private float m_lastRealTime;
}
