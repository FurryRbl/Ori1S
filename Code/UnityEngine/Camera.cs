using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B1 RID: 177
	[UsedByNativeCode]
	public sealed class Camera : Behaviour
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		[Obsolete("use Camera.fieldOfView instead.")]
		public float fov
		{
			get
			{
				return this.fieldOfView;
			}
			set
			{
				this.fieldOfView = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		[Obsolete("use Camera.nearClipPlane instead.")]
		public float near
		{
			get
			{
				return this.nearClipPlane;
			}
			set
			{
				this.nearClipPlane = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		[Obsolete("use Camera.farClipPlane instead.")]
		public float far
		{
			get
			{
				return this.farClipPlane;
			}
			set
			{
				this.farClipPlane = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A11 RID: 2577
		// (set) Token: 0x06000A12 RID: 2578
		public extern float fieldOfView { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A13 RID: 2579
		// (set) Token: 0x06000A14 RID: 2580
		public extern float nearClipPlane { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A15 RID: 2581
		// (set) Token: 0x06000A16 RID: 2582
		public extern float farClipPlane { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A17 RID: 2583
		// (set) Token: 0x06000A18 RID: 2584
		public extern RenderingPath renderingPath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A19 RID: 2585
		public extern RenderingPath actualRenderingPath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A1A RID: 2586
		// (set) Token: 0x06000A1B RID: 2587
		public extern bool hdr { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A1C RID: 2588
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] GetHDRWarnings();

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A1D RID: 2589
		// (set) Token: 0x06000A1E RID: 2590
		public extern float orthographicSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A1F RID: 2591
		// (set) Token: 0x06000A20 RID: 2592
		public extern bool orthographic { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A21 RID: 2593
		// (set) Token: 0x06000A22 RID: 2594
		public extern OpaqueSortMode opaqueSortMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A23 RID: 2595
		// (set) Token: 0x06000A24 RID: 2596
		public extern TransparencySortMode transparencySortMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A25 RID: 2597
		// (set) Token: 0x06000A26 RID: 2598
		public extern float depth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A27 RID: 2599
		// (set) Token: 0x06000A28 RID: 2600
		public extern float aspect { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A29 RID: 2601
		// (set) Token: 0x06000A2A RID: 2602
		public extern int cullingMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A2B RID: 2603
		internal static extern int PreviewCullingLayer { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A2C RID: 2604
		// (set) Token: 0x06000A2D RID: 2605
		public extern int eventMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x0000E700 File Offset: 0x0000C900
		public Color backgroundColor
		{
			get
			{
				Color result;
				this.INTERNAL_get_backgroundColor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x06000A30 RID: 2608
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_backgroundColor(out Color value);

		// Token: 0x06000A31 RID: 2609
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0000E70C File Offset: 0x0000C90C
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0000E724 File Offset: 0x0000C924
		public Rect rect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rect(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_rect(ref value);
			}
		}

		// Token: 0x06000A34 RID: 2612
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x06000A35 RID: 2613
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rect(ref Rect value);

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0000E730 File Offset: 0x0000C930
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x0000E748 File Offset: 0x0000C948
		public Rect pixelRect
		{
			get
			{
				Rect result;
				this.INTERNAL_get_pixelRect(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_pixelRect(ref value);
			}
		}

		// Token: 0x06000A38 RID: 2616
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pixelRect(out Rect value);

		// Token: 0x06000A39 RID: 2617
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_pixelRect(ref Rect value);

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A3A RID: 2618
		// (set) Token: 0x06000A3B RID: 2619
		public extern RenderTexture targetTexture { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A3C RID: 2620
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersImpl(out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x06000A3D RID: 2621
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersMRTImpl(RenderBuffer[] color, out RenderBuffer depth);

		// Token: 0x06000A3E RID: 2622 RVA: 0x0000E754 File Offset: 0x0000C954
		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersImpl(out colorBuffer, out depthBuffer);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000E760 File Offset: 0x0000C960
		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersMRTImpl(colorBuffer, out depthBuffer);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A40 RID: 2624
		public extern int pixelWidth { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A41 RID: 2625
		public extern int pixelHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0000E76C File Offset: 0x0000C96C
		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_cameraToWorldMatrix(out result);
				return result;
			}
		}

		// Token: 0x06000A43 RID: 2627
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_cameraToWorldMatrix(out Matrix4x4 value);

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0000E784 File Offset: 0x0000C984
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x0000E79C File Offset: 0x0000C99C
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_worldToCameraMatrix(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_worldToCameraMatrix(ref value);
			}
		}

		// Token: 0x06000A46 RID: 2630
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldToCameraMatrix(out Matrix4x4 value);

		// Token: 0x06000A47 RID: 2631
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_worldToCameraMatrix(ref Matrix4x4 value);

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public void ResetWorldToCameraMatrix()
		{
			Camera.INTERNAL_CALL_ResetWorldToCameraMatrix(this);
		}

		// Token: 0x06000A49 RID: 2633
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetWorldToCameraMatrix(Camera self);

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		public Matrix4x4 projectionMatrix
		{
			get
			{
				Matrix4x4 result;
				this.INTERNAL_get_projectionMatrix(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_projectionMatrix(ref value);
			}
		}

		// Token: 0x06000A4C RID: 2636
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_projectionMatrix(out Matrix4x4 value);

		// Token: 0x06000A4D RID: 2637
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_projectionMatrix(ref Matrix4x4 value);

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		public void ResetProjectionMatrix()
		{
			Camera.INTERNAL_CALL_ResetProjectionMatrix(this);
		}

		// Token: 0x06000A4F RID: 2639
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetProjectionMatrix(Camera self);

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public void ResetAspect()
		{
			Camera.INTERNAL_CALL_ResetAspect(this);
		}

		// Token: 0x06000A51 RID: 2641
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetAspect(Camera self);

		// Token: 0x06000A52 RID: 2642 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		public void ResetFieldOfView()
		{
			Camera.INTERNAL_CALL_ResetFieldOfView(this);
		}

		// Token: 0x06000A53 RID: 2643
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetFieldOfView(Camera self);

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
		}

		// Token: 0x06000A55 RID: 2645
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A56 RID: 2646
		// (set) Token: 0x06000A57 RID: 2647
		public extern CameraClearFlags clearFlags { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A58 RID: 2648
		public extern bool stereoEnabled { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A59 RID: 2649
		// (set) Token: 0x06000A5A RID: 2650
		public extern float stereoSeparation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A5B RID: 2651
		// (set) Token: 0x06000A5C RID: 2652
		public extern float stereoConvergence { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A5D RID: 2653
		// (set) Token: 0x06000A5E RID: 2654
		public extern CameraType cameraType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A5F RID: 2655
		// (set) Token: 0x06000A60 RID: 2656
		public extern bool stereoMirrorMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000E804 File Offset: 0x0000CA04
		public void SetStereoViewMatrices(Matrix4x4 leftMatrix, Matrix4x4 rightMatrix)
		{
			Camera.INTERNAL_CALL_SetStereoViewMatrices(this, ref leftMatrix, ref rightMatrix);
		}

		// Token: 0x06000A62 RID: 2658
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetStereoViewMatrices(Camera self, ref Matrix4x4 leftMatrix, ref Matrix4x4 rightMatrix);

		// Token: 0x06000A63 RID: 2659
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetStereoViewMatrices();

		// Token: 0x06000A64 RID: 2660 RVA: 0x0000E810 File Offset: 0x0000CA10
		public void SetStereoProjectionMatrices(Matrix4x4 leftMatrix, Matrix4x4 rightMatrix)
		{
			Camera.INTERNAL_CALL_SetStereoProjectionMatrices(this, ref leftMatrix, ref rightMatrix);
		}

		// Token: 0x06000A65 RID: 2661
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetStereoProjectionMatrices(Camera self, ref Matrix4x4 leftMatrix, ref Matrix4x4 rightMatrix);

		// Token: 0x06000A66 RID: 2662
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetStereoProjectionMatrices();

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A67 RID: 2663
		// (set) Token: 0x06000A68 RID: 2664
		public extern int targetDisplay { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A69 RID: 2665 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			Vector3 result;
			Camera.INTERNAL_CALL_WorldToScreenPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A6A RID: 2666
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_WorldToScreenPoint(Camera self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000E834 File Offset: 0x0000CA34
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			Vector3 result;
			Camera.INTERNAL_CALL_WorldToViewportPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A6C RID: 2668
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_WorldToViewportPoint(Camera self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000E84C File Offset: 0x0000CA4C
		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			Vector3 result;
			Camera.INTERNAL_CALL_ViewportToWorldPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A6E RID: 2670
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ViewportToWorldPoint(Camera self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000A6F RID: 2671 RVA: 0x0000E864 File Offset: 0x0000CA64
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			Vector3 result;
			Camera.INTERNAL_CALL_ScreenToWorldPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A70 RID: 2672
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ScreenToWorldPoint(Camera self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000E87C File Offset: 0x0000CA7C
		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			Vector3 result;
			Camera.INTERNAL_CALL_ScreenToViewportPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A72 RID: 2674
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ScreenToViewportPoint(Camera self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000A73 RID: 2675 RVA: 0x0000E894 File Offset: 0x0000CA94
		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			Vector3 result;
			Camera.INTERNAL_CALL_ViewportToScreenPoint(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A74 RID: 2676
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ViewportToScreenPoint(Camera self, ref Vector3 position, out Vector3 value);

		// Token: 0x06000A75 RID: 2677 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		public Ray ViewportPointToRay(Vector3 position)
		{
			Ray result;
			Camera.INTERNAL_CALL_ViewportPointToRay(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A76 RID: 2678
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ViewportPointToRay(Camera self, ref Vector3 position, out Ray value);

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public Ray ScreenPointToRay(Vector3 position)
		{
			Ray result;
			Camera.INTERNAL_CALL_ScreenPointToRay(this, ref position, out result);
			return result;
		}

		// Token: 0x06000A78 RID: 2680
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ScreenPointToRay(Camera self, ref Vector3 position, out Ray value);

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A79 RID: 2681
		public static extern Camera main { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A7A RID: 2682
		public static extern Camera current { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A7B RID: 2683
		public static extern Camera[] allCameras { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000A7C RID: 2684
		public static extern int allCamerasCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000A7D RID: 2685
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAllCameras(Camera[] cameras);

		// Token: 0x06000A7E RID: 2686 RVA: 0x0000E8DC File Offset: 0x0000CADC
		[RequiredByNativeCode]
		private static void FireOnPreCull(Camera cam)
		{
			if (Camera.onPreCull != null)
			{
				Camera.onPreCull(cam);
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		[RequiredByNativeCode]
		private static void FireOnPreRender(Camera cam)
		{
			if (Camera.onPreRender != null)
			{
				Camera.onPreRender(cam);
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0000E90C File Offset: 0x0000CB0C
		[RequiredByNativeCode]
		private static void FireOnPostRender(Camera cam)
		{
			if (Camera.onPostRender != null)
			{
				Camera.onPostRender(cam);
			}
		}

		// Token: 0x06000A81 RID: 2689
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Render();

		// Token: 0x06000A82 RID: 2690
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		// Token: 0x06000A83 RID: 2691
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		// Token: 0x06000A84 RID: 2692 RVA: 0x0000E924 File Offset: 0x0000CB24
		public void ResetReplacementShader()
		{
			Camera.INTERNAL_CALL_ResetReplacementShader(this);
		}

		// Token: 0x06000A85 RID: 2693
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetReplacementShader(Camera self);

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A86 RID: 2694
		// (set) Token: 0x06000A87 RID: 2695
		public extern bool useOcclusionCulling { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A88 RID: 2696
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderDontRestore();

		// Token: 0x06000A89 RID: 2697
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetupCurrent(Camera cur);

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000E92C File Offset: 0x0000CB2C
		[ExcludeFromDocs]
		public bool RenderToCubemap(Cubemap cubemap)
		{
			int faceMask = 63;
			return this.RenderToCubemap(cubemap, faceMask);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000E944 File Offset: 0x0000CB44
		public bool RenderToCubemap(Cubemap cubemap, [DefaultValue("63")] int faceMask)
		{
			return this.Internal_RenderToCubemapTexture(cubemap, faceMask);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000E950 File Offset: 0x0000CB50
		[ExcludeFromDocs]
		public bool RenderToCubemap(RenderTexture cubemap)
		{
			int faceMask = 63;
			return this.RenderToCubemap(cubemap, faceMask);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000E968 File Offset: 0x0000CB68
		public bool RenderToCubemap(RenderTexture cubemap, [DefaultValue("63")] int faceMask)
		{
			return this.Internal_RenderToCubemapRT(cubemap, faceMask);
		}

		// Token: 0x06000A8E RID: 2702
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_RenderToCubemapRT(RenderTexture cubemap, int faceMask);

		// Token: 0x06000A8F RID: 2703
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_RenderToCubemapTexture(Cubemap cubemap, int faceMask);

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A90 RID: 2704
		// (set) Token: 0x06000A91 RID: 2705
		public extern float[] layerCullDistances { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A92 RID: 2706
		// (set) Token: 0x06000A93 RID: 2707
		public extern bool layerCullSpherical { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A94 RID: 2708
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyFrom(Camera other);

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A95 RID: 2709
		// (set) Token: 0x06000A96 RID: 2710
		public extern DepthTextureMode depthTextureMode { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A97 RID: 2711
		// (set) Token: 0x06000A98 RID: 2712
		public extern bool clearStencilAfterLightingPass { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000A99 RID: 2713
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsFiltered(GameObject go);

		// Token: 0x06000A9A RID: 2714
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddCommandBuffer(CameraEvent evt, CommandBuffer buffer);

		// Token: 0x06000A9B RID: 2715
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffer(CameraEvent evt, CommandBuffer buffer);

		// Token: 0x06000A9C RID: 2716
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffers(CameraEvent evt);

		// Token: 0x06000A9D RID: 2717
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveAllCommandBuffers();

		// Token: 0x06000A9E RID: 2718
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern CommandBuffer[] GetCommandBuffers(CameraEvent evt);

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A9F RID: 2719
		public extern int commandBufferCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0000E974 File Offset: 0x0000CB74
		internal GameObject RaycastTry(Ray ray, float distance, int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Camera.INTERNAL_CALL_RaycastTry(this, ref ray, distance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000E984 File Offset: 0x0000CB84
		[ExcludeFromDocs]
		internal GameObject RaycastTry(Ray ray, float distance, int layerMask)
		{
			QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			return Camera.INTERNAL_CALL_RaycastTry(this, ref ray, distance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000AA2 RID: 2722
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject INTERNAL_CALL_RaycastTry(Camera self, ref Ray ray, float distance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		internal GameObject RaycastTry2D(Ray ray, float distance, int layerMask)
		{
			return Camera.INTERNAL_CALL_RaycastTry2D(this, ref ray, distance, layerMask);
		}

		// Token: 0x06000AA4 RID: 2724
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject INTERNAL_CALL_RaycastTry2D(Camera self, ref Ray ray, float distance, int layerMask);

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			Matrix4x4 result;
			Camera.INTERNAL_CALL_CalculateObliqueMatrix(this, ref clipPlane, out result);
			return result;
		}

		// Token: 0x06000AA6 RID: 2726
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_CalculateObliqueMatrix(Camera self, ref Vector4 clipPlane, out Matrix4x4 value);

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		internal void OnlyUsedForTesting1()
		{
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		internal void OnlyUsedForTesting2()
		{
		}

		// Token: 0x04000219 RID: 537
		public static Camera.CameraCallback onPreCull;

		// Token: 0x0400021A RID: 538
		public static Camera.CameraCallback onPreRender;

		// Token: 0x0400021B RID: 539
		public static Camera.CameraCallback onPostRender;

		// Token: 0x02000342 RID: 834
		// (Invoke) Token: 0x06002862 RID: 10338
		public delegate void CameraCallback(Camera cam);
	}
}
