using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200000C RID: 12
	public abstract class PrimeGeneratorBase
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004B10 File Offset: 0x00002D10
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004B14 File Offset: 0x00002D14
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004B24 File Offset: 0x00002D24
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004B2C File Offset: 0x00002D2C
		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return this.PrimalityTest(bi, this.Confidence);
		}

		// Token: 0x06000073 RID: 115
		public abstract BigInteger GenerateNewPrime(int bits);
	}
}
