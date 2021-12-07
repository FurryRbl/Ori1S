using System;
using System.Collections.Generic;
using Sein.World;
using UnityEngine;

// Token: 0x02000651 RID: 1617
public class SpiritLightAffectorBasedTrigger : MonoBehaviour
{
	// Token: 0x0600278E RID: 10126 RVA: 0x000AC214 File Offset: 0x000AA414
	private void FixedUpdate()
	{
		SpiritLightAffectorBasedTrigger.State state = this.CalculateCurrentState();
		if (this.m_currentState != state)
		{
			switch (state)
			{
			case SpiritLightAffectorBasedTrigger.State.NoPointsCovered:
				if (this.NoPointsCoveredInLightAction != null)
				{
					this.NoPointsCoveredInLightAction.Perform(null);
				}
				break;
			case SpiritLightAffectorBasedTrigger.State.SomePointsCovered:
				if (this.SomePointsCoveredInLightAction != null)
				{
					this.SomePointsCoveredInLightAction.Perform(null);
				}
				if ((this.FirstPointCoveredInLightAction && this.m_currentState == SpiritLightAffectorBasedTrigger.State.NoPointsCovered && !this.InvertedTrigger) || (this.m_currentState == SpiritLightAffectorBasedTrigger.State.AllPointsCovered && this.InvertedTrigger))
				{
					this.FirstPointCoveredInLightAction.Perform(null);
				}
				break;
			case SpiritLightAffectorBasedTrigger.State.AllPointsCovered:
				if (this.AllPointsCoveredInLightAction != null)
				{
					this.AllPointsCoveredInLightAction.Perform(null);
				}
				break;
			}
		}
		this.m_currentState = state;
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x000AC308 File Offset: 0x000AA508
	private SpiritLightAffectorBasedTrigger.State CalculateCurrentState()
	{
		if (this.AssumeAllPointsCoveredIfDarknessIsLifted && Events.DarknessLifted)
		{
			return SpiritLightAffectorBasedTrigger.State.AllPointsCovered;
		}
		int num = 0;
		List<Vector3> worldSpaceWorldSpaceInteractionPoints = this.TriggerPoints.WorldSpaceWorldSpaceInteractionPoints;
		for (int i = 0; i < SpiritLightRadialVisualAffector.All.Count; i++)
		{
			SpiritLightRadialVisualAffector spiritLightRadialVisualAffector = SpiritLightRadialVisualAffector.All[i];
			if (spiritLightRadialVisualAffector.LightType == this.LightType)
			{
				for (int j = 0; j < worldSpaceWorldSpaceInteractionPoints.Count; j++)
				{
					Vector3 a = worldSpaceWorldSpaceInteractionPoints[j];
					bool flag = Vector3.Distance(a, spiritLightRadialVisualAffector.Position) < spiritLightRadialVisualAffector.LightRadiusInThisFrame * this.RadiusModulation;
					if (this.InvertedTrigger ^ flag)
					{
						num++;
					}
				}
			}
		}
		if (num == 0)
		{
			return SpiritLightAffectorBasedTrigger.State.NoPointsCovered;
		}
		if (num >= worldSpaceWorldSpaceInteractionPoints.Count)
		{
			return SpiritLightAffectorBasedTrigger.State.AllPointsCovered;
		}
		return SpiritLightAffectorBasedTrigger.State.SomePointsCovered;
	}

	// Token: 0x04002222 RID: 8738
	public float RadiusModulation = 1f;

	// Token: 0x04002223 RID: 8739
	public SpiritLightType LightType = SpiritLightType.LightVessel;

	// Token: 0x04002224 RID: 8740
	public LocalSpacePointSet TriggerPoints;

	// Token: 0x04002225 RID: 8741
	public ActionMethod AllPointsCoveredInLightAction;

	// Token: 0x04002226 RID: 8742
	public ActionMethod NoPointsCoveredInLightAction;

	// Token: 0x04002227 RID: 8743
	public ActionMethod SomePointsCoveredInLightAction;

	// Token: 0x04002228 RID: 8744
	public ActionMethod FirstPointCoveredInLightAction;

	// Token: 0x04002229 RID: 8745
	public bool AssumeAllPointsCoveredIfDarknessIsLifted = true;

	// Token: 0x0400222A RID: 8746
	public bool InvertedTrigger;

	// Token: 0x0400222B RID: 8747
	private SpiritLightAffectorBasedTrigger.State m_currentState;

	// Token: 0x02000652 RID: 1618
	private enum State
	{
		// Token: 0x0400222D RID: 8749
		Unknown,
		// Token: 0x0400222E RID: 8750
		NoPointsCovered,
		// Token: 0x0400222F RID: 8751
		SomePointsCovered,
		// Token: 0x04002230 RID: 8752
		AllPointsCovered
	}
}
