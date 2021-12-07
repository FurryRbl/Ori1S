using System;
using Core;
using UnityEngine;

// Token: 0x020004C5 RID: 1221
public class DebugMenuHandler : MonoBehaviour
{
	// Token: 0x06002120 RID: 8480 RVA: 0x000913D8 File Offset: 0x0008F5D8
	public void FixedUpdate()
	{
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		if ((CheatsHandler.Instance.DebugEnabled || CheatsHandler.DebugAlwaysEnabled) && Core.Input.RightStick.OnPressed)
		{
			if (Core.Input.Grab.Pressed)
			{
				DebugMenuB.MakeDebugMenuExist();
				if (DebugMenuB.Instance)
				{
					HierarchyDebugMenu component = DebugMenuB.Instance.GetComponent<HierarchyDebugMenu>();
					if (component)
					{
						component.enabled = true;
					}
				}
			}
			else
			{
				DebugMenuB.ToggleDebugMenu();
			}
		}
	}
}
