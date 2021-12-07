using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public abstract class PlatformMovement : SaveSerialize, IPlatformMovement, ISuspendable, IGoThroughPlatformTester
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060000A3 RID: 163 RVA: 0x00004963 File Offset: 0x00002B63
	// (remove) Token: 0x060000A4 RID: 164 RVA: 0x0000497C File Offset: 0x00002B7C
	public event Action<Vector3, Collider> OnCollisionGroundEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060000A5 RID: 165 RVA: 0x00004995 File Offset: 0x00002B95
	// (remove) Token: 0x060000A6 RID: 166 RVA: 0x000049AE File Offset: 0x00002BAE
	public event Action<Vector3, Collider> OnCollisionCeilingEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060000A7 RID: 167 RVA: 0x000049C7 File Offset: 0x00002BC7
	// (remove) Token: 0x060000A8 RID: 168 RVA: 0x000049E0 File Offset: 0x00002BE0
	public event Action<Vector3, Collider> OnCollisionWallLeftEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060000A9 RID: 169 RVA: 0x000049F9 File Offset: 0x00002BF9
	// (remove) Token: 0x060000AA RID: 170 RVA: 0x00004A12 File Offset: 0x00002C12
	public event Action<Vector3, Collider> OnCollisionWallRightEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x060000AB RID: 171 RVA: 0x00004A2B File Offset: 0x00002C2B
	// (remove) Token: 0x060000AC RID: 172 RVA: 0x00004A44 File Offset: 0x00002C44
	public event Action<Vector3, Collider> OnLandOnGroundEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x060000AD RID: 173 RVA: 0x00004A5D File Offset: 0x00002C5D
	// (remove) Token: 0x060000AE RID: 174 RVA: 0x00004A76 File Offset: 0x00002C76
	public event Action<Vector3, Collider> OnLandOnWallLeftEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x060000AF RID: 175 RVA: 0x00004A8F File Offset: 0x00002C8F
	// (remove) Token: 0x060000B0 RID: 176 RVA: 0x00004AA8 File Offset: 0x00002CA8
	public event Action<Vector3, Collider> OnLandOnWallRightEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x060000B1 RID: 177 RVA: 0x00004AC1 File Offset: 0x00002CC1
	// (remove) Token: 0x060000B2 RID: 178 RVA: 0x00004ADA File Offset: 0x00002CDA
	public event Action<Vector3, Collider> OnLandOnCeilingEvent = delegate(Vector3 A_0, Collider A_1)
	{
	};

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004AF3 File Offset: 0x00002CF3
	public Vector3 GroundBinormal
	{
		get
		{
			return Vector3.Cross(this.GroundNormal, Vector3.forward);
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004B05 File Offset: 0x00002D05
	public Vector3 CeilingBinormal
	{
		get
		{
			return Vector3.Cross(this.CeilingNormal, Vector3.back);
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004B17 File Offset: 0x00002D17
	public Vector3 WallLeftBinormal
	{
		get
		{
			return Vector3.Cross(this.WallLeftNormal, Vector3.back);
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004B29 File Offset: 0x00002D29
	public Vector3 WallRightBinormal
	{
		get
		{
			return Vector3.Cross(this.WallRightNormal, Vector3.forward);
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004B3B File Offset: 0x00002D3B
	public bool IsOnCeiling
	{
		get
		{
			return this.LocalSpeedY >= 0f && this.Ceiling.IsOn;
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004B5A File Offset: 0x00002D5A
	public bool IsOnGroundOrCeiling
	{
		get
		{
			return this.IsOnGround || this.IsOnCeiling;
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004B70 File Offset: 0x00002D70
	public bool IsInAir
	{
		get
		{
			return !this.IsOnGround;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000BA RID: 186 RVA: 0x00004B7B File Offset: 0x00002D7B
	public bool WasOnWall
	{
		get
		{
			return this.WallLeft.WasOn || this.WallRight.WasOn;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060000BB RID: 187 RVA: 0x00004B9B File Offset: 0x00002D9B
	public bool Falling
	{
		get
		{
			return !this.IsOnGround && !this.Jumping;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060000BC RID: 188 RVA: 0x00004BB4 File Offset: 0x00002DB4
	// (set) Token: 0x060000BD RID: 189 RVA: 0x00004BC1 File Offset: 0x00002DC1
	public float LocalSpeedX
	{
		get
		{
			return this.m_localSpeed.x;
		}
		set
		{
			this.m_localSpeed.x = value;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060000BE RID: 190 RVA: 0x00004BCF File Offset: 0x00002DCF
	// (set) Token: 0x060000BF RID: 191 RVA: 0x00004BDC File Offset: 0x00002DDC
	public float LocalSpeedY
	{
		get
		{
			return this.m_localSpeed.y;
		}
		set
		{
			this.m_localSpeed.y = value;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004BEA File Offset: 0x00002DEA
	public Vector3 GravityDirection
	{
		get
		{
			return MoonMath.Angle.Rotate(Vector3.down, this.GravityAngle);
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004C06 File Offset: 0x00002E06
	public Vector3 GravityBinormal
	{
		get
		{
			return Vector3.Cross(this.GravityDirection, Vector3.back);
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004C18 File Offset: 0x00002E18
	public float CeilingAngle
	{
		get
		{
			return 57.29578f * Mathf.Atan2(this.CeilingNormal.x, -this.CeilingNormal.y);
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004C47 File Offset: 0x00002E47
	public float WallRightAngle
	{
		get
		{
			return 57.29578f * Mathf.Atan2(-this.WallRightNormal.y, -this.WallRightNormal.x);
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004C6C File Offset: 0x00002E6C
	public float WallLeftAngle
	{
		get
		{
			return 57.29578f * -Mathf.Atan2(-this.WallLeftNormal.y, this.WallLeftNormal.x);
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004C91 File Offset: 0x00002E91
	// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004C99 File Offset: 0x00002E99
	public float CapsuleAngle
	{
		get
		{
			return this.m_gravityAngle;
		}
		set
		{
			this.m_gravityAngle = value;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004CA4 File Offset: 0x00002EA4
	// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004CC0 File Offset: 0x00002EC0
	public float PositionX
	{
		get
		{
			return this.Position.x;
		}
		set
		{
			Vector3 position = this.Position;
			position.x = value;
			this.Position = position;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004CE4 File Offset: 0x00002EE4
	// (set) Token: 0x060000CA RID: 202 RVA: 0x00004D00 File Offset: 0x00002F00
	public float PositionY
	{
		get
		{
			return this.Position.y;
		}
		set
		{
			Vector3 position = this.Position;
			position.y = value;
			this.Position = position;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x060000CB RID: 203 RVA: 0x00004D23 File Offset: 0x00002F23
	// (set) Token: 0x060000CC RID: 204 RVA: 0x00004D38 File Offset: 0x00002F38
	public Vector2 Position2D
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = new Vector3(value.x, value.y, base.transform.position.z);
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x060000CD RID: 205 RVA: 0x00004D78 File Offset: 0x00002F78
	public Vector3 LocalOffsetToBottomSphereOfCapsuleCollider
	{
		get
		{
			Vector3 localScale = base.transform.localScale;
			return Vector3.Scale(localScale, Vector3.down * (Mathf.Max(0f, this.m_capsuleCollider.height * 0.5f - this.m_capsuleCollider.radius) - this.m_capsuleCollider.center.y));
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x060000CE RID: 206 RVA: 0x00004DDC File Offset: 0x00002FDC
	public Vector3 LocalOffsetToTopSphereOfCapsuleCollider
	{
		get
		{
			Vector3 localScale = base.transform.localScale;
			return Vector3.Scale(localScale, Vector3.up * (Mathf.Max(0f, this.m_capsuleCollider.height * 0.5f - this.m_capsuleCollider.radius) + this.m_capsuleCollider.center.y));
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x060000CF RID: 207 RVA: 0x00004E40 File Offset: 0x00003040
	public Vector3 WorldOffsetToBottomSphereOfCapsuleCollider
	{
		get
		{
			return this.LocalToWorld(this.LocalOffsetToBottomSphereOfCapsuleCollider);
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004E58 File Offset: 0x00003058
	public Vector3 WorldOffsetToTopSphereOfCapsuleCollider
	{
		get
		{
			return this.LocalToWorld(this.LocalOffsetToTopSphereOfCapsuleCollider);
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004E70 File Offset: 0x00003070
	// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004E83 File Offset: 0x00003083
	public Vector3 HeadPosition
	{
		get
		{
			return this.Position + this.WorldOffsetToTopSphereOfCapsuleCollider;
		}
		set
		{
			this.Position = value - this.WorldOffsetToTopSphereOfCapsuleCollider;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004E97 File Offset: 0x00003097
	public CapsuleCollider CapsuleCollider
	{
		get
		{
			return this.m_capsuleCollider;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004E9F File Offset: 0x0000309F
	public MovingPlatformsController MovingPlatforms
	{
		get
		{
			return this.m_movingPlatform;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004EA7 File Offset: 0x000030A7
	public bool HeadAndFeetAgainstWall
	{
		get
		{
			return this.HeadAgainstWall && this.FeetAgainstWall;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004EBD File Offset: 0x000030BD
	public bool HeadOrFeetAgainstWall
	{
		get
		{
			return this.HeadAgainstWall || this.FeetAgainstWall;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004ED3 File Offset: 0x000030D3
	public bool IsOnGround
	{
		get
		{
			return this.LocalSpeedY <= 0.0001f && this.Ground.IsOn;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004EF4 File Offset: 0x000030F4
	public bool HasWallLeft
	{
		get
		{
			return this.LocalSpeedX + this.AdditionalXSpeed <= 0f && this.WallLeft.IsOn;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004F28 File Offset: 0x00003128
	public bool HasWallRight
	{
		get
		{
			return this.LocalSpeedX + this.AdditionalXSpeed >= 0f && this.WallRight.IsOn;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060000DA RID: 218 RVA: 0x00004F59 File Offset: 0x00003159
	public bool IsOnWall
	{
		get
		{
			return this.HasWallLeft || this.HasWallRight;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060000DB RID: 219 RVA: 0x00004F6F File Offset: 0x0000316F
	public bool MovingHorizontally
	{
		get
		{
			return Mathf.Abs(this.m_localSpeed.x) > 0f;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060000DC RID: 220 RVA: 0x00004F88 File Offset: 0x00003188
	public bool Jumping
	{
		get
		{
			return this.LocalSpeed.y > 0f;
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060000DD RID: 221 RVA: 0x00004FAA File Offset: 0x000031AA
	// (set) Token: 0x060000DE RID: 222 RVA: 0x00004FB7 File Offset: 0x000031B7
	public Vector2 LocalSpeed
	{
		get
		{
			return this.m_localSpeed;
		}
		set
		{
			this.m_localSpeed = value;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060000DF RID: 223 RVA: 0x00004FC5 File Offset: 0x000031C5
	// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004FD3 File Offset: 0x000031D3
	public Vector2 WorldSpeed
	{
		get
		{
			return this.LocalToWorld(this.LocalSpeed);
		}
		set
		{
			this.LocalSpeed = this.WorldToLocal(value);
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004FE2 File Offset: 0x000031E2
	// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004FEA File Offset: 0x000031EA
	public float GravityAngle
	{
		get
		{
			return this.m_gravityAngle;
		}
		set
		{
			this.m_gravityAngle = value;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004FF4 File Offset: 0x000031F4
	public float GroundAngle
	{
		get
		{
			if (this.UseCenterRayForGroundAngle)
			{
				return 57.29578f * Mathf.Atan2(-this.GroundRayNormal.x, this.GroundRayNormal.y);
			}
			return 57.29578f * Mathf.Atan2(-this.GroundNormal.x, this.GroundNormal.y);
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005051 File Offset: 0x00003251
	// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000505E File Offset: 0x0000325E
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000506C File Offset: 0x0000326C
	// (set) Token: 0x060000E7 RID: 231 RVA: 0x0000507F File Offset: 0x0000327F
	public Vector3 FeetPosition
	{
		get
		{
			return this.Position + this.WorldOffsetToBottomSphereOfCapsuleCollider;
		}
		set
		{
			this.Position = value - this.WorldOffsetToBottomSphereOfCapsuleCollider;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060000E8 RID: 232
	// (set) Token: 0x060000E9 RID: 233
	public abstract bool IsSuspended { get; set; }

	// Token: 0x060000EA RID: 234 RVA: 0x00005093 File Offset: 0x00003293
	public static bool IsWallLeft(Vector3 normal, Collider collidedWith, float maxWallAngle)
	{
		return MoonMath.Normal.WithinDegrees(normal, Vector3.right, maxWallAngle);
	}

	// Token: 0x060000EB RID: 235 RVA: 0x000050AB File Offset: 0x000032AB
	public static bool IsWallRight(Vector3 normal, Collider collidedWith, float maxWallAngle)
	{
		return MoonMath.Normal.WithinDegrees(normal, Vector3.left, maxWallAngle);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x000050C3 File Offset: 0x000032C3
	public static bool IsGround(Vector3 normal, Collider collidedWith, float maxSlopeAngle)
	{
		return MoonMath.Normal.WithinDegrees(normal, Vector3.up, maxSlopeAngle);
	}

	// Token: 0x060000ED RID: 237 RVA: 0x000050DB File Offset: 0x000032DB
	public static bool IsCeiling(Vector3 normal, Collider collidedWith, float maxCeilingAngle)
	{
		return MoonMath.Normal.WithinDegrees(normal, Vector3.down, maxCeilingAngle);
	}

	// Token: 0x060000EE RID: 238 RVA: 0x000050F3 File Offset: 0x000032F3
	public Vector2 WorldToLocal(Vector2 world)
	{
		return MoonMath.Angle.Unrotate(world, this.GravityAngle);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00005101 File Offset: 0x00003301
	public Vector2 LocalToWorld(Vector2 local)
	{
		return MoonMath.Angle.Rotate(local, this.GravityAngle);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000510F File Offset: 0x0000330F
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this.m_capsuleCollider = base.GetComponent<CapsuleCollider>();
		this.m_movingPlatform = new MovingPlatformsController(this);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00005135 File Offset: 0x00003335
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00005144 File Offset: 0x00003344
	public void OnCollisionGround(Vector2 localNormal, Collider collidedWith)
	{
		this.Ground.FutureOn = true;
		try
		{
			this.OnCollisionGroundEvent(localNormal, collidedWith);
			if (!this.Ground.IsOn)
			{
				this.OnLandOnGroundEvent(localNormal, collidedWith);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000051AC File Offset: 0x000033AC
	public void OnCollisionWallLeft(Vector2 localNormal, Collider collidedWith)
	{
		this.WallLeft.FutureOn = true;
		try
		{
			this.OnCollisionWallLeftEvent(localNormal, collidedWith);
			if (!this.WallLeft.IsOn)
			{
				this.OnLandOnWallLeftEvent(localNormal, collidedWith);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00005214 File Offset: 0x00003414
	public void OnCollisionWallRight(Vector2 localNormal, Collider collidedWith)
	{
		this.WallRight.FutureOn = true;
		try
		{
			this.OnCollisionWallRightEvent(localNormal, collidedWith);
			if (!this.WallRight.IsOn)
			{
				this.OnLandOnWallRightEvent(localNormal, collidedWith);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x0000527C File Offset: 0x0000347C
	public void OnCollisionCeiling(Vector2 localNormal, Collider collidedWith)
	{
		this.Ceiling.FutureOn = true;
		try
		{
			this.OnCollisionCeilingEvent(localNormal, collidedWith);
			if (!this.Ceiling.IsOn)
			{
				this.OnLandOnCeilingEvent(localNormal, collidedWith);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x000052E4 File Offset: 0x000034E4
	public void UpdateRays()
	{
		float maxDistance = this.m_capsuleCollider.radius + 0.5f;
		if (this.Ground.IsOnOrFutureOn)
		{
			Ray ray = new Ray(this.FeetPosition, this.LocalToWorld(Vector3.down));
			RaycastHit raycastHit;
			this.GroundRayHit = Physics.Raycast(ray, out raycastHit, maxDistance);
			if (this.GroundRayHit)
			{
				this.GroundRayNormal = raycastHit.normal;
			}
		}
		else
		{
			this.GroundRayHit = false;
		}
		if (this.Ceiling.IsOnOrFutureOn)
		{
			Ray ray2 = new Ray(this.HeadPosition, this.LocalToWorld(Vector3.up));
			RaycastHit raycastHit;
			this.CeilingRayHit = Physics.Raycast(ray2, out raycastHit, maxDistance);
		}
		if (this.WallLeft.IsOnOrFutureOn)
		{
			Ray ray3 = new Ray(this.FeetPosition, this.LocalToWorld(Vector3.left));
			RaycastHit raycastHit;
			this.WallLeftRayHit = Physics.Raycast(ray3, out raycastHit, maxDistance);
			if (!this.WallLeftRayHit)
			{
				ray3.origin = this.HeadPosition;
				this.WallLeftRayHit = Physics.Raycast(ray3, out raycastHit, maxDistance);
			}
		}
		if (this.WallRight.IsOnOrFutureOn)
		{
			Ray ray4 = new Ray(this.FeetPosition, this.LocalToWorld(Vector3.right));
			RaycastHit raycastHit;
			this.WallRightRayHit = Physics.Raycast(ray4, out raycastHit, maxDistance);
			if (!this.WallRightRayHit)
			{
				ray4.origin = this.HeadPosition;
				this.WallRightRayHit = Physics.Raycast(ray4, out raycastHit, maxDistance);
			}
		}
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x0000547D File Offset: 0x0000367D
	public void PreFixedUpdate()
	{
		this.m_movingPlatform.UpdateMovingPlatform();
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000548A File Offset: 0x0000368A
	public void PostFixedUpdate()
	{
		this.Ground.Update();
		this.WallLeft.Update();
		this.WallRight.Update();
		this.Ceiling.Update();
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000054B8 File Offset: 0x000036B8
	public virtual void PlaceOnGround(float lift = 0.5f, float distance = 0f)
	{
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000054BC File Offset: 0x000036BC
	public override void Serialize(Archive ar)
	{
		this.Position = ar.Serialize(this.Position);
		base.transform.eulerAngles = ar.Serialize(base.transform.eulerAngles);
		ar.Serialize(ref this.AdditionalXSpeed);
		this.Ceiling.Serialize(ar);
		ar.Serialize(ref this.CeilingNormal);
		ar.Serialize(ref this.CeilingRayHit);
		ar.Serialize(ref this.FeetAgainstWall);
		this.Ground.Serialize(ar);
		ar.Serialize(ref this.GroundNormal);
		ar.Serialize(ref this.GroundRayHit);
		ar.Serialize(ref this.HeadAgainstWall);
		ar.Serialize(ref this.KeepOnSurfaceDirection);
		ar.Serialize(ref this.WallLeftNormal);
		ar.Serialize(ref this.WallLeftRayHit);
		this.WallLeft.Serialize(ar);
		ar.Serialize(ref this.WallRightNormal);
		ar.Serialize(ref this.WallRightRayHit);
		this.WallRight.Serialize(ar);
		ar.Serialize(ref this.m_ceilingCenterRayNormal);
		ar.Serialize(ref this.m_gravityAngle);
		ar.Serialize(ref this.m_localSpeed);
		if (ar.Reading)
		{
			this.MovingPlatforms.DetachFromAll();
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060000FB RID: 251 RVA: 0x000055F4 File Offset: 0x000037F4
	public Ray GoThroughPlatformTestingRayLeft
	{
		get
		{
			float d = (this.LocalSpeedX >= 0f) ? 0f : (this.LocalSpeedX * Time.deltaTime);
			return new Ray(this.FeetPosition + this.GravityDirection * (this.CapsuleCollider.radius - 0.2f) - this.RaySidewaysOffset + d * this.GravityBinormal, this.GravityDirection);
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060000FC RID: 252 RVA: 0x00005678 File Offset: 0x00003878
	public Ray GoThroughPlatformTestingRayRight
	{
		get
		{
			float d = (this.LocalSpeedX <= 0f) ? 0f : (this.LocalSpeedX * Time.deltaTime);
			return new Ray(this.FeetPosition + this.GravityDirection * (this.CapsuleCollider.radius - 0.2f) + this.RaySidewaysOffset + d * this.GravityBinormal, this.GravityDirection);
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060000FD RID: 253 RVA: 0x000056FA File Offset: 0x000038FA
	public Collider GoThroughPlatformTesterCollider
	{
		get
		{
			return this.CapsuleCollider;
		}
	}

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060000FE RID: 254 RVA: 0x00005702 File Offset: 0x00003902
	public float GoThroughPlatformTestingRayRadius
	{
		get
		{
			return 2f;
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x060000FF RID: 255 RVA: 0x0000570C File Offset: 0x0000390C
	private Vector3 RaySidewaysOffset
	{
		get
		{
			return this.GravityBinormal * (this.CapsuleCollider.radius - 0.1f);
		}
	}

	// Token: 0x040000AA RID: 170
	public float AdditionalXSpeed;

	// Token: 0x040000AB RID: 171
	public IsOnCollisionState Ceiling = new IsOnCollisionState();

	// Token: 0x040000AC RID: 172
	public Vector3 CeilingNormal;

	// Token: 0x040000AD RID: 173
	public bool CeilingRayHit;

	// Token: 0x040000AE RID: 174
	public bool FeetAgainstWall;

	// Token: 0x040000AF RID: 175
	public IsOnCollisionState Ground = new IsOnCollisionState();

	// Token: 0x040000B0 RID: 176
	public Vector3 GroundNormal;

	// Token: 0x040000B1 RID: 177
	public bool GroundRayHit;

	// Token: 0x040000B2 RID: 178
	public Vector3 GroundRayNormal;

	// Token: 0x040000B3 RID: 179
	public bool HeadAgainstWall;

	// Token: 0x040000B4 RID: 180
	public Vector3 KeepOnSurfaceDirection;

	// Token: 0x040000B5 RID: 181
	public bool KinematicMode;

	// Token: 0x040000B6 RID: 182
	public bool UseCenterRayForGroundAngle;

	// Token: 0x040000B7 RID: 183
	public IsOnCollisionState WallLeft = new IsOnCollisionState();

	// Token: 0x040000B8 RID: 184
	public Vector3 WallLeftNormal;

	// Token: 0x040000B9 RID: 185
	public bool WallLeftRayHit;

	// Token: 0x040000BA RID: 186
	public IsOnCollisionState WallRight = new IsOnCollisionState();

	// Token: 0x040000BB RID: 187
	public Vector3 WallRightNormal;

	// Token: 0x040000BC RID: 188
	public bool WallRightRayHit;

	// Token: 0x040000BD RID: 189
	private CapsuleCollider m_capsuleCollider;

	// Token: 0x040000BE RID: 190
	private Vector2 m_ceilingCenterRayNormal;

	// Token: 0x040000BF RID: 191
	private float m_gravityAngle;

	// Token: 0x040000C0 RID: 192
	private Vector3 m_localSpeed;

	// Token: 0x040000C1 RID: 193
	protected MovingPlatformsController m_movingPlatform;

	// Token: 0x040000C2 RID: 194
	public bool ForceKeepInAir;
}
