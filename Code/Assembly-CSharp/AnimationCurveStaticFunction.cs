using System;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public static class AnimationCurveStaticFunction
{
	// Token: 0x06001656 RID: 5718 RVA: 0x00062898 File Offset: 0x00060A98
	public static float CurveDuration(this AnimationCurve curve)
	{
		return (curve.length <= 0) ? 0f : curve[curve.length - 1].time;
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x000628D1 File Offset: 0x00060AD1
	public static bool IsLooping(this AnimationCurve curve)
	{
		return curve.postWrapMode == WrapMode.Loop || curve.postWrapMode == WrapMode.PingPong;
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x000628EB File Offset: 0x00060AEB
	public static float EvaluateSlope(this AnimationCurve curve, float time, float delta = 0.01f)
	{
		return (curve.Evaluate(time + delta) - curve.Evaluate(time)) / delta;
	}
}
