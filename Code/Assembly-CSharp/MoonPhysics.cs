using System;
using UnityEngine;

// Token: 0x02000485 RID: 1157
public static class MoonPhysics
{
	// Token: 0x06001F9B RID: 8091 RVA: 0x0008B064 File Offset: 0x00089264
	public static bool FastSphereCast(Ray ray, float radius, out RaycastHit hitInfo, float distance, LayerMask layerMask)
	{
		if (radius == 0f)
		{
			return Physics.Raycast(ray, out hitInfo, distance, layerMask);
		}
		Vector3 a = Vector3.Cross(Vector3.forward, ray.direction);
		Ray ray2 = new Ray(ray.origin - a * radius, ray.direction);
		Ray ray3 = new Ray(ray.origin + a * radius, ray.direction);
		bool flag = Physics.Raycast(ray, out hitInfo, distance, layerMask);
		RaycastHit raycastHit;
		bool flag2 = Physics.Raycast(ray2, out raycastHit, distance, layerMask);
		RaycastHit raycastHit2;
		bool flag3 = Physics.Raycast(ray3, out raycastHit2, distance, layerMask);
		if (!flag && !flag2 && !flag3)
		{
			return false;
		}
		if ((flag2 && hitInfo.distance > raycastHit.distance) || !flag)
		{
			hitInfo = raycastHit;
		}
		if ((flag3 && hitInfo.distance > raycastHit2.distance) || (!flag && !flag2))
		{
			hitInfo = raycastHit2;
		}
		return true;
	}
}
