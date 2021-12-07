using System;

namespace System.Globalization
{
	// Token: 0x020001FA RID: 506
	internal class CCMath
	{
		// Token: 0x0600193F RID: 6463 RVA: 0x0005E7DC File Offset: 0x0005C9DC
		public static double round(double x)
		{
			return Math.Floor(x + 0.5);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x0005E7F0 File Offset: 0x0005C9F0
		public static double mod(double x, double y)
		{
			return x - y * Math.Floor(x / y);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x0005E800 File Offset: 0x0005CA00
		public static int div(int x, int y)
		{
			return (int)Math.Floor((double)x / (double)y);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x0005E810 File Offset: 0x0005CA10
		public static int mod(int x, int y)
		{
			return x - y * CCMath.div(x, y);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x0005E820 File Offset: 0x0005CA20
		public static int div_mod(out int remainder, int x, int y)
		{
			int num = CCMath.div(x, y);
			remainder = x - y * num;
			return num;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0005E840 File Offset: 0x0005CA40
		public static int signum(double x)
		{
			if (x < 0.0)
			{
				return -1;
			}
			if (x == 0.0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0005E868 File Offset: 0x0005CA68
		public static int signum(int x)
		{
			if (x < 0)
			{
				return -1;
			}
			if (x == 0)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x0005E87C File Offset: 0x0005CA7C
		public static double amod(double x, double y)
		{
			double num = CCMath.mod(x, y);
			return (num != 0.0) ? num : y;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0005E8A8 File Offset: 0x0005CAA8
		public static int amod(int x, int y)
		{
			int num = CCMath.mod(x, y);
			return (num != 0) ? num : y;
		}
	}
}
