using System;

namespace Mono.Math.Prime.Generator
{
	// Token: 0x0200000D RID: 13
	public class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00004B48 File Offset: 0x00002D48
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0U);
			return bigInteger;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004B64 File Offset: 0x00002D64
		public override BigInteger GenerateNewPrime(int bits)
		{
			return this.GenerateNewPrime(bits, null);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004B70 File Offset: 0x00002D70
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

		// Token: 0x06000078 RID: 120 RVA: 0x00004CA4 File Offset: 0x00002EA4
		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}
	}
}
