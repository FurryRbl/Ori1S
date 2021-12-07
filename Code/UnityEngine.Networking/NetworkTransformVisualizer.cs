using System;
using System.ComponentModel;

namespace UnityEngine.Networking
{
	// Token: 0x0200005B RID: 91
	[RequireComponent(typeof(NetworkTransform))]
	[DisallowMultipleComponent]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[AddComponentMenu("Network/NetworkTransformVisualizer")]
	public class NetworkTransformVisualizer : NetworkBehaviour
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00018A54 File Offset: 0x00016C54
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00018A5C File Offset: 0x00016C5C
		public GameObject visualizerPrefab
		{
			get
			{
				return this.m_VisualizerPrefab;
			}
			set
			{
				this.m_VisualizerPrefab = value;
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00018A68 File Offset: 0x00016C68
		public override void OnStartClient()
		{
			if (this.m_VisualizerPrefab != null)
			{
				this.m_NetworkTransform = base.GetComponent<NetworkTransform>();
				NetworkTransformVisualizer.CreateLineMaterial();
				this.m_Visualizer = (GameObject)Object.Instantiate(this.m_VisualizerPrefab, base.transform.position, Quaternion.identity);
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00018AC0 File Offset: 0x00016CC0
		public override void OnStartLocalPlayer()
		{
			if (this.m_Visualizer == null)
			{
				return;
			}
			if (this.m_NetworkTransform.localPlayerAuthority || base.isServer)
			{
				Object.Destroy(this.m_Visualizer);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00018B08 File Offset: 0x00016D08
		private void OnDestroy()
		{
			if (this.m_Visualizer != null)
			{
				Object.Destroy(this.m_Visualizer);
			}
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00018B28 File Offset: 0x00016D28
		[ClientCallback]
		private void FixedUpdate()
		{
			if (this.m_Visualizer == null)
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
			if (base.hasAuthority && this.m_NetworkTransform.localPlayerAuthority)
			{
				return;
			}
			this.m_Visualizer.transform.position = this.m_NetworkTransform.targetSyncPosition;
			if (this.m_NetworkTransform.rigidbody3D != null && this.m_Visualizer.GetComponent<Rigidbody>() != null)
			{
				this.m_Visualizer.GetComponent<Rigidbody>().velocity = this.m_NetworkTransform.targetSyncVelocity;
			}
			if (this.m_NetworkTransform.rigidbody2D != null && this.m_Visualizer.GetComponent<Rigidbody2D>() != null)
			{
				this.m_Visualizer.GetComponent<Rigidbody2D>().velocity = this.m_NetworkTransform.targetSyncVelocity;
			}
			Quaternion rotation = Quaternion.identity;
			if (this.m_NetworkTransform.rigidbody3D != null)
			{
				rotation = this.m_NetworkTransform.targetSyncRotation3D;
			}
			if (this.m_NetworkTransform.rigidbody2D != null)
			{
				rotation = Quaternion.Euler(0f, 0f, this.m_NetworkTransform.targetSyncRotation2D);
			}
			this.m_Visualizer.transform.rotation = rotation;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00018CA8 File Offset: 0x00016EA8
		private void OnRenderObject()
		{
			if (this.m_Visualizer == null)
			{
				return;
			}
			if (this.m_NetworkTransform.localPlayerAuthority && base.hasAuthority)
			{
				return;
			}
			if (this.m_NetworkTransform.lastSyncTime == 0f)
			{
				return;
			}
			NetworkTransformVisualizer.s_LineMaterial.SetPass(0);
			GL.Begin(1);
			GL.Color(Color.white);
			GL.Vertex3(base.transform.position.x, base.transform.position.y, base.transform.position.z);
			GL.Vertex3(this.m_NetworkTransform.targetSyncPosition.x, this.m_NetworkTransform.targetSyncPosition.y, this.m_NetworkTransform.targetSyncPosition.z);
			GL.End();
			this.DrawRotationInterpolation();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00018DA0 File Offset: 0x00016FA0
		private void DrawRotationInterpolation()
		{
			Quaternion quaternion = Quaternion.identity;
			if (this.m_NetworkTransform.rigidbody3D != null)
			{
				quaternion = this.m_NetworkTransform.targetSyncRotation3D;
			}
			if (this.m_NetworkTransform.rigidbody2D != null)
			{
				quaternion = Quaternion.Euler(0f, 0f, this.m_NetworkTransform.targetSyncRotation2D);
			}
			if (quaternion == Quaternion.identity)
			{
				return;
			}
			GL.Begin(1);
			GL.Color(Color.yellow);
			GL.Vertex3(base.transform.position.x, base.transform.position.y, base.transform.position.z);
			Vector3 vector = base.transform.position + base.transform.right;
			GL.Vertex3(vector.x, vector.y, vector.z);
			GL.End();
			GL.Begin(1);
			GL.Color(Color.green);
			GL.Vertex3(base.transform.position.x, base.transform.position.y, base.transform.position.z);
			Vector3 b = quaternion * Vector3.right;
			Vector3 vector2 = base.transform.position + b;
			GL.Vertex3(vector2.x, vector2.y, vector2.z);
			GL.End();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00018F34 File Offset: 0x00017134
		private static void CreateLineMaterial()
		{
			if (NetworkTransformVisualizer.s_LineMaterial)
			{
				return;
			}
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			if (!shader)
			{
				Debug.LogWarning("Could not find Colored builtin shader");
				return;
			}
			NetworkTransformVisualizer.s_LineMaterial = new Material(shader);
			NetworkTransformVisualizer.s_LineMaterial.hideFlags = HideFlags.HideAndDontSave;
			NetworkTransformVisualizer.s_LineMaterial.SetInt("_ZWrite", 0);
		}

		// Token: 0x040001F5 RID: 501
		[SerializeField]
		private GameObject m_VisualizerPrefab;

		// Token: 0x040001F6 RID: 502
		private NetworkTransform m_NetworkTransform;

		// Token: 0x040001F7 RID: 503
		private GameObject m_Visualizer;

		// Token: 0x040001F8 RID: 504
		private static Material s_LineMaterial;
	}
}
