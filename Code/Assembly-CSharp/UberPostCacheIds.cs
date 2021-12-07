using System;
using UnityEngine;

// Token: 0x0200084D RID: 2125
public class UberPostCacheIds
{
	// Token: 0x0600304C RID: 12364 RVA: 0x000CC70C File Offset: 0x000CA90C
	public static void Initialize()
	{
		if (UberPostCacheIds.GrainOffsetScale == -1)
		{
			UberPostCacheIds.GrainOffsetScale = Shader.PropertyToID("_GrainOffsetScale");
			UberPostCacheIds.ContrastBright = Shader.PropertyToID("_ContrastBright");
			UberPostCacheIds.ContrastBrightUI = Shader.PropertyToID("_ContrastBrightUI");
			UberPostCacheIds.Threshold = Shader.PropertyToID("_Threshold");
			UberPostCacheIds.BloomIntensity = Shader.PropertyToID("_BloomIntensity");
			UberPostCacheIds.Intensity = Shader.PropertyToID("_Intensity");
			UberPostCacheIds.Desat = Shader.PropertyToID("_Desat");
			UberPostCacheIds.ThresholdId = Shader.PropertyToID("threshhold");
			UberPostCacheIds.VignetteIntensity = Shader.PropertyToID("vignetteIntensity");
			UberPostCacheIds.BezierLengths = Shader.PropertyToID("_BezierLengths");
			UberPostCacheIds.CenterRadius = Shader.PropertyToID("_CenterRadius");
			UberPostCacheIds.TwirlAngle = Shader.PropertyToID("_TwirlAngle");
			UberPostCacheIds.BezierR = Shader.PropertyToID("_Bezier_R");
			UberPostCacheIds.BezierG = Shader.PropertyToID("_Bezier_G");
			UberPostCacheIds.BezierB = Shader.PropertyToID("_Bezier_B");
			UberPostCacheIds.Camera2World = Shader.PropertyToID("_Camera2World");
			UberPostCacheIds.Offsets = Shader.PropertyToID("offsets");
			UberPostCacheIds.TxtAlphaCenter = Shader.PropertyToID("_TxtAlphaCenter");
			UberPostCacheIds.SpeedVec = Shader.PropertyToID("_SpeedVec");
			UberPostCacheIds.CameraVelocity = Shader.PropertyToID("_CameraVelocity");
			UberPostCacheIds.ColorBuffer = Shader.PropertyToID("_ColorBuffer");
			UberPostCacheIds.Alpha = Shader.PropertyToID("_Alpha");
			UberPostCacheIds.Buf = Shader.PropertyToID("_Buf");
		}
	}

	// Token: 0x04002B78 RID: 11128
	public static int GrainOffsetScale = -1;

	// Token: 0x04002B79 RID: 11129
	public static int ContrastBright = -1;

	// Token: 0x04002B7A RID: 11130
	public static int Threshold = -1;

	// Token: 0x04002B7B RID: 11131
	public static int BloomIntensity = -1;

	// Token: 0x04002B7C RID: 11132
	public static int Intensity = -1;

	// Token: 0x04002B7D RID: 11133
	public static int Desat = -1;

	// Token: 0x04002B7E RID: 11134
	public static int ThresholdId = -1;

	// Token: 0x04002B7F RID: 11135
	public static int VignetteIntensity = -1;

	// Token: 0x04002B80 RID: 11136
	public static int ContrastBrightUI;

	// Token: 0x04002B81 RID: 11137
	public static int BezierLengths;

	// Token: 0x04002B82 RID: 11138
	public static int CenterRadius;

	// Token: 0x04002B83 RID: 11139
	public static int TwirlAngle;

	// Token: 0x04002B84 RID: 11140
	public static int BezierR;

	// Token: 0x04002B85 RID: 11141
	public static int BezierG;

	// Token: 0x04002B86 RID: 11142
	public static int BezierB;

	// Token: 0x04002B87 RID: 11143
	public static int Camera2World;

	// Token: 0x04002B88 RID: 11144
	public static int Offsets;

	// Token: 0x04002B89 RID: 11145
	public static int TxtAlphaCenter;

	// Token: 0x04002B8A RID: 11146
	public static int SpeedVec;

	// Token: 0x04002B8B RID: 11147
	public static int CameraVelocity;

	// Token: 0x04002B8C RID: 11148
	public static int ColorBuffer;

	// Token: 0x04002B8D RID: 11149
	public static int Alpha;

	// Token: 0x04002B8E RID: 11150
	public static int Buf;
}
