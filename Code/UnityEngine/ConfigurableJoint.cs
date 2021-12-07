using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000134 RID: 308
	public sealed class ConfigurableJoint : Joint
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00015EF0 File Offset: 0x000140F0
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x00015F08 File Offset: 0x00014108
		public Vector3 secondaryAxis
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_secondaryAxis(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_secondaryAxis(ref value);
			}
		}

		// Token: 0x0600139B RID: 5019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_secondaryAxis(out Vector3 value);

		// Token: 0x0600139C RID: 5020
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_secondaryAxis(ref Vector3 value);

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600139D RID: 5021
		// (set) Token: 0x0600139E RID: 5022
		public extern ConfigurableJointMotion xMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x0600139F RID: 5023
		// (set) Token: 0x060013A0 RID: 5024
		public extern ConfigurableJointMotion yMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060013A1 RID: 5025
		// (set) Token: 0x060013A2 RID: 5026
		public extern ConfigurableJointMotion zMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060013A3 RID: 5027
		// (set) Token: 0x060013A4 RID: 5028
		public extern ConfigurableJointMotion angularXMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060013A5 RID: 5029
		// (set) Token: 0x060013A6 RID: 5030
		public extern ConfigurableJointMotion angularYMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060013A7 RID: 5031
		// (set) Token: 0x060013A8 RID: 5032
		public extern ConfigurableJointMotion angularZMotion { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00015F14 File Offset: 0x00014114
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x00015F2C File Offset: 0x0001412C
		public SoftJointLimitSpring linearLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.INTERNAL_get_linearLimitSpring(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_linearLimitSpring(ref value);
			}
		}

		// Token: 0x060013AB RID: 5035
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_linearLimitSpring(out SoftJointLimitSpring value);

		// Token: 0x060013AC RID: 5036
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_linearLimitSpring(ref SoftJointLimitSpring value);

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00015F38 File Offset: 0x00014138
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00015F50 File Offset: 0x00014150
		public SoftJointLimitSpring angularXLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.INTERNAL_get_angularXLimitSpring(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularXLimitSpring(ref value);
			}
		}

		// Token: 0x060013AF RID: 5039
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularXLimitSpring(out SoftJointLimitSpring value);

		// Token: 0x060013B0 RID: 5040
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularXLimitSpring(ref SoftJointLimitSpring value);

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00015F5C File Offset: 0x0001415C
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00015F74 File Offset: 0x00014174
		public SoftJointLimitSpring angularYZLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.INTERNAL_get_angularYZLimitSpring(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularYZLimitSpring(ref value);
			}
		}

		// Token: 0x060013B3 RID: 5043
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularYZLimitSpring(out SoftJointLimitSpring value);

		// Token: 0x060013B4 RID: 5044
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularYZLimitSpring(ref SoftJointLimitSpring value);

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00015F80 File Offset: 0x00014180
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00015F98 File Offset: 0x00014198
		public SoftJointLimit linearLimit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_linearLimit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_linearLimit(ref value);
			}
		}

		// Token: 0x060013B7 RID: 5047
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_linearLimit(out SoftJointLimit value);

		// Token: 0x060013B8 RID: 5048
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_linearLimit(ref SoftJointLimit value);

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00015FA4 File Offset: 0x000141A4
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00015FBC File Offset: 0x000141BC
		public SoftJointLimit lowAngularXLimit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_lowAngularXLimit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_lowAngularXLimit(ref value);
			}
		}

		// Token: 0x060013BB RID: 5051
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lowAngularXLimit(out SoftJointLimit value);

		// Token: 0x060013BC RID: 5052
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_lowAngularXLimit(ref SoftJointLimit value);

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00015FC8 File Offset: 0x000141C8
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x00015FE0 File Offset: 0x000141E0
		public SoftJointLimit highAngularXLimit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_highAngularXLimit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_highAngularXLimit(ref value);
			}
		}

		// Token: 0x060013BF RID: 5055
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_highAngularXLimit(out SoftJointLimit value);

		// Token: 0x060013C0 RID: 5056
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_highAngularXLimit(ref SoftJointLimit value);

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00015FEC File Offset: 0x000141EC
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x00016004 File Offset: 0x00014204
		public SoftJointLimit angularYLimit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_angularYLimit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularYLimit(ref value);
			}
		}

		// Token: 0x060013C3 RID: 5059
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularYLimit(out SoftJointLimit value);

		// Token: 0x060013C4 RID: 5060
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularYLimit(ref SoftJointLimit value);

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00016010 File Offset: 0x00014210
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x00016028 File Offset: 0x00014228
		public SoftJointLimit angularZLimit
		{
			get
			{
				SoftJointLimit result;
				this.INTERNAL_get_angularZLimit(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularZLimit(ref value);
			}
		}

		// Token: 0x060013C7 RID: 5063
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularZLimit(out SoftJointLimit value);

		// Token: 0x060013C8 RID: 5064
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularZLimit(ref SoftJointLimit value);

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00016034 File Offset: 0x00014234
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0001604C File Offset: 0x0001424C
		public Vector3 targetPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_targetPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_targetPosition(ref value);
			}
		}

		// Token: 0x060013CB RID: 5067
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetPosition(out Vector3 value);

		// Token: 0x060013CC RID: 5068
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetPosition(ref Vector3 value);

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00016058 File Offset: 0x00014258
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x00016070 File Offset: 0x00014270
		public Vector3 targetVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_targetVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_targetVelocity(ref value);
			}
		}

		// Token: 0x060013CF RID: 5071
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetVelocity(out Vector3 value);

		// Token: 0x060013D0 RID: 5072
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetVelocity(ref Vector3 value);

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0001607C File Offset: 0x0001427C
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x00016094 File Offset: 0x00014294
		public JointDrive xDrive
		{
			get
			{
				JointDrive result;
				this.INTERNAL_get_xDrive(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_xDrive(ref value);
			}
		}

		// Token: 0x060013D3 RID: 5075
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_xDrive(out JointDrive value);

		// Token: 0x060013D4 RID: 5076
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_xDrive(ref JointDrive value);

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000160A0 File Offset: 0x000142A0
		// (set) Token: 0x060013D6 RID: 5078 RVA: 0x000160B8 File Offset: 0x000142B8
		public JointDrive yDrive
		{
			get
			{
				JointDrive result;
				this.INTERNAL_get_yDrive(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_yDrive(ref value);
			}
		}

		// Token: 0x060013D7 RID: 5079
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_yDrive(out JointDrive value);

		// Token: 0x060013D8 RID: 5080
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_yDrive(ref JointDrive value);

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x000160C4 File Offset: 0x000142C4
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x000160DC File Offset: 0x000142DC
		public JointDrive zDrive
		{
			get
			{
				JointDrive result;
				this.INTERNAL_get_zDrive(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_zDrive(ref value);
			}
		}

		// Token: 0x060013DB RID: 5083
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_zDrive(out JointDrive value);

		// Token: 0x060013DC RID: 5084
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_zDrive(ref JointDrive value);

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x000160E8 File Offset: 0x000142E8
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x00016100 File Offset: 0x00014300
		public Quaternion targetRotation
		{
			get
			{
				Quaternion result;
				this.INTERNAL_get_targetRotation(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_targetRotation(ref value);
			}
		}

		// Token: 0x060013DF RID: 5087
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetRotation(out Quaternion value);

		// Token: 0x060013E0 RID: 5088
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetRotation(ref Quaternion value);

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0001610C File Offset: 0x0001430C
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00016124 File Offset: 0x00014324
		public Vector3 targetAngularVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_targetAngularVelocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_targetAngularVelocity(ref value);
			}
		}

		// Token: 0x060013E3 RID: 5091
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_targetAngularVelocity(out Vector3 value);

		// Token: 0x060013E4 RID: 5092
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_targetAngularVelocity(ref Vector3 value);

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060013E5 RID: 5093
		// (set) Token: 0x060013E6 RID: 5094
		public extern RotationDriveMode rotationDriveMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00016130 File Offset: 0x00014330
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00016148 File Offset: 0x00014348
		public JointDrive angularXDrive
		{
			get
			{
				JointDrive result;
				this.INTERNAL_get_angularXDrive(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularXDrive(ref value);
			}
		}

		// Token: 0x060013E9 RID: 5097
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularXDrive(out JointDrive value);

		// Token: 0x060013EA RID: 5098
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularXDrive(ref JointDrive value);

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00016154 File Offset: 0x00014354
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x0001616C File Offset: 0x0001436C
		public JointDrive angularYZDrive
		{
			get
			{
				JointDrive result;
				this.INTERNAL_get_angularYZDrive(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_angularYZDrive(ref value);
			}
		}

		// Token: 0x060013ED RID: 5101
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularYZDrive(out JointDrive value);

		// Token: 0x060013EE RID: 5102
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularYZDrive(ref JointDrive value);

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00016178 File Offset: 0x00014378
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x00016190 File Offset: 0x00014390
		public JointDrive slerpDrive
		{
			get
			{
				JointDrive result;
				this.INTERNAL_get_slerpDrive(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_slerpDrive(ref value);
			}
		}

		// Token: 0x060013F1 RID: 5105
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_slerpDrive(out JointDrive value);

		// Token: 0x060013F2 RID: 5106
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_slerpDrive(ref JointDrive value);

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060013F3 RID: 5107
		// (set) Token: 0x060013F4 RID: 5108
		public extern JointProjectionMode projectionMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060013F5 RID: 5109
		// (set) Token: 0x060013F6 RID: 5110
		public extern float projectionDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060013F7 RID: 5111
		// (set) Token: 0x060013F8 RID: 5112
		public extern float projectionAngle { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060013F9 RID: 5113
		// (set) Token: 0x060013FA RID: 5114
		public extern bool configuredInWorldSpace { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060013FB RID: 5115
		// (set) Token: 0x060013FC RID: 5116
		public extern bool swapBodies { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
