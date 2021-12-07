using System;

// Token: 0x0200023D RID: 573
public class UserLicenseInformation
{
	// Token: 0x060012F4 RID: 4852 RVA: 0x00057DAC File Offset: 0x00055FAC
	private UserLicenseInformation()
	{
		this.OnLicenseChanged();
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00057DC6 File Offset: 0x00055FC6
	public static bool IsFullVersion
	{
		get
		{
			return UserLicenseInformation.Instance.m_isActive && !UserLicenseInformation.Instance.m_isTrial;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00057DE7 File Offset: 0x00055FE7
	public static bool IsTrial
	{
		get
		{
			return UserLicenseInformation.Instance.m_isActive && UserLicenseInformation.Instance.m_isTrial;
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00057E05 File Offset: 0x00056005
	public static bool IsTrialUsed
	{
		get
		{
			return !UserLicenseInformation.Instance.m_isActive && UserLicenseInformation.Instance.m_isTrial;
		}
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x00057E23 File Offset: 0x00056023
	private void OnLicenseChanged()
	{
		this.m_isActive = true;
		this.m_isTrial = false;
	}

	// Token: 0x040010AB RID: 4267
	private static readonly UserLicenseInformation Instance = new UserLicenseInformation();

	// Token: 0x040010AC RID: 4268
	private bool m_isActive;

	// Token: 0x040010AD RID: 4269
	private bool m_isTrial;
}
