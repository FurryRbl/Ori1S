using System;
using UnityEngine;

// Token: 0x0200019A RID: 410
public class RecordTransformPlugin : MonoBehaviour, IRecorderPlugin
{
	// Token: 0x06000FD7 RID: 4055 RVA: 0x00048A5F File Offset: 0x00046C5F
	public void Awake()
	{
		Recorder.Instance.RegisterPlugin(this);
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x00048A6C File Offset: 0x00046C6C
	public void OnDestroy()
	{
		Recorder.Instance.DeregisterPlugin(this);
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x00048A79 File Offset: 0x00046C79
	public void PlayCycle(int frame)
	{
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x00048A7B File Offset: 0x00046C7B
	public Vector4 QuaternionToVector(Quaternion q)
	{
		return new Vector4(q.x, q.y, q.z, q.w);
	}

	// Token: 0x06000FDB RID: 4059 RVA: 0x00048A9E File Offset: 0x00046C9E
	public void RecordCycle(int frame)
	{
		if (TransformRecordable.All.Count > 0)
		{
		}
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00048AB0 File Offset: 0x00046CB0
	public void Exit()
	{
		UnityEngine.Object.DestroyObject(this);
	}
}
