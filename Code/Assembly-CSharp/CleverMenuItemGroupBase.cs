using System;
using UnityEngine;

// Token: 0x02000107 RID: 263
public abstract class CleverMenuItemGroupBase : MonoBehaviour, ISuspendable
{
	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000A38 RID: 2616
	// (set) Token: 0x06000A39 RID: 2617
	public abstract bool IsVisible { get; set; }

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000A3A RID: 2618
	public abstract bool CanBeEntered { get; }

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002C458 File Offset: 0x0002A658
	// (set) Token: 0x06000A3C RID: 2620 RVA: 0x0002C460 File Offset: 0x0002A660
	public bool IsSuspended { get; set; }

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000A3D RID: 2621
	// (set) Token: 0x06000A3E RID: 2622
	public abstract bool IsActive { get; set; }

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06000A3F RID: 2623
	// (set) Token: 0x06000A40 RID: 2624
	public abstract bool IsHighlightVisible { get; set; }

	// Token: 0x06000A41 RID: 2625 RVA: 0x0002C469 File Offset: 0x0002A669
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0002C471 File Offset: 0x0002A671
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06000A43 RID: 2627
	public abstract void EnterInGroup();

	// Token: 0x06000A44 RID: 2628
	public abstract bool OnMenuItemChangedInGroup(CleverMenuItemGroup group);

	// Token: 0x04000864 RID: 2148
	public Action OnBackPressed = delegate()
	{
	};
}
