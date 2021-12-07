using System;
using System.IO;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class FPSPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000EF3 RID: 3827 RVA: 0x00044B88 File Offset: 0x00042D88
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
		this.m_streamWriter = new StreamWriter(new FileStream(Path.Combine(OutputFolder.BuildOutputPath, "fpsPluginReport.csv"), FileMode.Create));
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x00044BB8 File Offset: 0x00042DB8
	private void Update()
	{
		this.m_accumulatingFPS++;
		this.m_timeInterval += Time.deltaTime;
		if (this.m_timeInterval >= 1f)
		{
			this.m_lastFPS = this.m_accumulatingFPS;
			this.m_accumulatingFPS = 0;
			this.m_timeInterval = 0f;
		}
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x00044C14 File Offset: 0x00042E14
	public void PlayCycle(int frame)
	{
		if (this.m_streamWriter != null && frame > 0 && frame % 60 == 0)
		{
			this.m_streamWriter.WriteLine(this.m_lastFPS);
		}
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x00044C4D File Offset: 0x00042E4D
	public void RecordCycle(int frame)
	{
		FPSData.Record(Recorder.Instance.RecorderStream, this.m_lastFPS);
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x00044C64 File Offset: 0x00042E64
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x00044C6C File Offset: 0x00042E6C
	private void OnDestroy()
	{
		if (this.m_streamWriter == null)
		{
			return;
		}
		((IDisposable)this.m_streamWriter).Dispose();
		this.m_streamWriter = null;
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x00044C8C File Offset: 0x00042E8C
	private void OnApplicationQuit()
	{
		if (this.m_streamWriter == null)
		{
			return;
		}
		((IDisposable)this.m_streamWriter).Dispose();
		this.m_streamWriter = null;
	}

	// Token: 0x04000BE5 RID: 3045
	private int m_accumulatingFPS;

	// Token: 0x04000BE6 RID: 3046
	private int m_lastFPS;

	// Token: 0x04000BE7 RID: 3047
	private StreamWriter m_streamWriter;

	// Token: 0x04000BE8 RID: 3048
	private float m_timeInterval;
}
