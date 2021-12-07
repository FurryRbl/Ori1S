using System;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public class BlendVector4 : Blend<Vector4>
{
	// Token: 0x06001B00 RID: 6912 RVA: 0x00073A2E File Offset: 0x00071C2E
	public BlendVector4(Func<float, float> ease) : base(ease, new Func<Vector4, Vector4, float, Vector4>(BlendVector4.Vector4Lerp))
	{
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x00073A44 File Offset: 0x00071C44
	public static Vector4 Vector4Lerp(Vector4 start, Vector4 end, float s)
	{
		start.x *= start.w;
		start.y *= start.w;
		start.z *= start.w;
		end.x *= end.w;
		end.y *= end.w;
		end.z *= end.w;
		Vector4 result = Vector4.Lerp(start, end, s);
		result.x /= result.w;
		result.y /= result.w;
		result.z /= result.w;
		return result;
	}
}
