using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class AnimationMetaData : ScriptableObject
{
	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000919 RID: 2329 RVA: 0x000272FA File Offset: 0x000254FA
	// (set) Token: 0x0600091A RID: 2330 RVA: 0x0002730B File Offset: 0x0002550B
	public int FrameCount
	{
		get
		{
			return this.FrameEnd - this.FrameStart + 1;
		}
		set
		{
			this.FrameEnd = value + this.FrameStart - 1;
		}
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00027320 File Offset: 0x00025520
	[ContextMenu("Mirror")]
	public void Mirror()
	{
		for (int i = 0; i < this.Camera.PositionX.Values.Count; i++)
		{
			List<float> values;
			List<float> list = values = this.Camera.PositionX.Values;
			int index2;
			int index = index2 = i;
			float num = values[index2];
			list[index] = num * -1f;
		}
		for (int j = 0; j < this.CameraData.PositionX.Values.Count; j++)
		{
			List<float> values2;
			List<float> list2 = values2 = this.CameraData.PositionX.Values;
			int index2;
			int index3 = index2 = j;
			float num = values2[index2];
			list2[index3] = num * -1f;
		}
		foreach (AnimationMetaData.AnimationData animationData in this.Data)
		{
			for (int k = 0; k < animationData.PositionX.Values.Count; k++)
			{
				List<float> values3;
				List<float> list3 = values3 = animationData.PositionX.Values;
				int index2;
				int index4 = index2 = k;
				float num = values3[index2];
				list3[index4] = num * -1f;
			}
		}
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00027470 File Offset: 0x00025670
	public AnimationMetaData.AnimationData FindData(string name)
	{
		return this.Data.Find((AnimationMetaData.AnimationData a) => a.Name == name);
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x000274A4 File Offset: 0x000256A4
	public void Process(GameObject model)
	{
		this.CameraData = new AnimationMetaData.AnimationData();
		float duration = (float)this.FrameCount / this.FrameRate;
		float step = 1f / this.FrameRate;
		this.CameraData.PositionX.Values.Clear();
		this.CameraData.PositionX.Duration = duration;
		this.CameraData.PositionY.Values.Clear();
		this.CameraData.PositionY.Duration = duration;
		this.CameraData.PositionZ.Values.Clear();
		this.CameraData.PositionZ.Duration = duration;
		this.CameraData.RotationZ.Values.Clear();
		this.CameraData.RotationZ.Duration = duration;
		this.Camera.PositionX.Values.Clear();
		this.Camera.PositionX.Duration = duration;
		this.Camera.PositionY.Values.Clear();
		this.Camera.PositionY.Duration = duration;
		this.Camera.PositionZ.Values.Clear();
		this.Camera.PositionZ.Duration = duration;
		AnimationMetaDataCurveProcessor animationMetaDataCurveProcessor = new AnimationMetaDataCurveProcessor(this);
		animationMetaDataCurveProcessor.ProcessCurves(model, step, (float)this.FrameStart / this.FrameRate, (float)this.FrameEnd / this.FrameRate);
	}

	// Token: 0x0400074F RID: 1871
	public TextureAnimation Animation;

	// Token: 0x04000750 RID: 1872
	public float CameraTargetDistance = 212f;

	// Token: 0x04000751 RID: 1873
	public float CameraFieldOfView = 45f;

	// Token: 0x04000752 RID: 1874
	public float AspectRatio = 1f;

	// Token: 0x04000753 RID: 1875
	public bool Perspective = true;

	// Token: 0x04000754 RID: 1876
	public string CameraName = "camera";

	// Token: 0x04000755 RID: 1877
	public Vector2 PlaneSize;

	// Token: 0x04000756 RID: 1878
	public int FrameEnd;

	// Token: 0x04000757 RID: 1879
	public AnimationMetaData.ViewModes ViewMode;

	// Token: 0x04000758 RID: 1880
	public int FrameStart;

	// Token: 0x04000759 RID: 1881
	public float FrameRate = 30f;

	// Token: 0x0400075A RID: 1882
	public List<AnimationMetaData.AnimationData> Data = new List<AnimationMetaData.AnimationData>();

	// Token: 0x0400075B RID: 1883
	public AnimationMetaData.AnimationData CameraData = new AnimationMetaData.AnimationData();

	// Token: 0x0400075C RID: 1884
	public AnimationMetaData.AnimationData Camera = new AnimationMetaData.AnimationData();

	// Token: 0x020000E3 RID: 227
	[Serializable]
	public class AnimationData
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00027668 File Offset: 0x00025868
		public Vector3 GetPositionAtTime(float time)
		{
			return new Vector3(this.PositionX.GetValueAtTime(time), this.PositionY.GetValueAtTime(time), this.PositionZ.GetValueAtTime(time));
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000276A0 File Offset: 0x000258A0
		public Vector3 GetPositionAtFrame(int frame)
		{
			return new Vector3(this.PositionX.GetValueAtFrame(frame), this.PositionY.GetValueAtFrame(frame), this.PositionZ.GetValueAtFrame(frame));
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x000276D8 File Offset: 0x000258D8
		public Vector3 GetRawPositionAtTime(float time)
		{
			return new Vector3(this.PositionX.GetRawValueAtTime(time), this.PositionY.GetRawValueAtTime(time), this.PositionZ.GetRawValueAtTime(time));
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00027710 File Offset: 0x00025910
		public Vector3 GetRawPositionAtFrame(int frame)
		{
			return new Vector3(this.PositionX.GetRawValueAtFrame(frame), this.PositionY.GetRawValueAtFrame(frame), this.PositionZ.GetRawValueAtFrame(frame));
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00027746 File Offset: 0x00025946
		public Vector2 GetSpeedAtTime(float time)
		{
			return this.GetDeltaPositionAtTime(time) / Time.deltaTime;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002775E File Offset: 0x0002595E
		public Vector2 GetRawSpeedAtTime(float time)
		{
			return this.GetRawDeltaPositionAtTime(time) / Time.deltaTime;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00027778 File Offset: 0x00025978
		public Vector3 GetDeltaPositionAtTime(float time)
		{
			return new Vector3(this.PositionX.GetValueAtTime(time) - this.PositionX.GetValueAtTime(time - Time.deltaTime), this.PositionY.GetValueAtTime(time) - this.PositionY.GetValueAtTime(time - Time.deltaTime), this.PositionZ.GetValueAtTime(time) - this.PositionZ.GetValueAtTime(time - Time.deltaTime));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000277E8 File Offset: 0x000259E8
		public Vector3 GetRawDeltaPositionAtTime(float time)
		{
			return new Vector3(this.PositionX.GetRawValueAtTime(time) - this.PositionX.GetRawValueAtTime(time - Time.deltaTime), this.PositionY.GetRawValueAtTime(time) - this.PositionY.GetRawValueAtTime(time - Time.deltaTime), this.PositionZ.GetRawValueAtTime(time) - this.PositionZ.GetRawValueAtTime(time - Time.deltaTime));
		}

		// Token: 0x0400075D RID: 1885
		public string Name;

		// Token: 0x0400075E RID: 1886
		public AnimationMetaData.FloatAnimation PositionX = new AnimationMetaData.FloatAnimation();

		// Token: 0x0400075F RID: 1887
		public AnimationMetaData.FloatAnimation PositionY = new AnimationMetaData.FloatAnimation();

		// Token: 0x04000760 RID: 1888
		public AnimationMetaData.FloatAnimation PositionZ = new AnimationMetaData.FloatAnimation();

		// Token: 0x04000761 RID: 1889
		public AnimationMetaData.FloatAnimation RotationZ = new AnimationMetaData.FloatAnimation();

		// Token: 0x04000762 RID: 1890
		public AnimationMetaData.FloatAnimation ScaleX = new AnimationMetaData.FloatAnimation();

		// Token: 0x04000763 RID: 1891
		public AnimationMetaData.FloatAnimation ScaleY = new AnimationMetaData.FloatAnimation();
	}

	// Token: 0x02000200 RID: 512
	[Serializable]
	public class FloatAnimation
	{
		// Token: 0x060011C4 RID: 4548 RVA: 0x00051DC8 File Offset: 0x0004FFC8
		public float GetValueAtFrame(int frame)
		{
			float num = (float)Mathf.Clamp(frame, 0, this.Values.Count - 1);
			int num2 = Mathf.FloorToInt(num);
			int num3 = Mathf.CeilToInt(num);
			return Mathf.Lerp(this.Values[num2], this.Values[num3], Mathf.InverseLerp((float)num2, (float)num3, num));
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00051E20 File Offset: 0x00050020
		public float GetValueAtTime(float time)
		{
			float num = Mathf.Clamp(time / this.Duration * (float)this.Values.Count, 0f, (float)(this.Values.Count - 1));
			int num2 = Mathf.FloorToInt(num);
			int num3 = Mathf.CeilToInt(num);
			return Mathf.Lerp(this.Values[num2], this.Values[num3], Mathf.InverseLerp((float)num2, (float)num3, num));
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00051E90 File Offset: 0x00050090
		public float GetRawValueAtTime(float time)
		{
			int index = Mathf.FloorToInt(Mathf.Clamp(time / this.Duration * (float)this.Values.Count, 0f, (float)(this.Values.Count - 1)));
			return this.Values[index];
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00051EDC File Offset: 0x000500DC
		public float GetRawValueAtFrame(int frame)
		{
			return this.Values[frame];
		}

		// Token: 0x04000F4C RID: 3916
		public List<float> Values = new List<float>();

		// Token: 0x04000F4D RID: 3917
		public float Duration;
	}

	// Token: 0x02000394 RID: 916
	public enum ViewModes
	{
		// Token: 0x0400162C RID: 5676
		Left,
		// Token: 0x0400162D RID: 5677
		Front,
		// Token: 0x0400162E RID: 5678
		Right
	}
}
