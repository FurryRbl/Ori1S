using System;
using UnityEngine;

// Token: 0x020007B0 RID: 1968
public class AnimationCurveChangeDetector
{
	// Token: 0x06002D89 RID: 11657 RVA: 0x000C2664 File Offset: 0x000C0864
	public bool CheckForChanges(AnimationCurve curve)
	{
		if (curve == null)
		{
			return false;
		}
		Keyframe[] keys = curve.keys;
		Keyframe[] lastCurveKeys = this.m_lastCurveKeys;
		if (keys.Length != lastCurveKeys.Length)
		{
			this.m_lastCurveKeys = curve.keys;
			return true;
		}
		for (int i = 0; i < keys.Length; i++)
		{
			Keyframe keyframe = lastCurveKeys[i];
			Keyframe keyframe2 = keys[i];
			if (keyframe.inTangent != keyframe2.inTangent || keyframe.outTangent != keyframe2.outTangent || keyframe.tangentMode != keyframe2.tangentMode || keyframe.time != keyframe2.time || keyframe.value != keyframe2.value)
			{
				this.m_lastCurveKeys = curve.keys;
				return true;
			}
		}
		return false;
	}

	// Token: 0x04002908 RID: 10504
	private Keyframe[] m_lastCurveKeys = new Keyframe[0];
}
