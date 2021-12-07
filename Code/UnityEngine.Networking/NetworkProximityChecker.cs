using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x0200004D RID: 77
	[RequireComponent(typeof(NetworkIdentity))]
	[AddComponentMenu("Network/NetworkProximityChecker")]
	public class NetworkProximityChecker : NetworkBehaviour
	{
		// Token: 0x06000340 RID: 832 RVA: 0x000111E8 File Offset: 0x0000F3E8
		private void Update()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (Time.time - this.m_VisUpdateTime > this.visUpdateInterval)
			{
				base.GetComponent<NetworkIdentity>().RebuildObservers(false);
				this.m_VisUpdateTime = Time.time;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00011230 File Offset: 0x0000F430
		public override bool OnCheckObserver(NetworkConnection newObserver)
		{
			if (this.forceHidden)
			{
				return false;
			}
			GameObject gameObject = null;
			foreach (PlayerController playerController in newObserver.playerControllers)
			{
				if (playerController != null && playerController.gameObject != null)
				{
					gameObject = playerController.gameObject;
					break;
				}
			}
			if (gameObject == null)
			{
				return false;
			}
			Vector3 position = gameObject.transform.position;
			return (position - base.transform.position).magnitude < (float)this.visRange;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011300 File Offset: 0x0000F500
		public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initial)
		{
			if (this.forceHidden)
			{
				NetworkIdentity component = base.GetComponent<NetworkIdentity>();
				if (component.connectionToClient != null)
				{
					observers.Add(component.connectionToClient);
				}
				return true;
			}
			NetworkProximityChecker.CheckMethod checkMethod = this.checkMethod;
			if (checkMethod == NetworkProximityChecker.CheckMethod.Physics3D)
			{
				Collider[] array = Physics.OverlapSphere(base.transform.position, (float)this.visRange);
				foreach (Collider collider in array)
				{
					NetworkIdentity component2 = collider.GetComponent<NetworkIdentity>();
					if (component2 != null && component2.connectionToClient != null)
					{
						observers.Add(component2.connectionToClient);
					}
				}
				return true;
			}
			if (checkMethod != NetworkProximityChecker.CheckMethod.Physics2D)
			{
				return false;
			}
			Collider2D[] array3 = Physics2D.OverlapCircleAll(base.transform.position, (float)this.visRange);
			foreach (Collider2D collider2D in array3)
			{
				NetworkIdentity component3 = collider2D.GetComponent<NetworkIdentity>();
				if (component3 != null && component3.connectionToClient != null)
				{
					observers.Add(component3.connectionToClient);
				}
			}
			return true;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00011434 File Offset: 0x0000F634
		public override void OnSetLocalVisibility(bool vis)
		{
			NetworkProximityChecker.SetVis(base.gameObject, vis);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00011444 File Offset: 0x0000F644
		private static void SetVis(GameObject go, bool vis)
		{
			foreach (Renderer renderer in go.GetComponents<Renderer>())
			{
				renderer.enabled = vis;
			}
			for (int j = 0; j < go.transform.childCount; j++)
			{
				Transform child = go.transform.GetChild(j);
				NetworkProximityChecker.SetVis(child.gameObject, vis);
			}
		}

		// Token: 0x0400017F RID: 383
		public int visRange = 10;

		// Token: 0x04000180 RID: 384
		public float visUpdateInterval = 1f;

		// Token: 0x04000181 RID: 385
		public NetworkProximityChecker.CheckMethod checkMethod;

		// Token: 0x04000182 RID: 386
		public bool forceHidden;

		// Token: 0x04000183 RID: 387
		private float m_VisUpdateTime;

		// Token: 0x0200004E RID: 78
		public enum CheckMethod
		{
			// Token: 0x04000185 RID: 389
			Physics3D,
			// Token: 0x04000186 RID: 390
			Physics2D
		}
	}
}
