using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200009B RID: 155
	internal abstract class PrimeGeneratorBase
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00022714 File Offset: 0x00020914
		public virtual ConfidenceFactor Confidence
		{
			get
			{
				return ConfidenceFactor.Medium;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00022718 File Offset: 0x00020918
		public virtual PrimalityTest PrimalityTest
		{
			get
			{
				return new PrimalityTest(PrimalityTests.RabinMillerTest);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00022728 File Offset: 0x00020928
		public virtual int TrialDivisionBounds
		{
			get
			{
				return 4000;
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00022730 File Offset: 0x00020930
		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return this.PrimalityTest(bi, this.Confidence);
		}

		// Token: 0x060008F1 RID: 2289
		public abstract BigInteger GenerateNewPrime(int bits);
	}
}
