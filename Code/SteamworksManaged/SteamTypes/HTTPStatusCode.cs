using System;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000AD RID: 173
	public enum HTTPStatusCode
	{
		// Token: 0x040002F4 RID: 756
		Invalid,
		// Token: 0x040002F5 RID: 757
		Code100Continue = 100,
		// Token: 0x040002F6 RID: 758
		Code101SwitchingProtocols,
		// Token: 0x040002F7 RID: 759
		Code200OK = 200,
		// Token: 0x040002F8 RID: 760
		Code201Created,
		// Token: 0x040002F9 RID: 761
		Code202Accepted,
		// Token: 0x040002FA RID: 762
		Code203NonAuthoritative,
		// Token: 0x040002FB RID: 763
		Code204NoContent,
		// Token: 0x040002FC RID: 764
		Code205ResetContent,
		// Token: 0x040002FD RID: 765
		Code206PartialContent,
		// Token: 0x040002FE RID: 766
		Code300MultipleChoices = 300,
		// Token: 0x040002FF RID: 767
		Code301MovedPermanently,
		// Token: 0x04000300 RID: 768
		Code302Found,
		// Token: 0x04000301 RID: 769
		Code303SeeOther,
		// Token: 0x04000302 RID: 770
		Code304NotModified,
		// Token: 0x04000303 RID: 771
		Code305UseProxy,
		// Token: 0x04000304 RID: 772
		Code307TemporaryRedirect = 307,
		// Token: 0x04000305 RID: 773
		Code400BadRequest = 400,
		// Token: 0x04000306 RID: 774
		Code401Unauthorized,
		// Token: 0x04000307 RID: 775
		Code402PaymentRequired,
		// Token: 0x04000308 RID: 776
		Code403Forbidden,
		// Token: 0x04000309 RID: 777
		Code404NotFound,
		// Token: 0x0400030A RID: 778
		Code405MethodNotAllowed,
		// Token: 0x0400030B RID: 779
		Code406NotAcceptable,
		// Token: 0x0400030C RID: 780
		Code407ProxyAuthRequired,
		// Token: 0x0400030D RID: 781
		Code408RequestTimeout,
		// Token: 0x0400030E RID: 782
		Code409Conflict,
		// Token: 0x0400030F RID: 783
		Code410Gone,
		// Token: 0x04000310 RID: 784
		Code411LengthRequired,
		// Token: 0x04000311 RID: 785
		Code412PreconditionFailed,
		// Token: 0x04000312 RID: 786
		Code413RequestEntityTooLarge,
		// Token: 0x04000313 RID: 787
		Code414RequestURITooLong,
		// Token: 0x04000314 RID: 788
		Code415UnsupportedMediaType,
		// Token: 0x04000315 RID: 789
		Code416RequestedRangeNotSatisfiable,
		// Token: 0x04000316 RID: 790
		Code417ExpectationFailed,
		// Token: 0x04000317 RID: 791
		Code500InternalServerError = 500,
		// Token: 0x04000318 RID: 792
		Code501NotImplemented,
		// Token: 0x04000319 RID: 793
		Code502BadGateway,
		// Token: 0x0400031A RID: 794
		Code503ServiceUnavailable,
		// Token: 0x0400031B RID: 795
		Code504GatewayTimeout,
		// Token: 0x0400031C RID: 796
		Code505HTTPVersionNotSupported
	}
}
