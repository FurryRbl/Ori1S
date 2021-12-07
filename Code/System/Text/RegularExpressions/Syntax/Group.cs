using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200049A RID: 1178
	internal class Group : CompositeExpression
	{
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x00092184 File Offset: 0x00090384
		// (set) Token: 0x06002A6A RID: 10858 RVA: 0x00092194 File Offset: 0x00090394
		public Expression Expression
		{
			get
			{
				return base.Expressions[0];
			}
			set
			{
				base.Expressions[0] = value;
			}
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000921A4 File Offset: 0x000903A4
		public void AppendExpression(Expression e)
		{
			base.Expressions.Add(e);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000921B4 File Offset: 0x000903B4
		public override void Compile(ICompiler cmp, bool reverse)
		{
			int count = base.Expressions.Count;
			for (int i = 0; i < count; i++)
			{
				Expression expression;
				if (reverse)
				{
					expression = base.Expressions[count - i - 1];
				}
				else
				{
					expression = base.Expressions[i];
				}
				expression.Compile(cmp, reverse);
			}
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x00092210 File Offset: 0x00090410
		public override void GetWidth(out int min, out int max)
		{
			min = 0;
			max = 0;
			foreach (object obj in base.Expressions)
			{
				Expression expression = (Expression)obj;
				int num;
				int num2;
				expression.GetWidth(out num, out num2);
				min += num;
				if (max == 2147483647 || num2 == 2147483647)
				{
					max = int.MaxValue;
				}
				else
				{
					max += num2;
				}
			}
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000922BC File Offset: 0x000904BC
		public override AnchorInfo GetAnchorInfo(bool reverse)
		{
			int fixedWidth = base.GetFixedWidth();
			ArrayList arrayList = new ArrayList();
			IntervalCollection intervalCollection = new IntervalCollection();
			int num = 0;
			int count = base.Expressions.Count;
			for (int i = 0; i < count; i++)
			{
				Expression expression;
				if (reverse)
				{
					expression = base.Expressions[count - i - 1];
				}
				else
				{
					expression = base.Expressions[i];
				}
				AnchorInfo anchorInfo = expression.GetAnchorInfo(reverse);
				arrayList.Add(anchorInfo);
				if (anchorInfo.IsPosition)
				{
					return new AnchorInfo(this, num + anchorInfo.Offset, fixedWidth, anchorInfo.Position);
				}
				if (anchorInfo.IsSubstring)
				{
					intervalCollection.Add(anchorInfo.GetInterval(num));
				}
				if (anchorInfo.IsUnknownWidth)
				{
					break;
				}
				num += anchorInfo.Width;
			}
			intervalCollection.Normalize();
			Interval interval = Interval.Empty;
			foreach (object obj in intervalCollection)
			{
				Interval interval2 = (Interval)obj;
				if (interval2.Size > interval.Size)
				{
					interval = interval2;
				}
			}
			if (interval.IsEmpty)
			{
				return new AnchorInfo(this, fixedWidth);
			}
			bool flag = false;
			int num2 = 0;
			num = 0;
			for (int j = 0; j < arrayList.Count; j++)
			{
				AnchorInfo anchorInfo2 = (AnchorInfo)arrayList[j];
				if (anchorInfo2.IsSubstring && interval.Contains(anchorInfo2.GetInterval(num)))
				{
					flag |= anchorInfo2.IgnoreCase;
					arrayList[num2++] = anchorInfo2;
				}
				if (anchorInfo2.IsUnknownWidth)
				{
					break;
				}
				num += anchorInfo2.Width;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int k = 0; k < num2; k++)
			{
				AnchorInfo anchorInfo3;
				if (reverse)
				{
					anchorInfo3 = (AnchorInfo)arrayList[num2 - k - 1];
				}
				else
				{
					anchorInfo3 = (AnchorInfo)arrayList[k];
				}
				stringBuilder.Append(anchorInfo3.Substring);
			}
			if (stringBuilder.Length == interval.Size)
			{
				return new AnchorInfo(this, interval.low, fixedWidth, stringBuilder.ToString(), flag);
			}
			if (stringBuilder.Length > interval.Size)
			{
				Console.Error.WriteLine("overlapping?");
				return new AnchorInfo(this, fixedWidth);
			}
			throw new SystemException("Shouldn't happen");
		}
	}
}
