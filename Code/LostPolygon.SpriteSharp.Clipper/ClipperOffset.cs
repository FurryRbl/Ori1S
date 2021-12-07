using System;
using System.Collections.Generic;

namespace LostPolygon.SpriteSharp.ClipperLib
{
	// Token: 0x02000019 RID: 25
	public class ClipperOffset
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00008FF8 File Offset: 0x000071F8
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00009000 File Offset: 0x00007200
		public float ArcTolerance { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00009009 File Offset: 0x00007209
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00009011 File Offset: 0x00007211
		public float MiterLimit { get; set; }

		// Token: 0x060000C6 RID: 198 RVA: 0x0000901A File Offset: 0x0000721A
		public ClipperOffset(float miterLimit = 2f, float arcTolerance = 0.25f)
		{
			this.MiterLimit = miterLimit;
			this.ArcTolerance = arcTolerance;
			this.m_lowest.X = -1;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009052 File Offset: 0x00007252
		public void Clear()
		{
			this.m_polyNodes.Childs.Clear();
			this.m_lowest.X = -1;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006A12 File Offset: 0x00004C12
		internal static int Round(float value)
		{
			if (value >= 0f)
			{
				return (int)((double)value + 0.5);
			}
			return (int)((double)value - 0.5);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009070 File Offset: 0x00007270
		public void AddPath(List<IntPoint> path, JoinType joinType, EndType endType)
		{
			int num = path.Count - 1;
			if (num < 0)
			{
				return;
			}
			PolyNode polyNode = new PolyNode();
			polyNode.m_jointype = joinType;
			polyNode.m_endtype = endType;
			if (endType != EndType.etClosedLine)
			{
				if (endType != EndType.etClosedPolygon)
				{
					goto IL_48;
				}
			}
			while (num > 0 && path[0] == path[num])
			{
				num--;
			}
			IL_48:
			polyNode.m_polygon.Capacity = num + 1;
			polyNode.m_polygon.Add(path[0]);
			int num2 = 0;
			int num3 = 0;
			for (int i = 1; i <= num; i++)
			{
				if (polyNode.m_polygon[num2] != path[i])
				{
					num2++;
					polyNode.m_polygon.Add(path[i]);
					if (path[i].Y > polyNode.m_polygon[num3].Y || (path[i].Y == polyNode.m_polygon[num3].Y && path[i].X < polyNode.m_polygon[num3].X))
					{
						num3 = num2;
					}
				}
			}
			if ((endType == EndType.etClosedPolygon && num2 < 2) || (endType != EndType.etClosedPolygon && num2 < 0))
			{
				return;
			}
			this.m_polyNodes.AddChild(polyNode);
			if (endType != EndType.etClosedPolygon)
			{
				return;
			}
			if (this.m_lowest.X < 0)
			{
				this.m_lowest = new IntPoint(0, num3);
				return;
			}
			IntPoint intPoint = this.m_polyNodes.Childs[this.m_lowest.X].m_polygon[this.m_lowest.Y];
			if (polyNode.m_polygon[num3].Y > intPoint.Y || (polyNode.m_polygon[num3].Y == intPoint.Y && polyNode.m_polygon[num3].X < intPoint.X))
			{
				this.m_lowest = new IntPoint(this.m_polyNodes.ChildCount - 1, num3);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009268 File Offset: 0x00007468
		public void AddPaths(List<List<IntPoint>> paths, JoinType joinType, EndType endType)
		{
			foreach (List<IntPoint> path in paths)
			{
				this.AddPath(path, joinType, endType);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000092B8 File Offset: 0x000074B8
		private void FixOrientations()
		{
			if (this.m_lowest.X >= 0 && !Clipper.Orientation(this.m_polyNodes.Childs[this.m_lowest.X].m_polygon))
			{
				for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
				{
					PolyNode polyNode = this.m_polyNodes.Childs[i];
					if (polyNode.m_endtype == EndType.etClosedPolygon || (polyNode.m_endtype == EndType.etClosedLine && Clipper.Orientation(polyNode.m_polygon)))
					{
						polyNode.m_polygon.Reverse();
					}
				}
				return;
			}
			for (int j = 0; j < this.m_polyNodes.ChildCount; j++)
			{
				PolyNode polyNode2 = this.m_polyNodes.Childs[j];
				if (polyNode2.m_endtype == EndType.etClosedLine && !Clipper.Orientation(polyNode2.m_polygon))
				{
					polyNode2.m_polygon.Reverse();
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00009398 File Offset: 0x00007598
		internal static DoublePoint GetUnitNormal(IntPoint pt1, IntPoint pt2)
		{
			float num = (float)(pt2.X - pt1.X);
			float num2 = (float)(pt2.Y - pt1.Y);
			if (num == 0f && num2 == 0f)
			{
				return default(DoublePoint);
			}
			float num3 = (float)1.0;
			float num4 = num;
			double num5 = (double)(num4 * num4);
			float num6 = num2;
			float num7 = (float)((double)num3 / Math.Sqrt(num5 + (double)(num6 * num6)));
			num *= num7;
			num2 *= num7;
			return new DoublePoint(num2, -num);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009408 File Offset: 0x00007608
		private void DoOffset(float delta)
		{
			this.m_destPolys = new List<List<IntPoint>>();
			this.m_delta = delta;
			if (ClipperBase.near_zero(delta))
			{
				this.m_destPolys.Capacity = this.m_polyNodes.ChildCount;
				for (int i = 0; i < this.m_polyNodes.ChildCount; i++)
				{
					PolyNode polyNode = this.m_polyNodes.Childs[i];
					if (polyNode.m_endtype == EndType.etClosedPolygon)
					{
						this.m_destPolys.Add(polyNode.m_polygon);
					}
				}
				return;
			}
			if (this.MiterLimit > 2f)
			{
				this.m_miterLim = 2f / (this.MiterLimit * this.MiterLimit);
			}
			else
			{
				this.m_miterLim = 0.5f;
			}
			float num;
			if ((double)this.ArcTolerance <= 0.0)
			{
				num = 0.25f;
			}
			else if (this.ArcTolerance > Math.Abs(delta) * 0.25f)
			{
				num = Math.Abs(delta) * 0.25f;
			}
			else
			{
				num = this.ArcTolerance;
			}
			float num2 = (float)(3.141592653589793 / Math.Acos((double)(1f - num / Math.Abs(delta))));
			this.m_sin = (float)Math.Sin((double)(6.2831855f / num2));
			this.m_cos = (float)Math.Cos((double)(6.2831855f / num2));
			this.m_StepsPerRad = num2 / 6.2831855f;
			if ((double)delta < 0.0)
			{
				this.m_sin = -this.m_sin;
			}
			this.m_destPolys.Capacity = this.m_polyNodes.ChildCount * 2;
			for (int j = 0; j < this.m_polyNodes.ChildCount; j++)
			{
				PolyNode polyNode2 = this.m_polyNodes.Childs[j];
				this.m_srcPoly = polyNode2.m_polygon;
				int count = this.m_srcPoly.Count;
				if (count != 0 && (delta > 0f || (count >= 3 && polyNode2.m_endtype == EndType.etClosedPolygon)))
				{
					this.m_destPoly = new List<IntPoint>();
					if (count == 1)
					{
						if (polyNode2.m_jointype == JoinType.jtRound)
						{
							float num3 = 1f;
							float num4 = 0f;
							int num5 = 1;
							while ((float)num5 <= num2)
							{
								this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[0].X + num3 * delta), ClipperOffset.Round((float)this.m_srcPoly[0].Y + num4 * delta)));
								float num6 = num3;
								num3 = num3 * this.m_cos - this.m_sin * num4;
								num4 = num6 * this.m_sin + num4 * this.m_cos;
								num5++;
							}
						}
						else
						{
							float num7 = -1f;
							float num8 = -1f;
							for (int k = 0; k < 4; k++)
							{
								this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[0].X + num7 * delta), ClipperOffset.Round((float)this.m_srcPoly[0].Y + num8 * delta)));
								if (num7 < 0f)
								{
									num7 = 1f;
								}
								else if (num8 < 0f)
								{
									num8 = 1f;
								}
								else
								{
									num7 = -1f;
								}
							}
						}
						this.m_destPolys.Add(this.m_destPoly);
					}
					else
					{
						this.m_normals.Clear();
						this.m_normals.Capacity = count;
						for (int l = 0; l < count - 1; l++)
						{
							this.m_normals.Add(ClipperOffset.GetUnitNormal(this.m_srcPoly[l], this.m_srcPoly[l + 1]));
						}
						if (polyNode2.m_endtype == EndType.etClosedLine || polyNode2.m_endtype == EndType.etClosedPolygon)
						{
							this.m_normals.Add(ClipperOffset.GetUnitNormal(this.m_srcPoly[count - 1], this.m_srcPoly[0]));
						}
						else
						{
							this.m_normals.Add(new DoublePoint(this.m_normals[count - 2]));
						}
						if (polyNode2.m_endtype == EndType.etClosedPolygon)
						{
							int num9 = count - 1;
							for (int m = 0; m < count; m++)
							{
								this.OffsetPoint(m, ref num9, polyNode2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
						else if (polyNode2.m_endtype == EndType.etClosedLine)
						{
							int num10 = count - 1;
							for (int n = 0; n < count; n++)
							{
								this.OffsetPoint(n, ref num10, polyNode2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
							this.m_destPoly = new List<IntPoint>();
							DoublePoint doublePoint = this.m_normals[count - 1];
							for (int num11 = count - 1; num11 > 0; num11--)
							{
								this.m_normals[num11] = new DoublePoint(-this.m_normals[num11 - 1].X, -this.m_normals[num11 - 1].Y);
							}
							this.m_normals[0] = new DoublePoint(-doublePoint.X, -doublePoint.Y);
							num10 = 0;
							for (int num12 = count - 1; num12 >= 0; num12--)
							{
								this.OffsetPoint(num12, ref num10, polyNode2.m_jointype);
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
						else
						{
							int num13 = 0;
							for (int num14 = 1; num14 < count - 1; num14++)
							{
								this.OffsetPoint(num14, ref num13, polyNode2.m_jointype);
							}
							if (polyNode2.m_endtype == EndType.etOpenButt)
							{
								int index = count - 1;
								IntPoint item = new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[index].X + this.m_normals[index].X * delta), ClipperOffset.Round((float)this.m_srcPoly[index].Y + this.m_normals[index].Y * delta));
								this.m_destPoly.Add(item);
								item = new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[index].X - this.m_normals[index].X * delta), ClipperOffset.Round((float)this.m_srcPoly[index].Y - this.m_normals[index].Y * delta));
								this.m_destPoly.Add(item);
							}
							else
							{
								int num15 = count - 1;
								num13 = count - 2;
								this.m_sinA = 0f;
								this.m_normals[num15] = new DoublePoint(-this.m_normals[num15].X, -this.m_normals[num15].Y);
								if (polyNode2.m_endtype == EndType.etOpenSquare)
								{
									this.DoSquare(num15, num13);
								}
								else
								{
									this.DoRound(num15, num13);
								}
							}
							for (int num16 = count - 1; num16 > 0; num16--)
							{
								this.m_normals[num16] = new DoublePoint(-this.m_normals[num16 - 1].X, -this.m_normals[num16 - 1].Y);
							}
							this.m_normals[0] = new DoublePoint(-this.m_normals[1].X, -this.m_normals[1].Y);
							num13 = count - 1;
							for (int num17 = num13 - 1; num17 > 0; num17--)
							{
								this.OffsetPoint(num17, ref num13, polyNode2.m_jointype);
							}
							if (polyNode2.m_endtype == EndType.etOpenButt)
							{
								IntPoint item = new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[0].X - this.m_normals[0].X * delta), ClipperOffset.Round((float)this.m_srcPoly[0].Y - this.m_normals[0].Y * delta));
								this.m_destPoly.Add(item);
								item = new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[0].X + this.m_normals[0].X * delta), ClipperOffset.Round((float)this.m_srcPoly[0].Y + this.m_normals[0].Y * delta));
								this.m_destPoly.Add(item);
							}
							else
							{
								num13 = 1;
								this.m_sinA = 0f;
								if (polyNode2.m_endtype == EndType.etOpenSquare)
								{
									this.DoSquare(0, 1);
								}
								else
								{
									this.DoRound(0, 1);
								}
							}
							this.m_destPolys.Add(this.m_destPoly);
						}
					}
				}
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public void Execute(ref List<List<IntPoint>> solution, float delta)
		{
			solution.Clear();
			this.FixOrientations();
			this.DoOffset(delta);
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(this.m_destPolys, PolyType.ptSubject, true);
			if (delta > 0f)
			{
				clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);
				return;
			}
			IntRect bounds = ClipperBase.GetBounds(this.m_destPolys);
			clipper.AddPath(new List<IntPoint>(4)
			{
				new IntPoint(bounds.left - 10, bounds.bottom + 10),
				new IntPoint(bounds.right + 10, bounds.bottom + 10),
				new IntPoint(bounds.right + 10, bounds.top - 10),
				new IntPoint(bounds.left - 10, bounds.top - 10)
			}, PolyType.ptSubject, true);
			clipper.ReverseSolution = true;
			clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftNegative, PolyFillType.pftNegative);
			if (solution.Count > 0)
			{
				solution.RemoveAt(0);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public void Execute(ref PolyTree solution, float delta)
		{
			solution.Clear();
			this.FixOrientations();
			this.DoOffset(delta);
			Clipper clipper = new Clipper(0);
			clipper.AddPaths(this.m_destPolys, PolyType.ptSubject, true);
			if (delta > 0f)
			{
				clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftPositive, PolyFillType.pftPositive);
				return;
			}
			IntRect bounds = ClipperBase.GetBounds(this.m_destPolys);
			clipper.AddPath(new List<IntPoint>(4)
			{
				new IntPoint(bounds.left - 10, bounds.bottom + 10),
				new IntPoint(bounds.right + 10, bounds.bottom + 10),
				new IntPoint(bounds.right + 10, bounds.top - 10),
				new IntPoint(bounds.left - 10, bounds.top - 10)
			}, PolyType.ptSubject, true);
			clipper.ReverseSolution = true;
			clipper.Execute(ClipType.ctUnion, solution, PolyFillType.pftNegative, PolyFillType.pftNegative);
			if (solution.ChildCount == 1 && solution.Childs[0].ChildCount > 0)
			{
				PolyNode polyNode = solution.Childs[0];
				solution.Childs.Capacity = polyNode.ChildCount;
				solution.Childs[0] = polyNode.Childs[0];
				for (int i = 1; i < polyNode.ChildCount; i++)
				{
					solution.AddChild(polyNode.Childs[i]);
				}
				return;
			}
			solution.Clear();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00009F34 File Offset: 0x00008134
		private void OffsetPoint(int j, ref int k, JoinType jointype)
		{
			this.m_sinA = this.m_normals[k].X * this.m_normals[j].Y - this.m_normals[j].X * this.m_normals[k].Y;
			if ((double)this.m_sinA < 5E-05 && (double)this.m_sinA > -5E-05)
			{
				return;
			}
			if ((double)this.m_sinA > 1.0)
			{
				this.m_sinA = 1f;
			}
			else if ((double)this.m_sinA < -1.0)
			{
				this.m_sinA = -1f;
			}
			if (this.m_sinA * this.m_delta < 0f)
			{
				this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[j].X + this.m_normals[k].X * this.m_delta), ClipperOffset.Round((float)this.m_srcPoly[j].Y + this.m_normals[k].Y * this.m_delta)));
				this.m_destPoly.Add(this.m_srcPoly[j]);
				this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperOffset.Round((float)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta)));
			}
			else
			{
				switch (jointype)
				{
				case JoinType.jtSquare:
					this.DoSquare(j, k);
					break;
				case JoinType.jtRound:
					this.DoRound(j, k);
					break;
				case JoinType.jtMiter:
				{
					float num = 1f + (this.m_normals[j].X * this.m_normals[k].X + this.m_normals[j].Y * this.m_normals[k].Y);
					if (num >= this.m_miterLim)
					{
						this.DoMiter(j, k, num);
					}
					else
					{
						this.DoSquare(j, k);
					}
					break;
				}
				}
			}
			k = j;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000A1A4 File Offset: 0x000083A4
		internal void DoSquare(int j, int k)
		{
			float num = (float)Math.Tan(Math.Atan2((double)this.m_sinA, (double)(this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y)) / 4.0);
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[k].X - this.m_normals[k].Y * num)), ClipperOffset.Round((float)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[k].Y + this.m_normals[k].X * num))));
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[j].X + this.m_delta * (this.m_normals[j].X + this.m_normals[j].Y * num)), ClipperOffset.Round((float)this.m_srcPoly[j].Y + this.m_delta * (this.m_normals[j].Y - this.m_normals[j].X * num))));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000A348 File Offset: 0x00008548
		internal void DoMiter(int j, int k, float r)
		{
			float num = this.m_delta / r;
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[j].X + (this.m_normals[k].X + this.m_normals[j].X) * num), ClipperOffset.Round((float)this.m_srcPoly[j].Y + (this.m_normals[k].Y + this.m_normals[j].Y) * num)));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000A3E8 File Offset: 0x000085E8
		internal void DoRound(int j, int k)
		{
			float value = (float)Math.Atan2((double)this.m_sinA, (double)(this.m_normals[k].X * this.m_normals[j].X + this.m_normals[k].Y * this.m_normals[j].Y));
			int num = ClipperOffset.Round(this.m_StepsPerRad * Math.Abs(value));
			float num2 = this.m_normals[k].X;
			float num3 = this.m_normals[k].Y;
			for (int i = 0; i < num; i++)
			{
				this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[j].X + num2 * this.m_delta), ClipperOffset.Round((float)this.m_srcPoly[j].Y + num3 * this.m_delta)));
				float num4 = num2;
				num2 = num2 * this.m_cos - this.m_sin * num3;
				num3 = num4 * this.m_sin + num3 * this.m_cos;
			}
			this.m_destPoly.Add(new IntPoint(ClipperOffset.Round((float)this.m_srcPoly[j].X + this.m_normals[j].X * this.m_delta), ClipperOffset.Round((float)this.m_srcPoly[j].Y + this.m_normals[j].Y * this.m_delta)));
		}

		// Token: 0x04000077 RID: 119
		private List<List<IntPoint>> m_destPolys;

		// Token: 0x04000078 RID: 120
		private List<IntPoint> m_srcPoly;

		// Token: 0x04000079 RID: 121
		private List<IntPoint> m_destPoly;

		// Token: 0x0400007A RID: 122
		private List<DoublePoint> m_normals = new List<DoublePoint>();

		// Token: 0x0400007B RID: 123
		private float m_delta;

		// Token: 0x0400007C RID: 124
		private float m_sinA;

		// Token: 0x0400007D RID: 125
		private float m_sin;

		// Token: 0x0400007E RID: 126
		private float m_cos;

		// Token: 0x0400007F RID: 127
		private float m_miterLim;

		// Token: 0x04000080 RID: 128
		private float m_StepsPerRad;

		// Token: 0x04000081 RID: 129
		private IntPoint m_lowest;

		// Token: 0x04000082 RID: 130
		private PolyNode m_polyNodes = new PolyNode();

		// Token: 0x04000085 RID: 133
		private const float two_pi = 6.2831855f;

		// Token: 0x04000086 RID: 134
		private const float def_arc_tolerance = 0.25f;
	}
}
