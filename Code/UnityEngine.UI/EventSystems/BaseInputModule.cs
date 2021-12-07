using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000025 RID: 37
	[RequireComponent(typeof(EventSystem))]
	public abstract class BaseInputModule : UIBehaviour
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003704 File Offset: 0x00001904
		protected EventSystem eventSystem
		{
			get
			{
				return this.m_EventSystem;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000370C File Offset: 0x0000190C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_EventSystem = base.GetComponent<EventSystem>();
			this.m_EventSystem.UpdateModules();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000372C File Offset: 0x0000192C
		protected override void OnDisable()
		{
			this.m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		// Token: 0x060000C7 RID: 199
		public abstract void Process();

		// Token: 0x060000C8 RID: 200 RVA: 0x00003740 File Offset: 0x00001940
		protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
		{
			for (int i = 0; i < candidates.Count; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(RaycastResult);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003794 File Offset: 0x00001994
		protected static MoveDirection DetermineMoveDirection(float x, float y)
		{
			return BaseInputModule.DetermineMoveDirection(x, y, 0.6f);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000037A4 File Offset: 0x000019A4
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			Vector2 vector = new Vector2(x, y);
			if (vector.sqrMagnitude < deadZone * deadZone)
			{
				return MoveDirection.None;
			}
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x > 0f)
				{
					return MoveDirection.Right;
				}
				return MoveDirection.Left;
			}
			else
			{
				if (y > 0f)
				{
					return MoveDirection.Up;
				}
				return MoveDirection.Down;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000037FC File Offset: 0x000019FC
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform transform = g1.transform;
			while (transform != null)
			{
				Transform transform2 = g2.transform;
				while (transform2 != null)
				{
					if (transform == transform2)
					{
						return transform.gameObject;
					}
					transform2 = transform2.parent;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003878 File Offset: 0x00001A78
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (newEnterTarget == null || currentPointerData.pointerEnter == null)
			{
				for (int i = 0; i < currentPointerData.hovered.Count; i++)
				{
					ExecuteEvents.Execute<IPointerExitHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerExitHandler);
				}
				currentPointerData.hovered.Clear();
				if (newEnterTarget == null)
				{
					currentPointerData.pointerEnter = newEnterTarget;
					return;
				}
			}
			if (currentPointerData.pointerEnter == newEnterTarget && newEnterTarget)
			{
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			if (currentPointerData.pointerEnter != null)
			{
				Transform transform = currentPointerData.pointerEnter.transform;
				while (transform != null)
				{
					if (gameObject != null && gameObject.transform == transform)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerExitHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					currentPointerData.hovered.Remove(transform.gameObject);
					transform = transform.parent;
				}
			}
			currentPointerData.pointerEnter = newEnterTarget;
			if (newEnterTarget != null)
			{
				Transform transform2 = newEnterTarget.transform;
				while (transform2 != null && transform2.gameObject != gameObject)
				{
					ExecuteEvents.Execute<IPointerEnterHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					currentPointerData.hovered.Add(transform2.gameObject);
					transform2 = transform2.parent;
				}
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003A00 File Offset: 0x00001C00
		protected virtual AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this.m_AxisEventData == null)
			{
				this.m_AxisEventData = new AxisEventData(this.eventSystem);
			}
			this.m_AxisEventData.Reset();
			this.m_AxisEventData.moveVector = new Vector2(x, y);
			this.m_AxisEventData.moveDir = BaseInputModule.DetermineMoveDirection(x, y, moveDeadZone);
			return this.m_AxisEventData;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003A60 File Offset: 0x00001C60
		protected virtual BaseEventData GetBaseEventData()
		{
			if (this.m_BaseEventData == null)
			{
				this.m_BaseEventData = new BaseEventData(this.eventSystem);
			}
			this.m_BaseEventData.Reset();
			return this.m_BaseEventData;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003A90 File Offset: 0x00001C90
		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003A94 File Offset: 0x00001C94
		public virtual bool ShouldActivateModule()
		{
			return base.enabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public virtual void DeactivateModule()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public virtual void ActivateModule()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public virtual void UpdateModule()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003ABC File Offset: 0x00001CBC
		public virtual bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x0400006B RID: 107
		[NonSerialized]
		protected List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

		// Token: 0x0400006C RID: 108
		private AxisEventData m_AxisEventData;

		// Token: 0x0400006D RID: 109
		private EventSystem m_EventSystem;

		// Token: 0x0400006E RID: 110
		private BaseEventData m_BaseEventData;
	}
}
