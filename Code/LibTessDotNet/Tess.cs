using System;

namespace LibTessDotNet
{
	// Token: 0x0200000A RID: 10
	public class Tess
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003C11 File Offset: 0x00001E11
		private Tess.ActiveRegion RegionBelow(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._prev._key;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003C23 File Offset: 0x00001E23
		private Tess.ActiveRegion RegionAbove(Tess.ActiveRegion reg)
		{
			return reg._nodeUp._next._key;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003C38 File Offset: 0x00001E38
		private bool EdgeLeq(Tess.ActiveRegion reg1, Tess.ActiveRegion reg2)
		{
			MeshUtils.Edge eUp = reg1._eUp;
			MeshUtils.Edge eUp2 = reg2._eUp;
			if (eUp._Dst == this._event)
			{
				if (eUp2._Dst != this._event)
				{
					return Geom.EdgeSign(eUp2._Dst, this._event, eUp2._Org) <= 0f;
				}
				if (Geom.VertLeq(eUp._Org, eUp2._Org))
				{
					return Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) <= 0f;
				}
				return Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) >= 0f;
			}
			else
			{
				if (eUp2._Dst == this._event)
				{
					return Geom.EdgeSign(eUp._Dst, this._event, eUp._Org) >= 0f;
				}
				float num = Geom.EdgeEval(eUp._Dst, this._event, eUp._Org);
				float num2 = Geom.EdgeEval(eUp2._Dst, this._event, eUp2._Org);
				return num >= num2;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003D50 File Offset: 0x00001F50
		private void DeleteRegion(Tess.ActiveRegion reg)
		{
			bool fixUpperEdge = reg._fixUpperEdge;
			reg._eUp._activeRegion = null;
			this._dict.Remove(reg._nodeUp);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003D76 File Offset: 0x00001F76
		private void FixUpperEdge(Tess.ActiveRegion reg, MeshUtils.Edge newEdge)
		{
			this._mesh.Delete(reg._eUp);
			reg._fixUpperEdge = false;
			reg._eUp = newEdge;
			newEdge._activeRegion = reg;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003DA0 File Offset: 0x00001FA0
		private Tess.ActiveRegion TopLeftRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Vertex org = reg._eUp._Org;
			do
			{
				reg = this.RegionAbove(reg);
			}
			while (reg._eUp._Org == org);
			if (reg._fixUpperEdge)
			{
				MeshUtils.Edge newEdge = this._mesh.Connect(this.RegionBelow(reg)._eUp._Sym, reg._eUp._Lnext);
				this.FixUpperEdge(reg, newEdge);
				reg = this.RegionAbove(reg);
			}
			return reg;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003E14 File Offset: 0x00002014
		private Tess.ActiveRegion TopRightRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Vertex dst = reg._eUp._Dst;
			do
			{
				reg = this.RegionAbove(reg);
			}
			while (reg._eUp._Dst == dst);
			return reg;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003E48 File Offset: 0x00002048
		private Tess.ActiveRegion AddRegionBelow(Tess.ActiveRegion regAbove, MeshUtils.Edge eNewUp)
		{
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = eNewUp;
			activeRegion._nodeUp = this._dict.InsertBefore(regAbove._nodeUp, activeRegion);
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = false;
			activeRegion._dirty = false;
			eNewUp._activeRegion = activeRegion;
			return activeRegion;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003E97 File Offset: 0x00002097
		private void ComputeWinding(Tess.ActiveRegion reg)
		{
			reg._windingNumber = this.RegionAbove(reg)._windingNumber + reg._eUp._winding;
			reg._inside = Geom.IsWindingInside(this._windingRule, reg._windingNumber);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003ED0 File Offset: 0x000020D0
		private void FinishRegion(Tess.ActiveRegion reg)
		{
			MeshUtils.Edge eUp = reg._eUp;
			MeshUtils.Face lface = eUp._Lface;
			lface._inside = reg._inside;
			lface._anEdge = eUp;
			this.DeleteRegion(reg);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003F04 File Offset: 0x00002104
		private MeshUtils.Edge FinishLeftRegions(Tess.ActiveRegion regFirst, Tess.ActiveRegion regLast)
		{
			Tess.ActiveRegion activeRegion = regFirst;
			MeshUtils.Edge eUp = regFirst._eUp;
			while (activeRegion != regLast)
			{
				activeRegion._fixUpperEdge = false;
				Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
				MeshUtils.Edge edge = activeRegion2._eUp;
				if (edge._Org != eUp._Org)
				{
					if (!activeRegion2._fixUpperEdge)
					{
						this.FinishRegion(activeRegion);
						break;
					}
					edge = this._mesh.Connect(eUp._Lprev, edge._Sym);
					this.FixUpperEdge(activeRegion2, edge);
				}
				if (eUp._Onext != edge)
				{
					this._mesh.Splice(edge._Oprev, edge);
					this._mesh.Splice(eUp, edge);
				}
				this.FinishRegion(activeRegion);
				eUp = activeRegion2._eUp;
				activeRegion = activeRegion2;
			}
			return eUp;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003FB4 File Offset: 0x000021B4
		private void AddRightEdges(Tess.ActiveRegion regUp, MeshUtils.Edge eFirst, MeshUtils.Edge eLast, MeshUtils.Edge eTopLeft, bool cleanUp)
		{
			bool flag = true;
			MeshUtils.Edge edge = eFirst;
			do
			{
				this.AddRegionBelow(regUp, edge._Sym);
				edge = edge._Onext;
			}
			while (edge != eLast);
			if (eTopLeft == null)
			{
				eTopLeft = this.RegionBelow(regUp)._eUp._Rprev;
			}
			Tess.ActiveRegion activeRegion = regUp;
			MeshUtils.Edge edge2 = eTopLeft;
			for (;;)
			{
				Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
				edge = activeRegion2._eUp._Sym;
				if (edge._Org != edge2._Org)
				{
					break;
				}
				if (edge._Onext != edge2)
				{
					this._mesh.Splice(edge._Oprev, edge);
					this._mesh.Splice(edge2._Oprev, edge);
				}
				activeRegion2._windingNumber = activeRegion._windingNumber - edge._winding;
				activeRegion2._inside = Geom.IsWindingInside(this._windingRule, activeRegion2._windingNumber);
				activeRegion._dirty = true;
				if (!flag && this.CheckForRightSplice(activeRegion))
				{
					Geom.AddWinding(edge, edge2);
					this.DeleteRegion(activeRegion);
					this._mesh.Delete(edge2);
				}
				flag = false;
				activeRegion = activeRegion2;
				edge2 = edge;
			}
			activeRegion._dirty = true;
			if (cleanUp)
			{
				this.WalkDirtyRegions(activeRegion);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000040C7 File Offset: 0x000022C7
		private void SpliceMergeVertices(MeshUtils.Edge e1, MeshUtils.Edge e2)
		{
			this._mesh.Splice(e1, e2);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000040D8 File Offset: 0x000022D8
		private void VertexWeights(MeshUtils.Vertex isect, MeshUtils.Vertex org, MeshUtils.Vertex dst, out float w0, out float w1)
		{
			float num = Geom.VertL1dist(org, isect);
			float num2 = Geom.VertL1dist(dst, isect);
			w0 = 0.5f * num2 / (num + num2);
			w1 = 0.5f * num / (num + num2);
			isect._coords.X = isect._coords.X + (w0 * org._coords.X + w1 * dst._coords.X);
			isect._coords.Y = isect._coords.Y + (w0 * org._coords.Y + w1 * dst._coords.Y);
			isect._coords.Z = isect._coords.Z + (w0 * org._coords.Z + w1 * dst._coords.Z);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000419C File Offset: 0x0000239C
		private void GetIntersectData(MeshUtils.Vertex isect, MeshUtils.Vertex orgUp, MeshUtils.Vertex dstUp, MeshUtils.Vertex orgLo, MeshUtils.Vertex dstLo)
		{
			isect._coords = Vec3.Zero;
			float num;
			float num2;
			this.VertexWeights(isect, orgUp, dstUp, out num, out num2);
			float num3;
			float num4;
			this.VertexWeights(isect, orgLo, dstLo, out num3, out num4);
			if (this._combineCallback != null)
			{
				isect._data = this._combineCallback(isect._coords, new object[]
				{
					orgUp._data,
					dstUp._data,
					orgLo._data,
					dstLo._data
				}, new float[]
				{
					num,
					num2,
					num3,
					num4
				});
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004234 File Offset: 0x00002434
		private bool CheckForRightSplice(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (Geom.VertLeq(eUp._Org, eUp2._Org))
			{
				if (Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) > 0f)
				{
					return false;
				}
				if (!Geom.VertEq(eUp._Org, eUp2._Org))
				{
					this._mesh.SplitEdge(eUp2._Sym);
					this._mesh.Splice(eUp, eUp2._Oprev);
					regUp._dirty = (activeRegion._dirty = true);
				}
				else if (eUp._Org != eUp2._Org)
				{
					this._pq.Remove(eUp._Org._pqHandle);
					this.SpliceMergeVertices(eUp2._Oprev, eUp);
				}
			}
			else
			{
				if (Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) < 0f)
				{
					return false;
				}
				this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
				this._mesh.SplitEdge(eUp._Sym);
				this._mesh.Splice(eUp2._Oprev, eUp);
			}
			return true;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004368 File Offset: 0x00002568
		private bool CheckForLeftSplice(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (Geom.VertLeq(eUp._Dst, eUp2._Dst))
			{
				if (Geom.EdgeSign(eUp._Dst, eUp2._Dst, eUp._Org) < 0f)
				{
					return false;
				}
				this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
				MeshUtils.Edge edge = this._mesh.SplitEdge(eUp);
				this._mesh.Splice(eUp2._Sym, edge);
				edge._Lface._inside = regUp._inside;
			}
			else
			{
				if (Geom.EdgeSign(eUp2._Dst, eUp._Dst, eUp2._Org) > 0f)
				{
					return false;
				}
				regUp._dirty = (activeRegion._dirty = true);
				MeshUtils.Edge edge2 = this._mesh.SplitEdge(eUp2);
				this._mesh.Splice(eUp._Lnext, eUp2._Sym);
				edge2._Rface._inside = regUp._inside;
			}
			return true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004470 File Offset: 0x00002670
		private bool CheckForIntersect(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge edge = activeRegion._eUp;
			MeshUtils.Vertex org = eUp._Org;
			MeshUtils.Vertex org2 = edge._Org;
			MeshUtils.Vertex dst = eUp._Dst;
			MeshUtils.Vertex dst2 = edge._Dst;
			if (org == org2)
			{
				return false;
			}
			float num = Math.Min(org._t, dst._t);
			float num2 = Math.Max(org2._t, dst2._t);
			if (num > num2)
			{
				return false;
			}
			if (Geom.VertLeq(org, org2))
			{
				if (Geom.EdgeSign(dst2, org, org2) > 0f)
				{
					return false;
				}
			}
			else if (Geom.EdgeSign(dst, org2, org) < 0f)
			{
				return false;
			}
			MeshUtils.Vertex vertex = new MeshUtils.Vertex();
			Geom.EdgeIntersect(dst, org, dst2, org2, vertex);
			if (Geom.VertLeq(vertex, this._event))
			{
				vertex._s = this._event._s;
				vertex._t = this._event._t;
			}
			MeshUtils.Vertex vertex2 = Geom.VertLeq(org, org2) ? org : org2;
			if (Geom.VertLeq(vertex2, vertex))
			{
				vertex._s = vertex2._s;
				vertex._t = vertex2._t;
			}
			if (Geom.VertEq(vertex, org) || Geom.VertEq(vertex, org2))
			{
				this.CheckForRightSplice(regUp);
				return false;
			}
			if ((!Geom.VertEq(dst, this._event) && Geom.EdgeSign(dst, this._event, vertex) >= 0f) || (!Geom.VertEq(dst2, this._event) && Geom.EdgeSign(dst2, this._event, vertex) <= 0f))
			{
				if (dst2 == this._event)
				{
					this._mesh.SplitEdge(eUp._Sym);
					this._mesh.Splice(edge._Sym, eUp);
					regUp = this.TopLeftRegion(regUp);
					eUp = this.RegionBelow(regUp)._eUp;
					this.FinishLeftRegions(this.RegionBelow(regUp), activeRegion);
					this.AddRightEdges(regUp, eUp._Oprev, eUp, eUp, true);
					return true;
				}
				if (dst == this._event)
				{
					this._mesh.SplitEdge(edge._Sym);
					this._mesh.Splice(eUp._Lnext, edge._Oprev);
					activeRegion = regUp;
					regUp = this.TopRightRegion(regUp);
					MeshUtils.Edge rprev = this.RegionBelow(regUp)._eUp._Rprev;
					activeRegion._eUp = edge._Oprev;
					edge = this.FinishLeftRegions(activeRegion, null);
					this.AddRightEdges(regUp, edge._Onext, eUp._Rprev, rprev, true);
					return true;
				}
				if (Geom.EdgeSign(dst, this._event, vertex) >= 0f)
				{
					this.RegionAbove(regUp)._dirty = (regUp._dirty = true);
					this._mesh.SplitEdge(eUp._Sym);
					eUp._Org._s = this._event._s;
					eUp._Org._t = this._event._t;
				}
				if (Geom.EdgeSign(dst2, this._event, vertex) <= 0f)
				{
					regUp._dirty = (activeRegion._dirty = true);
					this._mesh.SplitEdge(edge._Sym);
					edge._Org._s = this._event._s;
					edge._Org._t = this._event._t;
				}
				return false;
			}
			else
			{
				this._mesh.SplitEdge(eUp._Sym);
				this._mesh.SplitEdge(edge._Sym);
				this._mesh.Splice(edge._Oprev, eUp);
				eUp._Org._s = vertex._s;
				eUp._Org._t = vertex._t;
				eUp._Org._pqHandle = this._pq.Insert(eUp._Org);
				if (eUp._Org._pqHandle._handle == PQHandle.Invalid)
				{
					throw new InvalidOperationException("PQHandle should not be invalid");
				}
				this.GetIntersectData(eUp._Org, org, dst, org2, dst2);
				this.RegionAbove(regUp)._dirty = (regUp._dirty = (activeRegion._dirty = true));
				return false;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000488C File Offset: 0x00002A8C
		private void WalkDirtyRegions(Tess.ActiveRegion regUp)
		{
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			for (;;)
			{
				if (!activeRegion._dirty)
				{
					if (!regUp._dirty)
					{
						activeRegion = regUp;
						regUp = this.RegionAbove(regUp);
						if (regUp == null || !regUp._dirty)
						{
							break;
						}
					}
					regUp._dirty = false;
					MeshUtils.Edge eUp = regUp._eUp;
					MeshUtils.Edge eUp2 = activeRegion._eUp;
					if (eUp._Dst != eUp2._Dst && this.CheckForLeftSplice(regUp))
					{
						if (activeRegion._fixUpperEdge)
						{
							this.DeleteRegion(activeRegion);
							this._mesh.Delete(eUp2);
							activeRegion = this.RegionBelow(regUp);
							eUp2 = activeRegion._eUp;
						}
						else if (regUp._fixUpperEdge)
						{
							this.DeleteRegion(regUp);
							this._mesh.Delete(eUp);
							regUp = this.RegionAbove(activeRegion);
							eUp = regUp._eUp;
						}
					}
					if (eUp._Org != eUp2._Org)
					{
						if (eUp._Dst != eUp2._Dst && !regUp._fixUpperEdge && !activeRegion._fixUpperEdge && (eUp._Dst == this._event || eUp2._Dst == this._event))
						{
							if (this.CheckForIntersect(regUp))
							{
								return;
							}
						}
						else
						{
							this.CheckForRightSplice(regUp);
						}
					}
					if (eUp._Org == eUp2._Org && eUp._Dst == eUp2._Dst)
					{
						Geom.AddWinding(eUp2, eUp);
						this.DeleteRegion(regUp);
						this._mesh.Delete(eUp);
						regUp = this.RegionAbove(activeRegion);
					}
				}
				else
				{
					regUp = activeRegion;
					activeRegion = this.RegionBelow(activeRegion);
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000049FC File Offset: 0x00002BFC
		private void ConnectRightVertex(Tess.ActiveRegion regUp, MeshUtils.Edge eBottomLeft)
		{
			MeshUtils.Edge edge = eBottomLeft._Onext;
			Tess.ActiveRegion activeRegion = this.RegionBelow(regUp);
			MeshUtils.Edge eUp = regUp._eUp;
			MeshUtils.Edge eUp2 = activeRegion._eUp;
			bool flag = false;
			if (eUp._Dst != eUp2._Dst)
			{
				this.CheckForIntersect(regUp);
			}
			if (Geom.VertEq(eUp._Org, this._event))
			{
				this._mesh.Splice(edge._Oprev, eUp);
				regUp = this.TopLeftRegion(regUp);
				edge = this.RegionBelow(regUp)._eUp;
				this.FinishLeftRegions(this.RegionBelow(regUp), activeRegion);
				flag = true;
			}
			if (Geom.VertEq(eUp2._Org, this._event))
			{
				this._mesh.Splice(eBottomLeft, eUp2._Oprev);
				eBottomLeft = this.FinishLeftRegions(activeRegion, null);
				flag = true;
			}
			if (flag)
			{
				this.AddRightEdges(regUp, eBottomLeft._Onext, edge, edge, true);
				return;
			}
			MeshUtils.Edge edge2;
			if (Geom.VertLeq(eUp2._Org, eUp._Org))
			{
				edge2 = eUp2._Oprev;
			}
			else
			{
				edge2 = eUp;
			}
			edge2 = this._mesh.Connect(eBottomLeft._Lprev, edge2);
			this.AddRightEdges(regUp, edge2, edge2._Onext, edge2._Onext, false);
			edge2._Sym._activeRegion._fixUpperEdge = true;
			this.WalkDirtyRegions(regUp);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004B3C File Offset: 0x00002D3C
		private void ConnectLeftDegenerate(Tess.ActiveRegion regUp, MeshUtils.Vertex vEvent)
		{
			MeshUtils.Edge eUp = regUp._eUp;
			if (Geom.VertEq(eUp._Org, vEvent))
			{
				throw new InvalidOperationException("Vertices should have been merged before");
			}
			if (!Geom.VertEq(eUp._Dst, vEvent))
			{
				this._mesh.SplitEdge(eUp._Sym);
				if (regUp._fixUpperEdge)
				{
					this._mesh.Delete(eUp._Onext);
					regUp._fixUpperEdge = false;
				}
				this._mesh.Splice(vEvent._anEdge, eUp);
				this.SweepEvent(vEvent);
				return;
			}
			throw new InvalidOperationException("Vertices should have been merged before");
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004BD0 File Offset: 0x00002DD0
		private void ConnectLeftVertex(MeshUtils.Vertex vEvent)
		{
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = vEvent._anEdge._Sym;
			Tess.ActiveRegion key = this._dict.Find(activeRegion).Key;
			Tess.ActiveRegion activeRegion2 = this.RegionBelow(key);
			if (activeRegion2 == null)
			{
				return;
			}
			MeshUtils.Edge eUp = key._eUp;
			MeshUtils.Edge eUp2 = activeRegion2._eUp;
			if (Geom.EdgeSign(eUp._Dst, vEvent, eUp._Org) == 0f)
			{
				this.ConnectLeftDegenerate(key, vEvent);
				return;
			}
			Tess.ActiveRegion activeRegion3 = Geom.VertLeq(eUp2._Dst, eUp._Dst) ? key : activeRegion2;
			if (key._inside || activeRegion3._fixUpperEdge)
			{
				MeshUtils.Edge edge;
				if (activeRegion3 == key)
				{
					edge = this._mesh.Connect(vEvent._anEdge._Sym, eUp._Lnext);
				}
				else
				{
					edge = this._mesh.Connect(eUp2._Dnext, vEvent._anEdge)._Sym;
				}
				if (activeRegion3._fixUpperEdge)
				{
					this.FixUpperEdge(activeRegion3, edge);
				}
				else
				{
					this.ComputeWinding(this.AddRegionBelow(key, edge));
				}
				this.SweepEvent(vEvent);
				return;
			}
			this.AddRightEdges(key, vEvent._anEdge, vEvent._anEdge, null, true);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004CF4 File Offset: 0x00002EF4
		private void SweepEvent(MeshUtils.Vertex vEvent)
		{
			this._event = vEvent;
			MeshUtils.Edge edge = vEvent._anEdge;
			while (edge._activeRegion == null)
			{
				edge = edge._Onext;
				if (edge == vEvent._anEdge)
				{
					this.ConnectLeftVertex(vEvent);
					return;
				}
			}
			Tess.ActiveRegion activeRegion = this.TopLeftRegion(edge._activeRegion);
			Tess.ActiveRegion activeRegion2 = this.RegionBelow(activeRegion);
			MeshUtils.Edge eUp = activeRegion2._eUp;
			MeshUtils.Edge edge2 = this.FinishLeftRegions(activeRegion2, null);
			if (edge2._Onext == eUp)
			{
				this.ConnectRightVertex(activeRegion, edge2);
				return;
			}
			this.AddRightEdges(activeRegion, edge2._Onext, eUp, eUp, true);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004D7C File Offset: 0x00002F7C
		private void AddSentinel(float smin, float smax, float t)
		{
			MeshUtils.Edge edge = this._mesh.MakeEdge();
			edge._Org._s = smax;
			edge._Org._t = t;
			edge._Dst._s = smin;
			edge._Dst._t = t;
			this._event = edge._Dst;
			Tess.ActiveRegion activeRegion = new Tess.ActiveRegion();
			activeRegion._eUp = edge;
			activeRegion._windingNumber = 0;
			activeRegion._inside = false;
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = true;
			activeRegion._dirty = false;
			activeRegion._nodeUp = this._dict.Insert(activeRegion);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004E14 File Offset: 0x00003014
		private void InitEdgeDict()
		{
			this._dict = new Dict<Tess.ActiveRegion>(new Dict<Tess.ActiveRegion>.LessOrEqual(this.EdgeLeq));
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, -this.SentinelCoord);
			this.AddSentinel(-this.SentinelCoord, this.SentinelCoord, this.SentinelCoord);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004E6C File Offset: 0x0000306C
		private void DoneEdgeDict()
		{
			Tess.ActiveRegion key;
			while ((key = this._dict.Min().Key) != null)
			{
				bool sentinel = key._sentinel;
				this.DeleteRegion(key);
			}
			this._dict = null;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004EA4 File Offset: 0x000030A4
		private void RemoveDegenerateEdges()
		{
			MeshUtils.Edge eHead = this._mesh._eHead;
			MeshUtils.Edge next;
			for (MeshUtils.Edge edge = eHead._next; edge != eHead; edge = next)
			{
				next = edge._next;
				MeshUtils.Edge lnext = edge._Lnext;
				if (Geom.VertEq(edge._Org, edge._Dst) && edge._Lnext._Lnext != edge)
				{
					this.SpliceMergeVertices(lnext, edge);
					this._mesh.Delete(edge);
					edge = lnext;
					lnext = edge._Lnext;
				}
				if (lnext._Lnext == edge)
				{
					if (lnext != edge)
					{
						if (lnext == next || lnext == next._Sym)
						{
							next = next._next;
						}
						this._mesh.Delete(lnext);
					}
					if (edge == next || edge == next._Sym)
					{
						next = next._next;
					}
					this._mesh.Delete(edge);
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004F6C File Offset: 0x0000316C
		private void InitPriorityQ()
		{
			MeshUtils.Vertex vHead = this._mesh._vHead;
			int num = 0;
			for (MeshUtils.Vertex next = vHead._next; next != vHead; next = next._next)
			{
				num++;
			}
			num += 8;
			this._pq = new PriorityQueue<MeshUtils.Vertex>(num, new PriorityHeap<MeshUtils.Vertex>.LessOrEqual(Geom.VertLeq));
			vHead = this._mesh._vHead;
			for (MeshUtils.Vertex next = vHead._next; next != vHead; next = next._next)
			{
				next._pqHandle = this._pq.Insert(next);
				if (next._pqHandle._handle == PQHandle.Invalid)
				{
					throw new InvalidOperationException("PQHandle should not be invalid");
				}
			}
			this._pq.Init();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005015 File Offset: 0x00003215
		private void DonePriorityQ()
		{
			this._pq = null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005020 File Offset: 0x00003220
		private void RemoveDegenerateFaces()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				MeshUtils.Edge anEdge = face._anEdge;
				if (anEdge._Lnext._Lnext == anEdge)
				{
					Geom.AddWinding(anEdge._Onext, anEdge);
					this._mesh.Delete(anEdge);
				}
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005084 File Offset: 0x00003284
		protected void ComputeInterior()
		{
			this.RemoveDegenerateEdges();
			this.InitPriorityQ();
			this.RemoveDegenerateFaces();
			this.InitEdgeDict();
			MeshUtils.Vertex vertex;
			while ((vertex = this._pq.ExtractMin()) != null)
			{
				for (;;)
				{
					MeshUtils.Vertex vertex2 = this._pq.Minimum();
					if (vertex2 == null || !Geom.VertEq(vertex2, vertex))
					{
						break;
					}
					vertex2 = this._pq.ExtractMin();
					this.SpliceMergeVertices(vertex._anEdge, vertex2._anEdge);
				}
				this.SweepEvent(vertex);
			}
			this.DoneEdgeDict();
			this.DonePriorityQ();
			this.RemoveDegenerateFaces();
			this._mesh.Check();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00005116 File Offset: 0x00003316
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000511E File Offset: 0x0000331E
		public Vec3 Normal
		{
			get
			{
				return this._normal;
			}
			set
			{
				this._normal = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00005127 File Offset: 0x00003327
		public ContourVertex[] Vertices
		{
			get
			{
				return this._vertices;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000512F File Offset: 0x0000332F
		public int VertexCount
		{
			get
			{
				return this._vertexCount;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00005137 File Offset: 0x00003337
		public int[] Elements
		{
			get
			{
				return this._elements;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000513F File Offset: 0x0000333F
		public int ElementCount
		{
			get
			{
				return this._elementCount;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005148 File Offset: 0x00003348
		public Tess()
		{
			this._normal = Vec3.Zero;
			this._bminX = (this._bminY = (this._bmaxX = (this._bmaxY = 0f)));
			this._windingRule = WindingRule.EvenOdd;
			this._mesh = null;
			this._vertices = null;
			this._vertexCount = 0;
			this._elements = null;
			this._elementCount = 0;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000051CC File Offset: 0x000033CC
		private void ComputeNormal(ref Vec3 norm)
		{
			MeshUtils.Vertex next = this._mesh._vHead._next;
			float[] array = new float[]
			{
				next._coords.X,
				next._coords.Y,
				next._coords.Z
			};
			MeshUtils.Vertex[] array2 = new MeshUtils.Vertex[]
			{
				next,
				next,
				next
			};
			float[] array3 = new float[]
			{
				next._coords.X,
				next._coords.Y,
				next._coords.Z
			};
			MeshUtils.Vertex[] array4 = new MeshUtils.Vertex[]
			{
				next,
				next,
				next
			};
			while (next != this._mesh._vHead)
			{
				if (next._coords.X < array[0])
				{
					array[0] = next._coords.X;
					array2[0] = next;
				}
				if (next._coords.Y < array[1])
				{
					array[1] = next._coords.Y;
					array2[1] = next;
				}
				if (next._coords.Z < array[2])
				{
					array[2] = next._coords.Z;
					array2[2] = next;
				}
				if (next._coords.X > array3[0])
				{
					array3[0] = next._coords.X;
					array4[0] = next;
				}
				if (next._coords.Y > array3[1])
				{
					array3[1] = next._coords.Y;
					array4[1] = next;
				}
				if (next._coords.Z > array3[2])
				{
					array3[2] = next._coords.Z;
					array4[2] = next;
				}
				next = next._next;
			}
			int num = 0;
			if (array3[1] - array[1] > array3[0] - array[0])
			{
				num = 1;
			}
			if (array3[2] - array[2] > array3[num] - array[num])
			{
				num = 2;
			}
			if (array[num] >= array3[num])
			{
				norm = new Vec3
				{
					X = 0f,
					Y = 0f,
					Z = 1f
				};
				return;
			}
			float num2 = 0f;
			MeshUtils.Vertex vertex = array2[num];
			MeshUtils.Vertex vertex2 = array4[num];
			Vec3 vec;
			Vec3.Sub(ref vertex._coords, ref vertex2._coords, out vec);
			for (next = this._mesh._vHead._next; next != this._mesh._vHead; next = next._next)
			{
				Vec3 vec2;
				Vec3.Sub(ref next._coords, ref vertex2._coords, out vec2);
				Vec3 vec3;
				vec3.X = vec.Y * vec2.Z - vec.Z * vec2.Y;
				vec3.Y = vec.Z * vec2.X - vec.X * vec2.Z;
				vec3.Z = vec.X * vec2.Y - vec.Y * vec2.X;
				float num3 = vec3.X * vec3.X + vec3.Y * vec3.Y + vec3.Z * vec3.Z;
				if (num3 > num2)
				{
					num2 = num3;
					norm = vec3;
				}
			}
			if (num2 <= 0f)
			{
				norm = Vec3.Zero;
				num = Vec3.LongAxis(ref vec);
				norm[num] = 1f;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005510 File Offset: 0x00003710
		private void CheckOrientation()
		{
			float num = 0f;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				MeshUtils.Edge edge = next._anEdge;
				if (edge._winding > 0)
				{
					do
					{
						num += (edge._Org._s - edge._Dst._s) * (edge._Org._t + edge._Dst._t);
						edge = edge._Lnext;
					}
					while (edge != next._anEdge);
				}
			}
			if (num < 0f)
			{
				for (MeshUtils.Vertex next2 = this._mesh._vHead._next; next2 != this._mesh._vHead; next2 = next2._next)
				{
					next2._t = -next2._t;
				}
				Vec3.Neg(ref this._tUnit);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000055E8 File Offset: 0x000037E8
		private void ProjectPolygon()
		{
			Vec3 normal = this._normal;
			bool flag = false;
			if (normal.X == 0f && normal.Y == 0f && normal.Z == 0f)
			{
				this.ComputeNormal(ref normal);
				flag = true;
			}
			int num = Vec3.LongAxis(ref normal);
			this._sUnit[num] = 0f;
			this._sUnit[(num + 1) % 3] = this.SUnitX;
			this._sUnit[(num + 2) % 3] = this.SUnitY;
			this._tUnit[num] = 0f;
			this._tUnit[(num + 1) % 3] = ((normal[num] > 0f) ? (-this.SUnitY) : this.SUnitY);
			this._tUnit[(num + 2) % 3] = ((normal[num] > 0f) ? this.SUnitX : (-this.SUnitX));
			for (MeshUtils.Vertex next = this._mesh._vHead._next; next != this._mesh._vHead; next = next._next)
			{
				Vec3.Dot(ref next._coords, ref this._sUnit, out next._s);
				Vec3.Dot(ref next._coords, ref this._tUnit, out next._t);
			}
			if (flag)
			{
				this.CheckOrientation();
			}
			bool flag2 = true;
			for (MeshUtils.Vertex next2 = this._mesh._vHead._next; next2 != this._mesh._vHead; next2 = next2._next)
			{
				if (flag2)
				{
					this._bminX = (this._bmaxX = next2._s);
					this._bminY = (this._bmaxY = next2._t);
					flag2 = false;
				}
				else
				{
					if (next2._s < this._bminX)
					{
						this._bminX = next2._s;
					}
					if (next2._s > this._bmaxX)
					{
						this._bmaxX = next2._s;
					}
					if (next2._t < this._bminY)
					{
						this._bminY = next2._t;
					}
					if (next2._t > this._bmaxY)
					{
						this._bmaxY = next2._t;
					}
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005824 File Offset: 0x00003A24
		private void TessellateMonoRegion(MeshUtils.Face face)
		{
			MeshUtils.Edge edge = face._anEdge;
			while (Geom.VertLeq(edge._Dst, edge._Org))
			{
				edge = edge._Lprev;
			}
			while (Geom.VertLeq(edge._Org, edge._Dst))
			{
				edge = edge._Lnext;
			}
			MeshUtils.Edge edge2 = edge._Lprev;
			while (edge._Lnext != edge2)
			{
				if (Geom.VertLeq(edge._Dst, edge2._Org))
				{
					while (edge2._Lnext != edge && (Geom.EdgeGoesLeft(edge2._Lnext) || Geom.EdgeSign(edge2._Org, edge2._Dst, edge2._Lnext._Dst) <= 0f))
					{
						edge2 = this._mesh.Connect(edge2._Lnext, edge2)._Sym;
					}
					edge2 = edge2._Lprev;
				}
				else
				{
					while (edge2._Lnext != edge && (Geom.EdgeGoesRight(edge._Lprev) || Geom.EdgeSign(edge._Dst, edge._Org, edge._Lprev._Org) >= 0f))
					{
						edge = this._mesh.Connect(edge, edge._Lprev)._Sym;
					}
					edge = edge._Lnext;
				}
			}
			while (edge2._Lnext._Lnext != edge)
			{
				edge2 = this._mesh.Connect(edge2._Lnext, edge2)._Sym;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005978 File Offset: 0x00003B78
		private void TessellateInterior()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				if (face._inside)
				{
					this.TessellateMonoRegion(face);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000059C0 File Offset: 0x00003BC0
		private void DiscardExterior()
		{
			MeshUtils.Face next;
			for (MeshUtils.Face face = this._mesh._fHead._next; face != this._mesh._fHead; face = next)
			{
				next = face._next;
				if (!face._inside)
				{
					this._mesh.ZapFace(face);
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005A0C File Offset: 0x00003C0C
		private void SetWindingNumber(int value, bool keepOnlyBoundary)
		{
			MeshUtils.Edge next;
			for (MeshUtils.Edge edge = this._mesh._eHead._next; edge != this._mesh._eHead; edge = next)
			{
				next = edge._next;
				if (edge._Rface._inside != edge._Lface._inside)
				{
					edge._winding = (edge._Lface._inside ? value : (-value));
				}
				else if (!keepOnlyBoundary)
				{
					edge._winding = 0;
				}
				else
				{
					this._mesh.Delete(edge);
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005A8D File Offset: 0x00003C8D
		private int GetNeighbourFace(MeshUtils.Edge edge)
		{
			if (edge._Rface == null)
			{
				return -1;
			}
			if (!edge._Rface._inside)
			{
				return -1;
			}
			return edge._Rface._n;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005AB4 File Offset: 0x00003CB4
		private void OutputPolymesh(ElementType elementType, int polySize)
		{
			int num = 0;
			int num2 = 0;
			if (polySize < 3)
			{
				polySize = 3;
			}
			if (polySize > 3)
			{
				this._mesh.MergeConvexFaces(polySize);
			}
			for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				vertex._n = -1;
			}
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				next._n = -1;
				if (next._inside)
				{
					MeshUtils.Edge edge = next._anEdge;
					int num3 = 0;
					do
					{
						MeshUtils.Vertex vertex = edge._Org;
						if (vertex._n == -1)
						{
							vertex._n = num2;
							num2++;
						}
						num3++;
						edge = edge._Lnext;
					}
					while (edge != next._anEdge);
					next._n = num;
					num++;
				}
			}
			this._elementCount = num;
			if (elementType == ElementType.ConnectedPolygons)
			{
				num *= 2;
			}
			this._elements = new int[num * polySize];
			this._vertexCount = num2;
			this._vertices = new ContourVertex[this._vertexCount];
			for (MeshUtils.Vertex vertex = this._mesh._vHead._next; vertex != this._mesh._vHead; vertex = vertex._next)
			{
				if (vertex._n != -1)
				{
					this._vertices[vertex._n].Position = vertex._coords;
					this._vertices[vertex._n].Data = vertex._data;
				}
			}
			int num4 = 0;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					MeshUtils.Edge edge = next._anEdge;
					int num3 = 0;
					do
					{
						MeshUtils.Vertex vertex = edge._Org;
						this._elements[num4++] = vertex._n;
						num3++;
						edge = edge._Lnext;
					}
					while (edge != next._anEdge);
					for (int i = num3; i < polySize; i++)
					{
						this._elements[num4++] = -1;
					}
					if (elementType == ElementType.ConnectedPolygons)
					{
						edge = next._anEdge;
						do
						{
							this._elements[num4++] = this.GetNeighbourFace(edge);
							edge = edge._Lnext;
						}
						while (edge != next._anEdge);
						for (int i = num3; i < polySize; i++)
						{
							this._elements[num4++] = -1;
						}
					}
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005D14 File Offset: 0x00003F14
		private void OutputContours()
		{
			this._vertexCount = 0;
			this._elementCount = 0;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					MeshUtils.Edge anEdge;
					MeshUtils.Edge edge = anEdge = next._anEdge;
					do
					{
						this._vertexCount++;
						edge = edge._Lnext;
					}
					while (edge != anEdge);
					this._elementCount++;
				}
			}
			this._elements = new int[this._elementCount * 2];
			this._vertices = new ContourVertex[this._vertexCount];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (MeshUtils.Face next = this._mesh._fHead._next; next != this._mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					int num4 = 0;
					MeshUtils.Edge anEdge;
					MeshUtils.Edge edge = anEdge = next._anEdge;
					do
					{
						this._vertices[num].Position = edge._Org._coords;
						this._vertices[num].Data = edge._Org._data;
						num++;
						num4++;
						edge = edge._Lnext;
					}
					while (edge != anEdge);
					this._elements[num2++] = num3;
					this._elements[num2++] = num4;
					num3 += num4;
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005E7C File Offset: 0x0000407C
		private float SignedArea(ContourVertex[] vertices)
		{
			float num = 0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				ContourVertex contourVertex = vertices[i];
				ContourVertex contourVertex2 = vertices[(i + 1) % vertices.Length];
				num += contourVertex.Position.X * contourVertex2.Position.Y;
				num -= contourVertex.Position.Y * contourVertex2.Position.X;
			}
			return num * 0.5f;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005EEE File Offset: 0x000040EE
		public void AddContour(ContourVertex[] vertices)
		{
			this.AddContour(vertices, ContourOrientation.Original);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00005EF8 File Offset: 0x000040F8
		public void AddContour(ContourVertex[] vertices, ContourOrientation forceOrientation)
		{
			if (this._mesh == null)
			{
				this._mesh = new Mesh();
			}
			bool flag = false;
			if (forceOrientation != ContourOrientation.Original)
			{
				float num = this.SignedArea(vertices);
				flag = ((forceOrientation == ContourOrientation.Clockwise && num < 0f) || (forceOrientation == ContourOrientation.CounterClockwise && num > 0f));
			}
			MeshUtils.Edge edge = null;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (edge == null)
				{
					edge = this._mesh.MakeEdge();
					this._mesh.Splice(edge, edge._Sym);
				}
				else
				{
					this._mesh.SplitEdge(edge);
					edge = edge._Lnext;
				}
				int num2 = flag ? (vertices.Length - 1 - i) : i;
				edge._Org._coords = vertices[num2].Position;
				edge._Org._data = vertices[num2].Data;
				edge._winding = 1;
				edge._Sym._winding = -1;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005FE3 File Offset: 0x000041E3
		public void Tessellate(WindingRule windingRule, ElementType elementType, int polySize)
		{
			this.Tessellate(windingRule, elementType, polySize, null);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00005FF0 File Offset: 0x000041F0
		public void Tessellate(WindingRule windingRule, ElementType elementType, int polySize, CombineCallback combineCallback)
		{
			this._vertices = null;
			this._elements = null;
			this._windingRule = windingRule;
			this._combineCallback = combineCallback;
			if (this._mesh == null)
			{
				return;
			}
			this.ProjectPolygon();
			this.ComputeInterior();
			if (elementType == ElementType.BoundaryContours)
			{
				this.SetWindingNumber(1, true);
			}
			else
			{
				this.TessellateInterior();
			}
			this._mesh.Check();
			if (elementType == ElementType.BoundaryContours)
			{
				this.OutputContours();
			}
			else
			{
				this.OutputPolymesh(elementType, polySize);
			}
			this._mesh = null;
		}

		// Token: 0x0400001C RID: 28
		private Mesh _mesh;

		// Token: 0x0400001D RID: 29
		private Vec3 _normal;

		// Token: 0x0400001E RID: 30
		private Vec3 _sUnit;

		// Token: 0x0400001F RID: 31
		private Vec3 _tUnit;

		// Token: 0x04000020 RID: 32
		private float _bminX;

		// Token: 0x04000021 RID: 33
		private float _bminY;

		// Token: 0x04000022 RID: 34
		private float _bmaxX;

		// Token: 0x04000023 RID: 35
		private float _bmaxY;

		// Token: 0x04000024 RID: 36
		private WindingRule _windingRule;

		// Token: 0x04000025 RID: 37
		private Dict<Tess.ActiveRegion> _dict;

		// Token: 0x04000026 RID: 38
		private PriorityQueue<MeshUtils.Vertex> _pq;

		// Token: 0x04000027 RID: 39
		private MeshUtils.Vertex _event;

		// Token: 0x04000028 RID: 40
		private CombineCallback _combineCallback;

		// Token: 0x04000029 RID: 41
		private ContourVertex[] _vertices;

		// Token: 0x0400002A RID: 42
		private int _vertexCount;

		// Token: 0x0400002B RID: 43
		private int[] _elements;

		// Token: 0x0400002C RID: 44
		private int _elementCount;

		// Token: 0x0400002D RID: 45
		public float SUnitX = 1f;

		// Token: 0x0400002E RID: 46
		public float SUnitY;

		// Token: 0x0400002F RID: 47
		public float SentinelCoord = 4E+30f;

		// Token: 0x02000019 RID: 25
		internal class ActiveRegion
		{
			// Token: 0x04000062 RID: 98
			internal MeshUtils.Edge _eUp;

			// Token: 0x04000063 RID: 99
			internal Dict<Tess.ActiveRegion>.Node _nodeUp;

			// Token: 0x04000064 RID: 100
			internal int _windingNumber;

			// Token: 0x04000065 RID: 101
			internal bool _inside;

			// Token: 0x04000066 RID: 102
			internal bool _sentinel;

			// Token: 0x04000067 RID: 103
			internal bool _dirty;

			// Token: 0x04000068 RID: 104
			internal bool _fixUpperEdge;
		}
	}
}
