using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000465 RID: 1125
	internal interface IMachine
	{
		// Token: 0x06002845 RID: 10309
		Match Scan(Regex regex, string text, int start, int end);

		// Token: 0x06002846 RID: 10310
		string[] Split(Regex regex, string input, int count, int startat);

		// Token: 0x06002847 RID: 10311
		string Replace(Regex regex, string input, string replacement, int count, int startat);

		// Token: 0x06002848 RID: 10312
		string Result(string replacement, Match match);
	}
}
