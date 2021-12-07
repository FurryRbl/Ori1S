using System;

namespace LibTessDotNet
{
	// Token: 0x02000004 RID: 4
	internal class Mesh
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000274C File Offset: 0x0000094C
		public Mesh()
		{
			MeshUtils.Vertex vertex = this._vHead = new MeshUtils.Vertex();
			MeshUtils.Face face = this._fHead = new MeshUtils.Face();
			MeshUtils.EdgePair edgePair = MeshUtils.EdgePair.Create();
			MeshUtils.Edge edge = this._eHead = edgePair._e;
			MeshUtils.Edge edge2 = this._eHeadSym = edgePair._eSym;
			MeshUtils.Vertex next = vertex;
			vertex._prev = vertex;
			vertex._next = next;
			vertex._anEdge = null;
			face._next = (face._prev = face);
			face._anEdge = null;
			face._trail = null;
			face._marked = false;
			face._inside = false;
			edge._next = edge;
			edge._Sym = edge2;
			edge._Onext = null;
			edge._Lnext = null;
			edge._Org = null;
			edge._Lface = null;
			edge._winding = 0;
			edge._activeRegion = null;
			edge2._next = edge2;
			edge2._Sym = edge;
			edge2._Onext = null;
			edge2._Lnext = null;
			edge2._Org = null;
			edge2._Lface = null;
			edge2._winding = 0;
			edge2._activeRegion = null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002860 File Offset: 0x00000A60
		public MeshUtils.Edge MakeEdge()
		{
			MeshUtils.Edge edge = MeshUtils.MakeEdge(this._eHead);
			MeshUtils.MakeVertex(new MeshUtils.Vertex(), edge, this._vHead);
			MeshUtils.MakeVertex(new MeshUtils.Vertex(), edge._Sym, this._vHead);
			MeshUtils.MakeFace(new MeshUtils.Face(), edge, this._fHead);
			return edge;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028B4 File Offset: 0x00000AB4
		public void Splice(MeshUtils.Edge eOrg, MeshUtils.Edge eDst)
		{
			if (eOrg == eDst)
			{
				return;
			}
			bool flag = false;
			if (eDst._Org != eOrg._Org)
			{
				flag = true;
				MeshUtils.KillVertex(eDst._Org, eOrg._Org);
			}
			bool flag2 = false;
			if (eDst._Lface != eOrg._Lface)
			{
				flag2 = true;
				MeshUtils.KillFace(eDst._Lface, eOrg._Lface);
			}
			MeshUtils.Splice(eDst, eOrg);
			if (!flag)
			{
				MeshUtils.MakeVertex(new MeshUtils.Vertex(), eDst, eOrg._Org);
				eOrg._Org._anEdge = eOrg;
			}
			if (!flag2)
			{
				MeshUtils.MakeFace(new MeshUtils.Face(), eDst, eOrg._Lface);
				eOrg._Lface._anEdge = eOrg;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002954 File Offset: 0x00000B54
		public void Delete(MeshUtils.Edge eDel)
		{
			MeshUtils.Edge sym = eDel._Sym;
			bool flag = false;
			if (eDel._Lface != eDel._Rface)
			{
				flag = true;
				MeshUtils.KillFace(eDel._Lface, eDel._Rface);
			}
			if (eDel._Onext == eDel)
			{
				MeshUtils.KillVertex(eDel._Org, null);
			}
			else
			{
				eDel._Rface._anEdge = eDel._Oprev;
				eDel._Org._anEdge = eDel._Onext;
				MeshUtils.Splice(eDel, eDel._Oprev);
				if (!flag)
				{
					MeshUtils.MakeFace(new MeshUtils.Face(), eDel, eDel._Lface);
				}
			}
			if (sym._Onext == sym)
			{
				MeshUtils.KillVertex(sym._Org, null);
				MeshUtils.KillFace(sym._Lface, null);
			}
			else
			{
				eDel._Lface._anEdge = sym._Oprev;
				sym._Org._anEdge = sym._Onext;
				MeshUtils.Splice(sym, sym._Oprev);
			}
			MeshUtils.KillEdge(eDel);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002A3C File Offset: 0x00000C3C
		public MeshUtils.Edge AddEdgeVertex(MeshUtils.Edge eOrg)
		{
			MeshUtils.Edge edge = MeshUtils.MakeEdge(eOrg);
			MeshUtils.Edge sym = edge._Sym;
			MeshUtils.Splice(edge, eOrg._Lnext);
			edge._Org = eOrg._Dst;
			MeshUtils.MakeVertex(new MeshUtils.Vertex(), sym, edge._Org);
			edge._Lface = (sym._Lface = eOrg._Lface);
			return edge;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A98 File Offset: 0x00000C98
		public MeshUtils.Edge SplitEdge(MeshUtils.Edge eOrg)
		{
			MeshUtils.Edge sym = this.AddEdgeVertex(eOrg)._Sym;
			MeshUtils.Splice(eOrg._Sym, eOrg._Sym._Oprev);
			MeshUtils.Splice(eOrg._Sym, sym);
			eOrg._Dst = sym._Org;
			sym._Dst._anEdge = sym._Sym;
			sym._Rface = eOrg._Rface;
			sym._winding = eOrg._winding;
			sym._Sym._winding = eOrg._Sym._winding;
			return sym;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B20 File Offset: 0x00000D20
		public MeshUtils.Edge Connect(MeshUtils.Edge eOrg, MeshUtils.Edge eDst)
		{
			MeshUtils.Edge edge = MeshUtils.MakeEdge(eOrg);
			MeshUtils.Edge sym = edge._Sym;
			bool flag = false;
			if (eDst._Lface != eOrg._Lface)
			{
				flag = true;
				MeshUtils.KillFace(eDst._Lface, eOrg._Lface);
			}
			MeshUtils.Splice(edge, eOrg._Lnext);
			MeshUtils.Splice(sym, eDst);
			edge._Org = eOrg._Dst;
			sym._Org = eDst._Org;
			edge._Lface = (sym._Lface = eOrg._Lface);
			eOrg._Lface._anEdge = sym;
			if (!flag)
			{
				MeshUtils.MakeFace(new MeshUtils.Face(), edge, eOrg._Lface);
			}
			return edge;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public void ZapFace(MeshUtils.Face fZap)
		{
			MeshUtils.Edge anEdge = fZap._anEdge;
			MeshUtils.Edge lnext = anEdge._Lnext;
			MeshUtils.Edge edge;
			do
			{
				edge = lnext;
				lnext = edge._Lnext;
				edge._Lface = null;
				if (edge._Rface == null)
				{
					if (edge._Onext == edge)
					{
						MeshUtils.KillVertex(edge._Org, null);
					}
					else
					{
						edge._Org._anEdge = edge._Onext;
						MeshUtils.Splice(edge, edge._Oprev);
					}
					MeshUtils.Edge sym = edge._Sym;
					if (sym._Onext == sym)
					{
						MeshUtils.KillVertex(sym._Org, null);
					}
					else
					{
						sym._Org._anEdge = sym._Onext;
						MeshUtils.Splice(sym, sym._Oprev);
					}
					MeshUtils.KillEdge(edge);
				}
			}
			while (edge != anEdge);
			MeshUtils.Face prev = fZap._prev;
			MeshUtils.Face next = fZap._next;
			next._prev = prev;
			prev._next = next;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002C94 File Offset: 0x00000E94
		public void MergeConvexFaces(int maxVertsPerFace)
		{
			for (MeshUtils.Face next = this._fHead._next; next != this._fHead; next = next._next)
			{
				if (next._inside)
				{
					MeshUtils.Edge edge = next._anEdge;
					MeshUtils.Vertex org = edge._Org;
					for (;;)
					{
						MeshUtils.Edge lnext = edge._Lnext;
						MeshUtils.Edge sym = edge._Sym;
						if (sym != null && sym._Lface != null && sym._Lface._inside)
						{
							int vertsCount = next.VertsCount;
							int vertsCount2 = sym._Lface.VertsCount;
							if (vertsCount + vertsCount2 - 2 <= maxVertsPerFace && Geom.VertCCW(edge._Lprev._Org, edge._Org, sym._Lnext._Lnext._Org) && Geom.VertCCW(sym._Lprev._Org, sym._Org, edge._Lnext._Lnext._Org))
							{
								lnext = sym._Lnext;
								this.Delete(sym);
								edge = null;
							}
						}
						if (edge != null && edge._Lnext._Org == org)
						{
							break;
						}
						edge = lnext;
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002DAC File Offset: 0x00000FAC
		public void Check()
		{
			MeshUtils.Face face = this._fHead;
			face = this._fHead;
			MeshUtils.Face next;
			MeshUtils.Edge edge;
			while ((next = face._next) != this._fHead)
			{
				edge = next._anEdge;
				do
				{
					edge = edge._Lnext;
				}
				while (edge != next._anEdge);
				face = next;
			}
			MeshUtils.Vertex vertex = this._vHead;
			vertex = this._vHead;
			MeshUtils.Vertex next2;
			while ((next2 = vertex._next) != this._vHead)
			{
				edge = next2._anEdge;
				do
				{
					edge = edge._Onext;
				}
				while (edge != next2._anEdge);
				vertex = next2;
			}
			MeshUtils.Edge edge2 = this._eHead;
			edge2 = this._eHead;
			while ((edge = edge2._next) != this._eHead)
			{
				edge2 = edge;
			}
		}

		// Token: 0x04000003 RID: 3
		internal MeshUtils.Vertex _vHead;

		// Token: 0x04000004 RID: 4
		internal MeshUtils.Face _fHead;

		// Token: 0x04000005 RID: 5
		internal MeshUtils.Edge _eHead;

		// Token: 0x04000006 RID: 6
		internal MeshUtils.Edge _eHeadSym;
	}
}
