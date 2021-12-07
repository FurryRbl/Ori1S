using System;
using UnityEngine.Networking.NetworkSystem;

namespace UnityEngine.Networking
{
	// Token: 0x02000031 RID: 49
	[AddComponentMenu("Network/NetworkAnimator")]
	[RequireComponent(typeof(NetworkIdentity))]
	[RequireComponent(typeof(Animator))]
	[DisallowMultipleComponent]
	public class NetworkAnimator : NetworkBehaviour
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005494 File Offset: 0x00003694
		// (set) Token: 0x060000DD RID: 221 RVA: 0x0000549C File Offset: 0x0000369C
		public Animator animator
		{
			get
			{
				return this.m_Animator;
			}
			set
			{
				this.m_Animator = value;
				this.ResetParameterOptions();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000054AC File Offset: 0x000036AC
		public void SetParameterAutoSend(int index, bool value)
		{
			if (value)
			{
				this.m_ParameterSendBits |= 1U << index;
			}
			else
			{
				this.m_ParameterSendBits &= ~(1U << index);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000054EC File Offset: 0x000036EC
		public bool GetParameterAutoSend(int index)
		{
			return (this.m_ParameterSendBits & 1U << index) != 0U;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005504 File Offset: 0x00003704
		internal void ResetParameterOptions()
		{
			Debug.Log("ResetParameterOptions");
			this.m_ParameterSendBits = 0U;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005518 File Offset: 0x00003718
		public override void OnStartAuthority()
		{
			this.m_ParameterWriter = new NetworkWriter();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005528 File Offset: 0x00003728
		private void FixedUpdate()
		{
			if (this.m_ParameterWriter == null)
			{
				return;
			}
			this.CheckSendRate();
			int stateHash;
			float normalizedTime;
			if (!this.CheckAnimStateChanged(out stateHash, out normalizedTime))
			{
				return;
			}
			AnimationMessage animationMessage = new AnimationMessage();
			animationMessage.netId = base.netId;
			animationMessage.stateHash = stateHash;
			animationMessage.normalizedTime = normalizedTime;
			this.m_ParameterWriter.SeekZero();
			this.WriteParameters(this.m_ParameterWriter, false);
			animationMessage.parameters = this.m_ParameterWriter.ToArray();
			if (base.hasAuthority || ClientScene.readyConnection != null)
			{
				ClientScene.readyConnection.Send(40, animationMessage);
				return;
			}
			if (base.isServer && !base.localPlayerAuthority)
			{
				NetworkServer.SendToReady(base.gameObject, 40, animationMessage);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000055EC File Offset: 0x000037EC
		private bool CheckAnimStateChanged(out int stateHash, out float normalizedTime)
		{
			stateHash = 0;
			normalizedTime = 0f;
			if (this.m_Animator.IsInTransition(0))
			{
				AnimatorTransitionInfo animatorTransitionInfo = this.m_Animator.GetAnimatorTransitionInfo(0);
				if (animatorTransitionInfo.fullPathHash != this.m_TransitionHash)
				{
					this.m_TransitionHash = animatorTransitionInfo.fullPathHash;
					this.m_AnimationHash = 0;
					return true;
				}
				return false;
			}
			else
			{
				AnimatorStateInfo currentAnimatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.fullPathHash != this.m_AnimationHash)
				{
					if (this.m_AnimationHash != 0)
					{
						stateHash = currentAnimatorStateInfo.fullPathHash;
						normalizedTime = currentAnimatorStateInfo.normalizedTime;
					}
					this.m_TransitionHash = 0;
					this.m_AnimationHash = currentAnimatorStateInfo.fullPathHash;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000056A0 File Offset: 0x000038A0
		private void CheckSendRate()
		{
			if (this.GetNetworkSendInterval() != 0f && this.m_SendTimer < Time.time)
			{
				this.m_SendTimer = Time.time + this.GetNetworkSendInterval();
				AnimationParametersMessage animationParametersMessage = new AnimationParametersMessage();
				animationParametersMessage.netId = base.netId;
				this.m_ParameterWriter.SeekZero();
				this.WriteParameters(this.m_ParameterWriter, true);
				animationParametersMessage.parameters = this.m_ParameterWriter.ToArray();
				if (base.hasAuthority && ClientScene.readyConnection != null)
				{
					ClientScene.readyConnection.Send(41, animationParametersMessage);
					return;
				}
				if (base.isServer && !base.localPlayerAuthority)
				{
					NetworkServer.SendToReady(base.gameObject, 41, animationParametersMessage);
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005764 File Offset: 0x00003964
		private void SetSendTrackingParam(string p, int i)
		{
			p = "Sent Param: " + p;
			if (i == 0)
			{
				this.param0 = p;
			}
			if (i == 1)
			{
				this.param1 = p;
			}
			if (i == 2)
			{
				this.param2 = p;
			}
			if (i == 3)
			{
				this.param3 = p;
			}
			if (i == 4)
			{
				this.param4 = p;
			}
			if (i == 5)
			{
				this.param5 = p;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000057D4 File Offset: 0x000039D4
		private void SetRecvTrackingParam(string p, int i)
		{
			p = "Recv Param: " + p;
			if (i == 0)
			{
				this.param0 = p;
			}
			if (i == 1)
			{
				this.param1 = p;
			}
			if (i == 2)
			{
				this.param2 = p;
			}
			if (i == 3)
			{
				this.param3 = p;
			}
			if (i == 4)
			{
				this.param4 = p;
			}
			if (i == 5)
			{
				this.param5 = p;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005844 File Offset: 0x00003A44
		internal void HandleAnimMsg(AnimationMessage msg, NetworkReader reader)
		{
			if (base.hasAuthority)
			{
				return;
			}
			if (msg.stateHash != 0)
			{
				this.m_Animator.Play(msg.stateHash, 0, msg.normalizedTime);
			}
			this.ReadParameters(reader, false);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005880 File Offset: 0x00003A80
		internal void HandleAnimParamsMsg(AnimationParametersMessage msg, NetworkReader reader)
		{
			if (base.hasAuthority)
			{
				return;
			}
			this.ReadParameters(reader, true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005898 File Offset: 0x00003A98
		internal void HandleAnimTriggerMsg(int hash)
		{
			this.m_Animator.SetTrigger(hash);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000058A8 File Offset: 0x00003AA8
		private void WriteParameters(NetworkWriter writer, bool autoSend)
		{
			for (int i = 0; i < this.m_Animator.parameters.Length; i++)
			{
				if (!autoSend || this.GetParameterAutoSend(i))
				{
					AnimatorControllerParameter animatorControllerParameter = this.m_Animator.parameters[i];
					if (animatorControllerParameter.type == AnimatorControllerParameterType.Int)
					{
						writer.WritePackedUInt32((uint)this.m_Animator.GetInteger(animatorControllerParameter.nameHash));
						this.SetSendTrackingParam(animatorControllerParameter.name + ":" + this.m_Animator.GetInteger(animatorControllerParameter.nameHash), i);
					}
					if (animatorControllerParameter.type == AnimatorControllerParameterType.Float)
					{
						writer.Write(this.m_Animator.GetFloat(animatorControllerParameter.nameHash));
						this.SetSendTrackingParam(animatorControllerParameter.name + ":" + this.m_Animator.GetFloat(animatorControllerParameter.nameHash), i);
					}
					if (animatorControllerParameter.type == AnimatorControllerParameterType.Bool)
					{
						writer.Write(this.m_Animator.GetBool(animatorControllerParameter.nameHash));
						this.SetSendTrackingParam(animatorControllerParameter.name + ":" + this.m_Animator.GetBool(animatorControllerParameter.nameHash), i);
					}
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000059E8 File Offset: 0x00003BE8
		private void ReadParameters(NetworkReader reader, bool autoSend)
		{
			for (int i = 0; i < this.m_Animator.parameters.Length; i++)
			{
				if (!autoSend || this.GetParameterAutoSend(i))
				{
					AnimatorControllerParameter animatorControllerParameter = this.m_Animator.parameters[i];
					if (animatorControllerParameter.type == AnimatorControllerParameterType.Int)
					{
						int num = (int)reader.ReadPackedUInt32();
						this.m_Animator.SetInteger(animatorControllerParameter.nameHash, num);
						this.SetRecvTrackingParam(animatorControllerParameter.name + ":" + num, i);
					}
					if (animatorControllerParameter.type == AnimatorControllerParameterType.Float)
					{
						float num2 = reader.ReadSingle();
						this.m_Animator.SetFloat(animatorControllerParameter.nameHash, num2);
						this.SetRecvTrackingParam(animatorControllerParameter.name + ":" + num2, i);
					}
					if (animatorControllerParameter.type == AnimatorControllerParameterType.Bool)
					{
						bool flag = reader.ReadBoolean();
						this.m_Animator.SetBool(animatorControllerParameter.nameHash, flag);
						this.SetRecvTrackingParam(animatorControllerParameter.name + ":" + flag, i);
					}
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005B04 File Offset: 0x00003D04
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				if (this.m_Animator.IsInTransition(0))
				{
					AnimatorStateInfo nextAnimatorStateInfo = this.m_Animator.GetNextAnimatorStateInfo(0);
					writer.Write(nextAnimatorStateInfo.fullPathHash);
					writer.Write(nextAnimatorStateInfo.normalizedTime);
				}
				else
				{
					AnimatorStateInfo currentAnimatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
					writer.Write(currentAnimatorStateInfo.fullPathHash);
					writer.Write(currentAnimatorStateInfo.normalizedTime);
				}
				this.WriteParameters(writer, false);
				return true;
			}
			return false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005B88 File Offset: 0x00003D88
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				int stateNameHash = reader.ReadInt32();
				float normalizedTime = reader.ReadSingle();
				this.ReadParameters(reader, false);
				this.m_Animator.Play(stateNameHash, 0, normalizedTime);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public void SetTrigger(string triggerName)
		{
			this.SetTrigger(Animator.StringToHash(triggerName));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005BD0 File Offset: 0x00003DD0
		public void SetTrigger(int hash)
		{
			AnimationTriggerMessage animationTriggerMessage = new AnimationTriggerMessage();
			animationTriggerMessage.netId = base.netId;
			animationTriggerMessage.hash = hash;
			if (base.hasAuthority && base.localPlayerAuthority)
			{
				if (NetworkClient.allClients.Count > 0)
				{
					NetworkConnection readyConnection = ClientScene.readyConnection;
					if (readyConnection != null)
					{
						readyConnection.Send(42, animationTriggerMessage);
					}
				}
				return;
			}
			if (base.isServer && !base.localPlayerAuthority)
			{
				NetworkServer.SendToReady(base.gameObject, 42, animationTriggerMessage);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005C58 File Offset: 0x00003E58
		internal static void OnAnimationServerMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<AnimationMessage>(NetworkAnimator.s_AnimationMessage);
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"OnAnimationMessage for netId=",
					NetworkAnimator.s_AnimationMessage.netId,
					" conn=",
					netMsg.conn
				}));
			}
			GameObject gameObject = NetworkServer.FindLocalObject(NetworkAnimator.s_AnimationMessage.netId);
			if (gameObject == null)
			{
				return;
			}
			NetworkAnimator component = gameObject.GetComponent<NetworkAnimator>();
			if (component != null)
			{
				NetworkReader reader = new NetworkReader(NetworkAnimator.s_AnimationMessage.parameters);
				component.HandleAnimMsg(NetworkAnimator.s_AnimationMessage, reader);
				NetworkServer.SendToReady(gameObject, 40, NetworkAnimator.s_AnimationMessage);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005D10 File Offset: 0x00003F10
		internal static void OnAnimationParametersServerMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<AnimationParametersMessage>(NetworkAnimator.s_AnimationParametersMessage);
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"OnAnimationParametersMessage for netId=",
					NetworkAnimator.s_AnimationParametersMessage.netId,
					" conn=",
					netMsg.conn
				}));
			}
			GameObject gameObject = NetworkServer.FindLocalObject(NetworkAnimator.s_AnimationParametersMessage.netId);
			if (gameObject == null)
			{
				return;
			}
			NetworkAnimator component = gameObject.GetComponent<NetworkAnimator>();
			if (component != null)
			{
				NetworkReader reader = new NetworkReader(NetworkAnimator.s_AnimationParametersMessage.parameters);
				component.HandleAnimParamsMsg(NetworkAnimator.s_AnimationParametersMessage, reader);
				NetworkServer.SendToReady(gameObject, 41, NetworkAnimator.s_AnimationParametersMessage);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005DC8 File Offset: 0x00003FC8
		internal static void OnAnimationTriggerServerMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<AnimationTriggerMessage>(NetworkAnimator.s_AnimationTriggerMessage);
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"OnAnimationTriggerMessage for netId=",
					NetworkAnimator.s_AnimationTriggerMessage.netId,
					" conn=",
					netMsg.conn
				}));
			}
			GameObject gameObject = NetworkServer.FindLocalObject(NetworkAnimator.s_AnimationTriggerMessage.netId);
			if (gameObject == null)
			{
				return;
			}
			NetworkAnimator component = gameObject.GetComponent<NetworkAnimator>();
			if (component != null)
			{
				component.HandleAnimTriggerMsg(NetworkAnimator.s_AnimationTriggerMessage.hash);
				NetworkServer.SendToReady(gameObject, 42, NetworkAnimator.s_AnimationTriggerMessage);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005E74 File Offset: 0x00004074
		internal static void OnAnimationClientMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<AnimationMessage>(NetworkAnimator.s_AnimationMessage);
			GameObject gameObject = ClientScene.FindLocalObject(NetworkAnimator.s_AnimationMessage.netId);
			if (gameObject == null)
			{
				return;
			}
			NetworkAnimator component = gameObject.GetComponent<NetworkAnimator>();
			if (component != null)
			{
				NetworkReader reader = new NetworkReader(NetworkAnimator.s_AnimationMessage.parameters);
				component.HandleAnimMsg(NetworkAnimator.s_AnimationMessage, reader);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005ED8 File Offset: 0x000040D8
		internal static void OnAnimationParametersClientMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<AnimationParametersMessage>(NetworkAnimator.s_AnimationParametersMessage);
			GameObject gameObject = ClientScene.FindLocalObject(NetworkAnimator.s_AnimationParametersMessage.netId);
			if (gameObject == null)
			{
				return;
			}
			NetworkAnimator component = gameObject.GetComponent<NetworkAnimator>();
			if (component != null)
			{
				NetworkReader reader = new NetworkReader(NetworkAnimator.s_AnimationParametersMessage.parameters);
				component.HandleAnimParamsMsg(NetworkAnimator.s_AnimationParametersMessage, reader);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005F3C File Offset: 0x0000413C
		internal static void OnAnimationTriggerClientMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<AnimationTriggerMessage>(NetworkAnimator.s_AnimationTriggerMessage);
			GameObject gameObject = ClientScene.FindLocalObject(NetworkAnimator.s_AnimationTriggerMessage.netId);
			if (gameObject == null)
			{
				return;
			}
			NetworkAnimator component = gameObject.GetComponent<NetworkAnimator>();
			if (component != null)
			{
				component.HandleAnimTriggerMsg(NetworkAnimator.s_AnimationTriggerMessage.hash);
			}
		}

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private Animator m_Animator;

		// Token: 0x0400008B RID: 139
		[SerializeField]
		private uint m_ParameterSendBits;

		// Token: 0x0400008C RID: 140
		private static AnimationMessage s_AnimationMessage = new AnimationMessage();

		// Token: 0x0400008D RID: 141
		private static AnimationParametersMessage s_AnimationParametersMessage = new AnimationParametersMessage();

		// Token: 0x0400008E RID: 142
		private static AnimationTriggerMessage s_AnimationTriggerMessage = new AnimationTriggerMessage();

		// Token: 0x0400008F RID: 143
		private int m_AnimationHash;

		// Token: 0x04000090 RID: 144
		private int m_TransitionHash;

		// Token: 0x04000091 RID: 145
		private NetworkWriter m_ParameterWriter;

		// Token: 0x04000092 RID: 146
		private float m_SendTimer;

		// Token: 0x04000093 RID: 147
		public string param0;

		// Token: 0x04000094 RID: 148
		public string param1;

		// Token: 0x04000095 RID: 149
		public string param2;

		// Token: 0x04000096 RID: 150
		public string param3;

		// Token: 0x04000097 RID: 151
		public string param4;

		// Token: 0x04000098 RID: 152
		public string param5;
	}
}
