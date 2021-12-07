using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000057 RID: 87
	[DisallowMultipleComponent]
	[AddComponentMenu("Network/NetworkTransform")]
	public class NetworkTransform : NetworkBehaviour
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00016A14 File Offset: 0x00014C14
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00016A1C File Offset: 0x00014C1C
		public NetworkTransform.TransformSyncMode transformSyncMode
		{
			get
			{
				return this.m_TransformSyncMode;
			}
			set
			{
				this.m_TransformSyncMode = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00016A28 File Offset: 0x00014C28
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00016A30 File Offset: 0x00014C30
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00016A3C File Offset: 0x00014C3C
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00016A44 File Offset: 0x00014C44
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00016A50 File Offset: 0x00014C50
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00016A58 File Offset: 0x00014C58
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00016A64 File Offset: 0x00014C64
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00016A6C File Offset: 0x00014C6C
		public bool syncSpin
		{
			get
			{
				return this.m_SyncSpin;
			}
			set
			{
				this.m_SyncSpin = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00016A78 File Offset: 0x00014C78
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00016A80 File Offset: 0x00014C80
		public float movementTheshold
		{
			get
			{
				return this.m_MovementTheshold;
			}
			set
			{
				this.m_MovementTheshold = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00016A8C File Offset: 0x00014C8C
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00016A94 File Offset: 0x00014C94
		public float snapThreshold
		{
			get
			{
				return this.m_SnapThreshold;
			}
			set
			{
				this.m_SnapThreshold = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00016AA0 File Offset: 0x00014CA0
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00016AA8 File Offset: 0x00014CA8
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00016AB4 File Offset: 0x00014CB4
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00016ABC File Offset: 0x00014CBC
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00016AC8 File Offset: 0x00014CC8
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00016AD0 File Offset: 0x00014CD0
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

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00016ADC File Offset: 0x00014CDC
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00016AE4 File Offset: 0x00014CE4
		public NetworkTransform.ClientMoveCallback2D clientMoveCallback2D
		{
			get
			{
				return this.m_ClientMoveCallback2D;
			}
			set
			{
				this.m_ClientMoveCallback2D = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00016AF0 File Offset: 0x00014CF0
		public CharacterController characterContoller
		{
			get
			{
				return this.m_CharacterController;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00016AF8 File Offset: 0x00014CF8
		public Rigidbody rigidbody3D
		{
			get
			{
				return this.m_RigidBody3D;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00016B00 File Offset: 0x00014D00
		public Rigidbody2D rigidbody2D
		{
			get
			{
				return this.m_RigidBody2D;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00016B08 File Offset: 0x00014D08
		public float lastSyncTime
		{
			get
			{
				return this.m_LastClientSyncTime;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00016B10 File Offset: 0x00014D10
		public Vector3 targetSyncPosition
		{
			get
			{
				return this.m_TargetSyncPosition;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00016B18 File Offset: 0x00014D18
		public Vector3 targetSyncVelocity
		{
			get
			{
				return this.m_TargetSyncVelocity;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00016B20 File Offset: 0x00014D20
		public Quaternion targetSyncRotation3D
		{
			get
			{
				return this.m_TargetSyncRotation3D;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00016B28 File Offset: 0x00014D28
		public float targetSyncRotation2D
		{
			get
			{
				return this.m_TargetSyncRotation2D;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x00016B30 File Offset: 0x00014D30
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00016B38 File Offset: 0x00014D38
		public bool grounded
		{
			get
			{
				return this.m_Grounded;
			}
			set
			{
				this.m_Grounded = value;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00016B44 File Offset: 0x00014D44
		private void OnValidate()
		{
			if (this.m_TransformSyncMode < NetworkTransform.TransformSyncMode.SyncNone || this.m_TransformSyncMode > NetworkTransform.TransformSyncMode.SyncCharacterController)
			{
				this.m_TransformSyncMode = NetworkTransform.TransformSyncMode.SyncTransform;
			}
			if (this.m_SendInterval < 0f)
			{
				this.m_SendInterval = 0f;
			}
			if (this.m_SyncRotationAxis < NetworkTransform.AxisSyncMode.None || this.m_SyncRotationAxis > NetworkTransform.AxisSyncMode.AxisXYZ)
			{
				this.m_SyncRotationAxis = NetworkTransform.AxisSyncMode.None;
			}
			if (this.m_MovementTheshold < 0f)
			{
				this.m_MovementTheshold = 0f;
			}
			if (this.m_SnapThreshold < 0f)
			{
				this.m_SnapThreshold = 0.01f;
			}
			if (this.m_InterpolateRotation < 0f)
			{
				this.m_InterpolateRotation = 0.01f;
			}
			if (this.m_InterpolateMovement < 0f)
			{
				this.m_InterpolateMovement = 0.01f;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00016C18 File Offset: 0x00014E18
		private void Awake()
		{
			this.m_RigidBody3D = base.GetComponent<Rigidbody>();
			this.m_RigidBody2D = base.GetComponent<Rigidbody2D>();
			this.m_CharacterController = base.GetComponent<CharacterController>();
			this.m_PrevPosition = base.transform.position;
			this.m_PrevRotation = base.transform.rotation;
			this.m_PrevVelocity = 0f;
			if (base.localPlayerAuthority)
			{
				this.m_LocalTransformWriter = new NetworkWriter();
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016C8C File Offset: 0x00014E8C
		public override void OnStartServer()
		{
			this.m_LastClientSyncTime = 0f;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016C9C File Offset: 0x00014E9C
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
			switch (this.transformSyncMode)
			{
			case NetworkTransform.TransformSyncMode.SyncNone:
				return false;
			case NetworkTransform.TransformSyncMode.SyncTransform:
				this.SerializeModeTransform(writer);
				break;
			case NetworkTransform.TransformSyncMode.SyncRigidbody2D:
				this.SerializeMode2D(writer);
				break;
			case NetworkTransform.TransformSyncMode.SyncRigidbody3D:
				this.SerializeMode3D(writer);
				break;
			case NetworkTransform.TransformSyncMode.SyncCharacterController:
				this.SerializeModeCharacterController(writer);
				break;
			}
			return true;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016D28 File Offset: 0x00014F28
		private void SerializeModeTransform(NetworkWriter writer)
		{
			writer.Write(base.transform.position);
			if (this.m_SyncRotationAxis != NetworkTransform.AxisSyncMode.None)
			{
				NetworkTransform.SerializeRotation3D(writer, base.transform.rotation, this.syncRotationAxis, this.rotationSyncCompression);
			}
			this.m_PrevPosition = base.transform.position;
			this.m_PrevRotation = base.transform.rotation;
			this.m_PrevVelocity = 0f;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016D9C File Offset: 0x00014F9C
		private void SerializeMode3D(NetworkWriter writer)
		{
			if (base.isServer && this.m_LastClientSyncTime != 0f)
			{
				writer.Write(this.m_TargetSyncPosition);
				NetworkTransform.SerializeVelocity3D(writer, this.m_TargetSyncVelocity, NetworkTransform.CompressionSyncMode.None);
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					NetworkTransform.SerializeRotation3D(writer, this.m_TargetSyncRotation3D, this.syncRotationAxis, this.rotationSyncCompression);
				}
			}
			else
			{
				writer.Write(this.m_RigidBody3D.position);
				NetworkTransform.SerializeVelocity3D(writer, this.m_RigidBody3D.velocity, NetworkTransform.CompressionSyncMode.None);
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					NetworkTransform.SerializeRotation3D(writer, this.m_RigidBody3D.rotation, this.syncRotationAxis, this.rotationSyncCompression);
				}
			}
			if (this.m_SyncSpin)
			{
				NetworkTransform.SerializeSpin3D(writer, this.m_RigidBody3D.angularVelocity, this.syncRotationAxis, this.rotationSyncCompression);
			}
			this.m_PrevPosition = this.m_RigidBody3D.position;
			this.m_PrevRotation = base.transform.rotation;
			this.m_PrevVelocity = this.m_RigidBody3D.velocity.sqrMagnitude;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00016EB4 File Offset: 0x000150B4
		private void SerializeModeCharacterController(NetworkWriter writer)
		{
			if (base.isServer && this.m_LastClientSyncTime != 0f)
			{
				writer.Write(this.m_TargetSyncPosition);
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					NetworkTransform.SerializeRotation3D(writer, this.m_TargetSyncRotation3D, this.syncRotationAxis, this.rotationSyncCompression);
				}
			}
			else
			{
				writer.Write(base.transform.position);
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					NetworkTransform.SerializeRotation3D(writer, base.transform.rotation, this.syncRotationAxis, this.rotationSyncCompression);
				}
			}
			this.m_PrevPosition = base.transform.position;
			this.m_PrevRotation = base.transform.rotation;
			this.m_PrevVelocity = 0f;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00016F78 File Offset: 0x00015178
		private void SerializeMode2D(NetworkWriter writer)
		{
			if (base.isServer && this.m_LastClientSyncTime != 0f)
			{
				writer.Write(this.m_TargetSyncPosition);
				NetworkTransform.SerializeVelocity2D(writer, this.m_TargetSyncVelocity, NetworkTransform.CompressionSyncMode.None);
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					float num = this.m_TargetSyncRotation2D % 360f;
					if (num < 0f)
					{
						num += 360f;
					}
					NetworkTransform.SerializeRotation2D(writer, num, this.rotationSyncCompression);
				}
			}
			else
			{
				writer.Write(this.m_RigidBody2D.position);
				NetworkTransform.SerializeVelocity2D(writer, this.m_RigidBody2D.velocity, NetworkTransform.CompressionSyncMode.None);
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					float num2 = this.m_RigidBody2D.rotation % 360f;
					if (num2 < 0f)
					{
						num2 += 360f;
					}
					NetworkTransform.SerializeRotation2D(writer, num2, this.rotationSyncCompression);
				}
			}
			if (this.m_SyncSpin)
			{
				NetworkTransform.SerializeSpin2D(writer, this.m_RigidBody2D.angularVelocity, this.rotationSyncCompression);
			}
			this.m_PrevPosition = this.m_RigidBody2D.position;
			this.m_PrevRotation = base.transform.rotation;
			this.m_PrevVelocity = this.m_RigidBody2D.velocity.sqrMagnitude;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000170C4 File Offset: 0x000152C4
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
			switch (this.transformSyncMode)
			{
			case NetworkTransform.TransformSyncMode.SyncNone:
				return;
			case NetworkTransform.TransformSyncMode.SyncTransform:
				this.UnserializeModeTransform(reader, initialState);
				break;
			case NetworkTransform.TransformSyncMode.SyncRigidbody2D:
				this.UnserializeMode2D(reader, initialState);
				break;
			case NetworkTransform.TransformSyncMode.SyncRigidbody3D:
				this.UnserializeMode3D(reader, initialState);
				break;
			case NetworkTransform.TransformSyncMode.SyncCharacterController:
				this.UnserializeModeCharacterController(reader, initialState);
				break;
			}
			this.m_LastClientSyncTime = Time.time;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00017160 File Offset: 0x00015360
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
				Vector3 position = reader.ReadVector3();
				Vector3 zero = Vector3.zero;
				Quaternion rotation = Quaternion.identity;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					rotation = NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
				if (!this.m_ClientMoveCallback3D(ref position, ref zero, ref rotation))
				{
					return;
				}
				base.transform.position = position;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					base.transform.rotation = rotation;
				}
			}
			else
			{
				base.transform.position = reader.ReadVector3();
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					base.transform.rotation = NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00017264 File Offset: 0x00015464
		private void UnserializeMode3D(NetworkReader reader, bool initialState)
		{
			if (base.hasAuthority)
			{
				reader.ReadVector3();
				reader.ReadVector3();
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
				if (this.syncSpin)
				{
					NetworkTransform.UnserializeSpin3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
				return;
			}
			if (base.isServer && this.m_ClientMoveCallback3D != null)
			{
				Vector3 targetSyncPosition = reader.ReadVector3();
				Vector3 targetSyncVelocity = reader.ReadVector3();
				Quaternion targetSyncRotation3D = Quaternion.identity;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					targetSyncRotation3D = NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
				if (!this.m_ClientMoveCallback3D(ref targetSyncPosition, ref targetSyncVelocity, ref targetSyncRotation3D))
				{
					return;
				}
				this.m_TargetSyncPosition = targetSyncPosition;
				this.m_TargetSyncVelocity = targetSyncVelocity;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_TargetSyncRotation3D = targetSyncRotation3D;
				}
			}
			else
			{
				this.m_TargetSyncPosition = reader.ReadVector3();
				this.m_TargetSyncVelocity = reader.ReadVector3();
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_TargetSyncRotation3D = NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
			}
			if (this.syncSpin)
			{
				this.m_TargetSyncAngularVelocity3D = NetworkTransform.UnserializeSpin3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
			}
			if (this.m_RigidBody3D == null)
			{
				return;
			}
			if (base.isServer && !base.isClient)
			{
				this.m_RigidBody3D.MovePosition(this.m_TargetSyncPosition);
				this.m_RigidBody3D.MoveRotation(this.m_TargetSyncRotation3D);
				this.m_RigidBody3D.velocity = this.m_TargetSyncVelocity;
				return;
			}
			if (this.GetNetworkSendInterval() == 0f)
			{
				this.m_RigidBody3D.MovePosition(this.m_TargetSyncPosition);
				this.m_RigidBody3D.velocity = this.m_TargetSyncVelocity;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_RigidBody3D.MoveRotation(this.m_TargetSyncRotation3D);
				}
				if (this.syncSpin)
				{
					this.m_RigidBody3D.angularVelocity = this.m_TargetSyncAngularVelocity3D;
				}
				return;
			}
			float magnitude = (this.m_RigidBody3D.position - this.m_TargetSyncPosition).magnitude;
			if (magnitude > this.snapThreshold)
			{
				this.m_RigidBody3D.position = this.m_TargetSyncPosition;
				this.m_RigidBody3D.velocity = this.m_TargetSyncVelocity;
			}
			if (this.interpolateRotation == 0f && this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
			{
				this.m_RigidBody3D.rotation = this.m_TargetSyncRotation3D;
				if (this.syncSpin)
				{
					this.m_RigidBody3D.angularVelocity = this.m_TargetSyncAngularVelocity3D;
				}
			}
			if (this.m_InterpolateMovement == 0f)
			{
				this.m_RigidBody3D.position = this.m_TargetSyncPosition;
			}
			if (initialState && this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
			{
				this.m_RigidBody3D.rotation = this.m_TargetSyncRotation3D;
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00017550 File Offset: 0x00015750
		private void UnserializeMode2D(NetworkReader reader, bool initialState)
		{
			if (base.hasAuthority)
			{
				reader.ReadVector2();
				reader.ReadVector2();
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					NetworkTransform.UnserializeRotation2D(reader, this.rotationSyncCompression);
				}
				if (this.syncSpin)
				{
					NetworkTransform.UnserializeSpin2D(reader, this.rotationSyncCompression);
				}
				return;
			}
			if (this.m_RigidBody2D == null)
			{
				return;
			}
			if (base.isServer && this.m_ClientMoveCallback2D != null)
			{
				Vector2 v = reader.ReadVector2();
				Vector2 v2 = reader.ReadVector2();
				float targetSyncRotation2D = 0f;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					targetSyncRotation2D = NetworkTransform.UnserializeRotation2D(reader, this.rotationSyncCompression);
				}
				if (!this.m_ClientMoveCallback2D(ref v, ref v2, ref targetSyncRotation2D))
				{
					return;
				}
				this.m_TargetSyncPosition = v;
				this.m_TargetSyncVelocity = v2;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_TargetSyncRotation2D = targetSyncRotation2D;
				}
			}
			else
			{
				this.m_TargetSyncPosition = reader.ReadVector2();
				this.m_TargetSyncVelocity = reader.ReadVector2();
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_TargetSyncRotation2D = NetworkTransform.UnserializeRotation2D(reader, this.rotationSyncCompression);
				}
			}
			if (this.syncSpin)
			{
				this.m_TargetSyncAngularVelocity2D = NetworkTransform.UnserializeSpin2D(reader, this.rotationSyncCompression);
			}
			if (base.isServer && !base.isClient)
			{
				base.transform.position = this.m_TargetSyncPosition;
				this.m_RigidBody2D.MoveRotation(this.m_TargetSyncRotation2D);
				this.m_RigidBody2D.velocity = this.m_TargetSyncVelocity;
				return;
			}
			if (this.GetNetworkSendInterval() == 0f)
			{
				base.transform.position = this.m_TargetSyncPosition;
				this.m_RigidBody2D.velocity = this.m_TargetSyncVelocity;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					this.m_RigidBody2D.MoveRotation(this.m_TargetSyncRotation2D);
				}
				if (this.syncSpin)
				{
					this.m_RigidBody2D.angularVelocity = this.m_TargetSyncAngularVelocity2D;
				}
				return;
			}
			float magnitude = (this.m_RigidBody2D.position - this.m_TargetSyncPosition).magnitude;
			if (magnitude > this.snapThreshold)
			{
				this.m_RigidBody2D.position = this.m_TargetSyncPosition;
				this.m_RigidBody2D.velocity = this.m_TargetSyncVelocity;
			}
			if (this.interpolateRotation == 0f && this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
			{
				this.m_RigidBody2D.rotation = this.m_TargetSyncRotation2D;
				if (this.syncSpin)
				{
					this.m_RigidBody2D.angularVelocity = this.m_TargetSyncAngularVelocity2D;
				}
			}
			if (this.m_InterpolateMovement == 0f)
			{
				this.m_RigidBody2D.position = this.m_TargetSyncPosition;
			}
			if (initialState)
			{
				this.m_RigidBody2D.rotation = this.m_TargetSyncRotation2D;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00017844 File Offset: 0x00015A44
		private void UnserializeModeCharacterController(NetworkReader reader, bool initialState)
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
				Quaternion targetSyncRotation3D = Quaternion.identity;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					targetSyncRotation3D = NetworkTransform.UnserializeRotation3D(reader, this.syncRotationAxis, this.rotationSyncCompression);
				}
				if (this.m_CharacterController == null)
				{
					return;
				}
				Vector3 velocity = this.m_CharacterController.velocity;
				if (!this.m_ClientMoveCallback3D(ref targetSyncPosition, ref velocity, ref targetSyncRotation3D))
				{
					return;
				}
				this.m_TargetSyncPosition = targetSyncPosition;
				this.m_TargetSyncVelocity = velocity;
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
			if (this.m_CharacterController == null)
			{
				return;
			}
			Vector3 a = this.m_TargetSyncPosition - base.transform.position;
			Vector3 a2 = a / this.GetNetworkSendInterval();
			this.m_FixedPosDiff = a2 * Time.fixedDeltaTime;
			if (base.isServer && !base.isClient)
			{
				base.transform.position = this.m_TargetSyncPosition;
				base.transform.rotation = this.m_TargetSyncRotation3D;
				return;
			}
			if (this.GetNetworkSendInterval() == 0f)
			{
				base.transform.position = this.m_TargetSyncPosition;
				if (this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
				{
					base.transform.rotation = this.m_TargetSyncRotation3D;
				}
				return;
			}
			float magnitude = (base.transform.position - this.m_TargetSyncPosition).magnitude;
			if (magnitude > this.snapThreshold)
			{
				base.transform.position = this.m_TargetSyncPosition;
			}
			if (this.interpolateRotation == 0f && this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
			{
				base.transform.rotation = this.m_TargetSyncRotation3D;
			}
			if (this.m_InterpolateMovement == 0f)
			{
				base.transform.position = this.m_TargetSyncPosition;
			}
			if (initialState && this.syncRotationAxis != NetworkTransform.AxisSyncMode.None)
			{
				base.transform.rotation = this.m_TargetSyncRotation3D;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017AC0 File Offset: 0x00015CC0
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

		// Token: 0x0600048F RID: 1167 RVA: 0x00017AF0 File Offset: 0x00015CF0
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
			float num = (base.transform.position - this.m_PrevPosition).magnitude;
			if (num < this.movementTheshold)
			{
				num = Quaternion.Angle(this.m_PrevRotation, base.transform.rotation);
				if (num < this.movementTheshold)
				{
					return;
				}
			}
			base.SetDirtyBit(1U);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00017B88 File Offset: 0x00015D88
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
			switch (this.transformSyncMode)
			{
			case NetworkTransform.TransformSyncMode.SyncNone:
				return;
			case NetworkTransform.TransformSyncMode.SyncTransform:
				return;
			case NetworkTransform.TransformSyncMode.SyncRigidbody2D:
				this.InterpolateTransformMode2D();
				break;
			case NetworkTransform.TransformSyncMode.SyncRigidbody3D:
				this.InterpolateTransformMode3D();
				break;
			case NetworkTransform.TransformSyncMode.SyncCharacterController:
				this.InterpolateTransformModeCharacterController();
				break;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00017C38 File Offset: 0x00015E38
		private void InterpolateTransformMode3D()
		{
			if (this.m_InterpolateMovement != 0f)
			{
				Vector3 velocity = (this.m_TargetSyncPosition - this.m_RigidBody3D.position) * this.m_InterpolateMovement / this.GetNetworkSendInterval();
				this.m_RigidBody3D.velocity = velocity;
			}
			if (this.interpolateRotation != 0f)
			{
				this.m_RigidBody3D.MoveRotation(Quaternion.Slerp(this.m_RigidBody3D.rotation, this.m_TargetSyncRotation3D, Time.fixedDeltaTime * this.interpolateRotation));
			}
			this.m_TargetSyncPosition += this.m_TargetSyncVelocity * Time.fixedDeltaTime * 0.1f;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00017CF8 File Offset: 0x00015EF8
		private void InterpolateTransformModeCharacterController()
		{
			if (this.m_FixedPosDiff == Vector3.zero && this.m_TargetSyncRotation3D == base.transform.rotation)
			{
				return;
			}
			if (this.m_InterpolateMovement != 0f)
			{
				this.m_CharacterController.Move(this.m_FixedPosDiff * this.m_InterpolateMovement);
			}
			if (this.interpolateRotation != 0f)
			{
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.m_TargetSyncRotation3D, Time.fixedDeltaTime * this.interpolateRotation * 10f);
			}
			if (Time.time - this.m_LastClientSyncTime > this.GetNetworkSendInterval())
			{
				this.m_FixedPosDiff = Vector3.zero;
				Vector3 motion = this.m_TargetSyncPosition - base.transform.position;
				this.m_CharacterController.Move(motion);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00017DEC File Offset: 0x00015FEC
		private void InterpolateTransformMode2D()
		{
			if (this.m_InterpolateMovement != 0f)
			{
				Vector2 velocity = this.m_RigidBody2D.velocity;
				Vector2 velocity2 = (this.m_TargetSyncPosition - this.m_RigidBody2D.position) * this.m_InterpolateMovement / this.GetNetworkSendInterval();
				if (!this.m_Grounded && velocity2.y < 0f)
				{
					velocity2.y = velocity.y;
				}
				this.m_RigidBody2D.velocity = velocity2;
			}
			if (this.interpolateRotation != 0f)
			{
				float num = this.m_RigidBody2D.rotation % 360f;
				if (num < 0f)
				{
					num += 360f;
				}
				Quaternion quaternion = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, this.m_TargetSyncRotation2D), Time.fixedDeltaTime * this.interpolateRotation / this.GetNetworkSendInterval());
				this.m_RigidBody2D.MoveRotation(quaternion.eulerAngles.z);
				this.m_TargetSyncRotation2D += this.m_TargetSyncAngularVelocity2D * Time.fixedDeltaTime * 0.1f;
			}
			this.m_TargetSyncPosition += this.m_TargetSyncVelocity * Time.fixedDeltaTime * 0.1f;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00017F50 File Offset: 0x00016150
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

		// Token: 0x06000495 RID: 1173 RVA: 0x00017FA8 File Offset: 0x000161A8
		private bool HasMoved()
		{
			float num;
			if (this.m_RigidBody3D != null)
			{
				num = (this.m_RigidBody3D.position - this.m_PrevPosition).magnitude;
			}
			else if (this.m_RigidBody2D != null)
			{
				num = (this.m_RigidBody2D.position - this.m_PrevPosition).magnitude;
			}
			else
			{
				num = (base.transform.position - this.m_PrevPosition).magnitude;
			}
			if (num > 1E-05f)
			{
				return true;
			}
			if (this.m_RigidBody3D != null)
			{
				num = Quaternion.Angle(this.m_RigidBody3D.rotation, this.m_PrevRotation);
			}
			else if (this.m_RigidBody2D != null)
			{
				num = Math.Abs(this.m_RigidBody2D.rotation - this.m_PrevRotation2D);
			}
			else
			{
				num = Quaternion.Angle(base.transform.rotation, this.m_PrevRotation);
			}
			if (num > 1E-05f)
			{
				return true;
			}
			if (this.m_RigidBody3D != null)
			{
				num = Mathf.Abs(this.m_RigidBody3D.velocity.sqrMagnitude - this.m_PrevVelocity);
			}
			else if (this.m_RigidBody2D != null)
			{
				num = Mathf.Abs(this.m_RigidBody2D.velocity.sqrMagnitude - this.m_PrevVelocity);
			}
			return num > 1E-05f;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001814C File Offset: 0x0001634C
		[Client]
		private void SendTransform()
		{
			if (!this.HasMoved() || ClientScene.readyConnection == null)
			{
				return;
			}
			this.m_LocalTransformWriter.StartMessage(6);
			this.m_LocalTransformWriter.Write(base.netId);
			switch (this.transformSyncMode)
			{
			case NetworkTransform.TransformSyncMode.SyncNone:
				return;
			case NetworkTransform.TransformSyncMode.SyncTransform:
				this.SerializeModeTransform(this.m_LocalTransformWriter);
				break;
			case NetworkTransform.TransformSyncMode.SyncRigidbody2D:
				this.SerializeMode2D(this.m_LocalTransformWriter);
				break;
			case NetworkTransform.TransformSyncMode.SyncRigidbody3D:
				this.SerializeMode3D(this.m_LocalTransformWriter);
				break;
			case NetworkTransform.TransformSyncMode.SyncCharacterController:
				this.SerializeModeCharacterController(this.m_LocalTransformWriter);
				break;
			}
			if (this.m_RigidBody3D != null)
			{
				this.m_PrevPosition = this.m_RigidBody3D.position;
				this.m_PrevRotation = this.m_RigidBody3D.rotation;
				this.m_PrevVelocity = this.m_RigidBody3D.velocity.sqrMagnitude;
			}
			else if (this.m_RigidBody2D != null)
			{
				this.m_PrevPosition = this.m_RigidBody2D.position;
				this.m_PrevRotation2D = this.m_RigidBody2D.rotation;
				this.m_PrevVelocity = this.m_RigidBody2D.velocity.sqrMagnitude;
			}
			else
			{
				this.m_PrevPosition = base.transform.position;
				this.m_PrevRotation = base.transform.rotation;
			}
			this.m_LocalTransformWriter.FinishMessage();
			ClientScene.readyConnection.SendWriter(this.m_LocalTransformWriter, this.GetNetworkChannel());
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000182E4 File Offset: 0x000164E4
		public static void HandleTransform(NetworkMessage netMsg)
		{
			NetworkInstanceId networkInstanceId = netMsg.reader.ReadNetworkId();
			GameObject gameObject = NetworkServer.FindLocalObject(networkInstanceId);
			if (gameObject == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleTransform no gameObject");
				}
				return;
			}
			NetworkTransform component = gameObject.GetComponent<NetworkTransform>();
			if (component == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleTransform null target");
				}
				return;
			}
			if (!component.localPlayerAuthority)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleTransform no localPlayerAuthority");
				}
				return;
			}
			if (netMsg.conn.clientOwnedObjects == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("HandleTransform object not owned by connection");
				}
				return;
			}
			if (netMsg.conn.clientOwnedObjects.Contains(networkInstanceId))
			{
				switch (component.transformSyncMode)
				{
				case NetworkTransform.TransformSyncMode.SyncNone:
					return;
				case NetworkTransform.TransformSyncMode.SyncTransform:
					component.UnserializeModeTransform(netMsg.reader, false);
					break;
				case NetworkTransform.TransformSyncMode.SyncRigidbody2D:
					component.UnserializeMode2D(netMsg.reader, false);
					break;
				case NetworkTransform.TransformSyncMode.SyncRigidbody3D:
					component.UnserializeMode3D(netMsg.reader, false);
					break;
				case NetworkTransform.TransformSyncMode.SyncCharacterController:
					component.UnserializeModeCharacterController(netMsg.reader, false);
					break;
				}
				component.m_LastClientSyncTime = Time.time;
				return;
			}
			if (LogFilter.logWarn)
			{
				Debug.LogWarning("HandleTransform netId:" + networkInstanceId + " is not for a valid player");
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00018448 File Offset: 0x00016648
		private static void WriteAngle(NetworkWriter writer, float angle, NetworkTransform.CompressionSyncMode compression)
		{
			switch (compression)
			{
			case NetworkTransform.CompressionSyncMode.None:
				writer.Write(angle);
				break;
			case NetworkTransform.CompressionSyncMode.Low:
				writer.Write((short)angle);
				break;
			case NetworkTransform.CompressionSyncMode.High:
				writer.Write((short)angle);
				break;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00018494 File Offset: 0x00016694
		private static float ReadAngle(NetworkReader reader, NetworkTransform.CompressionSyncMode compression)
		{
			switch (compression)
			{
			case NetworkTransform.CompressionSyncMode.None:
				return reader.ReadSingle();
			case NetworkTransform.CompressionSyncMode.Low:
				return (float)reader.ReadInt16();
			case NetworkTransform.CompressionSyncMode.High:
				return (float)reader.ReadInt16();
			default:
				return 0f;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000184D8 File Offset: 0x000166D8
		public static void SerializeVelocity3D(NetworkWriter writer, Vector3 velocity, NetworkTransform.CompressionSyncMode compression)
		{
			writer.Write(velocity);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000184E4 File Offset: 0x000166E4
		public static void SerializeVelocity2D(NetworkWriter writer, Vector2 velocity, NetworkTransform.CompressionSyncMode compression)
		{
			writer.Write(velocity);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000184F0 File Offset: 0x000166F0
		public static void SerializeRotation3D(NetworkWriter writer, Quaternion rot, NetworkTransform.AxisSyncMode mode, NetworkTransform.CompressionSyncMode compression)
		{
			switch (mode)
			{
			case NetworkTransform.AxisSyncMode.AxisX:
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.x, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisY:
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.y, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisZ:
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.z, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisXY:
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.x, compression);
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.y, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisXZ:
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.x, compression);
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.z, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisYZ:
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.y, compression);
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.z, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisXYZ:
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.x, compression);
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.y, compression);
				NetworkTransform.WriteAngle(writer, rot.eulerAngles.z, compression);
				break;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00018664 File Offset: 0x00016864
		public static void SerializeRotation2D(NetworkWriter writer, float rot, NetworkTransform.CompressionSyncMode compression)
		{
			NetworkTransform.WriteAngle(writer, rot, compression);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00018670 File Offset: 0x00016870
		public static void SerializeSpin3D(NetworkWriter writer, Vector3 angularVelocity, NetworkTransform.AxisSyncMode mode, NetworkTransform.CompressionSyncMode compression)
		{
			switch (mode)
			{
			case NetworkTransform.AxisSyncMode.AxisX:
				NetworkTransform.WriteAngle(writer, angularVelocity.x, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisY:
				NetworkTransform.WriteAngle(writer, angularVelocity.y, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisZ:
				NetworkTransform.WriteAngle(writer, angularVelocity.z, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisXY:
				NetworkTransform.WriteAngle(writer, angularVelocity.x, compression);
				NetworkTransform.WriteAngle(writer, angularVelocity.y, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisXZ:
				NetworkTransform.WriteAngle(writer, angularVelocity.x, compression);
				NetworkTransform.WriteAngle(writer, angularVelocity.z, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisYZ:
				NetworkTransform.WriteAngle(writer, angularVelocity.y, compression);
				NetworkTransform.WriteAngle(writer, angularVelocity.z, compression);
				break;
			case NetworkTransform.AxisSyncMode.AxisXYZ:
				NetworkTransform.WriteAngle(writer, angularVelocity.x, compression);
				NetworkTransform.WriteAngle(writer, angularVelocity.y, compression);
				NetworkTransform.WriteAngle(writer, angularVelocity.z, compression);
				break;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001877C File Offset: 0x0001697C
		public static void SerializeSpin2D(NetworkWriter writer, float angularVelocity, NetworkTransform.CompressionSyncMode compression)
		{
			NetworkTransform.WriteAngle(writer, angularVelocity, compression);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00018788 File Offset: 0x00016988
		public static Vector3 UnserializeVelocity3D(NetworkReader reader, NetworkTransform.CompressionSyncMode compression)
		{
			return reader.ReadVector3();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00018790 File Offset: 0x00016990
		public static Vector3 UnserializeVelocity2D(NetworkReader reader, NetworkTransform.CompressionSyncMode compression)
		{
			return reader.ReadVector2();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000187A0 File Offset: 0x000169A0
		public static Quaternion UnserializeRotation3D(NetworkReader reader, NetworkTransform.AxisSyncMode mode, NetworkTransform.CompressionSyncMode compression)
		{
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			switch (mode)
			{
			case NetworkTransform.AxisSyncMode.AxisX:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), 0f, 0f);
				identity.eulerAngles = zero;
				break;
			case NetworkTransform.AxisSyncMode.AxisY:
				zero.Set(0f, NetworkTransform.ReadAngle(reader, compression), 0f);
				identity.eulerAngles = zero;
				break;
			case NetworkTransform.AxisSyncMode.AxisZ:
				zero.Set(0f, 0f, NetworkTransform.ReadAngle(reader, compression));
				identity.eulerAngles = zero;
				break;
			case NetworkTransform.AxisSyncMode.AxisXY:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression), 0f);
				identity.eulerAngles = zero;
				break;
			case NetworkTransform.AxisSyncMode.AxisXZ:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), 0f, NetworkTransform.ReadAngle(reader, compression));
				identity.eulerAngles = zero;
				break;
			case NetworkTransform.AxisSyncMode.AxisYZ:
				zero.Set(0f, NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression));
				identity.eulerAngles = zero;
				break;
			case NetworkTransform.AxisSyncMode.AxisXYZ:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression));
				identity.eulerAngles = zero;
				break;
			}
			return identity;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000188FC File Offset: 0x00016AFC
		public static float UnserializeRotation2D(NetworkReader reader, NetworkTransform.CompressionSyncMode compression)
		{
			return NetworkTransform.ReadAngle(reader, compression);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00018908 File Offset: 0x00016B08
		public static Vector3 UnserializeSpin3D(NetworkReader reader, NetworkTransform.AxisSyncMode mode, NetworkTransform.CompressionSyncMode compression)
		{
			Vector3 zero = Vector3.zero;
			switch (mode)
			{
			case NetworkTransform.AxisSyncMode.AxisX:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), 0f, 0f);
				break;
			case NetworkTransform.AxisSyncMode.AxisY:
				zero.Set(0f, NetworkTransform.ReadAngle(reader, compression), 0f);
				break;
			case NetworkTransform.AxisSyncMode.AxisZ:
				zero.Set(0f, 0f, NetworkTransform.ReadAngle(reader, compression));
				break;
			case NetworkTransform.AxisSyncMode.AxisXY:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression), 0f);
				break;
			case NetworkTransform.AxisSyncMode.AxisXZ:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), 0f, NetworkTransform.ReadAngle(reader, compression));
				break;
			case NetworkTransform.AxisSyncMode.AxisYZ:
				zero.Set(0f, NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression));
				break;
			case NetworkTransform.AxisSyncMode.AxisXYZ:
				zero.Set(NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression), NetworkTransform.ReadAngle(reader, compression));
				break;
			}
			return zero;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00018A24 File Offset: 0x00016C24
		public static float UnserializeSpin2D(NetworkReader reader, NetworkTransform.CompressionSyncMode compression)
		{
			return NetworkTransform.ReadAngle(reader, compression);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00018A30 File Offset: 0x00016C30
		public override int GetNetworkChannel()
		{
			return 1;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00018A34 File Offset: 0x00016C34
		public override float GetNetworkSendInterval()
		{
			return this.m_SendInterval;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00018A3C File Offset: 0x00016C3C
		public override void OnStartAuthority()
		{
			this.m_LastClientSyncTime = 0f;
		}

		// Token: 0x040001C1 RID: 449
		private const float k_LocalMovementThreshold = 1E-05f;

		// Token: 0x040001C2 RID: 450
		private const float k_LocalRotationThreshold = 1E-05f;

		// Token: 0x040001C3 RID: 451
		private const float k_LocalVelocityThreshold = 1E-05f;

		// Token: 0x040001C4 RID: 452
		private const float k_MoveAheadRatio = 0.1f;

		// Token: 0x040001C5 RID: 453
		[SerializeField]
		private NetworkTransform.TransformSyncMode m_TransformSyncMode;

		// Token: 0x040001C6 RID: 454
		[SerializeField]
		private float m_SendInterval = 0.1f;

		// Token: 0x040001C7 RID: 455
		[SerializeField]
		private NetworkTransform.AxisSyncMode m_SyncRotationAxis = NetworkTransform.AxisSyncMode.AxisXYZ;

		// Token: 0x040001C8 RID: 456
		[SerializeField]
		private NetworkTransform.CompressionSyncMode m_RotationSyncCompression;

		// Token: 0x040001C9 RID: 457
		[SerializeField]
		private bool m_SyncSpin;

		// Token: 0x040001CA RID: 458
		[SerializeField]
		private float m_MovementTheshold = 0.001f;

		// Token: 0x040001CB RID: 459
		[SerializeField]
		private float m_SnapThreshold = 5f;

		// Token: 0x040001CC RID: 460
		[SerializeField]
		private float m_InterpolateRotation = 1f;

		// Token: 0x040001CD RID: 461
		[SerializeField]
		private float m_InterpolateMovement = 1f;

		// Token: 0x040001CE RID: 462
		[SerializeField]
		private NetworkTransform.ClientMoveCallback3D m_ClientMoveCallback3D;

		// Token: 0x040001CF RID: 463
		[SerializeField]
		private NetworkTransform.ClientMoveCallback2D m_ClientMoveCallback2D;

		// Token: 0x040001D0 RID: 464
		private Rigidbody m_RigidBody3D;

		// Token: 0x040001D1 RID: 465
		private Rigidbody2D m_RigidBody2D;

		// Token: 0x040001D2 RID: 466
		private CharacterController m_CharacterController;

		// Token: 0x040001D3 RID: 467
		private bool m_Grounded = true;

		// Token: 0x040001D4 RID: 468
		private Vector3 m_TargetSyncPosition;

		// Token: 0x040001D5 RID: 469
		private Vector3 m_TargetSyncVelocity;

		// Token: 0x040001D6 RID: 470
		private Vector3 m_FixedPosDiff;

		// Token: 0x040001D7 RID: 471
		private Quaternion m_TargetSyncRotation3D;

		// Token: 0x040001D8 RID: 472
		private Vector3 m_TargetSyncAngularVelocity3D;

		// Token: 0x040001D9 RID: 473
		private float m_TargetSyncRotation2D;

		// Token: 0x040001DA RID: 474
		private float m_TargetSyncAngularVelocity2D;

		// Token: 0x040001DB RID: 475
		private float m_LastClientSyncTime;

		// Token: 0x040001DC RID: 476
		private float m_LastClientSendTime;

		// Token: 0x040001DD RID: 477
		private Vector3 m_PrevPosition;

		// Token: 0x040001DE RID: 478
		private Quaternion m_PrevRotation;

		// Token: 0x040001DF RID: 479
		private float m_PrevRotation2D;

		// Token: 0x040001E0 RID: 480
		private float m_PrevVelocity;

		// Token: 0x040001E1 RID: 481
		private NetworkWriter m_LocalTransformWriter;

		// Token: 0x02000058 RID: 88
		public enum TransformSyncMode
		{
			// Token: 0x040001E3 RID: 483
			SyncNone,
			// Token: 0x040001E4 RID: 484
			SyncTransform,
			// Token: 0x040001E5 RID: 485
			SyncRigidbody2D,
			// Token: 0x040001E6 RID: 486
			SyncRigidbody3D,
			// Token: 0x040001E7 RID: 487
			SyncCharacterController
		}

		// Token: 0x02000059 RID: 89
		public enum AxisSyncMode
		{
			// Token: 0x040001E9 RID: 489
			None,
			// Token: 0x040001EA RID: 490
			AxisX,
			// Token: 0x040001EB RID: 491
			AxisY,
			// Token: 0x040001EC RID: 492
			AxisZ,
			// Token: 0x040001ED RID: 493
			AxisXY,
			// Token: 0x040001EE RID: 494
			AxisXZ,
			// Token: 0x040001EF RID: 495
			AxisYZ,
			// Token: 0x040001F0 RID: 496
			AxisXYZ
		}

		// Token: 0x0200005A RID: 90
		public enum CompressionSyncMode
		{
			// Token: 0x040001F2 RID: 498
			None,
			// Token: 0x040001F3 RID: 499
			Low,
			// Token: 0x040001F4 RID: 500
			High
		}

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x06000536 RID: 1334
		public delegate bool ClientMoveCallback3D(ref Vector3 position, ref Vector3 velocity, ref Quaternion rotation);

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x0600053A RID: 1338
		public delegate bool ClientMoveCallback2D(ref Vector2 position, ref Vector2 velocity, ref float rotation);
	}
}
