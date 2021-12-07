using System;
using UnityEngine;

// Token: 0x0200027E RID: 638
public class XboxOneUsers
{
	// Token: 0x06001509 RID: 5385 RVA: 0x0005E416 File Offset: 0x0005C616
	public static bool CurrentUserControllerMatch()
	{
		return true;
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x0600150A RID: 5386 RVA: 0x0005E419 File Offset: 0x0005C619
	// (set) Token: 0x0600150B RID: 5387 RVA: 0x0005E420 File Offset: 0x0005C620
	public static Action<int> OnUserWillChange { get; set; }

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x0600150C RID: 5388 RVA: 0x0005E428 File Offset: 0x0005C628
	// (set) Token: 0x0600150D RID: 5389 RVA: 0x0005E42F File Offset: 0x0005C62F
	public static Action OnUserChanged { get; set; }

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x0600150E RID: 5390 RVA: 0x0005E437 File Offset: 0x0005C637
	// (set) Token: 0x0600150F RID: 5391 RVA: 0x0005E43E File Offset: 0x0005C63E
	public static Action OnUserPicked { get; set; }

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x06001510 RID: 5392 RVA: 0x0005E446 File Offset: 0x0005C646
	// (set) Token: 0x06001511 RID: 5393 RVA: 0x0005E44D File Offset: 0x0005C64D
	public static Action OnUserSignedOut { get; set; }

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x06001512 RID: 5394 RVA: 0x0005E455 File Offset: 0x0005C655
	// (set) Token: 0x06001513 RID: 5395 RVA: 0x0005E45C File Offset: 0x0005C65C
	public static Action OnLoginCancel { get; set; }

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x06001514 RID: 5396 RVA: 0x0005E464 File Offset: 0x0005C664
	public static bool CanViewProfiles
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06001515 RID: 5397 RVA: 0x0005E467 File Offset: 0x0005C667
	public static string CurrentUserHandle
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06001516 RID: 5398 RVA: 0x0005E46A File Offset: 0x0005C66A
	public static bool HasCurrentUser
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x06001517 RID: 5399 RVA: 0x0005E46D File Offset: 0x0005C66D
	public static string CurrentUserID
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x06001518 RID: 5400 RVA: 0x0005E470 File Offset: 0x0005C670
	public static int CurrentUserLocalID
	{
		get
		{
			return -1;
		}
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x06001519 RID: 5401 RVA: 0x0005E473 File Offset: 0x0005C673
	public static Texture2D CurrentUserPicture
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x0600151A RID: 5402 RVA: 0x0005E476 File Offset: 0x0005C676
	// (set) Token: 0x0600151B RID: 5403 RVA: 0x0005E479 File Offset: 0x0005C679
	public static bool AutoSwitchUsers
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x0600151C RID: 5404 RVA: 0x0005E47B File Offset: 0x0005C67B
	public static bool ResolvingUser
	{
		get
		{
			return false;
		}
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x0005E47E File Offset: 0x0005C67E
	public static bool ClearUserCachedData()
	{
		return false;
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x0005E481 File Offset: 0x0005C681
	public static bool RequireUser(Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x0005E484 File Offset: 0x0005C684
	public static bool RequestUser(Action success = null, Action failure = null)
	{
		return false;
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x0005E487 File Offset: 0x0005C687
	public static void SignOutCurrentUser()
	{
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x0005E489 File Offset: 0x0005C689
	public static bool UpdateUserPicture(Action success = null)
	{
		return false;
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x0005E48C File Offset: 0x0005C68C
	public static bool ShowProfileCard(string userID)
	{
		return false;
	}
}
