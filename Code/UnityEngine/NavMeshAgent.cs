using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200016E RID: 366
	public sealed class NavMeshAgent : Behaviour
	{
		// Token: 0x06001742 RID: 5954 RVA: 0x000180A0 File Offset: 0x000162A0
		public bool SetDestination(Vector3 target)
		{
			return NavMeshAgent.INTERNAL_CALL_SetDestination(this, ref target);
		}

		// Token: 0x06001743 RID: 5955
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SetDestination(NavMeshAgent self, ref Vector3 target);

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x000180AC File Offset: 0x000162AC
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x000180C4 File Offset: 0x000162C4
		public Vector3 destination
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_destination(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_destination(ref value);
			}
		}

		// Token: 0x06001746 RID: 5958
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_destination(out Vector3 value);

		// Token: 0x06001747 RID: 5959
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_destination(ref Vector3 value);

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001748 RID: 5960
		// (set) Token: 0x06001749 RID: 5961
		public extern float stoppingDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x000180D0 File Offset: 0x000162D0
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x000180E8 File Offset: 0x000162E8
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_velocity(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x0600174C RID: 5964
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x0600174D RID: 5965
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x000180F4 File Offset: 0x000162F4
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x0001810C File Offset: 0x0001630C
		public Vector3 nextPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_nextPosition(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_nextPosition(ref value);
			}
		}

		// Token: 0x06001750 RID: 5968
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_nextPosition(out Vector3 value);

		// Token: 0x06001751 RID: 5969
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_nextPosition(ref Vector3 value);

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001752 RID: 5970 RVA: 0x00018118 File Offset: 0x00016318
		public Vector3 steeringTarget
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_steeringTarget(out result);
				return result;
			}
		}

		// Token: 0x06001753 RID: 5971
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_steeringTarget(out Vector3 value);

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001754 RID: 5972 RVA: 0x00018130 File Offset: 0x00016330
		public Vector3 desiredVelocity
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_desiredVelocity(out result);
				return result;
			}
		}

		// Token: 0x06001755 RID: 5973
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_desiredVelocity(out Vector3 value);

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001756 RID: 5974
		public extern float remainingDistance { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001757 RID: 5975
		// (set) Token: 0x06001758 RID: 5976
		public extern float baseOffset { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001759 RID: 5977
		public extern bool isOnOffMeshLink { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600175A RID: 5978
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ActivateCurrentOffMeshLink(bool activated);

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x00018148 File Offset: 0x00016348
		public OffMeshLinkData currentOffMeshLinkData
		{
			get
			{
				return this.GetCurrentOffMeshLinkDataInternal();
			}
		}

		// Token: 0x0600175C RID: 5980
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern OffMeshLinkData GetCurrentOffMeshLinkDataInternal();

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600175D RID: 5981 RVA: 0x00018150 File Offset: 0x00016350
		public OffMeshLinkData nextOffMeshLinkData
		{
			get
			{
				return this.GetNextOffMeshLinkDataInternal();
			}
		}

		// Token: 0x0600175E RID: 5982
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern OffMeshLinkData GetNextOffMeshLinkDataInternal();

		// Token: 0x0600175F RID: 5983
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CompleteOffMeshLink();

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001760 RID: 5984
		// (set) Token: 0x06001761 RID: 5985
		public extern bool autoTraverseOffMeshLink { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001762 RID: 5986
		// (set) Token: 0x06001763 RID: 5987
		public extern bool autoBraking { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001764 RID: 5988
		// (set) Token: 0x06001765 RID: 5989
		public extern bool autoRepath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001766 RID: 5990
		public extern bool hasPath { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001767 RID: 5991
		public extern bool pathPending { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001768 RID: 5992
		public extern bool isPathStale { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001769 RID: 5993
		public extern NavMeshPathStatus pathStatus { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00018158 File Offset: 0x00016358
		public Vector3 pathEndPosition
		{
			get
			{
				Vector3 result;
				this.INTERNAL_get_pathEndPosition(out result);
				return result;
			}
		}

		// Token: 0x0600176B RID: 5995
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pathEndPosition(out Vector3 value);

		// Token: 0x0600176C RID: 5996 RVA: 0x00018170 File Offset: 0x00016370
		public bool Warp(Vector3 newPosition)
		{
			return NavMeshAgent.INTERNAL_CALL_Warp(this, ref newPosition);
		}

		// Token: 0x0600176D RID: 5997
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Warp(NavMeshAgent self, ref Vector3 newPosition);

		// Token: 0x0600176E RID: 5998 RVA: 0x0001817C File Offset: 0x0001637C
		public void Move(Vector3 offset)
		{
			NavMeshAgent.INTERNAL_CALL_Move(this, ref offset);
		}

		// Token: 0x0600176F RID: 5999
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Move(NavMeshAgent self, ref Vector3 offset);

		// Token: 0x06001770 RID: 6000 RVA: 0x00018188 File Offset: 0x00016388
		public void Stop()
		{
			this.StopInternal();
		}

		// Token: 0x06001771 RID: 6001
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void StopInternal();

		// Token: 0x06001772 RID: 6002 RVA: 0x00018190 File Offset: 0x00016390
		[Obsolete("Use Stop() instead")]
		public void Stop(bool stopUpdates)
		{
			this.StopInternal();
		}

		// Token: 0x06001773 RID: 6003
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Resume();

		// Token: 0x06001774 RID: 6004
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetPath();

		// Token: 0x06001775 RID: 6005
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPath(NavMeshPath path);

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00018198 File Offset: 0x00016398
		// (set) Token: 0x06001777 RID: 6007 RVA: 0x000181B4 File Offset: 0x000163B4
		public NavMeshPath path
		{
			get
			{
				NavMeshPath navMeshPath = new NavMeshPath();
				this.CopyPathTo(navMeshPath);
				return navMeshPath;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException();
				}
				this.SetPath(value);
			}
		}

		// Token: 0x06001778 RID: 6008
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void CopyPathTo(NavMeshPath path);

		// Token: 0x06001779 RID: 6009
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool FindClosestEdge(out NavMeshHit hit);

		// Token: 0x0600177A RID: 6010 RVA: 0x000181CC File Offset: 0x000163CC
		public bool Raycast(Vector3 targetPosition, out NavMeshHit hit)
		{
			return NavMeshAgent.INTERNAL_CALL_Raycast(this, ref targetPosition, out hit);
		}

		// Token: 0x0600177B RID: 6011
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Raycast(NavMeshAgent self, ref Vector3 targetPosition, out NavMeshHit hit);

		// Token: 0x0600177C RID: 6012 RVA: 0x000181D8 File Offset: 0x000163D8
		public bool CalculatePath(Vector3 targetPosition, NavMeshPath path)
		{
			path.ClearCorners();
			return this.CalculatePathInternal(targetPosition, path);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000181E8 File Offset: 0x000163E8
		private bool CalculatePathInternal(Vector3 targetPosition, NavMeshPath path)
		{
			return NavMeshAgent.INTERNAL_CALL_CalculatePathInternal(this, ref targetPosition, path);
		}

		// Token: 0x0600177E RID: 6014
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CalculatePathInternal(NavMeshAgent self, ref Vector3 targetPosition, NavMeshPath path);

		// Token: 0x0600177F RID: 6015
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SamplePathPosition(int areaMask, float maxDistance, out NavMeshHit hit);

		// Token: 0x06001780 RID: 6016
		[Obsolete("Use SetAreaCost instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerCost(int layer, float cost);

		// Token: 0x06001781 RID: 6017
		[WrapperlessIcall]
		[Obsolete("Use GetAreaCost instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerCost(int layer);

		// Token: 0x06001782 RID: 6018
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAreaCost(int areaIndex, float areaCost);

		// Token: 0x06001783 RID: 6019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetAreaCost(int areaIndex);

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001784 RID: 6020
		// (set) Token: 0x06001785 RID: 6021
		[Obsolete("Use areaMask instead.")]
		public extern int walkableMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001786 RID: 6022
		// (set) Token: 0x06001787 RID: 6023
		public extern int areaMask { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001788 RID: 6024
		// (set) Token: 0x06001789 RID: 6025
		public extern float speed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600178A RID: 6026
		// (set) Token: 0x0600178B RID: 6027
		public extern float angularSpeed { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600178C RID: 6028
		// (set) Token: 0x0600178D RID: 6029
		public extern float acceleration { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600178E RID: 6030
		// (set) Token: 0x0600178F RID: 6031
		public extern bool updatePosition { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001790 RID: 6032
		// (set) Token: 0x06001791 RID: 6033
		public extern bool updateRotation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001792 RID: 6034
		// (set) Token: 0x06001793 RID: 6035
		public extern float radius { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001794 RID: 6036
		// (set) Token: 0x06001795 RID: 6037
		public extern float height { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001796 RID: 6038
		// (set) Token: 0x06001797 RID: 6039
		public extern ObstacleAvoidanceType obstacleAvoidanceType { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001798 RID: 6040
		// (set) Token: 0x06001799 RID: 6041
		public extern int avoidancePriority { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600179A RID: 6042
		public extern bool isOnNavMesh { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }
	}
}
