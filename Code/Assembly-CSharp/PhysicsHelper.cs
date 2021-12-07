using System;
using UnityEngine;

// Token: 0x0200034D RID: 845
public static class PhysicsHelper
{
	// Token: 0x06001829 RID: 6185 RVA: 0x00067827 File Offset: 0x00065A27
	public static void AddForceSafe(this Rigidbody rigidbody, Vector2 force)
	{
		if (rigidbody.gameObject.activeInHierarchy)
		{
			rigidbody.AddForce(force);
		}
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x00067848 File Offset: 0x00065A48
	public static void AddForceSafe(this Rigidbody rigidbody, Vector2 force, ForceMode forceMode)
	{
		if (rigidbody.gameObject.activeInHierarchy)
		{
			rigidbody.AddForce(force, forceMode);
		}
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x00067874 File Offset: 0x00065A74
	public static Vector3 CalculateAverageNormalFromContactPoints(ContactPoint[] contacts)
	{
		Vector3 a = Vector3.zero;
		foreach (ContactPoint contactPoint in contacts)
		{
			a += contactPoint.normal;
		}
		a.z = 0f;
		return a.normalized;
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x000678CC File Offset: 0x00065ACC
	public static Vector3 CalculateAverageGroundNormalFromContactPoints(ContactPoint[] contacts)
	{
		Vector3 a = Vector3.zero;
		float num = Mathf.Cos(0.7853982f);
		foreach (ContactPoint contactPoint in contacts)
		{
			if (Vector3.Dot(contactPoint.normal, Vector3.up) > num)
			{
				a += contactPoint.normal;
			}
		}
		a.z = 0f;
		return a.normalized;
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x00067944 File Offset: 0x00065B44
	public static void CalculateArcTrajectory(float gravity, Vector3 delta, out float time, out Vector2 speed, float height = 2f)
	{
		Vector2 vector = delta;
		Vector2 vector2 = Vector3.zero;
		float num = height;
		if (vector.y > 0f)
		{
			num = vector.y + num;
		}
		vector2.y = Mathf.Sqrt(2f * gravity * num);
		float num2 = Mathf.Max((-vector2.y + Mathf.Sqrt(vector2.y * vector2.y - 2f * gravity * vector.y)) / -gravity, (-vector2.y - Mathf.Sqrt(vector2.y * vector2.y - 2f * gravity * vector.y)) / -gravity);
		vector2.x = vector.x / num2;
		time = num2;
		speed = vector2;
	}

	// Token: 0x0600182E RID: 6190 RVA: 0x00067A1C File Offset: 0x00065C1C
	public static bool ArcSphereCast(float gravity, float radius, Vector3 origin, Vector3 speed, float duration, LayerMask layerMask, GameObject target, out RaycastHit finalHitInfo)
	{
		finalHitInfo = default(RaycastHit);
		Vector3 vector = origin;
		for (float num = 0f; num < duration; num += 0.016666668f)
		{
			Vector3 vector2 = vector;
			speed += gravity * Vector3.down * 0.016666668f;
			vector += speed * 0.016666668f;
			Vector3 vector3 = vector - vector2;
			bool flag = false;
			bool flag2 = false;
			RaycastHit[] array = Physics.SphereCastAll(vector2, radius, vector3.normalized, vector3.magnitude, layerMask);
			foreach (RaycastHit raycastHit in array)
			{
				if (!raycastHit.collider.isTrigger)
				{
					if (raycastHit.collider.gameObject == target)
					{
						finalHitInfo = raycastHit;
						flag = true;
						break;
					}
					flag2 = true;
				}
			}
			if (flag)
			{
				return true;
			}
			if (flag2)
			{
				return false;
			}
		}
		return false;
	}

	// Token: 0x0600182F RID: 6191 RVA: 0x00067B2E File Offset: 0x00065D2E
	public static float CalculateSpeedFromHeight(float height, float gravity)
	{
		return Mathf.Sqrt(height * 2f * gravity);
	}
}
