//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;
using RDR2.NaturalMotion;
using System;

namespace RDR2
{
	public sealed class Ped : Entity
	{
		#region Fields
		Tasks _tasks;
		NaturalMotion.Euphoria _euphoria;
		WeaponCollection _weapons;


		#endregion

		public Ped(int handle) : base(handle)
		{
		}

		public void Clone()
		{
			PED.CLONE_PED(Handle, true, true, false);
		}

		

		#region Styling
		public float Scale
		{
			set => PED._SET_PED_SCALE(Handle, value);
		}
		public bool IsHuman => PED.IS_PED_HUMAN(Handle);

		public bool IsCuffed => TASK.IS_PED_CUFFED(Handle);

		public void ClearBloodDamage()
		{
			PED.CLEAR_PED_BLOOD_DAMAGE(Handle);
		}

		public void ResetVisibleDamage()
		{
			PED._0x88A5564B19C15391(Handle);
		}

		public Gender Gender => PED.IS_PED_MALE(Handle) ? Gender.Male : Gender.Female;

		public float Sweat
		{
			set {
				if (value < 0)
				{
					value = 0;
				}
				if (value > 100)
				{
					value = 100;
				}

				PED.SET_PED_SWEAT(Handle, value);
			}
		}

		public float WetnessHeight
		{
			set => PED.SET_PED_WETNESS_HEIGHT(Handle, value);
		}

		public void RandomizeOutfit()
		{
			PED._SET_RANDOM_OUTFIT_VARIATION(Handle, true);
		}

		public void UpdateVariation()
		{
			// All true params for now
			PED._UPDATE_PED_VARIATION(Handle, true, true, true, true, true);
		}

		#endregion

		#region Configuration


		public override int MaxHealth
		{
			get => PED.GET_PED_MAX_HEALTH(Handle);
			set => PED.SET_PED_MAX_HEALTH(Handle, value);
		}

		public bool IsPlayer => PED.IS_PED_A_PLAYER(Handle);

		public void SetPedPromptName(string name)
		{
			PED._SET_PED_PROMPT_NAME(Handle, MISC.VAR_STRING(10, "LITERAL_STRING", name));
		}
		
		public bool GetConfigFlag(ePedScriptConfigFlags flagID)
		{
			return PED.GET_PED_CONFIG_FLAG(Handle, (int)flagID, true);
		}

		public void SetConfigFlag(ePedScriptConfigFlags flagID, bool value)
		{
			PED.SET_PED_CONFIG_FLAG(Handle, (int)flagID, value);
		}

		public void SetResetFlag(ePedScriptResetFlags flagID)
		{
			PED.SET_PED_RESET_FLAG(Handle, (int)flagID, true);
		}

		public bool GetResetFlag(ePedScriptResetFlags flagID)
		{
			return PED.GET_PED_RESET_FLAG(Handle, (int)flagID);
		}

		public int GetBoneIndex(BoneID BoneID)
		{
			return PED.GET_PED_BONE_INDEX(Handle, (int)BoneID);
		}

		public Vector3 GetBoneCoord(BoneID BoneID)
		{
			return GetBoneCoord(BoneID, Vector3.Zero);
		}
		public Vector3 GetBoneCoord(BoneID BoneID, Vector3 Offset)
		{
			return PED.GET_PED_BONE_COORDS(Handle, (int)BoneID, Offset.X, Offset.Y, Offset.Z);
		}

		#endregion

		#region Tasks

		public bool IsIdle => !IsInjured && !IsRagdoll && !IsInAir && !IsOnFire && !IsGettingIntoAVehicle && !IsInCombat && !IsInMeleeCombat && !(IsInVehicle() && !IsSittingInVehicle());

		public bool IsProne => PED.IS_PED_PRONE(Handle);

		public bool IsGettingUp => TASK.IS_PED_GETTING_UP(Handle);

		public bool IsDiving => PED.IS_PED_DIVING(Handle);

		public bool IsJumping => PED.IS_PED_JUMPING(Handle);

		public bool IsFalling => PED.IS_PED_FALLING(Handle);

		public bool IsVaulting => PED.IS_PED_VAULTING(Handle);

		public bool IsClimbing => PED.IS_PED_CLIMBING(Handle);

		public bool IsWalking => TASK.IS_PED_WALKING(Handle);

		public bool IsRunning => TASK.IS_PED_RUNNING(Handle);

		public bool IsSprinting => TASK.IS_PED_SPRINTING(Handle);

		public bool IsStopped => PED.IS_PED_STOPPED(Handle);

		public bool IsSwimming => PED.IS_PED_SWIMMING(Handle);

		public bool IsSwimmingUnderWater => PED.IS_PED_SWIMMING_UNDER_WATER(Handle);

		public bool IsOnMount => PED.IS_PED_ON_MOUNT(Handle);


		public bool IsHeadTracking(Entity entity)
		{
			return PED.IS_PED_HEADTRACKING_ENTITY(Handle, entity.Handle);
		}

		public bool AlwaysKeepTask
		{
			set => PED.SET_PED_KEEP_TASK(Handle, value);
		}

		public bool BlockPermanentEvents
		{
			set => PED.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS(Handle, value);
		}

		public Tasks Task => _tasks ?? (_tasks = new Tasks(this));

		public int TaskSequenceProgress => TASK.GET_SEQUENCE_PROGRESS(Handle);

		#endregion

		#region Euphoria & Ragdoll

		public bool IsRagdoll => PED.IS_PED_RAGDOLL(Handle);

		public bool CanRagdoll
		{
			get => PED.CAN_PED_RAGDOLL(Handle);
			set => PED.SET_PED_CAN_RAGDOLL(Handle, value);
		}

		public Euphoria Euphoria => _euphoria ?? (_euphoria = new Euphoria(this));

		#endregion

		#region Weapon Interaction

		public int Accuracy
		{
			get => PED.GET_PED_ACCURACY(Handle);
			set => PED.SET_PED_ACCURACY(Handle, value);
		}

		public int ShootRate
		{
			set => PED.SET_PED_SHOOT_RATE(Handle, value);
		}

		/*public FiringPattern FiringPattern
		{
			set => PED.SET_PED_FIRING_PATTERN(Handle, (int)value);
		}*/ // firing patterns

		public WeaponCollection Weapons => _weapons ??= new WeaponCollection(this);

		/*public bool CanSwitchWeapons
		{
			set => PED.SET_PED_CAN_SWITCH_WEAPON(Handle, value);
		}*/

		
		public void GiveWeapon(eWeapon weapon, int ammoCount, eWeaponAttachPoint attachPoint, bool bForceInHand = false, bool bForceInHolster = false, bool bAllowMultipleCopies = false)
		{
			WEAPON.GIVE_WEAPON_TO_PED(Handle, (uint)weapon, ammoCount, bForceInHand, bForceInHolster, (int)attachPoint, bAllowMultipleCopies, 0.0f, 0.0f, (uint)eAddItemReason.ADD_REASON_DEFAULT, true, 0.0f, false);
		}

		#endregion

		#region Vehicle Interaction


		public bool IsOnFoot => PED.IS_PED_ON_FOOT(Handle);

		public bool IsInTrain => PED.IS_PED_IN_ANY_TRAIN(Handle);

		public bool IsInBoat => PED.IS_PED_IN_ANY_BOAT(Handle);

		public bool IsGettingIntoAVehicle => PED.IS_PED_GETTING_INTO_A_VEHICLE(Handle);

		public bool IsOnHorse => PED.IS_PED_ON_MOUNT(Handle);

		public Ped CurrentMount => (Ped)Ped.FromHandle(PED.GET_MOUNT(Handle));

		public bool IsInVehicle()
		{
			return PED.IS_PED_IN_ANY_VEHICLE(Handle, false);
		}
		public bool IsInVehicle(Vehicle vehicle)
		{
			return PED.IS_PED_IN_VEHICLE(Handle, vehicle.Handle, false);
		}

		public bool IsSittingInVehicle()
		{
			return PED.IS_PED_SITTING_IN_ANY_VEHICLE(Handle);
		}
		public bool IsSittingInVehicle(Vehicle vehicle)
		{
			return PED.IS_PED_SITTING_IN_VEHICLE(Handle, vehicle.Handle);
		}

		public void SetIntoVehicle(Vehicle vehicle, int seat)
		{
			PED.SET_PED_INTO_VEHICLE(Handle, vehicle.Handle, (int)seat);
		}

		public Vehicle LastVehicle => (Vehicle)FromHandle(PED.GET_VEHICLE_PED_IS_IN(Handle, true));

		public Vehicle CurrentVehicle => IsInVehicle() ? (Vehicle)FromHandle(PED.GET_VEHICLE_PED_IS_IN(Handle, false)) : null;


		#endregion

		#region Driving

		public float DrivingSpeed
		{
			set => TASK.SET_DRIVE_TASK_CRUISE_SPEED(Handle, value);
		}

		public float MaxDrivingSpeed
		{
			set => TASK.SET_DRIVE_TASK_MAX_CRUISE_SPEED(Handle, value);
		}
		#endregion

		#region Jacking

		public bool IsJacking => PED.IS_PED_JACKING(Handle);

		public bool IsBeingJacked => PED.IS_PED_BEING_JACKED(Handle);

		#endregion

		#region Combat

		/*public bool IsEnemy
		{
			set => PED.SET_PED_AS_ENEMY(Handle, value);
		}*/

		public bool IsPriorityTargetForEnemies
		{
			set => ENTITY.SET_ENTITY_IS_TARGET_PRIORITY(Handle, value, 0);
		}

		public bool IsFleeing => PED.IS_PED_FLEEING(Handle);

		public bool IsInjured => PED.IS_PED_INJURED(Handle);

		public bool IsInCombat => PED.IS_PED_IN_COMBAT(Handle, 0);

		public bool IsInMeleeCombat => PED.IS_PED_IN_MELEE_COMBAT(Handle);

		public bool IsShooting => PED.IS_PED_SHOOTING(Handle);

		public bool IsReloading => PED.IS_PED_RELOADING(Handle);

		public bool IsGoingIntoCover => PED.IS_PED_GOING_INTO_COVER(Handle);

		public bool IsAimingFromCover => PED.IS_PED_AIMING_FROM_COVER(Handle);

		public bool IsBeingStunned => PED.IS_PED_BEING_STUNNED(Handle, 0);

		public bool IsBeingStealthKilled => PED.IS_PED_BEING_STEALTH_KILLED(Handle);

		public bool IsInCover()
		{
			return IsInCover(false);
		}
		public bool IsInCover(bool expectUseWeapon)
		{
			return PED.IS_PED_IN_COVER(Handle, expectUseWeapon, false);
		}

		public bool IsInCoverFacingLeft => PED.IS_PED_IN_COVER_FACING_LEFT(Handle);

		public bool CanBeTargetted
		{
			set => PED.SET_PED_CAN_BE_TARGETTED(Handle, value);
		}

		public Ped GetMeleeTarget()
		{
			return (Ped)FromHandle(PED.GET_MELEE_TARGET_FOR_PED(Handle));
		}

		public bool IsInCombatAgainst(Ped target)
		{
			return PED.IS_PED_IN_COMBAT(Handle, target.Handle);
		}

		#endregion

		#region Damaging

		/*public bool CanWrithe
		{
			get => !GetConfigFlag(281);
			set => SetConfigFlag(281, !value);
		}*/

		public bool DropsWeaponsOnDeath
		{
			set => WEAPON.SET_PED_DROPS_WEAPONS_WHEN_DEAD(Handle, value);
		}

		public void ApplyDamage(int damageAmount)
		{
			PED.APPLY_DAMAGE_TO_PED(Handle, damageAmount, 0, 0, 0);
		}

		public unsafe Vector3 GetLastWeaponImpactCoords()
		{
			Vector3 outCoords;
			if (WEAPON.GET_PED_LAST_WEAPON_IMPACT_COORD(Handle, &outCoords)) {
				return outCoords;
			}
			return Vector3.Zero;
		}

		public void Kill()
		{
			PED._FORCE_PED_DEATH(Handle, 0, 0);
		}

		#endregion

		#region Relationship

		public Relationship GetRelationshipWithPed(Ped ped)
		{
			return (Relationship)PED.GET_RELATIONSHIP_BETWEEN_PEDS(Handle, ped.Handle);
		}

		public uint RelationshipGroup
		{
			get => PED.GET_PED_RELATIONSHIP_GROUP_HASH(Handle);
			set => PED.SET_PED_RELATIONSHIP_GROUP_HASH(Handle, value);
		}

		#endregion

		#region Group

		public bool IsInGroup => PED.IS_PED_IN_GROUP(Handle);

		public void LeaveGroup()
		{
			PED.REMOVE_PED_FROM_GROUP(Handle);
		}

		/*public bool NeverLeavesGroup
		{
			set => PED.SET_PED_NEVER_LEAVES_GROUP(Handle, value);
		}*/

		public RelationshipGroup CurrentPedGroup => IsInGroup ? PED.GET_PED_RELATIONSHIP_GROUP_HASH(Handle) : 0;

		#endregion

		#region Speech & Animation

		public bool CanPlayGestures
		{
			set => PED.SET_PED_CAN_PLAY_GESTURE_ANIMS(Handle, (ulong)(value == true ? 1 : 0), 0);
		}

		public string Voice
		{
			set => AUDIO.SET_AMBIENT_VOICE_NAME(Handle, value);
		}

		#endregion

		#region Cores
		public int HealthCore
		{
			get => GetCoreValue(PedCore.Health);
			set => SetCoreValue(PedCore.Health, value);
		}

		public int HealthCoreRank
		{
			get => GetCoreRank(PedCore.Health);
			set => SetCoreRank(PedCore.Health, value);
		}

		public int StaminaCore
		{
			get => GetCoreValue(PedCore.Stamina);
			set => SetCoreValue(PedCore.Stamina, value);
		}

		public int StaminaCoreRank
		{
			get => GetCoreRank(PedCore.Stamina);
			set => SetCoreRank(PedCore.Stamina, value);
		}

		public int DeadEyeCore
		{
			get => GetCoreValue(PedCore.DeadEye);
			set => SetCoreValue(PedCore.DeadEye, value);
		}

		public int DeadEyeRank
		{
			get => GetCoreRank(PedCore.DeadEye);
			set => SetCoreRank(PedCore.DeadEye, value);
		}

		public int GetCoreValue(PedCore core)
		{
			return ATTRIBUTE._GET_ATTRIBUTE_CORE_VALUE(Handle, (int)core);
		}

		public void SetCoreValue(PedCore core, int value)
		{
			ATTRIBUTE._SET_ATTRIBUTE_CORE_VALUE(Handle, (int)core, value);
		}

		public int GetCoreRank(PedCore core)
		{
			return ATTRIBUTE.GET_ATTRIBUTE_RANK(Handle, (int)core);
		}

		public void SetCoreRank(PedCore core, int level)
		{
			ATTRIBUTE.SET_ATTRIBUTE_POINTS(Handle, (int)core, GetExperienceByRank(level));
		}

		private static int GetExperienceByRank(int level)
		{
			switch (level)
			{
				case -1:
					return -1;
				case 0:
					return 0;

				case 1:
					return 50;

				case 2:
					return 100;

				case 3:
					return 200;

				case 4:
					return 350;

				case 5:
					return 550;

				case 6:
					return 800;

				case 7:
					return 1100;

				default:
					return 0;
			}
		}
		#endregion
	}

	public enum PedCore
	{
		Health = 0,
		Stamina,
		DeadEye
	}
}
