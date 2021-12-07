using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000367 RID: 871
	internal class IcmpV6MessageTypes
	{
		// Token: 0x040012F6 RID: 4854
		public const int DestinationUnreachable = 1;

		// Token: 0x040012F7 RID: 4855
		public const int PacketTooBig = 2;

		// Token: 0x040012F8 RID: 4856
		public const int TimeExceeded = 3;

		// Token: 0x040012F9 RID: 4857
		public const int ParameterProblem = 4;

		// Token: 0x040012FA RID: 4858
		public const int EchoRequest = 128;

		// Token: 0x040012FB RID: 4859
		public const int EchoReply = 129;

		// Token: 0x040012FC RID: 4860
		public const int GroupMembershipQuery = 130;

		// Token: 0x040012FD RID: 4861
		public const int GroupMembershipReport = 131;

		// Token: 0x040012FE RID: 4862
		public const int GroupMembershipReduction = 132;

		// Token: 0x040012FF RID: 4863
		public const int RouterSolicitation = 133;

		// Token: 0x04001300 RID: 4864
		public const int RouterAdvertisement = 134;

		// Token: 0x04001301 RID: 4865
		public const int NeighborSolicitation = 135;

		// Token: 0x04001302 RID: 4866
		public const int NeighborAdvertisement = 136;

		// Token: 0x04001303 RID: 4867
		public const int Redirect = 137;

		// Token: 0x04001304 RID: 4868
		public const int RouterRenumbering = 138;
	}
}
