using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200009C RID: 156
	internal class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x0002274C File Offset: 0x0002094C
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00022768 File Offset: 0x00020968
		public override BigInteger GenerateNewPrime(int bits)
		{
			return this.GenerateNewPrime(bits, null);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00022774 File Offset: 0x00020974
		public virtual BigInteger GenerateNewPrime(int bits, object context)
		{
			BigInteger bigInteger = this.GenerateSearchBase(bits, context);
			uint num = bigInteger % 3234846615U;
			int trialDivisionBounds = this.TrialDivisionBounds;
			uint[] smallPrimes = BigInteger.smallPrimes;
			for (;;)
			{
				if (num % 3U != 0U)
				{
					if (num % 5U != 0U)
					{
						if (num % 7U != 0U)
						{
							if (num % 11U != 0U)
							{
								if (num % 13U != 0U)
								{
									if (num % 17U != 0U)
									{
										if (num % 19U != 0U)
										{
											if (num % 23U != 0U)
											{
												if (num % 29U != 0U)
												{
													int num2 = 10;
													while (num2 < smallPrimes.Length && (ulong)smallPrimes[num2] <= (ulong)((long)trialDivisionBounds))
													{
														if (bigInteger % smallPrimes[num2] == 0U)
														{
															goto IL_105;
														}
														num2++;
													}
													if (this.IsPrimeAcceptable(bigInteger, context))
													{
														if (this.PrimalityTest(bigInteger, this.Confidence))
														{
															break;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				IL_105:
				num += 2U;
				if (num >= 3234846615U)
				{
					num -= 3234846615U;
				}
				bigInteger.Incr2();
			}
			return bigInteger;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000228A8 File Offset: 0x00020AA8
		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}
	}
}
