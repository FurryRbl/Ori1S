using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class ToggleAllKinematic : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x0600218E RID: 8590 RVA: 0x00093128 File Offset: 0x00091328
	public void Awake()
	{
		this.m_currentOption = 0;
	}

	// Token: 0x170005C5 RID: 1477
	// (get) Token: 0x0600218F RID: 8591 RVA: 0x00093131 File Offset: 0x00091331
	public string Name
	{
		get
		{
			return "All Kinematic";
		}
	}

	// Token: 0x170005C6 RID: 1478
	// (get) Token: 0x06002190 RID: 8592 RVA: 0x00093138 File Offset: 0x00091338
	public string HelpText
	{
		get
		{
			return "Toggle all rigid bodies from kinematic and back";
		}
	}

	// Token: 0x170005C7 RID: 1479
	// (get) Token: 0x06002191 RID: 8593 RVA: 0x0009313F File Offset: 0x0009133F
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"AllKinematic Off",
				"AllKinematic On"
			};
		}
	}

	// Token: 0x170005C8 RID: 1480
	// (get) Token: 0x06002192 RID: 8594 RVA: 0x00093157 File Offset: 0x00091357
	// (set) Token: 0x06002193 RID: 8595 RVA: 0x00093160 File Offset: 0x00091360
	public int CurrentToggleOptionId
	{
		get
		{
			return this.m_currentOption;
		}
		set
		{
			this.m_currentOption = Mathf.Abs(value) % 2;
			bool flag = this.m_currentOption == 1;
			if (flag)
			{
				Rigidbody[] array = (Rigidbody[])Resources.FindObjectsOfTypeAll(typeof(Rigidbody));
				this.m_modifiedRigidBodies.Clear();
				foreach (Rigidbody rigidbody in array)
				{
					if (!rigidbody.isKinematic)
					{
						bool flag2 = true;
						if (rigidbody.gameObject.FindComponent<Projectile>() != null)
						{
							flag2 = false;
						}
						if (rigidbody.gameObject.FindComponent<CameraChaseTarget>() != null)
						{
							flag2 = false;
						}
						if (rigidbody.gameObject.FindComponent<PlatformMovementRigidbodyMoonCharacterControllerPenetrate>() != null)
						{
							flag2 = false;
						}
						if (rigidbody.gameObject.FindComponent<CameraStraightLineMotion>() != null)
						{
							flag2 = false;
						}
						if (rigidbody.gameObject.FindComponent<EntityPlatformingMovement>() != null)
						{
							flag2 = false;
						}
						if (rigidbody.gameObject.FindComponent<RigidbodyMovement>() != null)
						{
							flag2 = false;
						}
						if (rigidbody.gameObject.FindComponent<TraceGroundMovement>() != null)
						{
							flag2 = false;
						}
						if (flag2)
						{
							rigidbody.isKinematic = true;
							this.m_modifiedRigidBodies.Add(rigidbody);
						}
					}
				}
			}
			else
			{
				foreach (Rigidbody rigidbody2 in this.m_modifiedRigidBodies)
				{
					if (rigidbody2 != null)
					{
						rigidbody2.isKinematic = false;
					}
				}
				this.m_modifiedRigidBodies.Clear();
			}
		}
	}

	// Token: 0x04001C43 RID: 7235
	private int m_currentOption;

	// Token: 0x04001C44 RID: 7236
	private List<Rigidbody> m_modifiedRigidBodies = new List<Rigidbody>();
}
