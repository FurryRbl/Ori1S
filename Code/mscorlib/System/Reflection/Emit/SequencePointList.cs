using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x020002E3 RID: 739
	internal class SequencePointList
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x00085B54 File Offset: 0x00083D54
		public SequencePointList(ISymbolDocumentWriter doc)
		{
			this.doc = doc;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x00085B64 File Offset: 0x00083D64
		public ISymbolDocumentWriter Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00085B6C File Offset: 0x00083D6C
		public int[] GetOffsets()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Offset;
			}
			return array;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00085BB4 File Offset: 0x00083DB4
		public int[] GetLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Line;
			}
			return array;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00085BFC File Offset: 0x00083DFC
		public int[] GetColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Col;
			}
			return array;
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00085C44 File Offset: 0x00083E44
		public int[] GetEndLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndLine;
			}
			return array;
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00085C8C File Offset: 0x00083E8C
		public int[] GetEndColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndCol;
			}
			return array;
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x00085CD4 File Offset: 0x00083ED4
		public int StartLine
		{
			get
			{
				return this.points[0].Line;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x00085CE8 File Offset: 0x00083EE8
		public int EndLine
		{
			get
			{
				return this.points[this.count - 1].Line;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x00085D04 File Offset: 0x00083F04
		public int StartColumn
		{
			get
			{
				return this.points[0].Col;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x00085D18 File Offset: 0x00083F18
		public int EndColumn
		{
			get
			{
				return this.points[this.count - 1].Col;
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00085D34 File Offset: 0x00083F34
		public void AddSequencePoint(int offset, int line, int col, int endLine, int endCol)
		{
			SequencePoint sequencePoint = default(SequencePoint);
			sequencePoint.Offset = offset;
			sequencePoint.Line = line;
			sequencePoint.Col = col;
			sequencePoint.EndLine = endLine;
			sequencePoint.EndCol = endCol;
			if (this.points == null)
			{
				this.points = new SequencePoint[10];
			}
			else if (this.count >= this.points.Length)
			{
				SequencePoint[] destinationArray = new SequencePoint[this.count + 10];
				Array.Copy(this.points, destinationArray, this.points.Length);
				this.points = destinationArray;
			}
			this.points[this.count] = sequencePoint;
			this.count++;
		}

		// Token: 0x04000E30 RID: 3632
		private const int arrayGrow = 10;

		// Token: 0x04000E31 RID: 3633
		private ISymbolDocumentWriter doc;

		// Token: 0x04000E32 RID: 3634
		private SequencePoint[] points;

		// Token: 0x04000E33 RID: 3635
		private int count;
	}
}
