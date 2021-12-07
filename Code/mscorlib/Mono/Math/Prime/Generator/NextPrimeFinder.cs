using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200009A RID: 154
	internal class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x000226D8 File Offset: 0x000208D8
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
