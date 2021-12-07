using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200007F RID: 127
	public sealed class RectTransform : Transform
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000799 RID: 1945 RVA: 0x0000AB88 File Offset: 0x00008D88
		// (remove) Token: 0x0600079A RID: 1946 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public static event RectTransform.ReapplyDrivenProperties reapplyDrivenProperties;

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public Rect rect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rect(out result);
				return result;
			}
		}

		// Token: 0x0600079C RID: 1948
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public Vector2 anchorMin
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_anchorMin(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchorMin(ref value);
			}
		}

		// Token: 0x0600079F RID: 1951
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchorMin(out Vector2 value);

		// Token: 0x060007A0 RID: 1952
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchorMin(ref Vector2 value);

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public Vector2 anchorMax
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_anchorMax(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchorMax(ref value);
			}
		}

		// Token: 0x060007A3 RID: 1955
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchorMax(out Vector2 value);

		// Token: 0x060007A4 RID: 1956
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchorMax(ref Vector2 value);

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0000AC18 File Offset: 0x00008E18
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0000AC50 File Offset: 0x00008E50
		public Vector3 anchoredPosition3D
		{
			get
			{
				Vector2 anchoredPosition = this.anchoredPosition;
				return new Vector3(anchoredPosition.x, anchoredPosition.y, base.localPosition.z);
			}
			set
			{
				this.anchoredPosition = new Vector2(value.x, value.y);
				Vector3 localPosition = base.localPosition;
				localPosition.z = value.z;
				base.localPosition = localPosition;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0000AC94 File Offset: 0x00008E94
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public Vector2 anchoredPosition
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_anchoredPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_anchoredPosition(ref value);
			}
		}

		// Token: 0x060007A9 RID: 1961
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_anchoredPosition(out Vector2 value);

		// Token: 0x060007AA RID: 1962
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_anchoredPosition(ref Vector2 value);

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public Vector2 sizeDelta
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_sizeDelta(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_sizeDelta(ref value);
			}
		}

		// Token: 0x060007AD RID: 1965
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_sizeDelta(out Vector2 value);

		// Token: 0x060007AE RID: 1966
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_sizeDelta(ref Vector2 value);

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000ACDC File Offset: 0x00008EDC
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		public Vector2 pivot
		{
			get
			{
				Vector2 result;
				this.INTERNAL_get_pivot(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_pivot(ref value);
			}
		}

		// Token: 0x060007B1 RID: 1969
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pivot(out Vector2 value);

		// Token: 0x060007B2 RID: 1970
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_pivot(ref Vector2 value);

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007B3 RID: 1971
		// (set) Token: 0x060007B4 RID: 1972
		internal extern Object drivenByObject { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007B5 RID: 1973
		// (set) Token: 0x060007B6 RID: 1974
		internal extern DrivenTransformProperties drivenProperties { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000AD00 File Offset: 0x00008F00
		[RequiredByNativeCode]
		internal static void SendReapplyDrivenProperties(RectTransform driven)
		{
			if (RectTransform.reapplyDrivenProperties != null)
			{
				RectTransform.reapplyDrivenProperties(driven);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000AD18 File Offset: 0x00008F18
		public void GetLocalCorners(Vector3[] fourCornersArray)
		{
			if (fourCornersArray == null || fourCornersArray.Length < 4)
			{
				Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
				return;
			}
			Rect rect = this.rect;
			float x = rect.x;
			float y = rect.y;
			float xMax = rect.xMax;
			float yMax = rect.yMax;
			fourCornersArray[0] = new Vector3(x, y, 0f);
			fourCornersArray[1] = new Vector3(x, yMax, 0f);
			fourCornersArray[2] = new Vector3(xMax, yMax, 0f);
			fourCornersArray[3] = new Vector3(xMax, y, 0f);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000ADCC File Offset: 0x00008FCC
		public void GetWorldCorners(Vector3[] fourCornersArray)
		{
			if (fourCornersArray == null || fourCornersArray.Length < 4)
			{
				Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
				return;
			}
			this.GetLocalCorners(fourCornersArray);
			Transform transform = base.transform;
			for (int i = 0; i < 4; i++)
			{
				fourCornersArray[i] = transform.TransformPoint(fourCornersArray[i]);
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000AE34 File Offset: 0x00009034
		internal Rect GetRectInParentSpace()
		{
			Rect rect = this.rect;
			Vector2 a = this.offsetMin + Vector2.Scale(this.pivot, rect.size);
			Transform parent = base.transform.parent;
			if (parent)
			{
				RectTransform component = parent.GetComponent<RectTransform>();
				if (component)
				{
					a += Vector2.Scale(this.anchorMin, component.rect.size);
				}
			}
			rect.x += a.x;
			rect.y += a.y;
			return rect;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000AEDC File Offset: 0x000090DC
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0000AF08 File Offset: 0x00009108
		public Vector2 offsetMin
		{
			get
			{
				return this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition - Vector2.Scale(this.sizeDelta, this.pivot));
				this.sizeDelta -= vector;
				this.anchoredPosition += Vector2.Scale(vector, Vector2.one - this.pivot);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000AF74 File Offset: 0x00009174
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public Vector2 offsetMax
		{
			get
			{
				return this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot);
			}
			set
			{
				Vector2 vector = value - (this.anchoredPosition + Vector2.Scale(this.sizeDelta, Vector2.one - this.pivot));
				this.sizeDelta += vector;
				this.anchoredPosition += Vector2.Scale(vector, this.pivot);
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0000B014 File Offset: 0x00009214
		public void SetInsetAndSizeFromParentEdge(RectTransform.Edge edge, float inset, float size)
		{
			int index = (edge != RectTransform.Edge.Top && edge != RectTransform.Edge.Bottom) ? 0 : 1;
			bool flag = edge == RectTransform.Edge.Top || edge == RectTransform.Edge.Right;
			float value = (float)((!flag) ? 0 : 1);
			Vector2 vector = this.anchorMin;
			vector[index] = value;
			this.anchorMin = vector;
			vector = this.anchorMax;
			vector[index] = value;
			this.anchorMax = vector;
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[index] = size;
			this.sizeDelta = sizeDelta;
			Vector2 anchoredPosition = this.anchoredPosition;
			anchoredPosition[index] = ((!flag) ? (inset + size * this.pivot[index]) : (-inset - size * (1f - this.pivot[index])));
			this.anchoredPosition = anchoredPosition;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0000B0F0 File Offset: 0x000092F0
		public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
		{
			Vector2 sizeDelta = this.sizeDelta;
			sizeDelta[(int)axis] = size - this.GetParentSize()[(int)axis] * (this.anchorMax[(int)axis] - this.anchorMin[(int)axis]);
			this.sizeDelta = sizeDelta;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0000B148 File Offset: 0x00009348
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = base.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		// Token: 0x02000080 RID: 128
		public enum Edge
		{
			// Token: 0x0400017A RID: 378
			Left,
			// Token: 0x0400017B RID: 379
			Right,
			// Token: 0x0400017C RID: 380
			Top,
			// Token: 0x0400017D RID: 381
			Bottom
		}

		// Token: 0x02000081 RID: 129
		public enum Axis
		{
			// Token: 0x0400017F RID: 383
			Horizontal,
			// Token: 0x04000180 RID: 384
			Vertical
		}

		// Token: 0x0200033F RID: 831
		// (Invoke) Token: 0x06002856 RID: 10326
		public delegate void ReapplyDrivenProperties(RectTransform driven);
	}
}
