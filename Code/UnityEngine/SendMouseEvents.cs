using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002E5 RID: 741
	internal class SendMouseEvents
	{
		// Token: 0x06002676 RID: 9846 RVA: 0x00035DC8 File Offset: 0x00033FC8
		[RequiredByNativeCode]
		private static void SetMouseMoved()
		{
			SendMouseEvents.s_MouseUsed = true;
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x00035DD0 File Offset: 0x00033FD0
		[RequiredByNativeCode]
		private static void DoSendMouseEvents(int skipRTCameras)
		{
			Vector3 mousePosition = Input.mousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			if (SendMouseEvents.m_Cameras == null || SendMouseEvents.m_Cameras.Length != allCamerasCount)
			{
				SendMouseEvents.m_Cameras = new Camera[allCamerasCount];
			}
			Camera.GetAllCameras(SendMouseEvents.m_Cameras);
			for (int i = 0; i < SendMouseEvents.m_CurrentHit.Length; i++)
			{
				SendMouseEvents.m_CurrentHit[i] = default(SendMouseEvents.HitInfo);
			}
			if (!SendMouseEvents.s_MouseUsed)
			{
				foreach (Camera camera in SendMouseEvents.m_Cameras)
				{
					if (!(camera == null) && (skipRTCameras == 0 || !(camera.targetTexture != null)))
					{
						if (camera.pixelRect.Contains(mousePosition))
						{
							GUILayer component = camera.GetComponent<GUILayer>();
							if (component)
							{
								GUIElement guielement = component.HitTest(mousePosition);
								if (guielement)
								{
									SendMouseEvents.m_CurrentHit[0].target = guielement.gameObject;
									SendMouseEvents.m_CurrentHit[0].camera = camera;
								}
								else
								{
									SendMouseEvents.m_CurrentHit[0].target = null;
									SendMouseEvents.m_CurrentHit[0].camera = null;
								}
							}
							if (camera.eventMask != 0)
							{
								Ray ray = camera.ScreenPointToRay(mousePosition);
								float z = ray.direction.z;
								float distance = (!Mathf.Approximately(0f, z)) ? Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z) : float.PositiveInfinity;
								GameObject gameObject = camera.RaycastTry(ray, distance, camera.cullingMask & camera.eventMask);
								if (gameObject != null)
								{
									SendMouseEvents.m_CurrentHit[1].target = gameObject;
									SendMouseEvents.m_CurrentHit[1].camera = camera;
								}
								else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
								{
									SendMouseEvents.m_CurrentHit[1].target = null;
									SendMouseEvents.m_CurrentHit[1].camera = null;
								}
								GameObject gameObject2 = camera.RaycastTry2D(ray, distance, camera.cullingMask & camera.eventMask);
								if (gameObject2 != null)
								{
									SendMouseEvents.m_CurrentHit[2].target = gameObject2;
									SendMouseEvents.m_CurrentHit[2].camera = camera;
								}
								else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
								{
									SendMouseEvents.m_CurrentHit[2].target = null;
									SendMouseEvents.m_CurrentHit[2].camera = null;
								}
							}
						}
					}
				}
			}
			for (int k = 0; k < SendMouseEvents.m_CurrentHit.Length; k++)
			{
				SendMouseEvents.SendEvents(k, SendMouseEvents.m_CurrentHit[k]);
			}
			SendMouseEvents.s_MouseUsed = false;
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000360DC File Offset: 0x000342DC
		private static void SendEvents(int i, SendMouseEvents.HitInfo hit)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			bool mouseButton = Input.GetMouseButton(0);
			if (mouseButtonDown)
			{
				if (hit)
				{
					SendMouseEvents.m_MouseDownHit[i] = hit;
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else if (!mouseButton)
			{
				if (SendMouseEvents.m_MouseDownHit[i])
				{
					if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_MouseDownHit[i]))
					{
						SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
					}
					SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseUp");
					SendMouseEvents.m_MouseDownHit[i] = default(SendMouseEvents.HitInfo);
				}
			}
			else if (SendMouseEvents.m_MouseDownHit[i])
			{
				SendMouseEvents.m_MouseDownHit[i].SendMessage("OnMouseDrag");
			}
			if (SendMouseEvents.HitInfo.Compare(hit, SendMouseEvents.m_LastHit[i]))
			{
				if (hit)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				if (SendMouseEvents.m_LastHit[i])
				{
					SendMouseEvents.m_LastHit[i].SendMessage("OnMouseExit");
				}
				if (hit)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			SendMouseEvents.m_LastHit[i] = hit;
		}

		// Token: 0x04000BD1 RID: 3025
		private const int m_HitIndexGUI = 0;

		// Token: 0x04000BD2 RID: 3026
		private const int m_HitIndexPhysics3D = 1;

		// Token: 0x04000BD3 RID: 3027
		private const int m_HitIndexPhysics2D = 2;

		// Token: 0x04000BD4 RID: 3028
		private static bool s_MouseUsed = false;

		// Token: 0x04000BD5 RID: 3029
		private static readonly SendMouseEvents.HitInfo[] m_LastHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x04000BD6 RID: 3030
		private static readonly SendMouseEvents.HitInfo[] m_MouseDownHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x04000BD7 RID: 3031
		private static readonly SendMouseEvents.HitInfo[] m_CurrentHit = new SendMouseEvents.HitInfo[]
		{
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo),
			default(SendMouseEvents.HitInfo)
		};

		// Token: 0x04000BD8 RID: 3032
		private static Camera[] m_Cameras;

		// Token: 0x020002E6 RID: 742
		private struct HitInfo
		{
			// Token: 0x06002679 RID: 9849 RVA: 0x00036280 File Offset: 0x00034480
			public void SendMessage(string name)
			{
				this.target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			// Token: 0x0600267A RID: 9850 RVA: 0x00036290 File Offset: 0x00034490
			public static bool Compare(SendMouseEvents.HitInfo lhs, SendMouseEvents.HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}

			// Token: 0x0600267B RID: 9851 RVA: 0x000362CC File Offset: 0x000344CC
			public static implicit operator bool(SendMouseEvents.HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			// Token: 0x04000BD9 RID: 3033
			public GameObject target;

			// Token: 0x04000BDA RID: 3034
			public Camera camera;
		}
	}
}
