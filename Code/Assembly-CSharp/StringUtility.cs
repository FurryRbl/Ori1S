using System;
using System.Collections.Generic;

// Token: 0x02000256 RID: 598
public static class StringUtility
{
	// Token: 0x06001429 RID: 5161 RVA: 0x0005BCF4 File Offset: 0x00059EF4
	public static string AddSpaces(IEnumerable<char> text)
	{
		bool flag = false;
		List<char> list = new List<char>();
		foreach (char c in text)
		{
			if (char.IsUpper(c))
			{
				if (flag)
				{
					list.Add(' ');
				}
				flag = true;
			}
			list.Add(c);
		}
		return new string(list.ToArray());
	}
}
