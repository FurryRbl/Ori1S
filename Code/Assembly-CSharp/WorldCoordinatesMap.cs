using System;
using UnityEngine;

// Token: 0x0200089A RID: 2202
public class WorldCoordinatesMap : MonoBehaviour
{
	// Token: 0x0600315E RID: 12638 RVA: 0x000D2014 File Offset: 0x000D0214
	public Vector3 WorldToLocal(Vector3 position)
	{
		int num = this.Segments.Length - 1;
		Vector3 position2 = base.transform.position;
		Vector2 vector = base.transform.lossyScale;
		if (this.Direction == WorldCoordinatesMap.SegmentDirection.Horizontal)
		{
			for (int i = 0; i < this.Segments.Length - 1; i++)
			{
				int num2 = Mathf.Clamp(i, 0, num);
				int num3 = Mathf.Clamp(i + 1, 0, num);
				WorldCoordinatesMap.SegmentInfo segmentInfo = this.Segments[num2];
				WorldCoordinatesMap.SegmentInfo segmentInfo2 = this.Segments[num3];
				float num4 = (float)num2 / (float)num;
				float num5 = (float)num3 / (float)num;
				float num6 = Mathf.Lerp(position2.x - vector.x * 0.5f, position2.x + vector.x * 0.5f, num4 + segmentInfo.Spacing);
				float num7 = Mathf.Lerp(position2.x - vector.x * 0.5f, position2.x + vector.x * 0.5f, num5 + segmentInfo2.Spacing);
				if (position.x >= num6 && position.x <= num7)
				{
					Vector2 vector2 = new Vector2(num6, position2.y + segmentInfo.High * vector.y);
					Vector2 vector3 = new Vector2(num6, position2.y + segmentInfo.Low * vector.y);
					Vector2 vector4 = new Vector2(num7, position2.y + segmentInfo2.High * vector.y);
					Vector2 vector5 = new Vector2(num7, position2.y + segmentInfo2.Low * vector.y);
					float t = Mathf.InverseLerp(num6, num7, position.x);
					float x = Mathf.Lerp(num4, num5, t);
					float b = Mathf.Lerp(vector2.y, vector4.y, t);
					float a = Mathf.Lerp(vector3.y, vector5.y, t);
					float y = Mathf.InverseLerp(a, b, position.y);
					return new Vector3(x, y, position2.z);
				}
			}
		}
		else
		{
			for (int j = 0; j < this.Segments.Length - 1; j++)
			{
				int num8 = Mathf.Clamp(j, 0, num);
				int num9 = Mathf.Clamp(j + 1, 0, num);
				WorldCoordinatesMap.SegmentInfo segmentInfo3 = this.Segments[num8];
				WorldCoordinatesMap.SegmentInfo segmentInfo4 = this.Segments[num9];
				float num10 = (float)num8 / (float)num;
				float num11 = (float)num9 / (float)num;
				float num12 = Mathf.Lerp(position2.y - vector.y * 0.5f, position2.y + vector.y * 0.5f, num10 + segmentInfo3.Spacing);
				float num13 = Mathf.Lerp(position2.y - vector.y * 0.5f, position2.y + vector.y * 0.5f, num11 + segmentInfo4.Spacing);
				if (position.y >= num12 && position.y <= num13)
				{
					Vector2 vector6 = new Vector2(position2.x + segmentInfo3.High * vector.x, num12);
					Vector2 vector7 = new Vector2(position2.x + segmentInfo3.Low * vector.x, num12);
					Vector2 vector8 = new Vector2(position2.x + segmentInfo4.High * vector.x, num13);
					Vector2 vector9 = new Vector2(position2.x + segmentInfo4.Low * vector.x, num13);
					float t2 = Mathf.InverseLerp(num12, num13, position.y);
					float y2 = Mathf.Lerp(num10, num11, t2);
					float b2 = Mathf.Lerp(vector6.x, vector8.x, t2);
					float a2 = Mathf.Lerp(vector7.x, vector9.x, t2);
					float x2 = Mathf.InverseLerp(a2, b2, position.x);
					return new Vector3(x2, y2, position2.z);
				}
			}
		}
		return new Vector2(0f, 0f);
	}

	// Token: 0x0600315F RID: 12639 RVA: 0x000D2428 File Offset: 0x000D0628
	public bool IsInside(Vector2 position)
	{
		int num = this.Segments.Length - 1;
		Vector2 vector = base.transform.position;
		Vector2 vector2 = base.transform.lossyScale;
		if (this.Direction == WorldCoordinatesMap.SegmentDirection.Horizontal)
		{
			for (int i = 0; i < this.Segments.Length - 1; i++)
			{
				int num2 = Mathf.Clamp(i, 0, num);
				int num3 = Mathf.Clamp(i + 1, 0, num);
				WorldCoordinatesMap.SegmentInfo segmentInfo = this.Segments[num2];
				WorldCoordinatesMap.SegmentInfo segmentInfo2 = this.Segments[num3];
				float num4 = (float)num2 / (float)num;
				float num5 = (float)num3 / (float)num;
				float num6 = Mathf.Lerp(vector.x - vector2.x * 0.5f, vector.x + vector2.x * 0.5f, num4 + segmentInfo.Spacing);
				float num7 = Mathf.Lerp(vector.x - vector2.x * 0.5f, vector.x + vector2.x * 0.5f, num5 + segmentInfo2.Spacing);
				if (position.x >= num6 && position.x <= num7)
				{
					Vector2 vector3 = new Vector2(num6, vector.y + segmentInfo.High * vector2.y);
					Vector2 vector4 = new Vector2(num6, vector.y + segmentInfo.Low * vector2.y);
					Vector2 vector5 = new Vector2(num7, vector.y + segmentInfo2.High * vector2.y);
					Vector2 vector6 = new Vector2(num7, vector.y + segmentInfo2.Low * vector2.y);
					float t = Mathf.InverseLerp(num6, num7, position.x);
					float num8 = Mathf.Lerp(vector3.y, vector5.y, t);
					float num9 = Mathf.Lerp(vector4.y, vector6.y, t);
					if (position.y >= num9 && position.y <= num8)
					{
						return true;
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < this.Segments.Length - 1; j++)
			{
				int num10 = Mathf.Clamp(j, 0, num);
				int num11 = Mathf.Clamp(j + 1, 0, num);
				WorldCoordinatesMap.SegmentInfo segmentInfo3 = this.Segments[num10];
				WorldCoordinatesMap.SegmentInfo segmentInfo4 = this.Segments[num11];
				float num12 = (float)num10 / (float)num;
				float num13 = (float)num11 / (float)num;
				float num14 = Mathf.Lerp(vector.y - vector2.y * 0.5f, vector.y + vector2.y * 0.5f, num12 + segmentInfo3.Spacing);
				float num15 = Mathf.Lerp(vector.y - vector2.y * 0.5f, vector.y + vector2.y * 0.5f, num13 + segmentInfo4.Spacing);
				if (position.y >= num14 && position.y <= num15)
				{
					Vector2 vector7 = new Vector2(vector.x + segmentInfo3.High * vector2.x, num14);
					Vector2 vector8 = new Vector2(vector.x + segmentInfo3.Low * vector2.x, num14);
					Vector2 vector9 = new Vector2(vector.x + segmentInfo4.High * vector2.x, num15);
					Vector2 vector10 = new Vector2(vector.x + segmentInfo4.Low * vector2.x, num15);
					float t2 = Mathf.InverseLerp(num14, num15, position.y);
					float num16 = Mathf.Lerp(vector7.x, vector9.x, t2);
					float num17 = Mathf.Lerp(vector8.x, vector10.x, t2);
					if (position.x >= num17 && position.x <= num16)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x000D2808 File Offset: 0x000D0A08
	public Vector3 LocalToWorld(Vector3 position)
	{
		int num = this.Segments.Length - 1;
		Vector3 position2 = base.transform.position;
		Vector2 vector = base.transform.lossyScale;
		if (this.Direction == WorldCoordinatesMap.SegmentDirection.Horizontal)
		{
			int num2 = Mathf.FloorToInt(position.x * (float)num);
			int num3 = Mathf.Clamp(num2, 0, num);
			int num4 = Mathf.Clamp(num2 + 1, 0, num);
			WorldCoordinatesMap.SegmentInfo segmentInfo = this.Segments[num3];
			WorldCoordinatesMap.SegmentInfo segmentInfo2 = this.Segments[num4];
			float num5 = (float)num3 / (float)num;
			float num6 = (float)num4 / (float)num;
			float x = Mathf.Lerp(position2.x - vector.x * 0.5f, position2.x + vector.x * 0.5f, num5 + segmentInfo.Spacing);
			float x2 = Mathf.Lerp(position2.x - vector.x * 0.5f, position2.x + vector.x * 0.5f, num6 + segmentInfo2.Spacing);
			Vector3 b = new Vector3(x, position2.y + segmentInfo.High * vector.y, position2.z);
			Vector3 a = new Vector3(x, position2.y + segmentInfo.Low * vector.y, position2.z);
			Vector3 b2 = new Vector3(x2, position2.y + segmentInfo2.High * vector.y, position2.z);
			Vector3 a2 = new Vector3(x2, position2.y + segmentInfo2.Low * vector.y, position2.z);
			float t = Mathf.InverseLerp(num5, num6, position.x);
			return Vector3.Lerp(Vector3.Lerp(a, b, position.y), Vector3.Lerp(a2, b2, position.y), t);
		}
		int num7 = Mathf.FloorToInt(position.y * (float)num);
		int num8 = Mathf.Clamp(num7, 0, num);
		int num9 = Mathf.Clamp(num7 + 1, 0, num);
		WorldCoordinatesMap.SegmentInfo segmentInfo3 = this.Segments[num8];
		WorldCoordinatesMap.SegmentInfo segmentInfo4 = this.Segments[num9];
		float num10 = (float)num8 / (float)num;
		float num11 = (float)num9 / (float)num;
		float y = Mathf.Lerp(position2.y - vector.y * 0.5f, position2.y + vector.y * 0.5f, num10 + segmentInfo3.Spacing);
		float y2 = Mathf.Lerp(position2.y - vector.y * 0.5f, position2.y + vector.y * 0.5f, num11 + segmentInfo4.Spacing);
		Vector3 b3 = new Vector3(position2.x + segmentInfo3.High * vector.x, y, position2.z);
		Vector3 a3 = new Vector3(position2.x + segmentInfo3.Low * vector.x, y, position2.z);
		Vector3 b4 = new Vector3(position2.x + segmentInfo4.High * vector.x, y2, position2.z);
		Vector3 a4 = new Vector3(position2.x + segmentInfo4.Low * vector.x, y2, position2.z);
		float t2 = Mathf.InverseLerp(num10, num11, position.y);
		return Vector3.Lerp(Vector3.Lerp(a3, b3, position.x), Vector3.Lerp(a4, b4, position.x), t2);
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x000D2B7C File Offset: 0x000D0D7C
	public void OnDrawGizmos()
	{
		if (this.Segments.Length == 0)
		{
			return;
		}
		int num = this.Segments.Length - 1;
		if (this.Direction == WorldCoordinatesMap.SegmentDirection.Horizontal)
		{
			for (int i = 0; i < num; i++)
			{
				float x = (float)i / (float)num;
				float x2 = (float)(i + 1) / (float)num;
				Vector3 vector = this.LocalToWorld(new Vector2(x, 0f));
				Vector3 vector2 = this.LocalToWorld(new Vector2(x, 1f));
				Vector3 vector3 = this.LocalToWorld(new Vector2(x2, 0f));
				Vector3 vector4 = this.LocalToWorld(new Vector2(x2, 1f));
				Gizmos.DrawLine(vector2, vector4);
				Gizmos.DrawLine(vector4, vector3);
				Gizmos.DrawLine(vector3, vector);
				Gizmos.DrawLine(vector, vector2);
			}
		}
		else
		{
			for (int j = 0; j < num; j++)
			{
				float y = (float)j / (float)num;
				float y2 = (float)(j + 1) / (float)num;
				Vector3 vector5 = this.LocalToWorld(new Vector2(0f, y));
				Vector3 vector6 = this.LocalToWorld(new Vector2(1f, y));
				Vector3 vector7 = this.LocalToWorld(new Vector2(0f, y2));
				Vector3 vector8 = this.LocalToWorld(new Vector2(1f, y2));
				Gizmos.DrawLine(vector6, vector8);
				Gizmos.DrawLine(vector8, vector7);
				Gizmos.DrawLine(vector7, vector5);
				Gizmos.DrawLine(vector5, vector6);
			}
		}
	}

	// Token: 0x04002CA5 RID: 11429
	public WorldCoordinatesMap.SegmentDirection Direction;

	// Token: 0x04002CA6 RID: 11430
	public WorldCoordinatesMap.SegmentInfo[] Segments = new WorldCoordinatesMap.SegmentInfo[]
	{
		new WorldCoordinatesMap.SegmentInfo(),
		new WorldCoordinatesMap.SegmentInfo(),
		new WorldCoordinatesMap.SegmentInfo(),
		new WorldCoordinatesMap.SegmentInfo()
	};

	// Token: 0x0200089B RID: 2203
	public enum SegmentDirection
	{
		// Token: 0x04002CA8 RID: 11432
		Horizontal,
		// Token: 0x04002CA9 RID: 11433
		Vertical
	}

	// Token: 0x0200089C RID: 2204
	[Serializable]
	public class SegmentInfo
	{
		// Token: 0x04002CAA RID: 11434
		public float High = 0.5f;

		// Token: 0x04002CAB RID: 11435
		public float Low = -0.5f;

		// Token: 0x04002CAC RID: 11436
		public float Spacing;
	}
}
