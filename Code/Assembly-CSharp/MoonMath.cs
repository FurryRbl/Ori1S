using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public static class MoonMath
{
	// Token: 0x02000027 RID: 39
	public static class Float
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00008019 File Offset: 0x00006219
		public static float Normalize(float x)
		{
			return (x != 0f) ? Mathf.Sign(x) : 0f;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008036 File Offset: 0x00006236
		public static float MoveTowards(float start, float target, float distance)
		{
			if (target > start)
			{
				return Mathf.Min(target, start + distance);
			}
			return Mathf.Max(target, start - distance);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008052 File Offset: 0x00006252
		public static float ClampedAdd(float start, float offset, float min, float max)
		{
			if (offset > 0f && start < max)
			{
				return Mathf.Min(max, start + offset);
			}
			if (offset < 0f && start > min)
			{
				return Mathf.Max(min, start + offset);
			}
			return start;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000808D File Offset: 0x0000628D
		public static float ClampedSubtract(float start, float offset, float min, float max)
		{
			if (start < min)
			{
				return Mathf.Min(min, start - offset);
			}
			if (start > max)
			{
				return Mathf.Max(max, start - offset);
			}
			return start;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000080B2 File Offset: 0x000062B2
		public static float ClampedDecrease(float start, float amount, float min, float max)
		{
			if (start < min)
			{
				return Mathf.Min(min, start + amount);
			}
			if (start > max)
			{
				return Mathf.Max(max, start - amount);
			}
			return start;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000080D7 File Offset: 0x000062D7
		public static float Wrap(float value, float min, float max)
		{
			value -= min;
			max -= min;
			value -= Mathf.Floor(value / max) * max;
			return value + min;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000080F4 File Offset: 0x000062F4
		public static float AbsoluteMax(float a, float b)
		{
			return (Mathf.Abs(a) <= Mathf.Abs(b)) ? b : a;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000810E File Offset: 0x0000630E
		public static float AbsoluteMin(float a, float b)
		{
			return (Mathf.Abs(a) >= Mathf.Abs(b)) ? b : a;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008128 File Offset: 0x00006328
		public static float AbsoluteDifference(float a, float b)
		{
			return Mathf.Abs(a - b);
		}
	}

	// Token: 0x02000029 RID: 41
	public static class Physics
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x00008181 File Offset: 0x00006381
		public static float SpeedFromHeightAndGravity(float gravity, float height)
		{
			return Mathf.Sqrt(height * 2f * gravity);
		}
	}

	// Token: 0x02000034 RID: 52
	public static class Angle
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00009F9B File Offset: 0x0000819B
		public static float Wrap(float angle)
		{
			while (angle >= 360f)
			{
				angle -= 360f;
			}
			while (angle < 0f)
			{
				angle += 360f;
			}
			return angle;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009FD0 File Offset: 0x000081D0
		public static float Wrap180(float angle)
		{
			while (angle >= 180f)
			{
				angle -= 360f;
			}
			while (angle < -180f)
			{
				angle += 360f;
			}
			return angle;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000A005 File Offset: 0x00008205
		public static float Difference(float value1, float value2)
		{
			return Mathf.Min(Mathf.Abs(value1 - value2), 360f - Mathf.Abs(value1 - value2));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A024 File Offset: 0x00008224
		public static float AngleSubtract(float start, float target)
		{
			float num = MoonMath.Angle.Wrap(target);
			float num2 = MoonMath.Angle.Wrap(start);
			int num3 = (num <= num2) ? -1 : 1;
			if (num3 == -1)
			{
				num = MoonMath.Angle.Wrap(start);
				num2 = MoonMath.Angle.Wrap(target);
			}
			if (Mathf.Abs(num - num2) < 360f - Mathf.Abs(num - num2))
			{
				return Mathf.Abs(num - num2) * (float)num3;
			}
			return (360f - Math.Abs(num - num2)) * (float)num3 * -1f;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public static float RotateTowards(float startDegrees, float targetDegrees, float degrees)
		{
			if (Mathf.Abs(degrees) > MoonMath.Angle.Difference(startDegrees, targetDegrees))
			{
				return targetDegrees;
			}
			if (MoonMath.Angle.Difference(MoonMath.Angle.Wrap(startDegrees + degrees), targetDegrees) < MoonMath.Angle.Difference(MoonMath.Angle.Wrap(startDegrees - degrees), targetDegrees))
			{
				return MoonMath.Angle.Wrap(startDegrees + degrees);
			}
			return MoonMath.Angle.Wrap(startDegrees - degrees);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public static Vector2 Rotate(Vector2 v, float angle)
		{
			if (angle == 0f)
			{
				return v;
			}
			float f = angle * 0.017453292f;
			float num = Mathf.Cos(f);
			float num2 = Mathf.Sin(f);
			return new Vector2(v.x * num - v.y * num2, v.x * num2 + v.y * num);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A153 File Offset: 0x00008353
		public static Vector2 Unrotate(Vector2 v, float angle)
		{
			if (angle == 0f)
			{
				return v;
			}
			return MoonMath.Angle.Rotate(v, -angle);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A16C File Offset: 0x0000836C
		public static float AngleFromVector(Vector2 delta)
		{
			delta.Normalize();
			return Mathf.Atan2(delta.y, delta.x) * 57.29578f;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A199 File Offset: 0x00008399
		public static float AngleFromDirection(Vector2 delta)
		{
			return Mathf.Atan2(delta.y, delta.x) * 57.29578f;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public static Vector2 VectorFromAngle(float angle)
		{
			return new Vector2(Mathf.Cos(angle * 0.017453292f), Mathf.Sin(angle * 0.017453292f));
		}
	}

	// Token: 0x02000289 RID: 649
	public static class Normal
	{
		// Token: 0x0600153A RID: 5434 RVA: 0x0005E8C8 File Offset: 0x0005CAC8
		public static bool WithinDegrees(Vector2 normal1, Vector2 normal2, float degrees)
		{
			return Vector3.Dot(normal1, normal2) >= Mathf.Cos(0.017453292f * degrees);
		}
	}

	// Token: 0x0200037B RID: 891
	public static class Vector
	{
		// Token: 0x06001965 RID: 6501 RVA: 0x0006D5F4 File Offset: 0x0006B7F4
		public static Vector2 ApplyCircleDeadzone(Vector2 axis, float deadzoneRadius)
		{
			if (axis.magnitude < deadzoneRadius)
			{
				return Vector2.zero;
			}
			return axis.normalized * (axis.magnitude - deadzoneRadius) / (1f - deadzoneRadius);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0006D638 File Offset: 0x0006B838
		public static Vector2 ApplyRectangleDeadzone(Vector2 axis, float deadzoneX, float deadzoneY)
		{
			axis.x = Mathf.Sign(axis.x) * Mathf.Max(Mathf.Abs(axis.x) - deadzoneX, 0f) / (1f - deadzoneX);
			axis.y = Mathf.Sign(axis.y) * Mathf.Max(Mathf.Abs(axis.y) - deadzoneY, 0f) / (1f - deadzoneY);
			return axis;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0006D6AE File Offset: 0x0006B8AE
		public static Vector3 Abs(Vector3 v)
		{
			return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0006D6DC File Offset: 0x0006B8DC
		public static Vector3 Divide(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0006D71C File Offset: 0x0006B91C
		public static Vector2 RotateTowards(Vector2 angleVector, Vector2 targetVector, float delta)
		{
			float num = MoonMath.Angle.AngleFromVector(angleVector);
			float target = MoonMath.Angle.AngleFromVector(targetVector);
			num = Mathf.MoveTowardsAngle(num, target, Time.deltaTime * delta);
			return MoonMath.Angle.VectorFromAngle(num) * angleVector.magnitude;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0006D758 File Offset: 0x0006B958
		public static bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
		{
			bool flag = MoonMath.Vector.sign(pt, v1, v2) < 0f;
			bool flag2 = MoonMath.Vector.sign(pt, v2, v3) < 0f;
			bool flag3 = MoonMath.Vector.sign(pt, v3, v1) < 0f;
			return flag == flag2 && flag2 == flag3;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0006D7A4 File Offset: 0x0006B9A4
		private static float sign(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0006D7F0 File Offset: 0x0006B9F0
		public static float Distance(Vector3 start, Vector3 target)
		{
			float num = start.x - target.x;
			float num2 = start.y - target.y;
			float num3 = start.z - target.z;
			return Mathf.Sqrt(num * num + num2 * num2 + num3 * num3);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0006D840 File Offset: 0x0006BA40
		public static float Distance(Vector3 start, Vector2 target)
		{
			float num = start.x - target.x;
			float num2 = start.y - target.y;
			float z = start.z;
			return Mathf.Sqrt(num * num + num2 * num2 + z * z);
		}
	}

	// Token: 0x02000552 RID: 1362
	public static class Movement
	{
		// Token: 0x0600239E RID: 9118 RVA: 0x0009C12C File Offset: 0x0009A32C
		public static float AccelerateSpeed(float speed, float acceleration, float maxSpeed, bool left)
		{
			if (left && speed < -maxSpeed)
			{
				return speed;
			}
			if (!left && speed > maxSpeed)
			{
				return speed;
			}
			return (!left) ? Mathf.Min(maxSpeed, speed + acceleration * Time.deltaTime) : Mathf.Max(-maxSpeed, speed - acceleration * Time.deltaTime);
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0009C184 File Offset: 0x0009A384
		public static float DecelerateSpeed(float speed, float deceleration)
		{
			return (speed <= 0f) ? Mathf.Min(0f, speed + deceleration * Time.deltaTime) : Mathf.Max(0f, speed - deceleration * Time.deltaTime);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0009C1C7 File Offset: 0x0009A3C7
		public static float ApplyGravity(float speed, float gravity, float maxSpeed)
		{
			return Mathf.Max(-maxSpeed, speed - gravity * Time.deltaTime);
		}
	}

	// Token: 0x020006DE RID: 1758
	public static class Line
	{
		// Token: 0x06002A0D RID: 10765 RVA: 0x000B4D4C File Offset: 0x000B2F4C
		public static Vector3 ClosestPointOnLineSegmentToPoint(Vector3 p1, Vector3 p2, Vector3 p)
		{
			Vector3 vector = p2 - p1;
			if (vector.sqrMagnitude < Mathf.Epsilon)
			{
				return (p1 + p2) / 2f;
			}
			float num = ((p.x - p1.x) * vector.x + (p.y - p1.y) * vector.y) / vector.sqrMagnitude;
			num = Mathf.Clamp01(num);
			return Vector3.Lerp(p1, p2, num);
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000B4DCB File Offset: 0x000B2FCB
		public static float DistancePointToLine(Vector3 p1, Vector3 p2, Vector3 p)
		{
			return Vector3.Distance(p, MoonMath.Line.ClosestPointOnLineSegmentToPoint(p1, p2, p));
		}
	}

	// Token: 0x020006DF RID: 1759
	public static class Rectangle
	{
		// Token: 0x06002A0F RID: 10767 RVA: 0x000B4DDC File Offset: 0x000B2FDC
		public static Rect Absolute(Rect rect)
		{
			return Rect.MinMaxRect(Mathf.Min(rect.xMin, rect.xMax), Mathf.Min(rect.yMin, rect.yMax), Mathf.Max(rect.xMin, rect.xMax), Mathf.Max(rect.yMin, rect.yMax));
		}
	}

	// Token: 0x020006E0 RID: 1760
	public static class Int
	{
		// Token: 0x06002A10 RID: 10768 RVA: 0x000B4E3C File Offset: 0x000B303C
		public static int GreatestCommonDenominator(int x, int y)
		{
			while (y != 0)
			{
				int num = x % y;
				x = y;
				y = num;
			}
			return x;
		}
	}
}
