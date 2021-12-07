using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020006D2 RID: 1746
public class RigidbodyInteractionController : MonoBehaviour
{
	// Token: 0x060029E6 RID: 10726 RVA: 0x000B4488 File Offset: 0x000B2688
	private void OnCollisionEnter(Collision collision)
	{
		this.m_lastInteraction = new RigidbodyInteractionController.InteractionInformation(collision);
		this.HandleCollision(this.m_lastInteraction, this.CollisionEnterInstanciation, this.CollisionEnterSound);
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x000B44B9 File Offset: 0x000B26B9
	private void OnCollisionStay(Collision collision)
	{
		this.m_lastInteraction = new RigidbodyInteractionController.InteractionInformation(collision);
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x000B44C7 File Offset: 0x000B26C7
	private void OnCollisionExit(Collision collision)
	{
		this.m_lastInteraction.RelativeVelocity = collision.relativeVelocity;
		this.HandleCollision(this.m_lastInteraction, this.CollisionExitInstanciation, this.CollisionExitSound);
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x000B44F4 File Offset: 0x000B26F4
	private void OnApplyForce(Vector3 force)
	{
		RigidbodyInteractionController.InteractionInformation interactionInformation = new RigidbodyInteractionController.InteractionInformation(base.transform.position, force.normalized, force, false);
		this.HandleCollision(interactionInformation, this.CollisionEnterInstanciation, this.CollisionEnterSound);
	}

	// Token: 0x060029EA RID: 10730 RVA: 0x000B4530 File Offset: 0x000B2730
	private void HandleCollision(RigidbodyInteractionController.InteractionInformation interactionInformation, RigidbodyInteractionController.InstanciationInteractionSettings[] collisionInstanciationSettings, RigidbodyInteractionController.SoundInteractionSettings[] collisionSoundSettings)
	{
		float time = Time.time;
		if (time - this.m_lastCollisionTime < 0.3f)
		{
			return;
		}
		bool flag = false;
		float magnitude = interactionInformation.RelativeVelocity.magnitude;
		foreach (RigidbodyInteractionController.InstanciationInteractionSettings instanciationInteractionSettings in collisionInstanciationSettings)
		{
			if (instanciationInteractionSettings.MinCollisionMagnitude < magnitude && instanciationInteractionSettings.CollisionPrefab)
			{
				GameObject gameObject = null;
				RigidbodyInteractionController.InstanciationPositionModes instanciationPosition = instanciationInteractionSettings.InstanciationPosition;
				if (instanciationPosition != RigidbodyInteractionController.InstanciationPositionModes.FirstCollisionContactPoint)
				{
					if (instanciationPosition != RigidbodyInteractionController.InstanciationPositionModes.AllInteractionPoints)
					{
						if (instanciationPosition == RigidbodyInteractionController.InstanciationPositionModes.ClosestInteractionPoint)
						{
							if (instanciationInteractionSettings.InteractionPoints != null)
							{
								List<Vector3> worldSpaceWorldSpaceInteractionPoints = instanciationInteractionSettings.InteractionPoints.WorldSpaceWorldSpaceInteractionPoints;
								if (worldSpaceWorldSpaceInteractionPoints.Count != 0)
								{
									Vector3 point = interactionInformation.Point;
									Vector3 vector = worldSpaceWorldSpaceInteractionPoints[0];
									float num = (vector - point).magnitude;
									for (int j = 1; j < worldSpaceWorldSpaceInteractionPoints.Count; j++)
									{
										Vector3 vector2 = worldSpaceWorldSpaceInteractionPoints[j];
										float magnitude2 = (vector2 - point).magnitude;
										if (magnitude2 < num)
										{
											vector = vector2;
											num = magnitude2;
										}
									}
									gameObject = (GameObject)InstantiateUtility.Instantiate(instanciationInteractionSettings.CollisionPrefab, vector, Quaternion.identity);
								}
							}
						}
					}
					else if (instanciationInteractionSettings.InteractionPoints != null)
					{
						List<Vector3> worldSpaceWorldSpaceInteractionPoints2 = instanciationInteractionSettings.InteractionPoints.WorldSpaceWorldSpaceInteractionPoints;
						for (int k = 0; k < worldSpaceWorldSpaceInteractionPoints2.Count; k++)
						{
							Vector3 position = worldSpaceWorldSpaceInteractionPoints2[k];
							gameObject = (GameObject)InstantiateUtility.Instantiate(instanciationInteractionSettings.CollisionPrefab, position, Quaternion.identity);
						}
					}
				}
				else
				{
					gameObject = (GameObject)InstantiateUtility.Instantiate(instanciationInteractionSettings.CollisionPrefab, interactionInformation.Point, Quaternion.Euler(0f, 0f, MoonMath.Angle.AngleFromVector(interactionInformation.Normal)));
				}
				float remappedModulationValue = this.GetRemappedModulationValue(instanciationInteractionSettings.ParticleEmissionRemapping, magnitude);
				if (!Mathf.Approximately(remappedModulationValue, 1f))
				{
					gameObject.GetComponentsInChildren<ParticleSystem>(RigidbodyInteractionController.s_particleSystemList);
					instanciationInteractionSettings.CollisionPrefab.GetComponentsInChildren<ParticleSystem>(RigidbodyInteractionController.s_particleSystemListB);
					int num2 = Mathf.Min(RigidbodyInteractionController.s_particleSystemList.Count, RigidbodyInteractionController.s_particleSystemListB.Count);
					for (int l = 0; l < num2; l++)
					{
						RigidbodyInteractionController.s_particleSystemList[l].emissionRate = RigidbodyInteractionController.s_particleSystemListB[l].emissionRate * remappedModulationValue;
					}
					RigidbodyInteractionController.s_particleSystemList.Clear();
					RigidbodyInteractionController.s_particleSystemListB.Clear();
					flag = true;
				}
			}
		}
		bool flag2 = false;
		foreach (RigidbodyInteractionController.SoundInteractionSettings soundInteractionSettings in collisionSoundSettings)
		{
			if (soundInteractionSettings.MinCollisionMagnitude < magnitude && soundInteractionSettings.SoundProvider)
			{
				SoundDescriptor sound = soundInteractionSettings.SoundProvider.GetSound(null);
				float remappedModulationValue2 = this.GetRemappedModulationValue(soundInteractionSettings.SoundVolumeRemapping, magnitude);
				sound.Volume *= remappedModulationValue2;
				if (sound != null)
				{
					Sound.Play(sound, interactionInformation.Point, null);
					flag2 = true;
				}
			}
		}
		if (flag || flag2)
		{
			this.m_lastCollisionTime = Time.time;
		}
	}

	// Token: 0x060029EB RID: 10731 RVA: 0x000B4880 File Offset: 0x000B2A80
	private float GetRemappedModulationValue(Rect rangeRemapping, float inputParameter)
	{
		float t = Mathf.InverseLerp(rangeRemapping.width, rangeRemapping.height, inputParameter);
		return Mathf.Lerp(rangeRemapping.x, rangeRemapping.y, t);
	}

	// Token: 0x04002554 RID: 9556
	private const float MIN_TIME_BETWEEN_INTERACTIONS = 0.3f;

	// Token: 0x04002555 RID: 9557
	public RigidbodyInteractionController.InstanciationInteractionSettings[] CollisionEnterInstanciation;

	// Token: 0x04002556 RID: 9558
	public RigidbodyInteractionController.InstanciationInteractionSettings[] CollisionExitInstanciation;

	// Token: 0x04002557 RID: 9559
	public RigidbodyInteractionController.SoundInteractionSettings[] CollisionEnterSound;

	// Token: 0x04002558 RID: 9560
	public RigidbodyInteractionController.SoundInteractionSettings[] CollisionExitSound;

	// Token: 0x04002559 RID: 9561
	private static List<ParticleSystem> s_particleSystemList = new List<ParticleSystem>();

	// Token: 0x0400255A RID: 9562
	private static List<ParticleSystem> s_particleSystemListB = new List<ParticleSystem>();

	// Token: 0x0400255B RID: 9563
	private float m_lastCollisionTime = -10000f;

	// Token: 0x0400255C RID: 9564
	private RigidbodyInteractionController.InteractionInformation m_lastInteraction;

	// Token: 0x020006D3 RID: 1747
	public enum InstanciationPositionModes
	{
		// Token: 0x0400255E RID: 9566
		FirstCollisionContactPoint = 1,
		// Token: 0x0400255F RID: 9567
		AllInteractionPoints = 20,
		// Token: 0x04002560 RID: 9568
		ClosestInteractionPoint = 30
	}

	// Token: 0x020006D4 RID: 1748
	[Serializable]
	public class InstanciationInteractionSettings
	{
		// Token: 0x04002561 RID: 9569
		public float MinCollisionMagnitude = 1f;

		// Token: 0x04002562 RID: 9570
		public GameObject CollisionPrefab;

		// Token: 0x04002563 RID: 9571
		public RigidbodyInteractionController.InstanciationPositionModes InstanciationPosition = RigidbodyInteractionController.InstanciationPositionModes.FirstCollisionContactPoint;

		// Token: 0x04002564 RID: 9572
		public LocalSpacePointSet InteractionPoints;

		// Token: 0x04002565 RID: 9573
		public Rect ParticleEmissionRemapping = new Rect(1f, 1f, 1f, 13f);
	}

	// Token: 0x020006D5 RID: 1749
	[Serializable]
	public class SoundInteractionSettings
	{
		// Token: 0x04002566 RID: 9574
		public float MinCollisionMagnitude = 1f;

		// Token: 0x04002567 RID: 9575
		public SoundProvider SoundProvider;

		// Token: 0x04002568 RID: 9576
		public Rect SoundVolumeRemapping = new Rect(1f, 1f, 1f, 13f);
	}

	// Token: 0x020006D6 RID: 1750
	private struct InteractionInformation
	{
		// Token: 0x060029EE RID: 10734 RVA: 0x000B4930 File Offset: 0x000B2B30
		public InteractionInformation(Collision collision)
		{
			this.IsPlayer = (collision.transform && collision.transform.CompareTag("Player"));
			if (collision.contacts.Length == 0)
			{
				this.Point = new Vector3(0f, 0f, 0f);
				this.Normal = new Vector3(0f, 0f, 0f);
				this.RelativeVelocity = new Vector3(0f, 0f, 0f);
			}
			else
			{
				this.Point = collision.contacts[0].point;
				this.Normal = collision.contacts[0].normal;
				this.RelativeVelocity = collision.relativeVelocity;
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000B4A00 File Offset: 0x000B2C00
		public InteractionInformation(Vector3 point, Vector3 normal, Vector3 relativeVelocity, bool isPlayer)
		{
			this.Point = point;
			this.Normal = normal;
			this.RelativeVelocity = relativeVelocity;
			this.IsPlayer = isPlayer;
		}

		// Token: 0x04002569 RID: 9577
		public Vector3 Normal;

		// Token: 0x0400256A RID: 9578
		public Vector3 Point;

		// Token: 0x0400256B RID: 9579
		public Vector3 RelativeVelocity;

		// Token: 0x0400256C RID: 9580
		public bool IsPlayer;
	}
}
