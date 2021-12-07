using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines a set of commonly used security identifiers (SIDs).</summary>
	// Token: 0x02000668 RID: 1640
	[ComVisible(false)]
	public enum WellKnownSidType
	{
		/// <summary>Indicates a null SID.</summary>
		// Token: 0x04001AD2 RID: 6866
		NullSid,
		/// <summary>Indicates a SID that matches everyone.</summary>
		// Token: 0x04001AD3 RID: 6867
		WorldSid,
		/// <summary>Indicates a local SID.</summary>
		// Token: 0x04001AD4 RID: 6868
		LocalSid,
		/// <summary>Indicates a SID that matches the owner or creator of an object.</summary>
		// Token: 0x04001AD5 RID: 6869
		CreatorOwnerSid,
		/// <summary>Indicates a SID that matches the creator group of an object.</summary>
		// Token: 0x04001AD6 RID: 6870
		CreatorGroupSid,
		/// <summary>Indicates a creator owner server SID.</summary>
		// Token: 0x04001AD7 RID: 6871
		CreatorOwnerServerSid,
		/// <summary>Indicates a creator group server SID.</summary>
		// Token: 0x04001AD8 RID: 6872
		CreatorGroupServerSid,
		/// <summary>Indicates a SID for the Windows NT authority.</summary>
		// Token: 0x04001AD9 RID: 6873
		NTAuthoritySid,
		/// <summary>Indicates a SID for a dial-up account.</summary>
		// Token: 0x04001ADA RID: 6874
		DialupSid,
		/// <summary>Indicates a SID for a network account. This SID is added to the process of a token when it logs on across a network.</summary>
		// Token: 0x04001ADB RID: 6875
		NetworkSid,
		/// <summary>Indicates a SID for a batch process. This SID is added to the process of a token when it logs on as a batch job.</summary>
		// Token: 0x04001ADC RID: 6876
		BatchSid,
		/// <summary>Indicates a SID for an interactive account. This SID is added to the process of a token when it logs on interactively.</summary>
		// Token: 0x04001ADD RID: 6877
		InteractiveSid,
		/// <summary>Indicates a SID for a service. This SID is added to the process of a token when it logs on as a service.</summary>
		// Token: 0x04001ADE RID: 6878
		ServiceSid,
		/// <summary>Indicates a SID for the anonymous account.</summary>
		// Token: 0x04001ADF RID: 6879
		AnonymousSid,
		/// <summary>Indicates a proxy SID.</summary>
		// Token: 0x04001AE0 RID: 6880
		ProxySid,
		/// <summary>Indicates a SID for an enterprise controller.</summary>
		// Token: 0x04001AE1 RID: 6881
		EnterpriseControllersSid,
		/// <summary>Indicates a SID for self.</summary>
		// Token: 0x04001AE2 RID: 6882
		SelfSid,
		/// <summary>Indicates a SID for an authenticated user.</summary>
		// Token: 0x04001AE3 RID: 6883
		AuthenticatedUserSid,
		/// <summary>Indicates a SID for restricted code.</summary>
		// Token: 0x04001AE4 RID: 6884
		RestrictedCodeSid,
		/// <summary>Indicates a SID that matches a terminal server account.</summary>
		// Token: 0x04001AE5 RID: 6885
		TerminalServerSid,
		/// <summary>Indicates a SID that matches remote logons.</summary>
		// Token: 0x04001AE6 RID: 6886
		RemoteLogonIdSid,
		/// <summary>Indicates a SID that matches logon IDs.</summary>
		// Token: 0x04001AE7 RID: 6887
		LogonIdsSid,
		/// <summary>Indicates a SID that matches the local system.</summary>
		// Token: 0x04001AE8 RID: 6888
		LocalSystemSid,
		/// <summary>Indicates a SID that matches a local service.</summary>
		// Token: 0x04001AE9 RID: 6889
		LocalServiceSid,
		/// <summary>Indicates a SID that matches a network service.</summary>
		// Token: 0x04001AEA RID: 6890
		NetworkServiceSid,
		/// <summary>Indicates a SID that matches the domain account.</summary>
		// Token: 0x04001AEB RID: 6891
		BuiltinDomainSid,
		/// <summary>Indicates a SID that matches the administrator account.</summary>
		// Token: 0x04001AEC RID: 6892
		BuiltinAdministratorsSid,
		/// <summary>Indicates a SID that matches built-in user accounts.</summary>
		// Token: 0x04001AED RID: 6893
		BuiltinUsersSid,
		/// <summary>Indicates a SID that matches the guest account.</summary>
		// Token: 0x04001AEE RID: 6894
		BuiltinGuestsSid,
		/// <summary>Indicates a SID that matches the power users group.</summary>
		// Token: 0x04001AEF RID: 6895
		BuiltinPowerUsersSid,
		/// <summary>Indicates a SID that matches the account operators account.</summary>
		// Token: 0x04001AF0 RID: 6896
		BuiltinAccountOperatorsSid,
		/// <summary>Indicates a SID that matches the system operators group.</summary>
		// Token: 0x04001AF1 RID: 6897
		BuiltinSystemOperatorsSid,
		/// <summary>Indicates a SID that matches the print operators group.</summary>
		// Token: 0x04001AF2 RID: 6898
		BuiltinPrintOperatorsSid,
		/// <summary>Indicates a SID that matches the backup operators group.</summary>
		// Token: 0x04001AF3 RID: 6899
		BuiltinBackupOperatorsSid,
		/// <summary>Indicates a SID that matches the replicator account.</summary>
		// Token: 0x04001AF4 RID: 6900
		BuiltinReplicatorSid,
		/// <summary>Indicates a SID that matches pre-Windows 2000 compatible accounts.</summary>
		// Token: 0x04001AF5 RID: 6901
		BuiltinPreWindows2000CompatibleAccessSid,
		/// <summary>Indicates a SID that matches remote desktop users.</summary>
		// Token: 0x04001AF6 RID: 6902
		BuiltinRemoteDesktopUsersSid,
		/// <summary>Indicates a SID that matches the network operators group.</summary>
		// Token: 0x04001AF7 RID: 6903
		BuiltinNetworkConfigurationOperatorsSid,
		/// <summary>Indicates a SID that matches the account administrators group.</summary>
		// Token: 0x04001AF8 RID: 6904
		AccountAdministratorSid,
		/// <summary>Indicates a SID that matches the account guest group.</summary>
		// Token: 0x04001AF9 RID: 6905
		AccountGuestSid,
		/// <summary>Indicates a SID that matches the account Kerberos target group.</summary>
		// Token: 0x04001AFA RID: 6906
		AccountKrbtgtSid,
		/// <summary>Indicates a SID that matches the account domain administrator group.</summary>
		// Token: 0x04001AFB RID: 6907
		AccountDomainAdminsSid,
		/// <summary>Indicates a SID that matches the account domain users group.</summary>
		// Token: 0x04001AFC RID: 6908
		AccountDomainUsersSid,
		/// <summary>Indicates a SID that matches the account domain guests group.</summary>
		// Token: 0x04001AFD RID: 6909
		AccountDomainGuestsSid,
		/// <summary>Indicates a SID that matches the account computer group.</summary>
		// Token: 0x04001AFE RID: 6910
		AccountComputersSid,
		/// <summary>Indicates a SID that matches the account controller group.</summary>
		// Token: 0x04001AFF RID: 6911
		AccountControllersSid,
		/// <summary>Indicates a SID that matches the certificate administrators group.</summary>
		// Token: 0x04001B00 RID: 6912
		AccountCertAdminsSid,
		/// <summary>Indicates a SID that matches the schema administrators group.</summary>
		// Token: 0x04001B01 RID: 6913
		AccountSchemaAdminsSid,
		/// <summary>Indicates a SID that matches the enterprise administrators group.</summary>
		// Token: 0x04001B02 RID: 6914
		AccountEnterpriseAdminsSid,
		/// <summary>Indicates a SID that matches the policy administrators group.</summary>
		// Token: 0x04001B03 RID: 6915
		AccountPolicyAdminsSid,
		/// <summary>Indicates a SID that matches the RAS and IAS server account.</summary>
		// Token: 0x04001B04 RID: 6916
		AccountRasAndIasServersSid,
		/// <summary>Indicates a SID present when the Microsoft NTLM authentication package authenticated the client.</summary>
		// Token: 0x04001B05 RID: 6917
		NtlmAuthenticationSid,
		/// <summary>Indicates a SID present when the Microsoft Digest authentication package authenticated the client.</summary>
		// Token: 0x04001B06 RID: 6918
		DigestAuthenticationSid,
		/// <summary>Indicates a SID present when the Secure Channel (SSL/TLS) authentication package authenticated the client.</summary>
		// Token: 0x04001B07 RID: 6919
		SChannelAuthenticationSid,
		/// <summary>Indicates a SID present when the user authenticated from within the forest or across a trust that does not have the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.OtherOrganizationSid" /> cannot be present.</summary>
		// Token: 0x04001B08 RID: 6920
		ThisOrganizationSid,
		/// <summary>Indicates a SID present when the user authenticated across a forest with the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.ThisOrganizationSid" /> cannot be present.</summary>
		// Token: 0x04001B09 RID: 6921
		OtherOrganizationSid,
		/// <summary>Indicates a SID that allows a user to create incoming forest trusts. It is added to the token of users who are a member of the Incoming Forest Trust Builders built-in group in the root domain of the forest.</summary>
		// Token: 0x04001B0A RID: 6922
		BuiltinIncomingForestTrustBuildersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to schedule logging of performance counters on this computer.</summary>
		// Token: 0x04001B0B RID: 6923
		BuiltinPerformanceMonitoringUsersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to monitor the computer.</summary>
		// Token: 0x04001B0C RID: 6924
		BuiltinPerformanceLoggingUsersSid,
		/// <summary>Indicates a SID that matches the Windows Authorization Access group.</summary>
		// Token: 0x04001B0D RID: 6925
		BuiltinAuthorizationAccessSid,
		/// <summary>Indicates a SID is present in a server that can issue Terminal Server licenses.</summary>
		// Token: 0x04001B0E RID: 6926
		WinBuiltinTerminalServerLicenseServersSid,
		/// <summary>Indicates the maximum defined SID in the <see cref="T:System.Security.Principal.WellKnownSidType" /> enumeration.</summary>
		// Token: 0x04001B0F RID: 6927
		MaxDefined = 60
	}
}
