using System;
using UnityEngine.Experimental.Director;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000309 RID: 777
	[RequiredByNativeCode]
	public abstract class StateMachineBehaviour : ScriptableObject
	{
		// Token: 0x060026FF RID: 9983 RVA: 0x00036E6C File Offset: 0x0003506C
		public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x00036E70 File Offset: 0x00035070
		public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00036E74 File Offset: 0x00035074
		public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x00036E78 File Offset: 0x00035078
		public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x00036E7C File Offset: 0x0003507C
		public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x00036E80 File Offset: 0x00035080
		public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
		{
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x00036E84 File Offset: 0x00035084
		public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash)
		{
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x00036E88 File Offset: 0x00035088
		public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x00036E8C File Offset: 0x0003508C
		public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x00036E90 File Offset: 0x00035090
		public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x00036E94 File Offset: 0x00035094
		public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x00036E98 File Offset: 0x00035098
		public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x00036E9C File Offset: 0x0003509C
		public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
		{
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x00036EA0 File Offset: 0x000350A0
		public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
		{
		}
	}
}
