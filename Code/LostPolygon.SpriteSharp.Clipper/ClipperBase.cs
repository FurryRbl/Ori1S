using System;
using System.Collections.Generic;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000017 RID: 23
	public class ClipperBase
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000028F5 File Offset: 0x00000AF5
		internal static bool near_zero(float val)
		{
			return val > -1E-05f && val < 1E-05f;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002909 File Offset: 0x00000B09
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002911 File Offset: 0x00000B11
		public bool PreserveCollinear { get; set; }

		// Token: 0x0600003A RID: 58 RVA: 0x0000291A File Offset: 0x00000B1A
		internal static bool IsHorizontal(TEdge e)
		{
			return e.Delta.Y == 0;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000292C File Offset: 0x00000B2C
		internal bool PointIsVertex(IntPoint pt, OutPt pp)
		{
			OutPt outPt = pp;
			while (!(outPt.Pt == pt))
			{
				outPt = outPt.Next;
				if (outPt == pp)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002958 File Offset: 0x00000B58
		internal bool PointOnLineSegment(IntPoint pt, IntPoint linePt1, IntPoint linePt2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && Int128.Int128Mul((long)(pt.X - linePt1.X), (long)(linePt2.Y - linePt1.Y)) == Int128.Int128Mul((long)(linePt2.X - linePt1.X), (long)(pt.Y - linePt1.Y)));
			}
			return (pt.X == linePt1.X && pt.Y == linePt1.Y) || (pt.X == linePt2.X && pt.Y == linePt2.Y) || (pt.X > linePt1.X == pt.X < linePt2.X && pt.Y > linePt1.Y == pt.Y < linePt2.Y && (pt.X - linePt1.X) * (linePt2.Y - linePt1.Y) == (linePt2.X - linePt1.X) * (pt.Y - linePt1.Y));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal bool PointOnPolygon(IntPoint pt, OutPt pp, bool UseFullRange)
		{
			OutPt outPt = pp;
			while (!this.PointOnLineSegment(pt, outPt.Pt, outPt.Next.Pt, UseFullRange))
			{
				outPt = outPt.Next;
				if (outPt == pp)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B20 File Offset: 0x00000D20
		internal static bool SlopesEqual(TEdge e1, TEdge e2, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul((long)e1.Delta.Y, (long)e2.Delta.X) == Int128.Int128Mul((long)e1.Delta.X, (long)e2.Delta.Y);
			}
			return e1.Delta.Y * e2.Delta.X == e1.Delta.X * e2.Delta.Y;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BA0 File Offset: 0x00000DA0
		protected static bool SlopesEqual(IntPoint pt1, IntPoint pt2, IntPoint pt3, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul((long)(pt1.Y - pt2.Y), (long)(pt2.X - pt3.X)) == Int128.Int128Mul((long)(pt1.X - pt2.X), (long)(pt2.Y - pt3.Y));
			}
			return (pt1.Y - pt2.Y) * (pt2.X - pt3.X) - (pt1.X - pt2.X) * (pt2.Y - pt3.Y) == 0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C34 File Offset: 0x00000E34
		protected static bool SlopesEqual(IntPoint pt1, IntPoint pt2, IntPoint pt3, IntPoint pt4, bool UseFullRange)
		{
			if (UseFullRange)
			{
				return Int128.Int128Mul((long)(pt1.Y - pt2.Y), (long)(pt3.X - pt4.X)) == Int128.Int128Mul((long)(pt1.X - pt2.X), (long)(pt3.Y - pt4.Y));
			}
			return (pt1.Y - pt2.Y) * (pt3.X - pt4.X) - (pt1.X - pt2.X) * (pt3.Y - pt4.Y) == 0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CC7 File Offset: 0x00000EC7
		internal ClipperBase()
		{
			this.m_MinimaList = null;
			this.m_CurrentLM = null;
			this.m_UseFullRange = false;
			this.m_HasOpenPaths = false;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public virtual void Clear()
		{
			this.DisposeLocalMinimaList();
			for (int i = 0; i < this.m_edges.Count; i++)
			{
				for (int j = 0; j < this.m_edges[i].Count; j++)
				{
					this.m_edges[i][j] = null;
				}
				this.m_edges[i].Clear();
			}
			this.m_edges.Clear();
			this.m_UseFullRange = false;
			this.m_HasOpenPaths = false;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D7C File Offset: 0x00000F7C
		private void DisposeLocalMinimaList()
		{
			while (this.m_MinimaList != null)
			{
				LocalMinima next = this.m_MinimaList.Next;
				this.m_MinimaList = null;
				this.m_MinimaList = next;
			}
			this.m_CurrentLM = null;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private void RangeTest(IntPoint Pt, ref bool useFullRange)
		{
			if (!useFullRange)
			{
				if (Pt.X > 46340 || Pt.Y > 46340 || -Pt.X > 46340 || -Pt.Y > 46340)
				{
					useFullRange = true;
					this.RangeTest(Pt, ref useFullRange);
				}
				return;
			}
			if (Pt.X > 46340 || Pt.Y > 46340 || -Pt.X > 46340 || -Pt.Y > 46340)
			{
				throw new ClipperException(string.Concat(new object[]
				{
					"Coordinate outside allowed range ",
					Pt.X,
					";",
					Pt.Y
				}));
			}
			throw new ClipperException("Coordinate outside allowed range");
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E84 File Offset: 0x00001084
		private void InitEdge(TEdge e, TEdge eNext, TEdge ePrev, IntPoint pt)
		{
			e.Next = eNext;
			e.Prev = ePrev;
			e.Curr = pt;
			e.OutIdx = -1;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002EA4 File Offset: 0x000010A4
		private void InitEdge2(TEdge e, PolyType polyType)
		{
			if (e.Curr.Y >= e.Next.Curr.Y)
			{
				e.Bot = e.Curr;
				e.Top = e.Next.Curr;
			}
			else
			{
				e.Top = e.Curr;
				e.Bot = e.Next.Curr;
			}
			this.SetDx(e);
			e.PolyTyp = polyType;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002F18 File Offset: 0x00001118
		private TEdge FindNextLocMin(TEdge E)
		{
			TEdge tedge;
			for (;;)
			{
				if (!(E.Bot != E.Prev.Bot) && !(E.Curr == E.Top))
				{
					if (E.Dx != -3.4E+38f && E.Prev.Dx != -3.4E+38f)
					{
						break;
					}
					while (E.Prev.Dx == -3.4E+38f)
					{
						E = E.Prev;
					}
					tedge = E;
					while (E.Dx == -3.4E+38f)
					{
						E = E.Next;
					}
					if (E.Top.Y != E.Prev.Bot.Y)
					{
						goto Block_7;
					}
				}
				else
				{
					E = E.Next;
				}
			}
			return E;
			Block_7:
			if (tedge.Prev.Bot.X < E.Bot.X)
			{
				E = tedge;
			}
			return E;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002FF0 File Offset: 0x000011F0
		private TEdge ProcessBound(TEdge E, bool IsClockwise)
		{
			TEdge tedge = E;
			TEdge tedge2 = E;
			if (E.Dx == -3.4E+38f)
			{
				int x;
				if (IsClockwise)
				{
					x = E.Prev.Bot.X;
				}
				else
				{
					x = E.Next.Bot.X;
				}
				if (E.Bot.X != x)
				{
					this.ReverseHorizontal(E);
				}
			}
			if (tedge2.OutIdx != -2)
			{
				if (IsClockwise)
				{
					while (tedge2.Top.Y == tedge2.Next.Bot.Y && tedge2.Next.OutIdx != -2)
					{
						tedge2 = tedge2.Next;
					}
					if (tedge2.Dx == -3.4E+38f && tedge2.Next.OutIdx != -2)
					{
						TEdge tedge3 = tedge2;
						while (tedge3.Prev.Dx == -3.4E+38f)
						{
							tedge3 = tedge3.Prev;
						}
						if (tedge3.Prev.Top.X == tedge2.Next.Top.X)
						{
							if (!IsClockwise)
							{
								tedge2 = tedge3.Prev;
							}
						}
						else if (tedge3.Prev.Top.X > tedge2.Next.Top.X)
						{
							tedge2 = tedge3.Prev;
						}
					}
					while (E != tedge2)
					{
						TEdge tedge4 = E;
						tedge4.NextInLML = tedge4.Next;
						if (E.Dx == -3.4E+38f && E != tedge && E.Bot.X != E.Prev.Top.X)
						{
							this.ReverseHorizontal(E);
						}
						E = E.Next;
					}
					if (E.Dx == -3.4E+38f && E != tedge && E.Bot.X != E.Prev.Top.X)
					{
						this.ReverseHorizontal(E);
					}
					tedge2 = tedge2.Next;
				}
				else
				{
					while (tedge2.Top.Y == tedge2.Prev.Bot.Y && tedge2.Prev.OutIdx != -2)
					{
						tedge2 = tedge2.Prev;
					}
					if (tedge2.Dx == -3.4E+38f && tedge2.Prev.OutIdx != -2)
					{
						TEdge tedge3 = tedge2;
						while (tedge3.Next.Dx == -3.4E+38f)
						{
							tedge3 = tedge3.Next;
						}
						if (tedge3.Next.Top.X == tedge2.Prev.Top.X)
						{
							if (!IsClockwise)
							{
								tedge2 = tedge3.Next;
							}
						}
						else if (tedge3.Next.Top.X > tedge2.Prev.Top.X)
						{
							tedge2 = tedge3.Next;
						}
					}
					while (E != tedge2)
					{
						TEdge tedge5 = E;
						tedge5.NextInLML = tedge5.Prev;
						if (E.Dx == -3.4E+38f && E != tedge && E.Bot.X != E.Next.Top.X)
						{
							this.ReverseHorizontal(E);
						}
						E = E.Prev;
					}
					if (E.Dx == -3.4E+38f && E != tedge && E.Bot.X != E.Next.Top.X)
					{
						this.ReverseHorizontal(E);
					}
					tedge2 = tedge2.Prev;
				}
			}
			if (tedge2.OutIdx == -2)
			{
				E = tedge2;
				if (IsClockwise)
				{
					while (E.Top.Y == E.Next.Bot.Y)
					{
						E = E.Next;
					}
					while (E != tedge2)
					{
						if (E.Dx != -3.4E+38f)
						{
							break;
						}
						E = E.Prev;
					}
				}
				else
				{
					while (E.Top.Y == E.Prev.Bot.Y)
					{
						E = E.Prev;
					}
					while (E != tedge2 && E.Dx == -3.4E+38f)
					{
						E = E.Next;
					}
				}
				if (E == tedge2)
				{
					if (IsClockwise)
					{
						tedge2 = E.Next;
					}
					else
					{
						tedge2 = E.Prev;
					}
				}
				else
				{
					if (IsClockwise)
					{
						E = tedge2.Next;
					}
					else
					{
						E = tedge2.Prev;
					}
					LocalMinima localMinima = new LocalMinima();
					localMinima.Next = null;
					localMinima.Y = E.Bot.Y;
					localMinima.LeftBound = null;
					localMinima.RightBound = E;
					localMinima.RightBound.WindDelta = 0;
					tedge2 = this.ProcessBound(localMinima.RightBound, IsClockwise);
					this.InsertLocalMinima(localMinima);
				}
			}
			return tedge2;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003428 File Offset: 0x00001628
		public bool AddPath(List<IntPoint> pg, PolyType polyType, bool Closed)
		{
			if (!Closed)
			{
				throw new ClipperException("AddPath: Open paths have been disabled.");
			}
			int i = pg.Count - 1;
			if (Closed)
			{
				while (i > 0)
				{
					if (!(pg[i] == pg[0]))
					{
						break;
					}
					i--;
				}
			}
			while (i > 0 && pg[i] == pg[i - 1])
			{
				i--;
			}
			if ((Closed && i < 2) || (!Closed && i < 1))
			{
				return false;
			}
			List<TEdge> list = new List<TEdge>(i + 1);
			for (int j = 0; j <= i; j++)
			{
				list.Add(new TEdge());
			}
			bool flag = true;
			list[1].Curr = pg[1];
			this.RangeTest(pg[0], ref this.m_UseFullRange);
			this.RangeTest(pg[i], ref this.m_UseFullRange);
			this.InitEdge(list[0], list[1], list[i], pg[0]);
			this.InitEdge(list[i], list[0], list[i - 1], pg[i]);
			for (int k = i - 1; k >= 1; k--)
			{
				this.RangeTest(pg[k], ref this.m_UseFullRange);
				this.InitEdge(list[k], list[k + 1], list[k - 1], pg[k]);
			}
			TEdge tedge = list[0];
			TEdge tedge2 = tedge;
			TEdge tedge3 = tedge;
			for (;;)
			{
				if (tedge2.Curr == tedge2.Next.Curr)
				{
					if (tedge2 == tedge2.Next)
					{
						break;
					}
					if (tedge2 == tedge)
					{
						tedge = tedge2.Next;
					}
					tedge2 = this.RemoveEdge(tedge2);
					tedge3 = tedge2;
				}
				else
				{
					if (tedge2.Prev == tedge2.Next)
					{
						break;
					}
					if (Closed && ClipperBase.SlopesEqual(tedge2.Prev.Curr, tedge2.Curr, tedge2.Next.Curr, this.m_UseFullRange) && (!this.PreserveCollinear || !this.Pt2IsBetweenPt1AndPt3(tedge2.Prev.Curr, tedge2.Curr, tedge2.Next.Curr)))
					{
						if (tedge2 == tedge)
						{
							tedge = tedge2.Next;
						}
						tedge2 = this.RemoveEdge(tedge2);
						tedge2 = tedge2.Prev;
						tedge3 = tedge2;
					}
					else
					{
						tedge2 = tedge2.Next;
						if (tedge2 == tedge3)
						{
							break;
						}
					}
				}
			}
			if ((!Closed && tedge2 == tedge2.Next) || (Closed && tedge2.Prev == tedge2.Next))
			{
				return false;
			}
			if (!Closed)
			{
				this.m_HasOpenPaths = true;
				tedge.Prev.OutIdx = -2;
			}
			tedge2 = tedge;
			do
			{
				this.InitEdge2(tedge2, polyType);
				tedge2 = tedge2.Next;
				if (flag && tedge2.Curr.Y != tedge.Curr.Y)
				{
					flag = false;
				}
			}
			while (tedge2 != tedge);
			if (!flag)
			{
				this.m_edges.Add(list);
				TEdge tedge4 = null;
				for (;;)
				{
					tedge2 = this.FindNextLocMin(tedge2);
					if (tedge2 == tedge4)
					{
						break;
					}
					if (tedge4 == null)
					{
						tedge4 = tedge2;
					}
					LocalMinima localMinima = new LocalMinima();
					localMinima.Next = null;
					localMinima.Y = tedge2.Bot.Y;
					bool flag2;
					if (tedge2.Dx < tedge2.Prev.Dx)
					{
						localMinima.LeftBound = tedge2.Prev;
						localMinima.RightBound = tedge2;
						flag2 = false;
					}
					else
					{
						localMinima.LeftBound = tedge2;
						localMinima.RightBound = tedge2.Prev;
						flag2 = true;
					}
					localMinima.LeftBound.Side = EdgeSide.esLeft;
					localMinima.RightBound.Side = EdgeSide.esRight;
					if (!Closed)
					{
						localMinima.LeftBound.WindDelta = 0;
					}
					else if (localMinima.LeftBound.Next == localMinima.RightBound)
					{
						localMinima.LeftBound.WindDelta = -1;
					}
					else
					{
						localMinima.LeftBound.WindDelta = 1;
					}
					localMinima.RightBound.WindDelta = -localMinima.LeftBound.WindDelta;
					tedge2 = this.ProcessBound(localMinima.LeftBound, flag2);
					TEdge tedge5 = this.ProcessBound(localMinima.RightBound, !flag2);
					if (localMinima.LeftBound.OutIdx == -2)
					{
						localMinima.LeftBound = null;
					}
					else if (localMinima.RightBound.OutIdx == -2)
					{
						localMinima.RightBound = null;
					}
					this.InsertLocalMinima(localMinima);
					if (!flag2)
					{
						tedge2 = tedge5;
					}
				}
				return true;
			}
			if (Closed)
			{
				return false;
			}
			tedge2.Prev.OutIdx = -2;
			if (tedge2.Prev.Bot.X < tedge2.Prev.Top.X)
			{
				this.ReverseHorizontal(tedge2.Prev);
			}
			LocalMinima localMinima2 = new LocalMinima();
			localMinima2.Next = null;
			localMinima2.Y = tedge2.Bot.Y;
			localMinima2.LeftBound = null;
			localMinima2.RightBound = tedge2;
			localMinima2.RightBound.Side = EdgeSide.esRight;
			localMinima2.RightBound.WindDelta = 0;
			while (tedge2.Next.OutIdx != -2)
			{
				TEdge tedge6 = tedge2;
				tedge6.NextInLML = tedge6.Next;
				if (tedge2.Bot.X != tedge2.Prev.Top.X)
				{
					this.ReverseHorizontal(tedge2);
				}
				tedge2 = tedge2.Next;
			}
			this.InsertLocalMinima(localMinima2);
			this.m_edges.Add(list);
			return true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003984 File Offset: 0x00001B84
		public bool AddPaths(List<List<IntPoint>> ppg, PolyType polyType, bool closed)
		{
			bool result = false;
			for (int i = 0; i < ppg.Count; i++)
			{
				if (this.AddPath(ppg[i], polyType, closed))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000039B8 File Offset: 0x00001BB8
		internal bool Pt2IsBetweenPt1AndPt3(IntPoint pt1, IntPoint pt2, IntPoint pt3)
		{
			if (pt1 == pt3 || pt1 == pt2 || pt3 == pt2)
			{
				return false;
			}
			if (pt1.X != pt3.X)
			{
				return pt2.X > pt1.X == pt2.X < pt3.X;
			}
			return pt2.Y > pt1.Y == pt2.Y < pt3.Y;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003A2D File Offset: 0x00001C2D
		private TEdge RemoveEdge(TEdge e)
		{
			e.Prev.Next = e.Next;
			e.Next.Prev = e.Prev;
			TEdge next = e.Next;
			e.Prev = null;
			return next;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003A60 File Offset: 0x00001C60
		private void SetDx(TEdge e)
		{
			e.Delta.X = e.Top.X - e.Bot.X;
			e.Delta.Y = e.Top.Y - e.Bot.Y;
			if (e.Delta.Y == 0)
			{
				e.Dx = -3.4E+38f;
				return;
			}
			e.Dx = (float)e.Delta.X / (float)e.Delta.Y;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003AEC File Offset: 0x00001CEC
		private void InsertLocalMinima(LocalMinima newLm)
		{
			if (this.m_MinimaList == null)
			{
				this.m_MinimaList = newLm;
				return;
			}
			if (newLm.Y >= this.m_MinimaList.Y)
			{
				newLm.Next = this.m_MinimaList;
				this.m_MinimaList = newLm;
				return;
			}
			LocalMinima localMinima = this.m_MinimaList;
			while (localMinima.Next != null && newLm.Y < localMinima.Next.Y)
			{
				localMinima = localMinima.Next;
			}
			newLm.Next = localMinima.Next;
			localMinima.Next = newLm;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003B6E File Offset: 0x00001D6E
		protected void PopLocalMinima()
		{
			if (this.m_CurrentLM == null)
			{
				return;
			}
			this.m_CurrentLM = this.m_CurrentLM.Next;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003B8C File Offset: 0x00001D8C
		private void ReverseHorizontal(TEdge e)
		{
			int x = e.Top.X;
			e.Top.X = e.Bot.X;
			e.Bot.X = x;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003BC8 File Offset: 0x00001DC8
		protected virtual void Reset()
		{
			this.m_CurrentLM = this.m_MinimaList;
			if (this.m_CurrentLM == null)
			{
				return;
			}
			for (LocalMinima localMinima = this.m_MinimaList; localMinima != null; localMinima = localMinima.Next)
			{
				TEdge tedge = localMinima.LeftBound;
				if (tedge != null)
				{
					TEdge tedge2 = tedge;
					tedge2.Curr = tedge2.Bot;
					tedge.Side = EdgeSide.esLeft;
					tedge.OutIdx = -1;
				}
				tedge = localMinima.RightBound;
				if (tedge != null)
				{
					TEdge tedge3 = tedge;
					tedge3.Curr = tedge3.Bot;
					tedge.Side = EdgeSide.esRight;
					tedge.OutIdx = -1;
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003C48 File Offset: 0x00001E48
		public static IntRect GetBounds(List<List<IntPoint>> paths)
		{
			int i = 0;
			int count = paths.Count;
			while (i < count && paths[i].Count == 0)
			{
				i++;
			}
			if (i == count)
			{
				return new IntRect(0, 0, 0, 0);
			}
			IntRect intRect = default(IntRect);
			intRect.left = paths[i][0].X;
			intRect.right = intRect.left;
			intRect.top = paths[i][0].Y;
			intRect.bottom = intRect.top;
			while (i < count)
			{
				for (int j = 0; j < paths[i].Count; j++)
				{
					if (paths[i][j].X < intRect.left)
					{
						intRect.left = paths[i][j].X;
					}
					else if (paths[i][j].X > intRect.right)
					{
						intRect.right = paths[i][j].X;
					}
					if (paths[i][j].Y < intRect.top)
					{
						intRect.top = paths[i][j].Y;
					}
					else if (paths[i][j].Y > intRect.bottom)
					{
						intRect.bottom = paths[i][j].Y;
					}
				}
				i++;
			}
			return intRect;
		}

		// Token: 0x04000059 RID: 89
		protected const float horizontal = -3.4E+38f;

		// Token: 0x0400005A RID: 90
		protected const int Skip = -2;

		// Token: 0x0400005B RID: 91
		protected const int Unassigned = -1;

		// Token: 0x0400005C RID: 92
		protected const float tolerance = 1E-05f;

		// Token: 0x0400005D RID: 93
		internal const int loRange = 46340;

		// Token: 0x0400005E RID: 94
		internal const int hiRange = 46340;

		// Token: 0x0400005F RID: 95
		internal LocalMinima m_MinimaList;

		// Token: 0x04000060 RID: 96
		internal LocalMinima m_CurrentLM;

		// Token: 0x04000061 RID: 97
		internal List<List<TEdge>> m_edges = new List<List<TEdge>>();

		// Token: 0x04000062 RID: 98
		internal bool m_UseFullRange;

		// Token: 0x04000063 RID: 99
		internal bool m_HasOpenPaths;
	}
}
