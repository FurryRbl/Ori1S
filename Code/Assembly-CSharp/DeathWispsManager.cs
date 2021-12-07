using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000345 RID: 837
public class DeathWispsManager : MonoBehaviour
{
	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00066B31 File Offset: 0x00064D31
	public SeinDeathsManager DeathsManager
	{
		get
		{
			return SeinDeathsManager.Instance;
		}
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x00066B38 File Offset: 0x00064D38
	public void Awake()
	{
		DeathWispsManager.Instance = this;
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x00066B5B File Offset: 0x00064D5B
	public void OnDestroy()
	{
		DeathWispsManager.Instance = null;
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x00066B7E File Offset: 0x00064D7E
	public void OnGameReset()
	{
		DeathWispsManager.Refresh();
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x00066B88 File Offset: 0x00064D88
	public static void Refresh()
	{
		if (DeathWispsManager.Instance)
		{
			DeathWispsManager.Instance.m_lastCameraBounds = default(Bounds);
		}
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x00066BB8 File Offset: 0x00064DB8
	public void FixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (!Characters.Sein.Active)
		{
			return;
		}
		Bounds cameraBoundingBox = UI.Cameras.Current.CameraBoundingBox;
		if (Vector3.Distance(this.m_lastCameraBounds.center, cameraBoundingBox.center) > 2f || Vector3.Distance(this.m_lastCameraBounds.extents, cameraBoundingBox.extents) > 2f)
		{
			this.m_lastCameraBounds = cameraBoundingBox;
			this.UpdateWisps();
		}
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x00066C40 File Offset: 0x00064E40
	private void UpdateWisps()
	{
		this.m_leftOvers.UnionWith(this.m_wisps.Values);
		foreach (DeathInformation deathInformation in this.DeathsManager.Deaths)
		{
			bool flag = UI.Cameras.Current.IsOnScreenPadded(deathInformation.Position, 10f);
			bool flag2 = this.m_wisps.ContainsKey(deathInformation);
			if (flag2)
			{
				this.m_leftOvers.Remove(this.m_wisps[deathInformation]);
			}
			if (flag)
			{
				if (!flag2)
				{
					GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Wisp, deathInformation.Position, Quaternion.identity);
					DeathWisp component = gameObject.GetComponent<DeathWisp>();
					component.DeathInfo = deathInformation;
					this.m_wisps.Add(deathInformation, component);
				}
			}
			else if (flag2)
			{
				DeathWisp deathWisp = this.m_wisps[deathInformation];
				InstantiateUtility.Destroy(deathWisp.gameObject);
				this.m_wisps.Remove(deathWisp.DeathInfo);
			}
		}
		foreach (DeathWisp deathWisp2 in this.m_leftOvers)
		{
			if (deathWisp2)
			{
				InstantiateUtility.Destroy(deathWisp2.gameObject);
			}
		}
		this.m_leftOvers.Clear();
	}

	// Token: 0x040014A6 RID: 5286
	public GameObject Wisp;

	// Token: 0x040014A7 RID: 5287
	public MessageProvider WispMessage;

	// Token: 0x040014A8 RID: 5288
	public GameObject WispIcon;

	// Token: 0x040014A9 RID: 5289
	public static DeathWispsManager Instance;

	// Token: 0x040014AA RID: 5290
	private Dictionary<DeathInformation, DeathWisp> m_wisps = new Dictionary<DeathInformation, DeathWisp>();

	// Token: 0x040014AB RID: 5291
	private Bounds m_lastCameraBounds;

	// Token: 0x040014AC RID: 5292
	private HashSet<DeathWisp> m_leftOvers = new HashSet<DeathWisp>();

	// Token: 0x040014AD RID: 5293
	public ActionMethod CollectWispAction;

	// Token: 0x040014AE RID: 5294
	public DeathInformation Collected = new DeathInformation();
}
