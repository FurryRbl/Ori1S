using System;
using System.Collections.Generic;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000018 RID: 24
	public class Clipper : ClipperBase
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public Clipper(int InitOptions = 0)
		{
			this.m_Scanbeam = null;
			this.m_ActiveEdges = null;
			this.m_SortedEdges = null;
			this.m_IntersectList = new List<IntersectNode>();
			this.m_IntersectNodeComparer = new MyIntersectNodeSort();
			this.m_ExecuteLocked = false;
			this.m_UsingPolyTree = false;
			this.m_PolyOuts = new List<OutRec>();
			this.m_Joins = new List<Join>();
			this.m_GhostJoins = new List<Join>();
			this.ReverseSolution = ((1 & InitOptions) != 0);
			this.StrictlySimple = ((2 & InitOptions) != 0);
			base.PreserveCollinear = ((4 & InitOptions) != 0);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003E6C File Offset: 0x0000206C
		private void DisposeScanbeamList()
		{
			while (this.m_Scanbeam != null)
			{
				Scanbeam next = this.m_Scanbeam.Next;
				this.m_Scanbeam = null;
				this.m_Scanbeam = next;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003EA0 File Offset: 0x000020A0
		protected override void Reset()
		{
			base.Reset();
			this.m_Scanbeam = null;
			this.m_ActiveEdges = null;
			this.m_SortedEdges = null;
			for (LocalMinima localMinima = this.m_MinimaList; localMinima != null; localMinima = localMinima.Next)
			{
				this.InsertScanbeam(localMinima.Y);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003EE7 File Offset: 0x000020E7
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003EEF File Offset: 0x000020EF
		public bool ReverseSolution { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003EF8 File Offset: 0x000020F8
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003F00 File Offset: 0x00002100
		public bool StrictlySimple { get; set; }

		// Token: 0x0600005A RID: 90 RVA: 0x00003F0C File Offset: 0x0000210C
		private void InsertScanbeam(int Y)
		{
			if (this.m_Scanbeam == null)
			{
				this.m_Scanbeam = new Scanbeam();
				this.m_Scanbeam.Next = null;
				this.m_Scanbeam.Y = Y;
				return;
			}
			if (Y > this.m_Scanbeam.Y)
			{
				this.m_Scanbeam = new Scanbeam
				{
					Y = Y,
					Next = this.m_Scanbeam
				};
				return;
			}
			Scanbeam scanbeam = this.m_Scanbeam;
			while (scanbeam.Next != null && Y <= scanbeam.Next.Y)
			{
				scanbeam = scanbeam.Next;
			}
			if (Y == scanbeam.Y)
			{
				return;
			}
			scanbeam.Next = new Scanbeam
			{
				Y = Y,
				Next = scanbeam.Next
			};
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003FC4 File Offset: 0x000021C4
		public bool Execute(ClipType clipType, List<List<IntPoint>> solution, PolyFillType subjFillType, PolyFillType clipFillType)
		{
			if (this.m_ExecuteLocked)
			{
				return false;
			}
			if (this.m_HasOpenPaths)
			{
				throw new ClipperException("Error: PolyTree struct is need for open path clipping.");
			}
			this.m_ExecuteLocked = true;
			solution.Clear();
			this.m_SubjFillType = subjFillType;
			this.m_ClipFillType = clipFillType;
			this.m_ClipType = clipType;
			this.m_UsingPolyTree = false;
			bool flag;
			try
			{
				flag = this.ExecuteInternal();
				if (flag)
				{
					this.BuildResult(solution);
				}
			}
			finally
			{
				this.DisposeAllPolyPts();
				this.m_ExecuteLocked = false;
			}
			return flag;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000404C File Offset: 0x0000224C
		public bool Execute(ClipType clipType, PolyTree polytree, PolyFillType subjFillType, PolyFillType clipFillType)
		{
			if (this.m_ExecuteLocked)
			{
				return false;
			}
			this.m_ExecuteLocked = true;
			this.m_SubjFillType = subjFillType;
			this.m_ClipFillType = clipFillType;
			this.m_ClipType = clipType;
			this.m_UsingPolyTree = true;
			bool flag;
			try
			{
				flag = this.ExecuteInternal();
				if (flag)
				{
					this.BuildResult2(polytree);
				}
			}
			finally
			{
				this.DisposeAllPolyPts();
				this.m_ExecuteLocked = false;
			}
			return flag;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000040BC File Offset: 0x000022BC
		public bool Execute(ClipType clipType, List<List<IntPoint>> solution)
		{
			return this.Execute(clipType, solution, PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000040C8 File Offset: 0x000022C8
		public bool Execute(ClipType clipType, PolyTree polytree)
		{
			return this.Execute(clipType, polytree, PolyFillType.pftEvenOdd, PolyFillType.pftEvenOdd);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000040D4 File Offset: 0x000022D4
		internal void FixHoleLinkage(OutRec outRec)
		{
			if (outRec.FirstLeft == null || (outRec.IsHole != outRec.FirstLeft.IsHole && outRec.FirstLeft.Pts != null))
			{
				return;
			}
			OutRec firstLeft = outRec.FirstLeft;
			while (firstLeft != null && (firstLeft.IsHole == outRec.IsHole || firstLeft.Pts == null))
			{
				firstLeft = firstLeft.FirstLeft;
			}
			outRec.FirstLeft = firstLeft;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000413C File Offset: 0x0000233C
		private bool ExecuteInternal()
		{
			bool result;
			try
			{
				this.Reset();
				if (this.m_CurrentLM == null)
				{
					result = false;
				}
				else
				{
					int botY = this.PopScanbeam();
					for (;;)
					{
						this.InsertLocalMinimaIntoAEL(botY);
						this.m_GhostJoins.Clear();
						this.ProcessHorizontals(false);
						if (this.m_Scanbeam == null)
						{
							goto IL_6E;
						}
						int num = this.PopScanbeam();
						if (!this.ProcessIntersections(botY, num))
						{
							break;
						}
						this.ProcessEdgesAtTopOfScanbeam(num);
						botY = num;
						if (this.m_Scanbeam == null && this.m_CurrentLM == null)
						{
							goto IL_6E;
						}
					}
					return false;
					IL_6E:
					for (int i = 0; i < this.m_PolyOuts.Count; i++)
					{
						OutRec outRec = this.m_PolyOuts[i];
						if (outRec.Pts != null && !outRec.IsOpen && (outRec.IsHole ^ this.ReverseSolution) == this.Area(outRec) > 0f)
						{
							this.ReversePolyPtLinks(outRec.Pts);
						}
					}
					this.JoinCommonEdges();
					for (int j = 0; j < this.m_PolyOuts.Count; j++)
					{
						OutRec outRec2 = this.m_PolyOuts[j];
						if (outRec2.Pts != null && !outRec2.IsOpen)
						{
							this.FixupOutPolygon(outRec2);
						}
					}
					if (this.StrictlySimple)
					{
						this.DoSimplePolygons();
					}
					result = true;
				}
			}
			finally
			{
				this.m_Joins.Clear();
				this.m_GhostJoins.Clear();
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000042A8 File Offset: 0x000024A8
		private int PopScanbeam()
		{
			int y = this.m_Scanbeam.Y;
			this.m_Scanbeam = this.m_Scanbeam.Next;
			return y;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000042C8 File Offset: 0x000024C8
		private void DisposeAllPolyPts()
		{
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				this.DisposeOutRec(i);
			}
			this.m_PolyOuts.Clear();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004300 File Offset: 0x00002500
		private void DisposeOutRec(int index)
		{
			OutRec outRec = this.m_PolyOuts[index];
			if (outRec.Pts != null)
			{
				this.DisposeOutPts(outRec.Pts);
			}
			this.m_PolyOuts[index] = null;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000433D File Offset: 0x0000253D
		private void DisposeOutPts(OutPt pp)
		{
			if (pp == null)
			{
				return;
			}
			pp.Prev.Next = null;
			while (pp != null)
			{
				pp = pp.Next;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000435C File Offset: 0x0000255C
		private void AddJoin(OutPt Op1, OutPt Op2, IntPoint OffPt)
		{
			Join join = new Join();
			join.OutPt1 = Op1;
			join.OutPt2 = Op2;
			join.OffPt = OffPt;
			this.m_Joins.Add(join);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004390 File Offset: 0x00002590
		private void AddGhostJoin(OutPt Op, IntPoint OffPt)
		{
			Join join = new Join();
			join.OutPt1 = Op;
			join.OffPt = OffPt;
			this.m_GhostJoins.Add(join);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000043C0 File Offset: 0x000025C0
		private void InsertLocalMinimaIntoAEL(int botY)
		{
			while (this.m_CurrentLM != null && this.m_CurrentLM.Y == botY)
			{
				TEdge leftBound = this.m_CurrentLM.LeftBound;
				TEdge rightBound = this.m_CurrentLM.RightBound;
				base.PopLocalMinima();
				OutPt outPt = null;
				if (leftBound == null)
				{
					this.InsertEdgeIntoAEL(rightBound, null);
					this.SetWindingCount(rightBound);
					if (this.IsContributing(rightBound))
					{
						TEdge tedge = rightBound;
						outPt = this.AddOutPt(tedge, tedge.Bot);
					}
				}
				else if (rightBound == null)
				{
					this.InsertEdgeIntoAEL(leftBound, null);
					this.SetWindingCount(leftBound);
					if (this.IsContributing(leftBound))
					{
						TEdge tedge2 = leftBound;
						outPt = this.AddOutPt(tedge2, tedge2.Bot);
					}
					this.InsertScanbeam(leftBound.Top.Y);
				}
				else
				{
					this.InsertEdgeIntoAEL(leftBound, null);
					this.InsertEdgeIntoAEL(rightBound, leftBound);
					this.SetWindingCount(leftBound);
					rightBound.WindCnt = leftBound.WindCnt;
					rightBound.WindCnt2 = leftBound.WindCnt2;
					if (this.IsContributing(leftBound))
					{
						outPt = this.AddLocalMinPoly(leftBound, rightBound, leftBound.Bot);
					}
					this.InsertScanbeam(leftBound.Top.Y);
				}
				if (rightBound != null)
				{
					if (ClipperBase.IsHorizontal(rightBound))
					{
						this.AddEdgeToSEL(rightBound);
					}
					else
					{
						this.InsertScanbeam(rightBound.Top.Y);
					}
				}
				if (leftBound != null && rightBound != null)
				{
					if (outPt != null && ClipperBase.IsHorizontal(rightBound) && this.m_GhostJoins.Count > 0 && rightBound.WindDelta != 0)
					{
						for (int i = 0; i < this.m_GhostJoins.Count; i++)
						{
							Join join = this.m_GhostJoins[i];
							if (this.HorzSegmentsOverlap(join.OutPt1.Pt, join.OffPt, rightBound.Bot, rightBound.Top))
							{
								this.AddJoin(join.OutPt1, outPt, join.OffPt);
							}
						}
					}
					if (leftBound.OutIdx >= 0 && leftBound.PrevInAEL != null && leftBound.PrevInAEL.Curr.X == leftBound.Bot.X && leftBound.PrevInAEL.OutIdx >= 0 && ClipperBase.SlopesEqual(leftBound.PrevInAEL, leftBound, this.m_UseFullRange) && leftBound.WindDelta != 0 && leftBound.PrevInAEL.WindDelta != 0)
					{
						OutPt op = this.AddOutPt(leftBound.PrevInAEL, leftBound.Bot);
						this.AddJoin(outPt, op, leftBound.Top);
					}
					if (leftBound.NextInAEL != rightBound)
					{
						if (rightBound.OutIdx >= 0 && rightBound.PrevInAEL.OutIdx >= 0 && ClipperBase.SlopesEqual(rightBound.PrevInAEL, rightBound, this.m_UseFullRange) && rightBound.WindDelta != 0 && rightBound.PrevInAEL.WindDelta != 0)
						{
							OutPt op2 = this.AddOutPt(rightBound.PrevInAEL, rightBound.Bot);
							this.AddJoin(outPt, op2, rightBound.Top);
						}
						TEdge nextInAEL = leftBound.NextInAEL;
						if (nextInAEL != null)
						{
							while (nextInAEL != rightBound)
							{
								this.IntersectEdges(rightBound, nextInAEL, leftBound.Curr, false);
								nextInAEL = nextInAEL.NextInAEL;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000046A8 File Offset: 0x000028A8
		private void InsertEdgeIntoAEL(TEdge edge, TEdge startEdge)
		{
			if (this.m_ActiveEdges == null)
			{
				edge.PrevInAEL = null;
				edge.NextInAEL = null;
				this.m_ActiveEdges = edge;
				return;
			}
			if (startEdge == null && this.E2InsertsBeforeE1(this.m_ActiveEdges, edge))
			{
				edge.PrevInAEL = null;
				edge.NextInAEL = this.m_ActiveEdges;
				this.m_ActiveEdges.PrevInAEL = edge;
				this.m_ActiveEdges = edge;
				return;
			}
			if (startEdge == null)
			{
				startEdge = this.m_ActiveEdges;
			}
			while (startEdge.NextInAEL != null && !this.E2InsertsBeforeE1(startEdge.NextInAEL, edge))
			{
				startEdge = startEdge.NextInAEL;
			}
			edge.NextInAEL = startEdge.NextInAEL;
			if (startEdge.NextInAEL != null)
			{
				startEdge.NextInAEL.PrevInAEL = edge;
			}
			edge.PrevInAEL = startEdge;
			startEdge.NextInAEL = edge;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004768 File Offset: 0x00002968
		private bool E2InsertsBeforeE1(TEdge e1, TEdge e2)
		{
			if (e2.Curr.X != e1.Curr.X)
			{
				return e2.Curr.X < e1.Curr.X;
			}
			if (e2.Top.Y > e1.Top.Y)
			{
				return e2.Top.X < Clipper.TopX(e1, e2.Top.Y);
			}
			return e1.Top.X > Clipper.TopX(e2, e1.Top.Y);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000047FB File Offset: 0x000029FB
		private bool IsEvenOddFillType(TEdge edge)
		{
			if (edge.PolyTyp == PolyType.ptSubject)
			{
				return this.m_SubjFillType == PolyFillType.pftEvenOdd;
			}
			return this.m_ClipFillType == PolyFillType.pftEvenOdd;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004818 File Offset: 0x00002A18
		private bool IsEvenOddAltFillType(TEdge edge)
		{
			if (edge.PolyTyp == PolyType.ptSubject)
			{
				return this.m_ClipFillType == PolyFillType.pftEvenOdd;
			}
			return this.m_SubjFillType == PolyFillType.pftEvenOdd;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004838 File Offset: 0x00002A38
		private bool IsContributing(TEdge edge)
		{
			PolyFillType polyFillType;
			PolyFillType polyFillType2;
			if (edge.PolyTyp == PolyType.ptSubject)
			{
				polyFillType = this.m_SubjFillType;
				polyFillType2 = this.m_ClipFillType;
			}
			else
			{
				polyFillType = this.m_ClipFillType;
				polyFillType2 = this.m_SubjFillType;
			}
			switch (polyFillType)
			{
			case PolyFillType.pftEvenOdd:
				if (edge.WindDelta == 0 && edge.WindCnt != 1)
				{
					return false;
				}
				break;
			case PolyFillType.pftNonZero:
				if (Math.Abs(edge.WindCnt) != 1)
				{
					return false;
				}
				break;
			case PolyFillType.pftPositive:
				if (edge.WindCnt != 1)
				{
					return false;
				}
				break;
			default:
				if (edge.WindCnt != -1)
				{
					return false;
				}
				break;
			}
			switch (this.m_ClipType)
			{
			case ClipType.ctIntersection:
				switch (polyFillType2)
				{
				case PolyFillType.pftEvenOdd:
				case PolyFillType.pftNonZero:
					return edge.WindCnt2 != 0;
				case PolyFillType.pftPositive:
					return edge.WindCnt2 > 0;
				default:
					return edge.WindCnt2 < 0;
				}
				break;
			case ClipType.ctUnion:
				switch (polyFillType2)
				{
				case PolyFillType.pftEvenOdd:
				case PolyFillType.pftNonZero:
					return edge.WindCnt2 == 0;
				case PolyFillType.pftPositive:
					return edge.WindCnt2 <= 0;
				default:
					return edge.WindCnt2 >= 0;
				}
				break;
			case ClipType.ctDifference:
				if (edge.PolyTyp == PolyType.ptSubject)
				{
					switch (polyFillType2)
					{
					case PolyFillType.pftEvenOdd:
					case PolyFillType.pftNonZero:
						return edge.WindCnt2 == 0;
					case PolyFillType.pftPositive:
						return edge.WindCnt2 <= 0;
					default:
						return edge.WindCnt2 >= 0;
					}
				}
				else
				{
					switch (polyFillType2)
					{
					case PolyFillType.pftEvenOdd:
					case PolyFillType.pftNonZero:
						return edge.WindCnt2 != 0;
					case PolyFillType.pftPositive:
						return edge.WindCnt2 > 0;
					default:
						return edge.WindCnt2 < 0;
					}
				}
				break;
			case ClipType.ctXor:
				if (edge.WindDelta != 0)
				{
					return true;
				}
				switch (polyFillType2)
				{
				case PolyFillType.pftEvenOdd:
				case PolyFillType.pftNonZero:
					return edge.WindCnt2 == 0;
				case PolyFillType.pftPositive:
					return edge.WindCnt2 <= 0;
				default:
					return edge.WindCnt2 >= 0;
				}
				break;
			default:
				return true;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000049FC File Offset: 0x00002BFC
		private void SetWindingCount(TEdge edge)
		{
			TEdge tedge = edge.PrevInAEL;
			while (tedge != null && (tedge.PolyTyp != edge.PolyTyp || tedge.WindDelta == 0))
			{
				tedge = tedge.PrevInAEL;
			}
			if (tedge == null)
			{
				edge.WindCnt = ((edge.WindDelta == 0) ? 1 : edge.WindDelta);
				edge.WindCnt2 = 0;
				tedge = this.m_ActiveEdges;
			}
			else if (edge.WindDelta == 0 && this.m_ClipType != ClipType.ctUnion)
			{
				edge.WindCnt = 1;
				edge.WindCnt2 = tedge.WindCnt2;
				tedge = tedge.NextInAEL;
			}
			else if (this.IsEvenOddFillType(edge))
			{
				if (edge.WindDelta == 0)
				{
					bool flag = true;
					for (TEdge prevInAEL = tedge.PrevInAEL; prevInAEL != null; prevInAEL = prevInAEL.PrevInAEL)
					{
						if (prevInAEL.PolyTyp == tedge.PolyTyp && prevInAEL.WindDelta != 0)
						{
							flag = !flag;
						}
					}
					edge.WindCnt = (flag ? 0 : 1);
				}
				else
				{
					edge.WindCnt = edge.WindDelta;
				}
				edge.WindCnt2 = tedge.WindCnt2;
				tedge = tedge.NextInAEL;
			}
			else
			{
				if (tedge.WindCnt * tedge.WindDelta < 0)
				{
					if (Math.Abs(tedge.WindCnt) > 1)
					{
						if (tedge.WindDelta * edge.WindDelta < 0)
						{
							edge.WindCnt = tedge.WindCnt;
						}
						else
						{
							edge.WindCnt = tedge.WindCnt + edge.WindDelta;
						}
					}
					else
					{
						edge.WindCnt = ((edge.WindDelta == 0) ? 1 : edge.WindDelta);
					}
				}
				else if (edge.WindDelta == 0)
				{
					edge.WindCnt = ((tedge.WindCnt < 0) ? (tedge.WindCnt - 1) : (tedge.WindCnt + 1));
				}
				else if (tedge.WindDelta * edge.WindDelta < 0)
				{
					edge.WindCnt = tedge.WindCnt;
				}
				else
				{
					edge.WindCnt = tedge.WindCnt + edge.WindDelta;
				}
				edge.WindCnt2 = tedge.WindCnt2;
				tedge = tedge.NextInAEL;
			}
			if (this.IsEvenOddAltFillType(edge))
			{
				while (tedge != edge)
				{
					if (tedge.WindDelta != 0)
					{
						edge.WindCnt2 = ((edge.WindCnt2 == 0) ? 1 : 0);
					}
					tedge = tedge.NextInAEL;
				}
				return;
			}
			while (tedge != edge)
			{
				edge.WindCnt2 += tedge.WindDelta;
				tedge = tedge.NextInAEL;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004C30 File Offset: 0x00002E30
		private void AddEdgeToSEL(TEdge edge)
		{
			if (this.m_SortedEdges == null)
			{
				this.m_SortedEdges = edge;
				edge.PrevInSEL = null;
				edge.NextInSEL = null;
				return;
			}
			edge.NextInSEL = this.m_SortedEdges;
			edge.PrevInSEL = null;
			this.m_SortedEdges.PrevInSEL = edge;
			this.m_SortedEdges = edge;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004C84 File Offset: 0x00002E84
		private void CopyAELToSEL()
		{
			TEdge tedge = this.m_ActiveEdges;
			this.m_SortedEdges = tedge;
			while (tedge != null)
			{
				TEdge tedge2 = tedge;
				tedge2.PrevInSEL = tedge2.PrevInAEL;
				TEdge tedge3 = tedge;
				tedge3.NextInSEL = tedge3.NextInAEL;
				tedge = tedge.NextInAEL;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private void SwapPositionsInAEL(TEdge edge1, TEdge edge2)
		{
			if (edge1.NextInAEL == edge1.PrevInAEL || edge2.NextInAEL == edge2.PrevInAEL)
			{
				return;
			}
			if (edge1.NextInAEL == edge2)
			{
				TEdge nextInAEL = edge2.NextInAEL;
				if (nextInAEL != null)
				{
					nextInAEL.PrevInAEL = edge1;
				}
				TEdge prevInAEL = edge1.PrevInAEL;
				if (prevInAEL != null)
				{
					prevInAEL.NextInAEL = edge2;
				}
				edge2.PrevInAEL = prevInAEL;
				edge2.NextInAEL = edge1;
				edge1.PrevInAEL = edge2;
				edge1.NextInAEL = nextInAEL;
			}
			else if (edge2.NextInAEL == edge1)
			{
				TEdge nextInAEL2 = edge1.NextInAEL;
				if (nextInAEL2 != null)
				{
					nextInAEL2.PrevInAEL = edge2;
				}
				TEdge prevInAEL2 = edge2.PrevInAEL;
				if (prevInAEL2 != null)
				{
					prevInAEL2.NextInAEL = edge1;
				}
				edge1.PrevInAEL = prevInAEL2;
				edge1.NextInAEL = edge2;
				edge2.PrevInAEL = edge1;
				edge2.NextInAEL = nextInAEL2;
			}
			else
			{
				TEdge nextInAEL3 = edge1.NextInAEL;
				TEdge prevInAEL3 = edge1.PrevInAEL;
				edge1.NextInAEL = edge2.NextInAEL;
				if (edge1.NextInAEL != null)
				{
					edge1.NextInAEL.PrevInAEL = edge1;
				}
				edge1.PrevInAEL = edge2.PrevInAEL;
				if (edge1.PrevInAEL != null)
				{
					edge1.PrevInAEL.NextInAEL = edge1;
				}
				edge2.NextInAEL = nextInAEL3;
				if (edge2.NextInAEL != null)
				{
					edge2.NextInAEL.PrevInAEL = edge2;
				}
				edge2.PrevInAEL = prevInAEL3;
				if (edge2.PrevInAEL != null)
				{
					edge2.PrevInAEL.NextInAEL = edge2;
				}
			}
			if (edge1.PrevInAEL == null)
			{
				this.m_ActiveEdges = edge1;
				return;
			}
			if (edge2.PrevInAEL == null)
			{
				this.m_ActiveEdges = edge2;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004E30 File Offset: 0x00003030
		private void SwapPositionsInSEL(TEdge edge1, TEdge edge2)
		{
			if (edge1.NextInSEL == null && edge1.PrevInSEL == null)
			{
				return;
			}
			if (edge2.NextInSEL == null && edge2.PrevInSEL == null)
			{
				return;
			}
			if (edge1.NextInSEL == edge2)
			{
				TEdge nextInSEL = edge2.NextInSEL;
				if (nextInSEL != null)
				{
					nextInSEL.PrevInSEL = edge1;
				}
				TEdge prevInSEL = edge1.PrevInSEL;
				if (prevInSEL != null)
				{
					prevInSEL.NextInSEL = edge2;
				}
				edge2.PrevInSEL = prevInSEL;
				edge2.NextInSEL = edge1;
				edge1.PrevInSEL = edge2;
				edge1.NextInSEL = nextInSEL;
			}
			else if (edge2.NextInSEL == edge1)
			{
				TEdge nextInSEL2 = edge1.NextInSEL;
				if (nextInSEL2 != null)
				{
					nextInSEL2.PrevInSEL = edge2;
				}
				TEdge prevInSEL2 = edge2.PrevInSEL;
				if (prevInSEL2 != null)
				{
					prevInSEL2.NextInSEL = edge1;
				}
				edge1.PrevInSEL = prevInSEL2;
				edge1.NextInSEL = edge2;
				edge2.PrevInSEL = edge1;
				edge2.NextInSEL = nextInSEL2;
			}
			else
			{
				TEdge nextInSEL3 = edge1.NextInSEL;
				TEdge prevInSEL3 = edge1.PrevInSEL;
				edge1.NextInSEL = edge2.NextInSEL;
				if (edge1.NextInSEL != null)
				{
					edge1.NextInSEL.PrevInSEL = edge1;
				}
				edge1.PrevInSEL = edge2.PrevInSEL;
				if (edge1.PrevInSEL != null)
				{
					edge1.PrevInSEL.NextInSEL = edge1;
				}
				edge2.NextInSEL = nextInSEL3;
				if (edge2.NextInSEL != null)
				{
					edge2.NextInSEL.PrevInSEL = edge2;
				}
				edge2.PrevInSEL = prevInSEL3;
				if (edge2.PrevInSEL != null)
				{
					edge2.PrevInSEL.NextInSEL = edge2;
				}
			}
			if (edge1.PrevInSEL == null)
			{
				this.m_SortedEdges = edge1;
				return;
			}
			if (edge2.PrevInSEL == null)
			{
				this.m_SortedEdges = edge2;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004FA0 File Offset: 0x000031A0
		private void AddLocalMaxPoly(TEdge e1, TEdge e2, IntPoint pt)
		{
			this.AddOutPt(e1, pt);
			if (e2.WindDelta == 0)
			{
				this.AddOutPt(e2, pt);
			}
			if (e1.OutIdx == e2.OutIdx)
			{
				e1.OutIdx = -1;
				e2.OutIdx = -1;
				return;
			}
			if (e1.OutIdx < e2.OutIdx)
			{
				this.AppendPolygon(e1, e2);
				return;
			}
			this.AppendPolygon(e2, e1);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005004 File Offset: 0x00003204
		private OutPt AddLocalMinPoly(TEdge e1, TEdge e2, IntPoint pt)
		{
			OutPt outPt;
			TEdge tedge;
			TEdge prevInAEL;
			if (ClipperBase.IsHorizontal(e2) || e1.Dx > e2.Dx)
			{
				outPt = this.AddOutPt(e1, pt);
				e2.OutIdx = e1.OutIdx;
				e1.Side = EdgeSide.esLeft;
				e2.Side = EdgeSide.esRight;
				tedge = e1;
				if (tedge.PrevInAEL == e2)
				{
					prevInAEL = e2.PrevInAEL;
				}
				else
				{
					prevInAEL = tedge.PrevInAEL;
				}
			}
			else
			{
				outPt = this.AddOutPt(e2, pt);
				e1.OutIdx = e2.OutIdx;
				e1.Side = EdgeSide.esRight;
				e2.Side = EdgeSide.esLeft;
				tedge = e2;
				if (tedge.PrevInAEL == e1)
				{
					prevInAEL = e1.PrevInAEL;
				}
				else
				{
					prevInAEL = tedge.PrevInAEL;
				}
			}
			if (prevInAEL != null && prevInAEL.OutIdx >= 0 && Clipper.TopX(prevInAEL, pt.Y) == Clipper.TopX(tedge, pt.Y) && ClipperBase.SlopesEqual(tedge, prevInAEL, this.m_UseFullRange) && tedge.WindDelta != 0 && prevInAEL.WindDelta != 0)
			{
				OutPt op = this.AddOutPt(prevInAEL, pt);
				this.AddJoin(outPt, op, tedge.Top);
			}
			return outPt;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005104 File Offset: 0x00003304
		private OutRec CreateOutRec()
		{
			OutRec outRec = new OutRec();
			outRec.Idx = -1;
			outRec.IsHole = false;
			outRec.IsOpen = false;
			outRec.FirstLeft = null;
			outRec.Pts = null;
			outRec.BottomPt = null;
			outRec.PolyNode = null;
			this.m_PolyOuts.Add(outRec);
			outRec.Idx = this.m_PolyOuts.Count - 1;
			return outRec;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005168 File Offset: 0x00003368
		private OutPt AddOutPt(TEdge e, IntPoint pt)
		{
			bool flag = e.Side == EdgeSide.esLeft;
			if (e.OutIdx < 0)
			{
				OutRec outRec = this.CreateOutRec();
				outRec.IsOpen = (e.WindDelta == 0);
				OutPt outPt = new OutPt();
				outRec.Pts = outPt;
				outPt.Idx = outRec.Idx;
				outPt.Pt = pt;
				OutPt outPt2 = outPt;
				outPt2.Next = outPt2;
				OutPt outPt3 = outPt;
				outPt3.Prev = outPt3;
				if (!outRec.IsOpen)
				{
					this.SetHoleState(e, outRec);
				}
				e.OutIdx = outRec.Idx;
				return outPt;
			}
			OutRec outRec2 = this.m_PolyOuts[e.OutIdx];
			OutPt pts = outRec2.Pts;
			if (flag && pt == pts.Pt)
			{
				return pts;
			}
			if (!flag && pt == pts.Prev.Pt)
			{
				return pts.Prev;
			}
			OutPt outPt4 = new OutPt();
			outPt4.Idx = outRec2.Idx;
			outPt4.Pt = pt;
			outPt4.Next = pts;
			outPt4.Prev = pts.Prev;
			outPt4.Prev.Next = outPt4;
			pts.Prev = outPt4;
			if (flag)
			{
				outRec2.Pts = outPt4;
			}
			return outPt4;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005290 File Offset: 0x00003490
		internal void SwapPoints(ref IntPoint pt1, ref IntPoint pt2)
		{
			IntPoint intPoint = new IntPoint(pt1);
			pt1 = pt2;
			pt2 = intPoint;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000052C0 File Offset: 0x000034C0
		private bool HorzSegmentsOverlap(IntPoint Pt1a, IntPoint Pt1b, IntPoint Pt2a, IntPoint Pt2b)
		{
			return Pt1a.X > Pt2a.X == Pt1a.X < Pt2b.X || Pt1b.X > Pt2a.X == Pt1b.X < Pt2b.X || Pt2a.X > Pt1a.X == Pt2a.X < Pt1b.X || Pt2b.X > Pt1a.X == Pt2b.X < Pt1b.X || (Pt1a.X == Pt2a.X && Pt1b.X == Pt2b.X) || (Pt1a.X == Pt2b.X && Pt1b.X == Pt2a.X);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005390 File Offset: 0x00003590
		private OutPt InsertPolyPtBetween(OutPt p1, OutPt p2, IntPoint pt)
		{
			OutPt outPt = new OutPt();
			outPt.Pt = pt;
			if (p2 == p1.Next)
			{
				p1.Next = outPt;
				p2.Prev = outPt;
				outPt.Next = p2;
				outPt.Prev = p1;
			}
			else
			{
				p2.Next = outPt;
				p1.Prev = outPt;
				outPt.Next = p1;
				outPt.Prev = p2;
			}
			return outPt;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000053F0 File Offset: 0x000035F0
		private void SetHoleState(TEdge e, OutRec outRec)
		{
			bool flag = false;
			for (TEdge prevInAEL = e.PrevInAEL; prevInAEL != null; prevInAEL = prevInAEL.PrevInAEL)
			{
				if (prevInAEL.OutIdx >= 0 && prevInAEL.WindDelta != 0)
				{
					flag = !flag;
					if (outRec.FirstLeft == null)
					{
						outRec.FirstLeft = this.m_PolyOuts[prevInAEL.OutIdx];
					}
				}
			}
			if (flag)
			{
				outRec.IsHole = true;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005451 File Offset: 0x00003651
		private float GetDx(IntPoint pt1, IntPoint pt2)
		{
			if (pt1.Y == pt2.Y)
			{
				return -3.4E+38f;
			}
			return (float)(pt2.X - pt1.X) / (float)(pt2.Y - pt1.Y);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005484 File Offset: 0x00003684
		private bool FirstIsBottomPt(OutPt btmPt1, OutPt btmPt2)
		{
			OutPt outPt = btmPt1.Prev;
			while (outPt.Pt == btmPt1.Pt && outPt != btmPt1)
			{
				outPt = outPt.Prev;
			}
			float num = Math.Abs(this.GetDx(btmPt1.Pt, outPt.Pt));
			outPt = btmPt1.Next;
			while (outPt.Pt == btmPt1.Pt && outPt != btmPt1)
			{
				outPt = outPt.Next;
			}
			float num2 = Math.Abs(this.GetDx(btmPt1.Pt, outPt.Pt));
			outPt = btmPt2.Prev;
			while (outPt.Pt == btmPt2.Pt && outPt != btmPt2)
			{
				outPt = outPt.Prev;
			}
			float num3 = Math.Abs(this.GetDx(btmPt2.Pt, outPt.Pt));
			outPt = btmPt2.Next;
			while (outPt.Pt == btmPt2.Pt && outPt != btmPt2)
			{
				outPt = outPt.Next;
			}
			float num4 = Math.Abs(this.GetDx(btmPt2.Pt, outPt.Pt));
			return (num >= num3 && num >= num4) || (num2 >= num3 && num2 >= num4);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000055A8 File Offset: 0x000037A8
		private OutPt GetBottomPt(OutPt pp)
		{
			OutPt outPt = null;
			OutPt next;
			for (next = pp.Next; next != pp; next = next.Next)
			{
				if (next.Pt.Y > pp.Pt.Y)
				{
					pp = next;
					outPt = null;
				}
				else if (next.Pt.Y == pp.Pt.Y && next.Pt.X <= pp.Pt.X)
				{
					if (next.Pt.X < pp.Pt.X)
					{
						outPt = null;
						pp = next;
					}
					else if (next.Next != pp && next.Prev != pp)
					{
						outPt = next;
					}
				}
			}
			if (outPt != null)
			{
				while (outPt != next)
				{
					if (!this.FirstIsBottomPt(next, outPt))
					{
						pp = outPt;
					}
					outPt = outPt.Next;
					while (outPt.Pt != pp.Pt)
					{
						outPt = outPt.Next;
					}
				}
			}
			return pp;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00005690 File Offset: 0x00003890
		private OutRec GetLowermostRec(OutRec outRec1, OutRec outRec2)
		{
			if (outRec1.BottomPt == null)
			{
				outRec1.BottomPt = this.GetBottomPt(outRec1.Pts);
			}
			if (outRec2.BottomPt == null)
			{
				outRec2.BottomPt = this.GetBottomPt(outRec2.Pts);
			}
			OutPt bottomPt = outRec1.BottomPt;
			OutPt bottomPt2 = outRec2.BottomPt;
			if (bottomPt.Pt.Y > bottomPt2.Pt.Y)
			{
				return outRec1;
			}
			if (bottomPt.Pt.Y < bottomPt2.Pt.Y)
			{
				return outRec2;
			}
			if (bottomPt.Pt.X < bottomPt2.Pt.X)
			{
				return outRec1;
			}
			if (bottomPt.Pt.X > bottomPt2.Pt.X)
			{
				return outRec2;
			}
			if (bottomPt.Next == bottomPt)
			{
				return outRec2;
			}
			if (bottomPt2.Next == bottomPt2)
			{
				return outRec1;
			}
			if (this.FirstIsBottomPt(bottomPt, bottomPt2))
			{
				return outRec1;
			}
			return outRec2;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000576A File Offset: 0x0000396A
		private bool Param1RightOfParam2(OutRec outRec1, OutRec outRec2)
		{
			for (;;)
			{
				outRec1 = outRec1.FirstLeft;
				if (outRec1 == outRec2)
				{
					break;
				}
				if (outRec1 == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00005780 File Offset: 0x00003980
		private OutRec GetOutRec(int idx)
		{
			OutRec outRec;
			for (outRec = this.m_PolyOuts[idx]; outRec != this.m_PolyOuts[outRec.Idx]; outRec = this.m_PolyOuts[outRec.Idx])
			{
			}
			return outRec;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000057C4 File Offset: 0x000039C4
		private void AppendPolygon(TEdge e1, TEdge e2)
		{
			OutRec outRec = this.m_PolyOuts[e1.OutIdx];
			OutRec outRec2 = this.m_PolyOuts[e2.OutIdx];
			OutRec outRec3;
			if (this.Param1RightOfParam2(outRec, outRec2))
			{
				outRec3 = outRec2;
			}
			else if (this.Param1RightOfParam2(outRec2, outRec))
			{
				outRec3 = outRec;
			}
			else
			{
				outRec3 = this.GetLowermostRec(outRec, outRec2);
			}
			OutPt pts = outRec.Pts;
			OutPt prev = pts.Prev;
			OutPt pts2 = outRec2.Pts;
			OutPt prev2 = pts2.Prev;
			EdgeSide side;
			if (e1.Side == EdgeSide.esLeft)
			{
				if (e2.Side == EdgeSide.esLeft)
				{
					this.ReversePolyPtLinks(pts2);
					pts2.Next = pts;
					pts.Prev = pts2;
					prev.Next = prev2;
					prev2.Prev = prev;
					outRec.Pts = prev2;
				}
				else
				{
					prev2.Next = pts;
					pts.Prev = prev2;
					pts2.Prev = prev;
					prev.Next = pts2;
					outRec.Pts = pts2;
				}
				side = EdgeSide.esLeft;
			}
			else
			{
				if (e2.Side == EdgeSide.esRight)
				{
					this.ReversePolyPtLinks(pts2);
					prev.Next = prev2;
					prev2.Prev = prev;
					pts2.Next = pts;
					pts.Prev = pts2;
				}
				else
				{
					prev.Next = pts2;
					pts2.Prev = prev;
					pts.Prev = prev2;
					prev2.Next = pts;
				}
				side = EdgeSide.esRight;
			}
			outRec.BottomPt = null;
			if (outRec3 == outRec2)
			{
				if (outRec2.FirstLeft != outRec)
				{
					outRec.FirstLeft = outRec2.FirstLeft;
				}
				outRec.IsHole = outRec2.IsHole;
			}
			outRec2.Pts = null;
			outRec2.BottomPt = null;
			outRec2.FirstLeft = outRec;
			int outIdx = e1.OutIdx;
			int outIdx2 = e2.OutIdx;
			e1.OutIdx = -1;
			e2.OutIdx = -1;
			for (TEdge tedge = this.m_ActiveEdges; tedge != null; tedge = tedge.NextInAEL)
			{
				if (tedge.OutIdx == outIdx2)
				{
					tedge.OutIdx = outIdx;
					tedge.Side = side;
					break;
				}
			}
			outRec2.Idx = outRec.Idx;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000059A8 File Offset: 0x00003BA8
		private void ReversePolyPtLinks(OutPt pp)
		{
			if (pp == null)
			{
				return;
			}
			OutPt outPt = pp;
			do
			{
				OutPt next = outPt.Next;
				OutPt outPt2 = outPt;
				outPt2.Next = outPt2.Prev;
				outPt.Prev = next;
				outPt = next;
			}
			while (outPt != pp);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000059DC File Offset: 0x00003BDC
		private static void SwapSides(TEdge edge1, TEdge edge2)
		{
			EdgeSide side = edge1.Side;
			edge1.Side = edge2.Side;
			edge2.Side = side;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005A04 File Offset: 0x00003C04
		private static void SwapPolyIndexes(TEdge edge1, TEdge edge2)
		{
			int outIdx = edge1.OutIdx;
			edge1.OutIdx = edge2.OutIdx;
			edge2.OutIdx = outIdx;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005A2C File Offset: 0x00003C2C
		private void IntersectEdges(TEdge e1, TEdge e2, IntPoint pt, bool protect = false)
		{
			bool flag = !protect && e1.NextInLML == null && e1.Top.X == pt.X && e1.Top.Y == pt.Y;
			bool flag2 = !protect && e2.NextInLML == null && e2.Top.X == pt.X && e2.Top.Y == pt.Y;
			bool flag3 = e1.OutIdx >= 0;
			bool flag4 = e2.OutIdx >= 0;
			if (e1.PolyTyp == e2.PolyTyp)
			{
				if (this.IsEvenOddFillType(e1))
				{
					int windCnt = e1.WindCnt;
					e1.WindCnt = e2.WindCnt;
					e2.WindCnt = windCnt;
				}
				else
				{
					if (e1.WindCnt + e2.WindDelta == 0)
					{
						e1.WindCnt = -e1.WindCnt;
					}
					else
					{
						e1.WindCnt += e2.WindDelta;
					}
					if (e2.WindCnt - e1.WindDelta == 0)
					{
						e2.WindCnt = -e2.WindCnt;
					}
					else
					{
						e2.WindCnt -= e1.WindDelta;
					}
				}
			}
			else
			{
				if (!this.IsEvenOddFillType(e2))
				{
					e1.WindCnt2 += e2.WindDelta;
				}
				else
				{
					e1.WindCnt2 = ((e1.WindCnt2 == 0) ? 1 : 0);
				}
				if (!this.IsEvenOddFillType(e1))
				{
					e2.WindCnt2 -= e1.WindDelta;
				}
				else
				{
					e2.WindCnt2 = ((e2.WindCnt2 == 0) ? 1 : 0);
				}
			}
			PolyFillType polyFillType;
			PolyFillType polyFillType2;
			if (e1.PolyTyp == PolyType.ptSubject)
			{
				polyFillType = this.m_SubjFillType;
				polyFillType2 = this.m_ClipFillType;
			}
			else
			{
				polyFillType = this.m_ClipFillType;
				polyFillType2 = this.m_SubjFillType;
			}
			PolyFillType polyFillType3;
			PolyFillType polyFillType4;
			if (e2.PolyTyp == PolyType.ptSubject)
			{
				polyFillType3 = this.m_SubjFillType;
				polyFillType4 = this.m_ClipFillType;
			}
			else
			{
				polyFillType3 = this.m_ClipFillType;
				polyFillType4 = this.m_SubjFillType;
			}
			int num;
			if (polyFillType != PolyFillType.pftPositive)
			{
				if (polyFillType != PolyFillType.pftNegative)
				{
					num = Math.Abs(e1.WindCnt);
				}
				else
				{
					num = -e1.WindCnt;
				}
			}
			else
			{
				num = e1.WindCnt;
			}
			int num2;
			if (polyFillType3 != PolyFillType.pftPositive)
			{
				if (polyFillType3 != PolyFillType.pftNegative)
				{
					num2 = Math.Abs(e2.WindCnt);
				}
				else
				{
					num2 = -e2.WindCnt;
				}
			}
			else
			{
				num2 = e2.WindCnt;
			}
			if (flag3 && flag4)
			{
				if (flag || flag2 || (num != 0 && num != 1) || (num2 != 0 && num2 != 1) || (e1.PolyTyp != e2.PolyTyp && this.m_ClipType != ClipType.ctXor))
				{
					this.AddLocalMaxPoly(e1, e2, pt);
				}
				else
				{
					this.AddOutPt(e1, pt);
					this.AddOutPt(e2, pt);
					Clipper.SwapSides(e1, e2);
					Clipper.SwapPolyIndexes(e1, e2);
				}
			}
			else if (flag3)
			{
				if (num2 == 0 || num2 == 1)
				{
					this.AddOutPt(e1, pt);
					Clipper.SwapSides(e1, e2);
					Clipper.SwapPolyIndexes(e1, e2);
				}
			}
			else if (flag4)
			{
				if (num == 0 || num == 1)
				{
					this.AddOutPt(e2, pt);
					Clipper.SwapSides(e1, e2);
					Clipper.SwapPolyIndexes(e1, e2);
				}
			}
			else if ((num == 0 || num == 1) && (num2 == 0 || num2 == 1) && !flag && !flag2)
			{
				int num3;
				if (polyFillType2 != PolyFillType.pftPositive)
				{
					if (polyFillType2 != PolyFillType.pftNegative)
					{
						num3 = Math.Abs(e1.WindCnt2);
					}
					else
					{
						num3 = -e1.WindCnt2;
					}
				}
				else
				{
					num3 = e1.WindCnt2;
				}
				int num4;
				if (polyFillType4 != PolyFillType.pftPositive)
				{
					if (polyFillType4 != PolyFillType.pftNegative)
					{
						num4 = Math.Abs(e2.WindCnt2);
					}
					else
					{
						num4 = -e2.WindCnt2;
					}
				}
				else
				{
					num4 = e2.WindCnt2;
				}
				if (e1.PolyTyp != e2.PolyTyp)
				{
					this.AddLocalMinPoly(e1, e2, pt);
				}
				else if (num == 1 && num2 == 1)
				{
					switch (this.m_ClipType)
					{
					case ClipType.ctIntersection:
						if (num3 > 0 && num4 > 0)
						{
							this.AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case ClipType.ctUnion:
						if (num3 <= 0 && num4 <= 0)
						{
							this.AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case ClipType.ctDifference:
						if ((e1.PolyTyp == PolyType.ptClip && num3 > 0 && num4 > 0) || (e1.PolyTyp == PolyType.ptSubject && num3 <= 0 && num4 <= 0))
						{
							this.AddLocalMinPoly(e1, e2, pt);
						}
						break;
					case ClipType.ctXor:
						this.AddLocalMinPoly(e1, e2, pt);
						break;
					}
				}
				else
				{
					Clipper.SwapSides(e1, e2);
				}
			}
			if (flag != flag2 && ((flag && e1.OutIdx >= 0) || (flag2 && e2.OutIdx >= 0)))
			{
				Clipper.SwapSides(e1, e2);
				Clipper.SwapPolyIndexes(e1, e2);
			}
			if (flag)
			{
				this.DeleteFromAEL(e1);
			}
			if (flag2)
			{
				this.DeleteFromAEL(e2);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005EA8 File Offset: 0x000040A8
		private void DeleteFromAEL(TEdge e)
		{
			TEdge prevInAEL = e.PrevInAEL;
			TEdge nextInAEL = e.NextInAEL;
			if (prevInAEL == null && nextInAEL == null && e != this.m_ActiveEdges)
			{
				return;
			}
			if (prevInAEL != null)
			{
				prevInAEL.NextInAEL = nextInAEL;
			}
			else
			{
				this.m_ActiveEdges = nextInAEL;
			}
			if (nextInAEL != null)
			{
				nextInAEL.PrevInAEL = prevInAEL;
			}
			e.NextInAEL = null;
			e.PrevInAEL = null;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005F00 File Offset: 0x00004100
		private void DeleteFromSEL(TEdge e)
		{
			TEdge prevInSEL = e.PrevInSEL;
			TEdge nextInSEL = e.NextInSEL;
			if (prevInSEL == null && nextInSEL == null && e != this.m_SortedEdges)
			{
				return;
			}
			if (prevInSEL != null)
			{
				prevInSEL.NextInSEL = nextInSEL;
			}
			else
			{
				this.m_SortedEdges = nextInSEL;
			}
			if (nextInSEL != null)
			{
				nextInSEL.PrevInSEL = prevInSEL;
			}
			e.NextInSEL = null;
			e.PrevInSEL = null;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005F58 File Offset: 0x00004158
		private void UpdateEdgeIntoAEL(ref TEdge e)
		{
			if (e.NextInLML == null)
			{
				throw new ClipperException("UpdateEdgeIntoAEL: invalid call");
			}
			TEdge prevInAEL = e.PrevInAEL;
			TEdge nextInAEL = e.NextInAEL;
			e.NextInLML.OutIdx = e.OutIdx;
			if (prevInAEL != null)
			{
				prevInAEL.NextInAEL = e.NextInLML;
			}
			else
			{
				this.m_ActiveEdges = e.NextInLML;
			}
			if (nextInAEL != null)
			{
				nextInAEL.PrevInAEL = e.NextInLML;
			}
			e.NextInLML.Side = e.Side;
			e.NextInLML.WindDelta = e.WindDelta;
			e.NextInLML.WindCnt = e.WindCnt;
			e.NextInLML.WindCnt2 = e.WindCnt2;
			e = e.NextInLML;
			TEdge tedge = e;
			tedge.Curr = tedge.Bot;
			e.PrevInAEL = prevInAEL;
			e.NextInAEL = nextInAEL;
			if (!ClipperBase.IsHorizontal(e))
			{
				this.InsertScanbeam(e.Top.Y);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006058 File Offset: 0x00004258
		private void ProcessHorizontals(bool isTopOfScanbeam)
		{
			for (TEdge sortedEdges = this.m_SortedEdges; sortedEdges != null; sortedEdges = this.m_SortedEdges)
			{
				this.DeleteFromSEL(sortedEdges);
				this.ProcessHorizontal(sortedEdges, isTopOfScanbeam);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006088 File Offset: 0x00004288
		private void GetHorzDirection(TEdge HorzEdge, out Direction Dir, out int Left, out int Right)
		{
			if (HorzEdge.Bot.X < HorzEdge.Top.X)
			{
				Left = HorzEdge.Bot.X;
				Right = HorzEdge.Top.X;
				Dir = Direction.dLeftToRight;
				return;
			}
			Left = HorzEdge.Top.X;
			Right = HorzEdge.Bot.X;
			Dir = Direction.dRightToLeft;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000060EC File Offset: 0x000042EC
		private void PrepareHorzJoins(TEdge horzEdge, bool isTopOfScanbeam)
		{
			OutPt outPt = this.m_PolyOuts[horzEdge.OutIdx].Pts;
			if (horzEdge.Side != EdgeSide.esLeft)
			{
				outPt = outPt.Prev;
			}
			if (isTopOfScanbeam)
			{
				if (outPt.Pt == horzEdge.Top)
				{
					this.AddGhostJoin(outPt, horzEdge.Bot);
					return;
				}
				this.AddGhostJoin(outPt, horzEdge.Top);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00006150 File Offset: 0x00004350
		private void ProcessHorizontal(TEdge horzEdge, bool isTopOfScanbeam)
		{
			Direction direction;
			int num;
			int num2;
			this.GetHorzDirection(horzEdge, out direction, out num, out num2);
			TEdge tedge = horzEdge;
			TEdge tedge2 = null;
			while (tedge.NextInLML != null && ClipperBase.IsHorizontal(tedge.NextInLML))
			{
				tedge = tedge.NextInLML;
			}
			if (tedge.NextInLML == null)
			{
				tedge2 = this.GetMaximaPair(tedge);
			}
			TEdge tedge3;
			for (;;)
			{
				bool flag = horzEdge == tedge;
				tedge3 = this.GetNextInAEL(horzEdge, direction);
				while (tedge3 != null && (tedge3.Curr.X != horzEdge.Top.X || horzEdge.NextInLML == null || tedge3.Dx >= horzEdge.NextInLML.Dx))
				{
					TEdge nextInAEL = this.GetNextInAEL(tedge3, direction);
					if ((direction == Direction.dLeftToRight && tedge3.Curr.X <= num2) || (direction == Direction.dRightToLeft && tedge3.Curr.X >= num))
					{
						if (horzEdge.OutIdx >= 0 && horzEdge.WindDelta != 0)
						{
							this.PrepareHorzJoins(horzEdge, isTopOfScanbeam);
						}
						if (tedge3 == tedge2 && flag)
						{
							goto Block_9;
						}
						if (direction == Direction.dLeftToRight)
						{
							IntPoint pt = new IntPoint(tedge3.Curr.X, horzEdge.Curr.Y);
							this.IntersectEdges(horzEdge, tedge3, pt, true);
						}
						else
						{
							IntPoint pt2 = new IntPoint(tedge3.Curr.X, horzEdge.Curr.Y);
							this.IntersectEdges(tedge3, horzEdge, pt2, true);
						}
						this.SwapPositionsInAEL(horzEdge, tedge3);
					}
					else if ((direction == Direction.dLeftToRight && tedge3.Curr.X >= num2) || (direction == Direction.dRightToLeft && tedge3.Curr.X <= num))
					{
						break;
					}
					tedge3 = nextInAEL;
				}
				if (horzEdge.OutIdx >= 0 && horzEdge.WindDelta != 0)
				{
					this.PrepareHorzJoins(horzEdge, isTopOfScanbeam);
				}
				if (horzEdge.NextInLML == null || !ClipperBase.IsHorizontal(horzEdge.NextInLML))
				{
					goto IL_219;
				}
				this.UpdateEdgeIntoAEL(ref horzEdge);
				if (horzEdge.OutIdx >= 0)
				{
					TEdge tedge4 = horzEdge;
					this.AddOutPt(tedge4, tedge4.Bot);
				}
				this.GetHorzDirection(horzEdge, out direction, out num, out num2);
			}
			Block_9:
			if (direction == Direction.dLeftToRight)
			{
				TEdge e = horzEdge;
				TEdge tedge5 = tedge3;
				this.IntersectEdges(e, tedge5, tedge5.Top, false);
			}
			else
			{
				this.IntersectEdges(tedge3, horzEdge, tedge3.Top, false);
			}
			if (tedge2.OutIdx >= 0)
			{
				throw new ClipperException("ProcessHorizontal error");
			}
			return;
			IL_219:
			if (horzEdge.NextInLML != null)
			{
				if (horzEdge.OutIdx < 0)
				{
					this.UpdateEdgeIntoAEL(ref horzEdge);
					return;
				}
				TEdge tedge6 = horzEdge;
				OutPt op = this.AddOutPt(tedge6, tedge6.Top);
				this.UpdateEdgeIntoAEL(ref horzEdge);
				if (horzEdge.WindDelta == 0)
				{
					return;
				}
				TEdge prevInAEL = horzEdge.PrevInAEL;
				TEdge nextInAEL2 = horzEdge.NextInAEL;
				if (prevInAEL != null && prevInAEL.Curr.X == horzEdge.Bot.X && prevInAEL.Curr.Y == horzEdge.Bot.Y && prevInAEL.WindDelta != 0 && prevInAEL.OutIdx >= 0 && prevInAEL.Curr.Y > prevInAEL.Top.Y && ClipperBase.SlopesEqual(horzEdge, prevInAEL, this.m_UseFullRange))
				{
					OutPt op2 = this.AddOutPt(prevInAEL, horzEdge.Bot);
					this.AddJoin(op, op2, horzEdge.Top);
					return;
				}
				if (nextInAEL2 != null && nextInAEL2.Curr.X == horzEdge.Bot.X && nextInAEL2.Curr.Y == horzEdge.Bot.Y && nextInAEL2.WindDelta != 0 && nextInAEL2.OutIdx >= 0 && nextInAEL2.Curr.Y > nextInAEL2.Top.Y && ClipperBase.SlopesEqual(horzEdge, nextInAEL2, this.m_UseFullRange))
				{
					OutPt op3 = this.AddOutPt(nextInAEL2, horzEdge.Bot);
					this.AddJoin(op, op3, horzEdge.Top);
					return;
				}
			}
			else if (tedge2 != null)
			{
				if (tedge2.OutIdx < 0)
				{
					this.DeleteFromAEL(horzEdge);
					this.DeleteFromAEL(tedge2);
					return;
				}
				if (direction == Direction.dLeftToRight)
				{
					this.IntersectEdges(horzEdge, tedge2, horzEdge.Top, false);
				}
				else
				{
					TEdge e2 = tedge2;
					TEdge tedge7 = horzEdge;
					this.IntersectEdges(e2, tedge7, tedge7.Top, false);
				}
				if (tedge2.OutIdx >= 0)
				{
					throw new ClipperException("ProcessHorizontal error");
				}
			}
			else
			{
				if (horzEdge.OutIdx >= 0)
				{
					TEdge tedge8 = horzEdge;
					this.AddOutPt(tedge8, tedge8.Top);
				}
				this.DeleteFromAEL(horzEdge);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000657D File Offset: 0x0000477D
		private TEdge GetNextInAEL(TEdge e, Direction Direction)
		{
			if (Direction != Direction.dLeftToRight)
			{
				return e.PrevInAEL;
			}
			return e.NextInAEL;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00006590 File Offset: 0x00004790
		private bool IsMinima(TEdge e)
		{
			return e != null && e.Prev.NextInLML != e && e.Next.NextInLML != e;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000065B6 File Offset: 0x000047B6
		private bool IsMaxima(TEdge e, float Y)
		{
			return e != null && (float)e.Top.Y == Y && e.NextInLML == null;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000065D5 File Offset: 0x000047D5
		private bool IsIntermediate(TEdge e, float Y)
		{
			return (float)e.Top.Y == Y && e.NextInLML != null;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000065F4 File Offset: 0x000047F4
		private TEdge GetMaximaPair(TEdge e)
		{
			TEdge tedge = null;
			if (e.Next.Top == e.Top && e.Next.NextInLML == null)
			{
				tedge = e.Next;
			}
			else if (e.Prev.Top == e.Top && e.Prev.NextInLML == null)
			{
				tedge = e.Prev;
			}
			if (tedge != null && (tedge.OutIdx == -2 || (tedge.NextInAEL == tedge.PrevInAEL && !ClipperBase.IsHorizontal(tedge))))
			{
				return null;
			}
			return tedge;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006684 File Offset: 0x00004884
		private bool ProcessIntersections(int botY, int topY)
		{
			if (this.m_ActiveEdges == null)
			{
				return true;
			}
			try
			{
				this.BuildIntersectList(botY, topY);
				if (this.m_IntersectList.Count == 0)
				{
					return true;
				}
				if (this.m_IntersectList.Count != 1 && !this.FixupIntersectionOrder())
				{
					return false;
				}
				this.ProcessIntersectList();
			}
			catch
			{
				this.m_SortedEdges = null;
				this.m_IntersectList.Clear();
				throw new ClipperException("ProcessIntersections error");
			}
			this.m_SortedEdges = null;
			return true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006714 File Offset: 0x00004914
		private void BuildIntersectList(int botY, int topY)
		{
			if (this.m_ActiveEdges == null)
			{
				return;
			}
			TEdge tedge = this.m_ActiveEdges;
			this.m_SortedEdges = tedge;
			while (tedge != null)
			{
				TEdge tedge2 = tedge;
				tedge2.PrevInSEL = tedge2.PrevInAEL;
				TEdge tedge3 = tedge;
				tedge3.NextInSEL = tedge3.NextInAEL;
				tedge.Curr.X = Clipper.TopX(tedge, topY);
				tedge = tedge.NextInAEL;
			}
			bool flag = true;
			while (flag && this.m_SortedEdges != null)
			{
				flag = false;
				tedge = this.m_SortedEdges;
				while (tedge.NextInSEL != null)
				{
					TEdge nextInSEL = tedge.NextInSEL;
					if (tedge.Curr.X > nextInSEL.Curr.X)
					{
						IntPoint intPoint;
						if (!this.IntersectPoint(tedge, nextInSEL, out intPoint) && tedge.Curr.X > nextInSEL.Curr.X + 1)
						{
							throw new ClipperException("Intersection error");
						}
						if (intPoint.Y > botY)
						{
							intPoint.Y = botY;
							if (Math.Abs(tedge.Dx) > Math.Abs(nextInSEL.Dx))
							{
								intPoint.X = Clipper.TopX(nextInSEL, botY);
							}
							else
							{
								intPoint.X = Clipper.TopX(tedge, botY);
							}
						}
						IntersectNode intersectNode = new IntersectNode();
						intersectNode.Edge1 = tedge;
						intersectNode.Edge2 = nextInSEL;
						intersectNode.Pt = intPoint;
						this.m_IntersectList.Add(intersectNode);
						this.SwapPositionsInSEL(tedge, nextInSEL);
						flag = true;
					}
					else
					{
						tedge = nextInSEL;
					}
				}
				if (tedge.PrevInSEL == null)
				{
					break;
				}
				tedge.PrevInSEL.NextInSEL = null;
			}
			this.m_SortedEdges = null;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000688B File Offset: 0x00004A8B
		private bool EdgesAdjacent(IntersectNode inode)
		{
			return inode.Edge1.NextInSEL == inode.Edge2 || inode.Edge1.PrevInSEL == inode.Edge2;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000068B5 File Offset: 0x00004AB5
		private static int IntersectNodeSort(IntersectNode node1, IntersectNode node2)
		{
			return node2.Pt.Y - node1.Pt.Y;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000068D0 File Offset: 0x00004AD0
		private bool FixupIntersectionOrder()
		{
			this.m_IntersectList.Sort(this.m_IntersectNodeComparer);
			this.CopyAELToSEL();
			int count = this.m_IntersectList.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.EdgesAdjacent(this.m_IntersectList[i]))
				{
					int num = i + 1;
					while (num < count && !this.EdgesAdjacent(this.m_IntersectList[num]))
					{
						num++;
					}
					if (num == count)
					{
						return false;
					}
					IntersectNode value = this.m_IntersectList[i];
					this.m_IntersectList[i] = this.m_IntersectList[num];
					this.m_IntersectList[num] = value;
				}
				this.SwapPositionsInSEL(this.m_IntersectList[i].Edge1, this.m_IntersectList[i].Edge2);
			}
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000069AC File Offset: 0x00004BAC
		private void ProcessIntersectList()
		{
			for (int i = 0; i < this.m_IntersectList.Count; i++)
			{
				IntersectNode intersectNode = this.m_IntersectList[i];
				this.IntersectEdges(intersectNode.Edge1, intersectNode.Edge2, intersectNode.Pt, true);
				this.SwapPositionsInAEL(intersectNode.Edge1, intersectNode.Edge2);
			}
			this.m_IntersectList.Clear();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006A12 File Offset: 0x00004C12
		internal static int Round(float value)
		{
			if (value >= 0f)
			{
				return (int)((double)value + 0.5);
			}
			return (int)((double)value - 0.5);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006A38 File Offset: 0x00004C38
		private static int TopX(TEdge edge, int currentY)
		{
			if (currentY == edge.Top.Y)
			{
				return edge.Top.X;
			}
			return edge.Bot.X + Clipper.Round(edge.Dx * (float)(currentY - edge.Bot.Y));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006A88 File Offset: 0x00004C88
		private bool IntersectPoint(TEdge edge1, TEdge edge2, out IntPoint ip)
		{
			ip = default(IntPoint);
			if (ClipperBase.SlopesEqual(edge1, edge2, this.m_UseFullRange) || edge1.Dx == edge2.Dx)
			{
				if (edge2.Bot.Y > edge1.Bot.Y)
				{
					ip = edge2.Bot;
				}
				else
				{
					ip = edge1.Bot;
				}
				return false;
			}
			if (edge1.Delta.X == 0)
			{
				ip.X = edge1.Bot.X;
				if (ClipperBase.IsHorizontal(edge2))
				{
					ip.Y = edge2.Bot.Y;
				}
				else
				{
					float num = (float)edge2.Bot.Y - (float)edge2.Bot.X / edge2.Dx;
					ip.Y = Clipper.Round((float)ip.X / edge2.Dx + num);
				}
			}
			else if (edge2.Delta.X == 0)
			{
				ip.X = edge2.Bot.X;
				if (ClipperBase.IsHorizontal(edge1))
				{
					ip.Y = edge1.Bot.Y;
				}
				else
				{
					float num2 = (float)edge1.Bot.Y - (float)edge1.Bot.X / edge1.Dx;
					ip.Y = Clipper.Round((float)ip.X / edge1.Dx + num2);
				}
			}
			else
			{
				float num2 = (float)edge1.Bot.X - (float)edge1.Bot.Y * edge1.Dx;
				float num = (float)edge2.Bot.X - (float)edge2.Bot.Y * edge2.Dx;
				float num3 = (num - num2) / (edge1.Dx - edge2.Dx);
				ip.Y = Clipper.Round(num3);
				if (Math.Abs(edge1.Dx) < Math.Abs(edge2.Dx))
				{
					ip.X = Clipper.Round(edge1.Dx * num3 + num2);
				}
				else
				{
					ip.X = Clipper.Round(edge2.Dx * num3 + num);
				}
			}
			if (ip.Y < edge1.Top.Y || ip.Y < edge2.Top.Y)
			{
				if (edge1.Top.Y > edge2.Top.Y)
				{
					ip.Y = edge1.Top.Y;
				}
				else
				{
					ip.Y = edge2.Top.Y;
				}
				if (Math.Abs(edge1.Dx) < Math.Abs(edge2.Dx))
				{
					ip.X = Clipper.TopX(edge1, ip.Y);
				}
				else
				{
					ip.X = Clipper.TopX(edge2, ip.Y);
				}
			}
			return true;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006D2C File Offset: 0x00004F2C
		private void ProcessEdgesAtTopOfScanbeam(int topY)
		{
			TEdge tedge = this.m_ActiveEdges;
			while (tedge != null)
			{
				bool flag = this.IsMaxima(tedge, (float)topY);
				if (flag)
				{
					TEdge maximaPair = this.GetMaximaPair(tedge);
					flag = (maximaPair == null || !ClipperBase.IsHorizontal(maximaPair));
				}
				if (flag)
				{
					TEdge prevInAEL = tedge.PrevInAEL;
					this.DoMaxima(tedge);
					if (prevInAEL == null)
					{
						tedge = this.m_ActiveEdges;
					}
					else
					{
						tedge = prevInAEL.NextInAEL;
					}
				}
				else
				{
					if (this.IsIntermediate(tedge, (float)topY) && ClipperBase.IsHorizontal(tedge.NextInLML))
					{
						this.UpdateEdgeIntoAEL(ref tedge);
						if (tedge.OutIdx >= 0)
						{
							TEdge tedge2 = tedge;
							this.AddOutPt(tedge2, tedge2.Bot);
						}
						this.AddEdgeToSEL(tedge);
					}
					else
					{
						tedge.Curr.X = Clipper.TopX(tedge, topY);
						tedge.Curr.Y = topY;
					}
					if (this.StrictlySimple)
					{
						TEdge prevInAEL2 = tedge.PrevInAEL;
						if (tedge.OutIdx >= 0 && tedge.WindDelta != 0 && prevInAEL2 != null && prevInAEL2.OutIdx >= 0 && prevInAEL2.Curr.X == tedge.Curr.X && prevInAEL2.WindDelta != 0)
						{
							OutPt op = this.AddOutPt(prevInAEL2, tedge.Curr);
							TEdge tedge3 = tedge;
							OutPt op2 = this.AddOutPt(tedge3, tedge3.Curr);
							this.AddJoin(op, op2, tedge.Curr);
						}
					}
					tedge = tedge.NextInAEL;
				}
			}
			this.ProcessHorizontals(true);
			for (tedge = this.m_ActiveEdges; tedge != null; tedge = tedge.NextInAEL)
			{
				if (this.IsIntermediate(tedge, (float)topY))
				{
					OutPt outPt = null;
					if (tedge.OutIdx >= 0)
					{
						TEdge tedge4 = tedge;
						outPt = this.AddOutPt(tedge4, tedge4.Top);
					}
					this.UpdateEdgeIntoAEL(ref tedge);
					TEdge prevInAEL3 = tedge.PrevInAEL;
					TEdge nextInAEL = tedge.NextInAEL;
					if (prevInAEL3 != null && prevInAEL3.Curr.X == tedge.Bot.X && prevInAEL3.Curr.Y == tedge.Bot.Y && outPt != null && prevInAEL3.OutIdx >= 0 && prevInAEL3.Curr.Y > prevInAEL3.Top.Y && ClipperBase.SlopesEqual(tedge, prevInAEL3, this.m_UseFullRange) && tedge.WindDelta != 0 && prevInAEL3.WindDelta != 0)
					{
						OutPt op3 = this.AddOutPt(prevInAEL3, tedge.Bot);
						this.AddJoin(outPt, op3, tedge.Top);
					}
					else if (nextInAEL != null && nextInAEL.Curr.X == tedge.Bot.X && nextInAEL.Curr.Y == tedge.Bot.Y && outPt != null && nextInAEL.OutIdx >= 0 && nextInAEL.Curr.Y > nextInAEL.Top.Y && ClipperBase.SlopesEqual(tedge, nextInAEL, this.m_UseFullRange) && tedge.WindDelta != 0 && nextInAEL.WindDelta != 0)
					{
						OutPt op4 = this.AddOutPt(nextInAEL, tedge.Bot);
						this.AddJoin(outPt, op4, tedge.Top);
					}
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007034 File Offset: 0x00005234
		private void DoMaxima(TEdge e)
		{
			TEdge maximaPair = this.GetMaximaPair(e);
			if (maximaPair == null)
			{
				if (e.OutIdx >= 0)
				{
					this.AddOutPt(e, e.Top);
				}
				this.DeleteFromAEL(e);
				return;
			}
			TEdge nextInAEL = e.NextInAEL;
			while (nextInAEL != null && nextInAEL != maximaPair)
			{
				this.IntersectEdges(e, nextInAEL, e.Top, true);
				this.SwapPositionsInAEL(e, nextInAEL);
				nextInAEL = e.NextInAEL;
			}
			if (e.OutIdx == -1 && maximaPair.OutIdx == -1)
			{
				this.DeleteFromAEL(e);
				this.DeleteFromAEL(maximaPair);
				return;
			}
			if (e.OutIdx >= 0 && maximaPair.OutIdx >= 0)
			{
				this.IntersectEdges(e, maximaPair, e.Top, false);
				return;
			}
			throw new ClipperException("DoMaxima error");
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000070E8 File Offset: 0x000052E8
		public static void ReversePaths(List<List<IntPoint>> polys)
		{
			foreach (List<IntPoint> list in polys)
			{
				list.Reverse();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007134 File Offset: 0x00005334
		public static bool Orientation(List<IntPoint> poly)
		{
			return Clipper.Area(poly) >= 0f;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00007148 File Offset: 0x00005348
		private int PointCount(OutPt pts)
		{
			if (pts == null)
			{
				return 0;
			}
			int num = 0;
			OutPt outPt = pts;
			do
			{
				num++;
				outPt = outPt.Next;
			}
			while (outPt != pts);
			return num;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00007170 File Offset: 0x00005370
		private void BuildResult(List<List<IntPoint>> polyg)
		{
			polyg.Clear();
			polyg.Capacity = this.m_PolyOuts.Count;
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				if (outRec.Pts != null)
				{
					OutPt prev = outRec.Pts.Prev;
					int num = this.PointCount(prev);
					if (num >= 2)
					{
						List<IntPoint> list = new List<IntPoint>(num);
						for (int j = 0; j < num; j++)
						{
							list.Add(prev.Pt);
							prev = prev.Prev;
						}
						polyg.Add(list);
					}
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000720C File Offset: 0x0000540C
		private void BuildResult2(PolyTree polytree)
		{
			polytree.Clear();
			polytree.m_AllPolys.Capacity = this.m_PolyOuts.Count;
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				int num = this.PointCount(outRec.Pts);
				if ((!outRec.IsOpen || num >= 2) && (outRec.IsOpen || num >= 3))
				{
					this.FixHoleLinkage(outRec);
					PolyNode polyNode = new PolyNode();
					polytree.m_AllPolys.Add(polyNode);
					outRec.PolyNode = polyNode;
					polyNode.m_polygon.Capacity = num;
					OutPt prev = outRec.Pts.Prev;
					for (int j = 0; j < num; j++)
					{
						polyNode.m_polygon.Add(prev.Pt);
						prev = prev.Prev;
					}
				}
			}
			polytree.m_Childs.Capacity = this.m_PolyOuts.Count;
			for (int k = 0; k < this.m_PolyOuts.Count; k++)
			{
				OutRec outRec2 = this.m_PolyOuts[k];
				if (outRec2.PolyNode != null)
				{
					if (outRec2.IsOpen)
					{
						outRec2.PolyNode.IsOpen = true;
						polytree.AddChild(outRec2.PolyNode);
					}
					else if (outRec2.FirstLeft != null && outRec2.FirstLeft.PolyNode != null)
					{
						outRec2.FirstLeft.PolyNode.AddChild(outRec2.PolyNode);
					}
					else
					{
						polytree.AddChild(outRec2.PolyNode);
					}
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00007398 File Offset: 0x00005598
		private void FixupOutPolygon(OutRec outRec)
		{
			OutPt outPt = null;
			outRec.BottomPt = null;
			OutPt outPt2 = outRec.Pts;
			while (outPt2.Prev != outPt2 && outPt2.Prev != outPt2.Next)
			{
				if (outPt2.Pt == outPt2.Next.Pt || outPt2.Pt == outPt2.Prev.Pt || (ClipperBase.SlopesEqual(outPt2.Prev.Pt, outPt2.Pt, outPt2.Next.Pt, this.m_UseFullRange) && (!base.PreserveCollinear || !base.Pt2IsBetweenPt1AndPt3(outPt2.Prev.Pt, outPt2.Pt, outPt2.Next.Pt))))
				{
					outPt = null;
					outPt2.Prev.Next = outPt2.Next;
					outPt2.Next.Prev = outPt2.Prev;
					outPt2 = outPt2.Prev;
				}
				else
				{
					if (outPt2 == outPt)
					{
						outRec.Pts = outPt2;
						return;
					}
					if (outPt == null)
					{
						outPt = outPt2;
					}
					outPt2 = outPt2.Next;
				}
			}
			this.DisposeOutPts(outPt2);
			outRec.Pts = null;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000074AC File Offset: 0x000056AC
		private OutPt DupOutPt(OutPt outPt, bool InsertAfter)
		{
			OutPt outPt2 = new OutPt();
			outPt2.Pt = outPt.Pt;
			outPt2.Idx = outPt.Idx;
			if (InsertAfter)
			{
				outPt2.Next = outPt.Next;
				outPt2.Prev = outPt;
				outPt.Next.Prev = outPt2;
				outPt.Next = outPt2;
			}
			else
			{
				outPt2.Prev = outPt.Prev;
				outPt2.Next = outPt;
				outPt.Prev.Next = outPt2;
				outPt.Prev = outPt2;
			}
			return outPt2;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000752C File Offset: 0x0000572C
		private bool GetOverlap(int a1, int a2, int b1, int b2, out int Left, out int Right)
		{
			if (a1 < a2)
			{
				if (b1 < b2)
				{
					Left = Math.Max(a1, b1);
					Right = Math.Min(a2, b2);
				}
				else
				{
					Left = Math.Max(a1, b2);
					Right = Math.Min(a2, b1);
				}
			}
			else if (b1 < b2)
			{
				Left = Math.Max(a2, b1);
				Right = Math.Min(a1, b2);
			}
			else
			{
				Left = Math.Max(a2, b2);
				Right = Math.Min(a1, b1);
			}
			return Left < Right;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000075AC File Offset: 0x000057AC
		private bool JoinHorz(OutPt op1, OutPt op1b, OutPt op2, OutPt op2b, IntPoint Pt, bool DiscardLeft)
		{
			Direction direction = (op1.Pt.X > op1b.Pt.X) ? Direction.dRightToLeft : Direction.dLeftToRight;
			Direction direction2 = (op2.Pt.X > op2b.Pt.X) ? Direction.dRightToLeft : Direction.dLeftToRight;
			if (direction == direction2)
			{
				return false;
			}
			if (direction == Direction.dLeftToRight)
			{
				while (op1.Next.Pt.X <= Pt.X && op1.Next.Pt.X >= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
				{
					op1 = op1.Next;
				}
				if (DiscardLeft && op1.Pt.X != Pt.X)
				{
					op1 = op1.Next;
				}
				op1b = this.DupOutPt(op1, !DiscardLeft);
				if (op1b.Pt != Pt)
				{
					op1 = op1b;
					op1.Pt = Pt;
					op1b = this.DupOutPt(op1, !DiscardLeft);
				}
			}
			else
			{
				while (op1.Next.Pt.X >= Pt.X && op1.Next.Pt.X <= op1.Pt.X && op1.Next.Pt.Y == Pt.Y)
				{
					op1 = op1.Next;
				}
				if (!DiscardLeft && op1.Pt.X != Pt.X)
				{
					op1 = op1.Next;
				}
				op1b = this.DupOutPt(op1, DiscardLeft);
				if (op1b.Pt != Pt)
				{
					op1 = op1b;
					op1.Pt = Pt;
					op1b = this.DupOutPt(op1, DiscardLeft);
				}
			}
			if (direction2 == Direction.dLeftToRight)
			{
				while (op2.Next.Pt.X <= Pt.X && op2.Next.Pt.X >= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
				{
					op2 = op2.Next;
				}
				if (DiscardLeft && op2.Pt.X != Pt.X)
				{
					op2 = op2.Next;
				}
				op2b = this.DupOutPt(op2, !DiscardLeft);
				if (op2b.Pt != Pt)
				{
					op2 = op2b;
					op2.Pt = Pt;
					op2b = this.DupOutPt(op2, !DiscardLeft);
				}
			}
			else
			{
				while (op2.Next.Pt.X >= Pt.X && op2.Next.Pt.X <= op2.Pt.X && op2.Next.Pt.Y == Pt.Y)
				{
					op2 = op2.Next;
				}
				if (!DiscardLeft && op2.Pt.X != Pt.X)
				{
					op2 = op2.Next;
				}
				op2b = this.DupOutPt(op2, DiscardLeft);
				if (op2b.Pt != Pt)
				{
					op2 = op2b;
					op2.Pt = Pt;
					op2b = this.DupOutPt(op2, DiscardLeft);
				}
			}
			if (direction == Direction.dLeftToRight == DiscardLeft)
			{
				op1.Prev = op2;
				op2.Next = op1;
				op1b.Next = op2b;
				op2b.Prev = op1b;
			}
			else
			{
				op1.Next = op2;
				op2.Prev = op1;
				op1b.Prev = op2b;
				op2b.Next = op1b;
			}
			return true;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00007910 File Offset: 0x00005B10
		private bool JoinPoints(Join j, OutRec outRec1, OutRec outRec2)
		{
			OutPt outPt = j.OutPt1;
			OutPt outPt2 = j.OutPt2;
			bool flag = j.OutPt1.Pt.Y == j.OffPt.Y;
			if (flag && j.OffPt == j.OutPt1.Pt && j.OffPt == j.OutPt2.Pt)
			{
				OutPt outPt3 = j.OutPt1.Next;
				while (outPt3 != outPt && outPt3.Pt == j.OffPt)
				{
					outPt3 = outPt3.Next;
				}
				bool flag2 = outPt3.Pt.Y > j.OffPt.Y;
				OutPt outPt4 = j.OutPt2.Next;
				while (outPt4 != outPt2 && outPt4.Pt == j.OffPt)
				{
					outPt4 = outPt4.Next;
				}
				bool flag3 = outPt4.Pt.Y > j.OffPt.Y;
				if (flag2 == flag3)
				{
					return false;
				}
				if (flag2)
				{
					outPt3 = this.DupOutPt(outPt, false);
					outPt4 = this.DupOutPt(outPt2, true);
					outPt.Prev = outPt2;
					outPt2.Next = outPt;
					outPt3.Next = outPt4;
					outPt4.Prev = outPt3;
					j.OutPt1 = outPt;
					j.OutPt2 = outPt3;
					return true;
				}
				outPt3 = this.DupOutPt(outPt, true);
				outPt4 = this.DupOutPt(outPt2, false);
				outPt.Next = outPt2;
				outPt2.Prev = outPt;
				outPt3.Prev = outPt4;
				outPt4.Next = outPt3;
				j.OutPt1 = outPt;
				j.OutPt2 = outPt3;
				return true;
			}
			else if (flag)
			{
				OutPt outPt3 = outPt;
				while (outPt.Prev.Pt.Y == outPt.Pt.Y && outPt.Prev != outPt3)
				{
					if (outPt.Prev == outPt2)
					{
						break;
					}
					outPt = outPt.Prev;
				}
				while (outPt3.Next.Pt.Y == outPt3.Pt.Y && outPt3.Next != outPt && outPt3.Next != outPt2)
				{
					outPt3 = outPt3.Next;
				}
				if (outPt3.Next == outPt || outPt3.Next == outPt2)
				{
					return false;
				}
				OutPt outPt4 = outPt2;
				while (outPt2.Prev.Pt.Y == outPt2.Pt.Y && outPt2.Prev != outPt4)
				{
					if (outPt2.Prev == outPt3)
					{
						break;
					}
					outPt2 = outPt2.Prev;
				}
				while (outPt4.Next.Pt.Y == outPt4.Pt.Y && outPt4.Next != outPt2 && outPt4.Next != outPt)
				{
					outPt4 = outPt4.Next;
				}
				if (outPt4.Next == outPt2 || outPt4.Next == outPt)
				{
					return false;
				}
				int num;
				int num2;
				if (!this.GetOverlap(outPt.Pt.X, outPt3.Pt.X, outPt2.Pt.X, outPt4.Pt.X, out num, out num2))
				{
					return false;
				}
				IntPoint pt;
				bool discardLeft;
				if (outPt.Pt.X >= num && outPt.Pt.X <= num2)
				{
					pt = outPt.Pt;
					discardLeft = (outPt.Pt.X > outPt3.Pt.X);
				}
				else if (outPt2.Pt.X >= num && outPt2.Pt.X <= num2)
				{
					pt = outPt2.Pt;
					discardLeft = (outPt2.Pt.X > outPt4.Pt.X);
				}
				else if (outPt3.Pt.X >= num && outPt3.Pt.X <= num2)
				{
					pt = outPt3.Pt;
					discardLeft = (outPt3.Pt.X > outPt.Pt.X);
				}
				else
				{
					pt = outPt4.Pt;
					discardLeft = (outPt4.Pt.X > outPt2.Pt.X);
				}
				j.OutPt1 = outPt;
				j.OutPt2 = outPt2;
				return this.JoinHorz(outPt, outPt3, outPt2, outPt4, pt, discardLeft);
			}
			else
			{
				OutPt outPt3 = outPt.Next;
				while (outPt3.Pt == outPt.Pt && outPt3 != outPt)
				{
					outPt3 = outPt3.Next;
				}
				bool flag4 = outPt3.Pt.Y > outPt.Pt.Y || !ClipperBase.SlopesEqual(outPt.Pt, outPt3.Pt, j.OffPt, this.m_UseFullRange);
				if (flag4)
				{
					outPt3 = outPt.Prev;
					while (outPt3.Pt == outPt.Pt && outPt3 != outPt)
					{
						outPt3 = outPt3.Prev;
					}
					if (outPt3.Pt.Y > outPt.Pt.Y || !ClipperBase.SlopesEqual(outPt.Pt, outPt3.Pt, j.OffPt, this.m_UseFullRange))
					{
						return false;
					}
				}
				OutPt outPt4 = outPt2.Next;
				while (outPt4.Pt == outPt2.Pt && outPt4 != outPt2)
				{
					outPt4 = outPt4.Next;
				}
				bool flag5 = outPt4.Pt.Y > outPt2.Pt.Y || !ClipperBase.SlopesEqual(outPt2.Pt, outPt4.Pt, j.OffPt, this.m_UseFullRange);
				if (flag5)
				{
					outPt4 = outPt2.Prev;
					while (outPt4.Pt == outPt2.Pt && outPt4 != outPt2)
					{
						outPt4 = outPt4.Prev;
					}
					if (outPt4.Pt.Y > outPt2.Pt.Y || !ClipperBase.SlopesEqual(outPt2.Pt, outPt4.Pt, j.OffPt, this.m_UseFullRange))
					{
						return false;
					}
				}
				if (outPt3 == outPt || outPt4 == outPt2 || outPt3 == outPt4 || (outRec1 == outRec2 && flag4 == flag5))
				{
					return false;
				}
				if (flag4)
				{
					outPt3 = this.DupOutPt(outPt, false);
					outPt4 = this.DupOutPt(outPt2, true);
					outPt.Prev = outPt2;
					outPt2.Next = outPt;
					outPt3.Next = outPt4;
					outPt4.Prev = outPt3;
					j.OutPt1 = outPt;
					j.OutPt2 = outPt3;
					return true;
				}
				outPt3 = this.DupOutPt(outPt, true);
				outPt4 = this.DupOutPt(outPt2, false);
				outPt.Next = outPt2;
				outPt2.Prev = outPt;
				outPt3.Prev = outPt4;
				outPt4.Next = outPt3;
				j.OutPt1 = outPt;
				j.OutPt2 = outPt3;
				return true;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00007F18 File Offset: 0x00006118
		public static int PointInPolygon(IntPoint pt, List<IntPoint> path)
		{
			int num = 0;
			int count = path.Count;
			if (count < 3)
			{
				return 0;
			}
			IntPoint intPoint = path[0];
			for (int i = 1; i <= count; i++)
			{
				IntPoint intPoint2 = (i == count) ? path[0] : path[i];
				if (intPoint2.Y == pt.Y && (intPoint2.X == pt.X || (intPoint.Y == pt.Y && intPoint2.X > pt.X == intPoint.X < pt.X)))
				{
					return -1;
				}
				if (intPoint.Y < pt.Y != intPoint2.Y < pt.Y)
				{
					if (intPoint.X >= pt.X)
					{
						if (intPoint2.X > pt.X)
						{
							num = 1 - num;
						}
						else
						{
							float num2 = (float)(intPoint.X - pt.X) * (float)(intPoint2.Y - pt.Y) - (float)(intPoint2.X - pt.X) * (float)(intPoint.Y - pt.Y);
							if (num2 == 0f)
							{
								return -1;
							}
							if (num2 > 0f == intPoint2.Y > intPoint.Y)
							{
								num = 1 - num;
							}
						}
					}
					else if (intPoint2.X > pt.X)
					{
						float num3 = (float)(intPoint.X - pt.X) * (float)(intPoint2.Y - pt.Y) - (float)(intPoint2.X - pt.X) * (float)(intPoint.Y - pt.Y);
						if (num3 == 0f)
						{
							return -1;
						}
						if (num3 > 0f == intPoint2.Y > intPoint.Y)
						{
							num = 1 - num;
						}
					}
				}
				intPoint = intPoint2;
			}
			return num;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000080E4 File Offset: 0x000062E4
		private int PointInPolygon(IntPoint pt, OutPt op)
		{
			int num = 0;
			OutPt outPt = op;
			for (;;)
			{
				float num2 = (float)op.Pt.X;
				float num3 = (float)op.Pt.Y;
				float num4 = (float)op.Next.Pt.X;
				float num5 = (float)op.Next.Pt.Y;
				if (num5 == (float)pt.Y && (num4 == (float)pt.X || (num3 == (float)pt.Y && num4 > (float)pt.X == num2 < (float)pt.X)))
				{
					break;
				}
				if (num3 < (float)pt.Y != num5 < (float)pt.Y)
				{
					if (num2 >= (float)pt.X)
					{
						if (num4 > (float)pt.X)
						{
							num = 1 - num;
						}
						else
						{
							float num6 = (num2 - (float)pt.X) * (num5 - (float)pt.Y) - (num4 - (float)pt.X) * (num3 - (float)pt.Y);
							if (num6 == 0f)
							{
								return -1;
							}
							if (num6 > 0f == num5 > num3)
							{
								num = 1 - num;
							}
						}
					}
					else if (num4 > (float)pt.X)
					{
						float num7 = (num2 - (float)pt.X) * (num5 - (float)pt.Y) - (num4 - (float)pt.X) * (num3 - (float)pt.Y);
						if (num7 == 0f)
						{
							return -1;
						}
						if (num7 > 0f == num5 > num3)
						{
							num = 1 - num;
						}
					}
				}
				op = op.Next;
				if (outPt == op)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000825C File Offset: 0x0000645C
		private bool Poly2ContainsPoly1(OutPt outPt1, OutPt outPt2)
		{
			OutPt outPt3 = outPt1;
			int num;
			for (;;)
			{
				num = this.PointInPolygon(outPt3.Pt, outPt2);
				if (num >= 0)
				{
					break;
				}
				outPt3 = outPt3.Next;
				if (outPt3 == outPt1)
				{
					return true;
				}
			}
			return num != 0;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008290 File Offset: 0x00006490
		private void FixupFirstLefts1(OutRec OldOutRec, OutRec NewOutRec)
		{
			for (int i = 0; i < this.m_PolyOuts.Count; i++)
			{
				OutRec outRec = this.m_PolyOuts[i];
				if (outRec.Pts != null && outRec.FirstLeft == OldOutRec && this.Poly2ContainsPoly1(outRec.Pts, NewOutRec.Pts))
				{
					outRec.FirstLeft = NewOutRec;
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000082EC File Offset: 0x000064EC
		private void FixupFirstLefts2(OutRec OldOutRec, OutRec NewOutRec)
		{
			foreach (OutRec outRec in this.m_PolyOuts)
			{
				if (outRec.FirstLeft == OldOutRec)
				{
					outRec.FirstLeft = NewOutRec;
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008348 File Offset: 0x00006548
		private static OutRec ParseFirstLeft(OutRec FirstLeft)
		{
			while (FirstLeft != null && FirstLeft.Pts == null)
			{
				FirstLeft = FirstLeft.FirstLeft;
			}
			return FirstLeft;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00008360 File Offset: 0x00006560
		private void JoinCommonEdges()
		{
			for (int i = 0; i < this.m_Joins.Count; i++)
			{
				Join join = this.m_Joins[i];
				OutRec outRec = this.GetOutRec(join.OutPt1.Idx);
				OutRec outRec2 = this.GetOutRec(join.OutPt2.Idx);
				if (outRec.Pts != null && outRec2.Pts != null)
				{
					OutRec outRec3;
					if (outRec == outRec2)
					{
						outRec3 = outRec;
					}
					else if (this.Param1RightOfParam2(outRec, outRec2))
					{
						outRec3 = outRec2;
					}
					else if (this.Param1RightOfParam2(outRec2, outRec))
					{
						outRec3 = outRec;
					}
					else
					{
						outRec3 = this.GetLowermostRec(outRec, outRec2);
					}
					if (this.JoinPoints(join, outRec, outRec2))
					{
						if (outRec == outRec2)
						{
							outRec.Pts = join.OutPt1;
							outRec.BottomPt = null;
							outRec2 = this.CreateOutRec();
							outRec2.Pts = join.OutPt2;
							this.UpdateOutPtIdxs(outRec2);
							if (this.m_UsingPolyTree)
							{
								for (int j = 0; j < this.m_PolyOuts.Count - 1; j++)
								{
									OutRec outRec4 = this.m_PolyOuts[j];
									if (outRec4.Pts != null && Clipper.ParseFirstLeft(outRec4.FirstLeft) == outRec && outRec4.IsHole != outRec.IsHole && this.Poly2ContainsPoly1(outRec4.Pts, join.OutPt2))
									{
										outRec4.FirstLeft = outRec2;
									}
								}
							}
							if (this.Poly2ContainsPoly1(outRec2.Pts, outRec.Pts))
							{
								outRec2.IsHole = !outRec.IsHole;
								outRec2.FirstLeft = outRec;
								if (this.m_UsingPolyTree)
								{
									this.FixupFirstLefts2(outRec2, outRec);
								}
								if ((outRec2.IsHole ^ this.ReverseSolution) == this.Area(outRec2) > 0f)
								{
									this.ReversePolyPtLinks(outRec2.Pts);
								}
							}
							else if (this.Poly2ContainsPoly1(outRec.Pts, outRec2.Pts))
							{
								outRec2.IsHole = outRec.IsHole;
								outRec.IsHole = !outRec2.IsHole;
								outRec2.FirstLeft = outRec.FirstLeft;
								outRec.FirstLeft = outRec2;
								if (this.m_UsingPolyTree)
								{
									this.FixupFirstLefts2(outRec, outRec2);
								}
								if ((outRec.IsHole ^ this.ReverseSolution) == this.Area(outRec) > 0f)
								{
									this.ReversePolyPtLinks(outRec.Pts);
								}
							}
							else
							{
								outRec2.IsHole = outRec.IsHole;
								outRec2.FirstLeft = outRec.FirstLeft;
								if (this.m_UsingPolyTree)
								{
									this.FixupFirstLefts1(outRec, outRec2);
								}
							}
						}
						else
						{
							outRec2.Pts = null;
							outRec2.BottomPt = null;
							outRec2.Idx = outRec.Idx;
							outRec.IsHole = outRec3.IsHole;
							if (outRec3 == outRec2)
							{
								outRec.FirstLeft = outRec2.FirstLeft;
							}
							outRec2.FirstLeft = outRec;
							if (this.m_UsingPolyTree)
							{
								this.FixupFirstLefts2(outRec2, outRec);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00008620 File Offset: 0x00006820
		private void UpdateOutPtIdxs(OutRec outrec)
		{
			OutPt outPt = outrec.Pts;
			do
			{
				outPt.Idx = outrec.Idx;
				outPt = outPt.Prev;
			}
			while (outPt != outrec.Pts);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008650 File Offset: 0x00006850
		private void DoSimplePolygons()
		{
			int i = 0;
			while (i < this.m_PolyOuts.Count)
			{
				OutRec outRec = this.m_PolyOuts[i++];
				OutPt outPt = outRec.Pts;
				if (outPt != null)
				{
					do
					{
						for (OutPt outPt2 = outPt.Next; outPt2 != outRec.Pts; outPt2 = outPt2.Next)
						{
							if (outPt.Pt == outPt2.Pt && outPt2.Next != outPt && outPt2.Prev != outPt)
							{
								OutPt prev = outPt.Prev;
								OutPt prev2 = outPt2.Prev;
								outPt.Prev = prev2;
								prev2.Next = outPt;
								outPt2.Prev = prev;
								prev.Next = outPt2;
								outRec.Pts = outPt;
								OutRec outRec2 = this.CreateOutRec();
								outRec2.Pts = outPt2;
								this.UpdateOutPtIdxs(outRec2);
								if (this.Poly2ContainsPoly1(outRec2.Pts, outRec.Pts))
								{
									outRec2.IsHole = !outRec.IsHole;
									outRec2.FirstLeft = outRec;
								}
								else if (this.Poly2ContainsPoly1(outRec.Pts, outRec2.Pts))
								{
									outRec2.IsHole = outRec.IsHole;
									outRec.IsHole = !outRec2.IsHole;
									outRec2.FirstLeft = outRec.FirstLeft;
									outRec.FirstLeft = outRec2;
								}
								else
								{
									outRec2.IsHole = outRec.IsHole;
									outRec2.FirstLeft = outRec.FirstLeft;
								}
								outPt2 = outPt;
							}
						}
						outPt = outPt.Next;
					}
					while (outPt != outRec.Pts);
				}
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000087D8 File Offset: 0x000069D8
		public static float Area(List<IntPoint> poly)
		{
			int count = poly.Count;
			if (count < 3)
			{
				return 0f;
			}
			float num = 0f;
			int i = 0;
			int index = count - 1;
			while (i < count)
			{
				num += ((float)poly[index].X + (float)poly[i].X) * ((float)poly[index].Y - (float)poly[i].Y);
				index = i;
				i++;
			}
			return -num * 0.5f;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008850 File Offset: 0x00006A50
		private float Area(OutRec outRec)
		{
			OutPt outPt = outRec.Pts;
			if (outPt == null)
			{
				return 0f;
			}
			float num = 0f;
			do
			{
				num += (float)(outPt.Prev.Pt.X + outPt.Pt.X) * (float)(outPt.Prev.Pt.Y - outPt.Pt.Y);
				outPt = outPt.Next;
			}
			while (outPt != outRec.Pts);
			return num * 0.5f;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000088C8 File Offset: 0x00006AC8
		public static List<List<IntPoint>> SimplifyPolygon(List<IntPoint> poly, PolyFillType fillType = PolyFillType.pftEvenOdd)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			Clipper clipper = new Clipper(0);
			clipper.StrictlySimple = true;
			clipper.AddPath(poly, PolyType.ptSubject, true);
			clipper.Execute(ClipType.ctUnion, list, fillType, fillType);
			return list;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008900 File Offset: 0x00006B00
		public static List<List<IntPoint>> SimplifyPolygons(List<List<IntPoint>> polys, PolyFillType fillType = PolyFillType.pftEvenOdd)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			Clipper clipper = new Clipper(0);
			clipper.StrictlySimple = true;
			clipper.AddPaths(polys, PolyType.ptSubject, true);
			clipper.Execute(ClipType.ctUnion, list, fillType, fillType);
			return list;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008938 File Offset: 0x00006B38
		private static float DistanceSqrd(IntPoint pt1, IntPoint pt2)
		{
			float num = (float)pt1.X - (float)pt2.X;
			float num2 = (float)pt1.Y - (float)pt2.Y;
			float num3 = num * num;
			float num4 = num2;
			return num3 + num4 * num4;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000896C File Offset: 0x00006B6C
		private static float DistanceFromLineSqrd(IntPoint pt, IntPoint ln1, IntPoint ln2)
		{
			float num = (float)(ln1.Y - ln2.Y);
			float num2 = (float)(ln2.X - ln1.X);
			float num3 = num * (float)ln1.X + num2 * (float)ln1.Y;
			num3 = num * (float)pt.X + num2 * (float)pt.Y - num3;
			float num4 = num3;
			float num5 = num4 * num4;
			float num6 = num;
			float num7 = num6 * num6;
			float num8 = num2;
			return num5 / (num7 + num8 * num8);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000089CC File Offset: 0x00006BCC
		private static bool SlopesNearCollinear(IntPoint pt1, IntPoint pt2, IntPoint pt3, float distSqrd)
		{
			return Clipper.DistanceFromLineSqrd(pt2, pt1, pt3) < distSqrd;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000089DC File Offset: 0x00006BDC
		private static bool PointsAreClose(IntPoint pt1, IntPoint pt2, float distSqrd)
		{
			float num = (float)pt1.X - (float)pt2.X;
			float num2 = (float)pt1.Y - (float)pt2.Y;
			float num3 = num * num;
			float num4 = num2;
			return num3 + num4 * num4 <= distSqrd;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00008A14 File Offset: 0x00006C14
		private static OutPt ExcludeOp(OutPt op)
		{
			OutPt prev = op.Prev;
			prev.Next = op.Next;
			op.Next.Prev = prev;
			prev.Idx = 0;
			return prev;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008A48 File Offset: 0x00006C48
		public static List<IntPoint> CleanPolygon(List<IntPoint> path, float distance = 1.415f)
		{
			int num = path.Count;
			if (num == 0)
			{
				return new List<IntPoint>();
			}
			OutPt[] array = new OutPt[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new OutPt();
			}
			for (int j = 0; j < num; j++)
			{
				array[j].Pt = path[j];
				array[j].Next = array[(j + 1) % num];
				array[j].Next.Prev = array[j];
				array[j].Idx = 0;
			}
			float distSqrd = distance * distance;
			OutPt outPt = array[0];
			while (outPt.Idx == 0 && outPt.Next != outPt.Prev)
			{
				if (Clipper.PointsAreClose(outPt.Pt, outPt.Prev.Pt, distSqrd))
				{
					outPt = Clipper.ExcludeOp(outPt);
					num--;
				}
				else if (Clipper.PointsAreClose(outPt.Prev.Pt, outPt.Next.Pt, distSqrd))
				{
					Clipper.ExcludeOp(outPt.Next);
					outPt = Clipper.ExcludeOp(outPt);
					num -= 2;
				}
				else if (Clipper.SlopesNearCollinear(outPt.Prev.Pt, outPt.Pt, outPt.Next.Pt, distSqrd))
				{
					outPt = Clipper.ExcludeOp(outPt);
					num--;
				}
				else
				{
					outPt.Idx = 1;
					outPt = outPt.Next;
				}
			}
			if (num < 3)
			{
				num = 0;
			}
			List<IntPoint> list = new List<IntPoint>(num);
			for (int k = 0; k < num; k++)
			{
				list.Add(outPt.Pt);
				outPt = outPt.Next;
			}
			return list;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008BCC File Offset: 0x00006DCC
		public static List<List<IntPoint>> CleanPolygons(List<List<IntPoint>> polys, float distance = 1.415f)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>(polys.Count);
			for (int i = 0; i < polys.Count; i++)
			{
				list.Add(Clipper.CleanPolygon(polys[i], distance));
			}
			return list;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008C0C File Offset: 0x00006E0C
		internal static List<List<IntPoint>> Minkowski(List<IntPoint> pattern, List<IntPoint> path, bool IsSum, bool IsClosed)
		{
			int num = IsClosed ? 1 : 0;
			int count = pattern.Count;
			int count2 = path.Count;
			List<List<IntPoint>> list = new List<List<IntPoint>>(count2);
			if (IsSum)
			{
				for (int i = 0; i < count2; i++)
				{
					List<IntPoint> list2 = new List<IntPoint>(count);
					foreach (IntPoint intPoint in pattern)
					{
						list2.Add(new IntPoint(path[i].X + intPoint.X, path[i].Y + intPoint.Y));
					}
					list.Add(list2);
				}
			}
			else
			{
				for (int j = 0; j < count2; j++)
				{
					List<IntPoint> list3 = new List<IntPoint>(count);
					foreach (IntPoint intPoint2 in pattern)
					{
						list3.Add(new IntPoint(path[j].X - intPoint2.X, path[j].Y - intPoint2.Y));
					}
					list.Add(list3);
				}
			}
			List<List<IntPoint>> list4 = new List<List<IntPoint>>((count2 + num) * (count + 1));
			for (int k = 0; k < count2 - 1 + num; k++)
			{
				for (int l = 0; l < count; l++)
				{
					List<IntPoint> list5 = new List<IntPoint>(4);
					list5.Add(list[k % count2][l % count]);
					list5.Add(list[(k + 1) % count2][l % count]);
					list5.Add(list[(k + 1) % count2][(l + 1) % count]);
					list5.Add(list[k % count2][(l + 1) % count]);
					if (!Clipper.Orientation(list5))
					{
						list5.Reverse();
					}
					list4.Add(list5);
				}
			}
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(list4, PolyType.ptSubject, true);
			clipper.Execute(ClipType.ctUnion, list, PolyFillType.pftNonZero, PolyFillType.pftNonZero);
			return list;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00008E4C File Offset: 0x0000704C
		public static List<List<IntPoint>> MinkowskiSum(List<IntPoint> pattern, List<IntPoint> path, bool pathIsClosed)
		{
			return Clipper.Minkowski(pattern, path, true, pathIsClosed);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00008E58 File Offset: 0x00007058
		public static List<List<IntPoint>> MinkowskiSum(List<IntPoint> pattern, List<List<IntPoint>> paths, PolyFillType pathFillType, bool pathIsClosed)
		{
			Clipper clipper = new Clipper(0);
			for (int i = 0; i < paths.Count; i++)
			{
				List<List<IntPoint>> ppg = Clipper.Minkowski(pattern, paths[i], true, pathIsClosed);
				clipper.AddPaths(ppg, PolyType.ptSubject, true);
			}
			if (pathIsClosed)
			{
				clipper.AddPaths(paths, PolyType.ptClip, true);
			}
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			clipper.Execute(ClipType.ctUnion, list, pathFillType, pathFillType);
			return list;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008EB6 File Offset: 0x000070B6
		public static List<List<IntPoint>> MinkowskiDiff(List<IntPoint> poly1, List<IntPoint> poly2)
		{
			return Clipper.Minkowski(poly1, poly2, false, true);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00008EC4 File Offset: 0x000070C4
		public static List<List<IntPoint>> PolyTreeToPaths(PolyTree polytree)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			list.Capacity = polytree.Total;
			Clipper.AddPolyNodeToPaths(polytree, Clipper.NodeType.ntAny, list);
			return list;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00008EEC File Offset: 0x000070EC
		internal static void AddPolyNodeToPaths(PolyNode polynode, Clipper.NodeType nt, List<List<IntPoint>> paths)
		{
			bool flag = true;
			if (nt != Clipper.NodeType.ntOpen)
			{
				if (nt == Clipper.NodeType.ntClosed)
				{
					flag = !polynode.IsOpen;
				}
				if (polynode.m_polygon.Count > 0 && flag)
				{
					paths.Add(polynode.m_polygon);
				}
				foreach (PolyNode polynode2 in polynode.Childs)
				{
					Clipper.AddPolyNodeToPaths(polynode2, nt, paths);
				}
				return;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00008F74 File Offset: 0x00007174
		public static List<List<IntPoint>> OpenPathsFromPolyTree(PolyTree polytree)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			list.Capacity = polytree.ChildCount;
			for (int i = 0; i < polytree.ChildCount; i++)
			{
				if (polytree.Childs[i].IsOpen)
				{
					list.Add(polytree.Childs[i].m_polygon);
				}
			}
			return list;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00008FD0 File Offset: 0x000071D0
		public static List<List<IntPoint>> ClosedPathsFromPolyTree(PolyTree polytree)
		{
			List<List<IntPoint>> list = new List<List<IntPoint>>();
			list.Capacity = polytree.Total;
			Clipper.AddPolyNodeToPaths(polytree, Clipper.NodeType.ntClosed, list);
			return list;
		}

		// Token: 0x04000065 RID: 101
		public const int ioReverseSolution = 1;

		// Token: 0x04000066 RID: 102
		public const int ioStrictlySimple = 2;

		// Token: 0x04000067 RID: 103
		public const int ioPreserveCollinear = 4;

		// Token: 0x04000068 RID: 104
		private List<OutRec> m_PolyOuts;

		// Token: 0x04000069 RID: 105
		private ClipType m_ClipType;

		// Token: 0x0400006A RID: 106
		private Scanbeam m_Scanbeam;

		// Token: 0x0400006B RID: 107
		private TEdge m_ActiveEdges;

		// Token: 0x0400006C RID: 108
		private TEdge m_SortedEdges;

		// Token: 0x0400006D RID: 109
		private List<IntersectNode> m_IntersectList;

		// Token: 0x0400006E RID: 110
		private IComparer<IntersectNode> m_IntersectNodeComparer;

		// Token: 0x0400006F RID: 111
		private bool m_ExecuteLocked;

		// Token: 0x04000070 RID: 112
		private PolyFillType m_ClipFillType;

		// Token: 0x04000071 RID: 113
		private PolyFillType m_SubjFillType;

		// Token: 0x04000072 RID: 114
		private List<Join> m_Joins;

		// Token: 0x04000073 RID: 115
		private List<Join> m_GhostJoins;

		// Token: 0x04000074 RID: 116
		private bool m_UsingPolyTree;

		// Token: 0x0200001B RID: 27
		internal enum NodeType
		{
			// Token: 0x04000088 RID: 136
			ntAny,
			// Token: 0x04000089 RID: 137
			ntOpen,
			// Token: 0x0400008A RID: 138
			ntClosed
		}
	}
}
