using System;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x020001CA RID: 458
	public interface IAnimatorControllerPlayable
	{
		// Token: 0x06001B62 RID: 7010
		float GetFloat(string name);

		// Token: 0x06001B63 RID: 7011
		float GetFloat(int id);

		// Token: 0x06001B64 RID: 7012
		void SetFloat(string name, float value);

		// Token: 0x06001B65 RID: 7013
		void SetFloat(int id, float value);

		// Token: 0x06001B66 RID: 7014
		bool GetBool(string name);

		// Token: 0x06001B67 RID: 7015
		bool GetBool(int id);

		// Token: 0x06001B68 RID: 7016
		void SetBool(string name, bool value);

		// Token: 0x06001B69 RID: 7017
		void SetBool(int id, bool value);

		// Token: 0x06001B6A RID: 7018
		int GetInteger(string name);

		// Token: 0x06001B6B RID: 7019
		int GetInteger(int id);

		// Token: 0x06001B6C RID: 7020
		void SetInteger(string name, int value);

		// Token: 0x06001B6D RID: 7021
		void SetInteger(int id, int value);

		// Token: 0x06001B6E RID: 7022
		void SetTrigger(string name);

		// Token: 0x06001B6F RID: 7023
		void SetTrigger(int id);

		// Token: 0x06001B70 RID: 7024
		void ResetTrigger(string name);

		// Token: 0x06001B71 RID: 7025
		void ResetTrigger(int id);

		// Token: 0x06001B72 RID: 7026
		bool IsParameterControlledByCurve(string name);

		// Token: 0x06001B73 RID: 7027
		bool IsParameterControlledByCurve(int id);

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001B74 RID: 7028
		int layerCount { get; }

		// Token: 0x06001B75 RID: 7029
		string GetLayerName(int layerIndex);

		// Token: 0x06001B76 RID: 7030
		int GetLayerIndex(string layerName);

		// Token: 0x06001B77 RID: 7031
		float GetLayerWeight(int layerIndex);

		// Token: 0x06001B78 RID: 7032
		void SetLayerWeight(int layerIndex, float weight);

		// Token: 0x06001B79 RID: 7033
		AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex);

		// Token: 0x06001B7A RID: 7034
		AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex);

		// Token: 0x06001B7B RID: 7035
		AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex);

		// Token: 0x06001B7C RID: 7036
		AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex);

		// Token: 0x06001B7D RID: 7037
		AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex);

		// Token: 0x06001B7E RID: 7038
		bool IsInTransition(int layerIndex);

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001B7F RID: 7039
		int parameterCount { get; }

		// Token: 0x06001B80 RID: 7040
		AnimatorControllerParameter GetParameter(int index);

		// Token: 0x06001B81 RID: 7041
		void CrossFadeInFixedTime(string stateName, float transitionDuration, int layer, float fixedTime);

		// Token: 0x06001B82 RID: 7042
		void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, int layer, float fixedTime);

		// Token: 0x06001B83 RID: 7043
		void CrossFade(string stateName, float transitionDuration, int layer, float normalizedTime);

		// Token: 0x06001B84 RID: 7044
		void CrossFade(int stateNameHash, float transitionDuration, int layer, float normalizedTime);

		// Token: 0x06001B85 RID: 7045
		void PlayInFixedTime(string stateName, int layer, float fixedTime);

		// Token: 0x06001B86 RID: 7046
		void PlayInFixedTime(int stateNameHash, int layer, float fixedTime);

		// Token: 0x06001B87 RID: 7047
		void Play(string stateName, int layer, float normalizedTime);

		// Token: 0x06001B88 RID: 7048
		void Play(int stateNameHash, int layer, float normalizedTime);

		// Token: 0x06001B89 RID: 7049
		bool HasState(int layerIndex, int stateID);
	}
}
