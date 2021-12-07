using System;

namespace LibTessDotNet
{
	// Token: 0x02000006 RID: 6
	internal static class MeshUtils
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003020 File Offset: 0x00001220
		public static MeshUtils.Edge MakeEdge(MeshUtils.Edge eNext)
		{
			MeshUtils.EdgePair edgePair = MeshUtils.EdgePair.Create();
			MeshUtils.Edge e = edgePair._e;
			MeshUtils.Edge eSym = edgePair._eSym;
			MeshUtils.Edge.EnsureFirst(ref eNext);
			MeshUtils.Edge next = eNext._Sym._next;
			eSym._next = next;
			next._Sym._next = e;
			e._next = eNext;
			eNext._Sym._next = eSym;
			e._Sym = eSym;
			e._Onext = e;
			e._Lnext = eSym;
			e._Org = null;
			e._Lface = null;
			e._winding = 0;
			e._activeRegion = null;
			eSym._Sym = e;
			eSym._Onext = eSym;
			eSym._Lnext = e;
			eSym._Org = null;
			eSym._Lface = null;
			eSym._winding = 0;
			eSym._activeRegion = null;
			return e;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030DC File Offset: 0x000012DC
		public static void Splice(MeshUtils.Edge a, MeshUtils.Edge b)
		{
			MeshUtils.Edge onext = a._Onext;
			MeshUtils.Edge onext2 = b._Onext;
			onext._Sym._Lnext = b;
			onext2._Sym._Lnext = a;
			a._Onext = onext2;
			b._Onext = onext;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003120 File Offset: 0x00001320
		public static void MakeVertex(MeshUtils.Vertex vNew, MeshUtils.Edge eOrig, MeshUtils.Vertex vNext)
		{
			MeshUtils.Vertex prev = vNext._prev;
			vNew._prev = prev;
			prev._next = vNew;
			vNew._next = vNext;
			vNext._prev = vNew;
			vNew._anEdge = eOrig;
			MeshUtils.Edge edge = eOrig;
			do
			{
				edge._Org = vNew;
				edge = edge._Onext;
			}
			while (edge != eOrig);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000316C File Offset: 0x0000136C
		public static void MakeFace(MeshUtils.Face fNew, MeshUtils.Edge eOrig, MeshUtils.Face fNext)
		{
			MeshUtils.Face prev = fNext._prev;
			fNew._prev = prev;
			prev._next = fNew;
			fNew._next = fNext;
			fNext._prev = fNew;
			fNew._anEdge = eOrig;
			fNew._trail = null;
			fNew._marked = false;
			fNew._inside = fNext._inside;
			MeshUtils.Edge edge = eOrig;
			do
			{
				edge._Lface = fNew;
				edge = edge._Lnext;
			}
			while (edge != eOrig);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000031D4 File Offset: 0x000013D4
		public static void KillEdge(MeshUtils.Edge eDel)
		{
			MeshUtils.Edge.EnsureFirst(ref eDel);
			MeshUtils.Edge next = eDel._next;
			MeshUtils.Edge next2 = eDel._Sym._next;
			next._Sym._next = next2;
			next2._Sym._next = next;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003214 File Offset: 0x00001414
		public static void KillVertex(MeshUtils.Vertex vDel, MeshUtils.Vertex newOrg)
		{
			MeshUtils.Edge anEdge = vDel._anEdge;
			MeshUtils.Edge edge = anEdge;
			do
			{
				edge._Org = newOrg;
				edge = edge._Onext;
			}
			while (edge != anEdge);
			MeshUtils.Vertex prev = vDel._prev;
			MeshUtils.Vertex next = vDel._next;
			next._prev = prev;
			prev._next = next;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003258 File Offset: 0x00001458
		public static void KillFace(MeshUtils.Face fDel, MeshUtils.Face newLFace)
		{
			MeshUtils.Edge anEdge = fDel._anEdge;
			MeshUtils.Edge edge = anEdge;
			do
			{
				edge._Lface = newLFace;
				edge = edge._Lnext;
			}
			while (edge != anEdge);
			MeshUtils.Face prev = fDel._prev;
			MeshUtils.Face next = fDel._next;
			next._prev = prev;
			prev._next = next;
		}

		// Token: 0x0400000B RID: 11
		public const int Undef = -1;

		// Token: 0x02000012 RID: 18
		public class Vertex
		{
			// Token: 0x04000043 RID: 67
			internal MeshUtils.Vertex _prev;

			// Token: 0x04000044 RID: 68
			internal MeshUtils.Vertex _next;

			// Token: 0x04000045 RID: 69
			internal MeshUtils.Edge _anEdge;

			// Token: 0x04000046 RID: 70
			internal Vec3 _coords;

			// Token: 0x04000047 RID: 71
			internal float _s;

			// Token: 0x04000048 RID: 72
			internal float _t;

			// Token: 0x04000049 RID: 73
			internal PQHandle _pqHandle;

			// Token: 0x0400004A RID: 74
			internal int _n;

			// Token: 0x0400004B RID: 75
			internal object _data;
		}

		// Token: 0x02000013 RID: 19
		public class Face
		{
			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000086 RID: 134 RVA: 0x000060A8 File Offset: 0x000042A8
			internal int VertsCount
			{
				get
				{
					int num = 0;
					MeshUtils.Edge edge = this._anEdge;
					do
					{
						num++;
						edge = edge._Lnext;
					}
					while (edge != this._anEdge);
					return num;
				}
			}

			// Token: 0x0400004C RID: 76
			internal MeshUtils.Face _prev;

			// Token: 0x0400004D RID: 77
			internal MeshUtils.Face _next;

			// Token: 0x0400004E RID: 78
			internal MeshUtils.Edge _anEdge;

			// Token: 0x0400004F RID: 79
			internal MeshUtils.Face _trail;

			// Token: 0x04000050 RID: 80
			internal int _n;

			// Token: 0x04000051 RID: 81
			internal bool _marked;

			// Token: 0x04000052 RID: 82
			internal bool _inside;
		}

		// Token: 0x02000014 RID: 20
		public struct EdgePair
		{
			// Token: 0x06000088 RID: 136 RVA: 0x000060D4 File Offset: 0x000042D4
			public static MeshUtils.EdgePair Create()
			{
				MeshUtils.EdgePair edgePair = default(MeshUtils.EdgePair);
				edgePair._e = new MeshUtils.Edge();
				edgePair._e._pair = edgePair;
				edgePair._eSym = new MeshUtils.Edge();
				edgePair._eSym._pair = edgePair;
				return edgePair;
			}

			// Token: 0x04000053 RID: 83
			internal MeshUtils.Edge _e;

			// Token: 0x04000054 RID: 84
			internal MeshUtils.Edge _eSym;
		}

		// Token: 0x02000015 RID: 21
		public class Edge
		{
			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000089 RID: 137 RVA: 0x0000611A File Offset: 0x0000431A
			// (set) Token: 0x0600008A RID: 138 RVA: 0x00006127 File Offset: 0x00004327
			internal MeshUtils.Face _Rface
			{
				get
				{
					return this._Sym._Lface;
				}
				set
				{
					this._Sym._Lface = value;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600008B RID: 139 RVA: 0x00006135 File Offset: 0x00004335
			// (set) Token: 0x0600008C RID: 140 RVA: 0x00006142 File Offset: 0x00004342
			internal MeshUtils.Vertex _Dst
			{
				get
				{
					return this._Sym._Org;
				}
				set
				{
					this._Sym._Org = value;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600008D RID: 141 RVA: 0x00006150 File Offset: 0x00004350
			// (set) Token: 0x0600008E RID: 142 RVA: 0x0000615D File Offset: 0x0000435D
			internal MeshUtils.Edge _Oprev
			{
				get
				{
					return this._Sym._Lnext;
				}
				set
				{
					this._Sym._Lnext = value;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600008F RID: 143 RVA: 0x0000616B File Offset: 0x0000436B
			// (set) Token: 0x06000090 RID: 144 RVA: 0x00006178 File Offset: 0x00004378
			internal MeshUtils.Edge _Lprev
			{
				get
				{
					return this._Onext._Sym;
				}
				set
				{
					this._Onext._Sym = value;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000091 RID: 145 RVA: 0x00006186 File Offset: 0x00004386
			// (set) Token: 0x06000092 RID: 146 RVA: 0x00006193 File Offset: 0x00004393
			internal MeshUtils.Edge _Dprev
			{
				get
				{
					return this._Lnext._Sym;
				}
				set
				{
					this._Lnext._Sym = value;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000093 RID: 147 RVA: 0x000061A1 File Offset: 0x000043A1
			// (set) Token: 0x06000094 RID: 148 RVA: 0x000061AE File Offset: 0x000043AE
			internal MeshUtils.Edge _Rprev
			{
				get
				{
					return this._Sym._Onext;
				}
				set
				{
					this._Sym._Onext = value;
				}
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000095 RID: 149 RVA: 0x000061BC File Offset: 0x000043BC
			// (set) Token: 0x06000096 RID: 150 RVA: 0x000061C9 File Offset: 0x000043C9
			internal MeshUtils.Edge _Dnext
			{
				get
				{
					return this._Rprev._Sym;
				}
				set
				{
					this._Rprev._Sym = value;
				}
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000097 RID: 151 RVA: 0x000061D7 File Offset: 0x000043D7
			// (set) Token: 0x06000098 RID: 152 RVA: 0x000061E4 File Offset: 0x000043E4
			internal MeshUtils.Edge _Rnext
			{
				get
				{
					return this._Oprev._Sym;
				}
				set
				{
					this._Oprev._Sym = value;
				}
			}

			// Token: 0x06000099 RID: 153 RVA: 0x0000609E File Offset: 0x0000429E
			internal Edge()
			{
			}

			// Token: 0x0600009A RID: 154 RVA: 0x000061F2 File Offset: 0x000043F2
			internal static void EnsureFirst(ref MeshUtils.Edge e)
			{
				if (e == e._pair._eSym)
				{
					e = e._Sym;
				}
			}

			// Token: 0x04000055 RID: 85
			internal MeshUtils.EdgePair _pair;

			// Token: 0x04000056 RID: 86
			internal MeshUtils.Edge _next;

			// Token: 0x04000057 RID: 87
			internal MeshUtils.Edge _Sym;

			// Token: 0x04000058 RID: 88
			internal MeshUtils.Edge _Onext;

			// Token: 0x04000059 RID: 89
			internal MeshUtils.Edge _Lnext;

			// Token: 0x0400005A RID: 90
			internal MeshUtils.Vertex _Org;

			// Token: 0x0400005B RID: 91
			internal MeshUtils.Face _Lface;

			// Token: 0x0400005C RID: 92
			internal Tess.ActiveRegion _activeRegion;

			// Token: 0x0400005D RID: 93
			internal int _winding;
		}
	}
}
