using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000467 RID: 1127
	internal abstract class BaseMachine : IMachine
	{
		// Token: 0x06002852 RID: 10322 RVA: 0x0007FCA4 File Offset: 0x0007DEA4
		public virtual string Replace(Regex regex, string input, string replacement, int count, int startat)
		{
			ReplacementEvaluator replacementEvaluator = new ReplacementEvaluator(regex, replacement);
			if (regex.RightToLeft)
			{
				return this.RTLReplace(regex, input, new MatchEvaluator(replacementEvaluator.Evaluate), count, startat);
			}
			return this.LTRReplace(regex, input, new BaseMachine.MatchAppendEvaluator(replacementEvaluator.EvaluateAppend), count, startat, replacementEvaluator.NeedsGroupsOrCaptures);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x0007FCFC File Offset: 0x0007DEFC
		public virtual string[] Split(Regex regex, string input, int count, int startat)
		{
			ArrayList arrayList = new ArrayList();
			if (count == 0)
			{
				count = int.MaxValue;
			}
			int num = startat;
			Match match = null;
			while (--count > 0)
			{
				if (match != null)
				{
					match = match.NextMatch();
				}
				else
				{
					match = regex.Match(input, num);
				}
				if (!match.Success)
				{
					break;
				}
				if (regex.RightToLeft)
				{
					arrayList.Add(input.Substring(match.Index + match.Length, num - match.Index - match.Length));
				}
				else
				{
					arrayList.Add(input.Substring(num, match.Index - num));
				}
				int count2 = match.Groups.Count;
				for (int i = 1; i < count2; i++)
				{
					Group group = match.Groups[i];
					arrayList.Add(input.Substring(group.Index, group.Length));
				}
				if (regex.RightToLeft)
				{
					num = match.Index;
				}
				else
				{
					num = match.Index + match.Length;
				}
			}
			if (regex.RightToLeft && num >= 0)
			{
				arrayList.Add(input.Substring(0, num));
			}
			if (!regex.RightToLeft && num <= input.Length)
			{
				arrayList.Add(input.Substring(num));
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x0007FE78 File Offset: 0x0007E078
		public virtual Match Scan(Regex regex, string text, int start, int end)
		{
			throw new NotImplementedException("Scan method must be implemented in derived classes");
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0007FE84 File Offset: 0x0007E084
		public virtual string Result(string replacement, Match match)
		{
			return ReplacementEvaluator.Evaluate(replacement, match);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0007FE90 File Offset: 0x0007E090
		internal string LTRReplace(Regex regex, string input, BaseMachine.MatchAppendEvaluator evaluator, int count, int startat)
		{
			return this.LTRReplace(regex, input, evaluator, count, startat, true);
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x0007FEA0 File Offset: 0x0007E0A0
		internal string LTRReplace(Regex regex, string input, BaseMachine.MatchAppendEvaluator evaluator, int count, int startat, bool needs_groups_or_captures)
		{
			this.needs_groups_or_captures = needs_groups_or_captures;
			Match match = this.Scan(regex, input, startat, input.Length);
			if (!match.Success)
			{
				return input;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length);
			int num = startat;
			int num2 = count;
			stringBuilder.Append(input, 0, num);
			while (count == -1 || num2-- > 0)
			{
				if (match.Index < num)
				{
					throw new SystemException("how");
				}
				stringBuilder.Append(input, num, match.Index - num);
				evaluator(match, stringBuilder);
				num = match.Index + match.Length;
				match = match.NextMatch();
				if (!match.Success)
				{
					IL_AA:
					stringBuilder.Append(input, num, input.Length - num);
					return stringBuilder.ToString();
				}
			}
			goto IL_AA;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x0007FF70 File Offset: 0x0007E170
		internal string RTLReplace(Regex regex, string input, MatchEvaluator evaluator, int count, int startat)
		{
			Match match = this.Scan(regex, input, startat, input.Length);
			if (!match.Success)
			{
				return input;
			}
			int num = startat;
			int num2 = count;
			System.Collections.Specialized.StringCollection stringCollection = new System.Collections.Specialized.StringCollection();
			stringCollection.Add(input.Substring(num));
			while (count == -1 || num2-- > 0)
			{
				if (match.Index + match.Length > num)
				{
					throw new SystemException("how");
				}
				stringCollection.Add(input.Substring(match.Index + match.Length, num - match.Index - match.Length));
				stringCollection.Add(evaluator(match));
				num = match.Index;
				match = match.NextMatch();
				if (!match.Success)
				{
					IL_BE:
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(input, 0, num);
					int i = stringCollection.Count;
					while (i > 0)
					{
						stringBuilder.Append(stringCollection[--i]);
					}
					stringCollection.Clear();
					return stringBuilder.ToString();
				}
			}
			goto IL_BE;
		}

		// Token: 0x040018FC RID: 6396
		protected bool needs_groups_or_captures = true;

		// Token: 0x020004EC RID: 1260
		// (Invoke) Token: 0x06002C5C RID: 11356
		internal delegate void MatchAppendEvaluator(Match match, StringBuilder sb);
	}
}
