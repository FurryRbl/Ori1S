using System;
using J2i.Net.XInputWrapper;
using UnityEngine;

namespace Core.Devices
{
	// Token: 0x020003D5 RID: 981
	public class XboxControllerManager : MonoBehaviour
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x000738E5 File Offset: 0x00071AE5
		public static XboxControllerManager CurrentController
		{
			get
			{
				return XboxControllerManager.s_currentControllerManager;
			}
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000738EC File Offset: 0x00071AEC
		public void Vibrate(Vector2 magnitude, float duration)
		{
			this.m_remainingVibrationDuration = duration;
			this.m_currentVibrationMagnitude = magnitude;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000738FC File Offset: 0x00071AFC
		private void Awake()
		{
			XboxControllerManager.s_currentControllerManager = this;
			string text = Environment.GetEnvironmentVariable("PATH");
			text = text + ";" + Application.dataPath + "/xInput";
			Environment.SetEnvironmentVariable("PATH", text, EnvironmentVariableTarget.Process);
			XboxController.StartPolling();
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00073941 File Offset: 0x00071B41
		private void OnDestroy()
		{
			XboxController.StopPolling();
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00073948 File Offset: 0x00071B48
		private void FixedUpdate()
		{
			if (this.m_remainingVibrationDuration > Mathf.Epsilon)
			{
				this.m_remainingVibrationDuration -= Time.fixedDeltaTime;
			}
			else
			{
				this.m_currentVibrationMagnitude = Vector2.zero;
			}
			if (this.m_currentVibrationMagnitude != this.m_setVibration)
			{
				this.m_setVibration = this.m_currentVibrationMagnitude;
				XboxControllerManager.SetCurrentMotorVibration((uint)XboxLiveController.Instance.GetCurrentUserIndex(), this.m_currentVibrationMagnitude);
			}
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000739BE File Offset: 0x00071BBE
		private void OnApplicationQuit()
		{
			XboxControllerManager.ResetControllerVibration();
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000739C5 File Offset: 0x00071BC5
		private void OnApplicationFocus(bool focusStatus)
		{
			XboxControllerManager.ResetControllerVibration();
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000739CC File Offset: 0x00071BCC
		public static void ResetControllerVibration()
		{
			if (XboxLiveController.Instance != null)
			{
				XboxControllerManager.SetCurrentMotorVibration((uint)XboxLiveController.Instance.GetCurrentUserIndex(), Vector2.zero);
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000739FD File Offset: 0x00071BFD
		private static void SetCurrentMotorVibration(uint controllerIndex, Vector2 magnitude)
		{
			XboxController.RetrieveController((int)controllerIndex).Vibrate((double)magnitude.x, (double)magnitude.y);
		}

		// Token: 0x04001768 RID: 5992
		private static XboxControllerManager s_currentControllerManager;

		// Token: 0x04001769 RID: 5993
		private Vector2 m_currentVibrationMagnitude = Vector2.zero;

		// Token: 0x0400176A RID: 5994
		private Vector2 m_setVibration = Vector2.zero;

		// Token: 0x0400176B RID: 5995
		private float m_remainingVibrationDuration;

		// Token: 0x0400176C RID: 5996
		public static XboxControllerManager.SetVibrationDelegate SetVibrationCallback;

		// Token: 0x020008A9 RID: 2217
		// (Invoke) Token: 0x06003185 RID: 12677
		public delegate void SetVibrationDelegate(uint controllerIndex, Vector2 magnitude);
	}
}
