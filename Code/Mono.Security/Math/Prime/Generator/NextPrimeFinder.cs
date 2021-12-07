using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200000B RID: 11
	public class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00004AD4 File Offset: 0x00002CD4
		protected override BigInteger GenerateSearchBase(int bits, object Context)
		{
			if (Context == null)
			{
				throw new ArgumentNullException("Context");
			}
			BigInteger bigInteger = new BigInteger((BigInteger)Context);
			bigInteger.SetBit(0U);
			return bigInteger;
		}
	}
}
