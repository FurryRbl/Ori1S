using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000056 RID: 86
	[AddComponentMenu("Network/NetworkTransformChild")]
	public class NetworkTransformChild : NetworkBehaviour
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00016094 File Offset: 0x00014294
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0001609C File Offset: 0x0001429C
		public Transform target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				this.m_Target = value;
				this.OnValidate();
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000160AC File Offset: 0x000142AC
		public uint childIndex
		{
			get
			{
				return this.m_ChildIndex;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000160B4 File Offset: 0x000142B4
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x000160BC File Offset: 0x000142BC
		public float sendInterval
		{
			get
			{
				return this.m_SendInterval;
			}
			set
			{
				this.m_SendInterval = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000160C8 File Offset: 0x000142C8
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x000160D0 File Offset: 0x000142D0
		public NetworkTransform.AxisSyncMode syncRotationAxis
		{
			get
			{
				return this.m_SyncRotationAxis;
			}
			set
			{
				this.m_SyncRotationAxis = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x000160DC File Offset: 0x000142DC
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x000160E4 File Offset: 0x000142E4
		public NetworkTransform.CompressionSyncMode rotationSyncCompression
		{
			get
			{
				return this.m_RotationSyncCompression;
			}
			set
			{
				this.m_RotationSyncCompression = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x000160F0 File Offset: 0x000142F0
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x000160F8 File Offset: 0x000142F8
		public float movementThreshold
		{
			get
			{
				return this.m_MovementThreshold;
			}
			set
			{
				this.m_MovementThreshold = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00016104 File Offset: 0x00014304
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001610C File Offset: 0x0001430C
		public float interpolateRotation
		{
			get
			{
				return this.m_InterpolateRotation;
			}
			set
			{
				this.m_InterpolateRotation = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00016118 File Offset: 0x00014318
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00016120 File Offset: 0x00014320
		public float interpolateMovement
		{
			get
			{
				return this.m_InterpolateMovement;
			}
			set
			{
				this.m_InterpolateMovement = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001612C File Offset: 0x0001432C
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00016134 File Offset: 0x00014334
		public NetworkTransform.ClientMoveCallback3D clientMoveCallback3D
		{
			get
			{
				return this.m_ClientMoveCallback3D;
			}
			set
			{
				this.m_ClientMoveCallback3D = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00016140 File Offset: 0x00014340
		public float lastSyncTime
		{
			get
			{
				return this.m_LastClientSyncTime;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00016148 File Offset: 0x00014348
		public Vector3 targetSyncPosition
		{
			get
			{
				return this.m_TargetSyncPosition;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00016150 File Offset: 0x00014350
		public Quaternion targetSyncRotation3D
		{
			get
			{
				return this.m_TargetSyncRotation3D;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00016158 File Offset: 0x00014358
		private void OnValidate()
		{
			if (this.m_Target != null)
			{
				Transform parent = this.m_Target.parent;
				if (parent == null)
				{
					if (LogFilter.logError)
					{
						Debug.LogError("NetworkTransformChild target cannot be the root transform.");
					}
					this.m_Target = null;
					return;
				}
				while (parent.parent != null)
				{
					parent = parent.parent;
				}
				this.m_Root = parent.gameObject.GetComponent<NetworkTransform>();
				if (this.m_Root == null)
				{
					if (LogFilter.logError)
					{
						Debug.LogError("NetworkTransformChild root must have NetworkTransform");
					}
					this.m_Target = null;
					return;
				}
			}
			this.m_ChildIndex = uint.MaxValue;
			NetworkTransformChild[] components = this.m_Root.GetComponents<NetworkTransformChild>();
			uint num = 0U;
			while ((ulong)num < (ulong)((long)components.Length))
			{
				if (components[(int)((UIntPtr)num)] == this)
				{
					this.m_ChildIndex = num;
					break;
				}
				num += 1U;
			}
			if (this.m_ChildIndex == 4294967295U)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("NetworkTransformChild component must be a child in the same hierarchy");
				}
				this.m_Target = null;
			}
			if (this.m_SendInterval < 0f)
			{
				this.m_SendInterval = 0f;
			}
			if (this.m_SyncRotationAxis < NetworkTransform.AxisSyncMode.None || this.m_SyncRotationAxis > NetworkTransform.AxisSyncMode.AxisXYZ)
			{
				this.m_SyncRotationAxis = NetworkTransform.AxisSyncMode.None;
			}
			if (this.movementThreshold < 0f)
			{
				this.movementThreshold = 0f;
			}
			if (this.interpolateRotation < 0f)
			{
				this.interpolateRotation = 0.01f;
			}
			if (this.interpolateRotation > 1f)
			{
				this.interpolateRotation = 1f;
			}
			if (this.interpolateMovement < 0f)
			{
				this.interpolateMovement = 0.01f;
			}
			if (this.interpolateMovement > 1f)
			{
				this.interpolateMovement = 1f;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00016334 File Offset: 0x00014534
		private void Awake()
		{
			this.m_PrevPosition = this.m_Target.localPosition;
			this.m_PrevRotation = this.m_Target.localRotation;
			if (base.localPlayerAuthority)
			{
				this.m_LocalTransformWriter = new NetworkWriter();
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001637C File Offset: 0x0001457C
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			if (!initialState)
			{
				if (base.syncVarDirtyBits == 0U)
				{
					writer.WritePackedUInt32(0U);
					return false;
				}
				writer.WritePackedUInt32(1U);
			}
			this.SerializeModeTransform(writer);
			return true;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000163B8 File Offset: 0x000145B8
		private void SerializeModeTransform(NetworkWriter writer)
		{
			writer.Write(this.m_Target.localPosition);
			if (this.m_SyncRotationAxis != NetworkTransform.AxisSyncMode.None)
			{
				NetworkTransform.SerializeRotation3D(writer, this.m_Target.localRotation, this.syncRotationAxis, this.rotationSyncCompression);
			}
			this.m_PrevPosition = this.m_Target.localPosition;
			this.m_PrevRotation = this.m_Target.localRotation;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00016420 File Offset: 0x00014620
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (base.isServer && NetworkServer.localClientActive)
			{
				return;
			}
			if (!initialState && reader.ReadPackedUInt32() == 0U)
			{
				return;
			}
			this.UnserializeModeTransform(reader, initialState);
			this.m_LastClientSyncTime = Time.time;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00016468 File Offset: 0x00014668
		private void UnserializeModeTransform(NetworkReader reader, bool initialState)
		{
			if (base.hasAuthority)
			{
				reader.ReadVector3();
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
				return;
			}
			if (base.isServer && this.m_ClientMoveCallback3D != null)
			{
				Vector3 targetSyncPosition = reader.ReadVector3();
				Vector3 zero = Vector3.zero;
				Quaternion targetSyncRotation3D = Quaternion.identity;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					targetSyncRotation3D = NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
				if (!this.m_ClientMoveCallback3D(ref targetSyncPosition, ref zero, ref targetSyncRotation3D))
				{
					return;
				}
				this.m_TargetSyncPosition = targetSyncPosition;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_TargetSyncRotation3D = targetSyncRotation3D;
				}
			}
			else
			{
				this.m_TargetSyncPosition = reader.ReadVector3();
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_TargetSyncRotation3D = NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00016558 File Offset: 0x00014758
		private void FixedUpdate()
		{
			if (base.isServer)
			{
				this.FixedUpdateServer();
			}
			if (base.isClient)
			{
				this.FixedUpdateClient();
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00016588 File Offset: 0x00014788
		private void FixedUpdateServer()
		{
			if (base.syncVarDirtyBits != 0U)
			{
				return;
			}
			if (!NetworkServer.active)
			{
				return;
			}
			if (!base.isServer)
			{
				return;
			}
			if (this.GetNetworkSendInterval() == 0f)
			{
				return;
			}
			float num = (this.m_Target.localPosition - this.m_PrevPosition).sqrMagnitude;
			if (num < this.movementThreshold)
			{
				num = Quaternion.Angle(this.m_PrevRotation, this.m_Target.localRotation);
				if (num < this.movementThreshold)
				{
					return;
				}
			}
			base.SetDirtyBit(1U);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00016620 File Offset: 0x00014820
		private void FixedUpdateClient()
		{
			if (this.m_LastClientSyncTime == 0f)
			{
				return;
			}
			if (!NetworkServer.active && !NetworkClient.active)
			{
				return;
			}
			if (!base.isServer && !base.isClient)
			{
				return;
			}
			if (this.GetNetworkSendInterval() == 0f)
			{
				return;
			}
			if (base.hasAuthority)
			{
				return;
			}
			if (this.m_LastClientSyncTime != 0f)
			{
				this.m_Target.localPosition = Vector3.Lerp(this.m_Target.localPosition, this.m_TargetSyncPosition, this.m_InterpolateMovement);
				this.m_Target.localRotation = Quaternion.Slerp(this.m_Target.localRotation, this.m_TargetSyncRotation3D, this.m_InterpolateRotation);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000166E8 File Offset: 0x000148E8
		private void Update()
		{
			if (!base.hasAuthority)
			{
				return;
			}
			if (!base.localPlayerAuthority)
			{
				return;
			}
			if (NetworkServer.active)
			{
				return;
			}
			if (Time.time - this.m_LastClientSendTime > this.GetNetworkSendInterval())
			{
				this.SendTransform();
				this.m_LastClientSendTime = Time.time;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00016740 File Offset: 0x00014940
		private bool HasMoved()
		{
			float num = (this.m_Target.localPosition - this.m_PrevPosition).sqrMagnitude;
			if (num > 1E-05f)
			{
				return true;
			}
			num = Quaternion.Angle(this.m_Target.localRotation, this.m_PrevRotation);
			return num > 1E-05f;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000167A4 File Offset: 0x000149A4
		[Client]
		private void SendTransform()
		{
			if (!this.HasMoved() || ClientScene.readyConnection == null)
			{
				return;
			}
			this.m_LocalTransformWriter.StartMessage(16);
			this.m_LocalTransformWriter.Write(base.netId);
			this.m_LocalTransformWriter.WritePackedUInt32(this.m_ChildIndex);
			this.SerializeModeTransform(this.m_LocalTransformWriter);
			this.m_PrevPosition = this.m_Target.localPosition;
			this.m_PrevRotation = this.m_Target.localRotation;
			this.m_LocalTransformWriter.FinishMessage();
			ClientScene.readyConnection.SendWriter(this.m_LocalTransformWriter, this.GetNetworkChannel());
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00016848 File Offset: 0x00014A48
		internal static void HandleChildTransform(NetworkMessage netMsg)
		{
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			uint num = netMsg.reader.ReadPackedUInt32();
			GameObject gameObject = NetworkServer.FindLocalObject(networkInstanceId);
			if (gameObject == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleChildTransform no gameObject");
				}
				return;
			}
			NetworkTransformChild[] components = gameObject.GetComponents<NetworkTransformChild>();
			if (components == null || components.Length == 0)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleChildTransform no children");
				}
				return;
			}
			if ((ulong)num >= (ulong)((long)components.Length))
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleChildTransform childIndex invalid");
				}
				return;
			}
			NetworkTransformChild networkTransformChild = components[(int)((UIntPtr)num)];
			if (networkTransformChild == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleChildTransform null target");
				}
				return;
			}
			if (!networkTransformChild.localPlayerAuthority)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleChildTransform no localPlayerAuthority");
				}
				return;
			}
			if (!netMsg.conn.clientOwnedObjects.Contains(networkInstanceId))
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("NetworkTransformChild netId:" + networkInstanceId + " is not for a valid player");
				}
				return;
			}
			networkTransformChild.UnserializeModeTransform(netMsg.reader, false);
			networkTransformChild.m_LastClientSyncTime = Time.time;
			if (!networkTransformChild.isClient)
			{
				networkTransformChild.m_Target.localPosition = networkTransformChild.m_TargetSyncPosition;
				networkTransformChild.m_Target.localRotation = networkTransformChild.m_TargetSyncRotation3D;
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000169B0 File Offset: 0x00014BB0
		public override int GetNetworkChannel()
		{
			return 1;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000169B4 File Offset: 0x00014BB4
		public override float GetNetworkSendInterval()
		{
			return this.m_SendInterval;
		}

		// Token: 0x040001AE RID: 430
		private const float k_LocalMovementThreshold = 1E-05f;

		// Token: 0x040001AF RID: 431
		private const float k_LocalRotationThreshold = 1E-05f;

		// Token: 0x040001B0 RID: 432
		[SerializeField]
		private Transform m_Target;

		// Token: 0x040001B1 RID: 433
		[SerializeField]
		private uint m_ChildIndex;

		// Token: 0x040001B2 RID: 434
		private NetworkTransform m_Root;

		// Token: 0x040001B3 RID: 435
		[SerializeField]
		private float m_SendInterval = 0.1f;

		// Token: 0x040001B4 RID: 436
		[SerializeField]
		private NetworkTransform.AxisSyncMode m_SyncRotationAxis = NetworkTransform.AxisSyncMode.AxisXYZ;

		// Token: 0x040001B5 RID: 437
		[SerializeField]
		private NetworkTransform.CompressionSyncMode m_RotationSyncCompression;

		// Token: 0x040001B6 RID: 438
		[SerializeField]
		private float m_MovementThreshold = 0.001f;

		// Token: 0x040001B7 RID: 439
		[SerializeField]
		private float m_InterpolateRotation = 0.5f;

		// Token: 0x040001B8 RID: 440
		[SerializeField]
		private float m_InterpolateMovement = 0.5f;

		// Token: 0x040001B9 RID: 441
		[SerializeField]
		private NetworkTransform.ClientMoveCallback3D m_ClientMoveCallback3D;

		// Token: 0x040001BA RID: 442
		private Vector3 m_TargetSyncPosition;

		// Token: 0x040001BB RID: 443
		private Quaternion m_TargetSyncRotation3D;

		// Token: 0x040001BC RID: 444
		private float m_LastClientSyncTime;

		// Token: 0x040001BD RID: 445
		private float m_LastClientSendTime;

		// Token: 0x040001BE RID: 446
		private Vector3 m_PrevPosition;

		// Token: 0x040001BF RID: 447
		private Quaternion m_PrevRotation;

		// Token: 0x040001C0 RID: 448
		private NetworkWriter m_LocalTransformWriter;
	}
}
