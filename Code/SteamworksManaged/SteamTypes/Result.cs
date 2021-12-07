using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000084 RID: 132
	public enum Result
	{
		// Token: 0x04000218 RID: 536
		OK = 1,
		// Token: 0x04000219 RID: 537
		Fail,
		// Token: 0x0400021A RID: 538
		NoConnection,
		// Token: 0x0400021B RID: 539
		InvalidPassword = 5,
		// Token: 0x0400021C RID: 540
		LoggedInElsewhere,
		// Token: 0x0400021D RID: 541
		InvalidProtocolVer,
		// Token: 0x0400021E RID: 542
		InvalidParam,
		// Token: 0x0400021F RID: 543
		FileNotFound,
		// Token: 0x04000220 RID: 544
		Busy,
		// Token: 0x04000221 RID: 545
		InvalidState,
		// Token: 0x04000222 RID: 546
		InvalidName,
		// Token: 0x04000223 RID: 547
		InvalidEmail,
		// Token: 0x04000224 RID: 548
		DuplicateName,
		// Token: 0x04000225 RID: 549
		AccessDenied,
		// Token: 0x04000226 RID: 550
		Timeout,
		// Token: 0x04000227 RID: 551
		Banned,
		// Token: 0x04000228 RID: 552
		AccountNotFound,
		// Token: 0x04000229 RID: 553
		InvalidSteamID,
		// Token: 0x0400022A RID: 554
		ServiceUnavailable,
		// Token: 0x0400022B RID: 555
		NotLoggedOn,
		// Token: 0x0400022C RID: 556
		Pending,
		// Token: 0x0400022D RID: 557
		EncryptionFailure,
		// Token: 0x0400022E RID: 558
		InsufficientPrivilege,
		// Token: 0x0400022F RID: 559
		LimitExceeded,
		// Token: 0x04000230 RID: 560
		Revoked,
		// Token: 0x04000231 RID: 561
		Expired,
		// Token: 0x04000232 RID: 562
		AlreadyRedeemed,
		// Token: 0x04000233 RID: 563
		DuplicateRequest,
		// Token: 0x04000234 RID: 564
		AlreadyOwned,
		// Token: 0x04000235 RID: 565
		IPNotFound,
		// Token: 0x04000236 RID: 566
		PersistFailed,
		// Token: 0x04000237 RID: 567
		LockingFailed,
		// Token: 0x04000238 RID: 568
		LogonSessionReplaced,
		// Token: 0x04000239 RID: 569
		ConnectFailed,
		// Token: 0x0400023A RID: 570
		HandshakeFailed,
		// Token: 0x0400023B RID: 571
		IOFailure,
		// Token: 0x0400023C RID: 572
		RemoteDisconnect,
		// Token: 0x0400023D RID: 573
		ShoppingCartNotFound,
		// Token: 0x0400023E RID: 574
		Blocked,
		// Token: 0x0400023F RID: 575
		Ignored,
		// Token: 0x04000240 RID: 576
		NoMatch,
		// Token: 0x04000241 RID: 577
		AccountDisabled,
		// Token: 0x04000242 RID: 578
		ServiceReadOnly,
		// Token: 0x04000243 RID: 579
		AccountNotFeatured,
		// Token: 0x04000244 RID: 580
		AdministratorOK,
		// Token: 0x04000245 RID: 581
		ContentVersion,
		// Token: 0x04000246 RID: 582
		TryAnotherCM,
		// Token: 0x04000247 RID: 583
		PasswordRequiredToKickSession,
		// Token: 0x04000248 RID: 584
		AlreadyLoggedInElsewhere,
		// Token: 0x04000249 RID: 585
		Suspended,
		// Token: 0x0400024A RID: 586
		Cancelled,
		// Token: 0x0400024B RID: 587
		DataCorruption,
		// Token: 0x0400024C RID: 588
		DiskFull,
		// Token: 0x0400024D RID: 589
		RemoteCallFailed,
		// Token: 0x0400024E RID: 590
		PasswordUnset,
		// Token: 0x0400024F RID: 591
		ExternalAccountUnlinked,
		// Token: 0x04000250 RID: 592
		PSNTicketInvalid,
		// Token: 0x04000251 RID: 593
		ExternalAccountAlreadyLinked,
		// Token: 0x04000252 RID: 594
		RemoteFileConflict,
		// Token: 0x04000253 RID: 595
		IllegalPassword,
		// Token: 0x04000254 RID: 596
		SameAsPreviousValue,
		// Token: 0x04000255 RID: 597
		AccountLogonDenied,
		// Token: 0x04000256 RID: 598
		CannotUseOldPassword,
		// Token: 0x04000257 RID: 599
		InvalidLoginAuthCode,
		// Token: 0x04000258 RID: 600
		AccountLogonDeniedNoMail,
		// Token: 0x04000259 RID: 601
		HardwareNotCapableOfIPT,
		// Token: 0x0400025A RID: 602
		IPTInitError,
		// Token: 0x0400025B RID: 603
		ParentalControlRestricted,
		// Token: 0x0400025C RID: 604
		FacebookQueryError,
		// Token: 0x0400025D RID: 605
		ExpiredLoginAuthCode,
		// Token: 0x0400025E RID: 606
		IPLoginRestrictionFailed,
		// Token: 0x0400025F RID: 607
		AccountLockedDown,
		// Token: 0x04000260 RID: 608
		AccountLogonDeniedVerifiedEmailRequired,
		// Token: 0x04000261 RID: 609
		NoMatchingURL,
		// Token: 0x04000262 RID: 610
		BadResponse,
		// Token: 0x04000263 RID: 611
		RequirePasswordReEntry,
		// Token: 0x04000264 RID: 612
		ValueOutOfRange,
		// Token: 0x04000265 RID: 613
		UnexpectedError,
		// Token: 0x04000266 RID: 614
		Disabled,
		// Token: 0x04000267 RID: 615
		InvalidCEGSubmission,
		// Token: 0x04000268 RID: 616
		RestrictedDevice,
		// Token: 0x04000269 RID: 617
		RegionLocked,
		// Token: 0x0400026A RID: 618
		RateLimitExceeded
	}
}
