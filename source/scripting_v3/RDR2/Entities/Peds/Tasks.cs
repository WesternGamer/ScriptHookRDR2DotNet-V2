//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;
using System;

namespace RDR2
{
	public class Tasks
	{
		Ped _ped;

		internal Tasks(Ped ped)
		{
			_ped = ped;
		}

		public void AchieveHeading(float heading)
		{
			AchieveHeading(heading, 0);
		}
		public void AchieveHeading(float heading, int timeout)
		{
			TASK.TASK_ACHIEVE_HEADING(_ped.Handle, heading, timeout);
		}
		public void AimAt(Entity target, int duration)
		{
			TASK.TASK_AIM_GUN_AT_ENTITY(_ped.Handle, target.Handle, duration, false, 0);
		}
		public void AimAt(Vector3 target, int duration)
		{
			TASK.TASK_AIM_GUN_AT_COORD(_ped.Handle, target.X, target.Y, target.Z, duration, false, false);
		}
		public void Arrest(Ped ped)
		{
			TASK.TASK_ARREST_PED(_ped.Handle, ped.Handle);
		}

		public void Climb()
		{
			TASK.TASK_CLIMB(_ped.Handle, true);
		}
		public void Cower(int duration)
		{
			TASK.TASK_COWER(_ped.Handle, duration, 0, "");
		}
		public void CruiseWithVehicle(Vehicle vehicle, float speed)
		{
			CruiseWithVehicle(vehicle, speed, 0);
		}
		public void CruiseWithVehicle(Vehicle vehicle, float speed, int drivingstyle)
		{
			TASK.TASK_VEHICLE_DRIVE_WANDER(_ped.Handle, vehicle.Handle, speed, drivingstyle);
		}
		public void EnterVehicle()
		{
			EnterVehicle(new Vehicle(0), eVehicleSeat.VS_ANY_PASSENGER, -1, 0.0f, 0);
		}
		public void EnterVehicle(Vehicle vehicle, eVehicleSeat seat)
		{
			EnterVehicle(vehicle, seat, -1, 0.0f, 0);
		}
		public void EnterVehicle(Vehicle vehicle, eVehicleSeat seat, int timeout)
		{
			EnterVehicle(vehicle, seat, timeout, 0.0f, 0);
		}
		public void EnterVehicle(Vehicle vehicle, eVehicleSeat seat, int timeout, float speed)
		{
			EnterVehicle(vehicle, seat, timeout, speed, 0);
		}
		public void EnterVehicle(Vehicle vehicle, eVehicleSeat seat, int timeout, float speed, int flag)
		{
			TASK.TASK_ENTER_VEHICLE(_ped.Handle, vehicle.Handle, timeout, (int)(seat), speed, flag, 0);
		}
		public void FightAgainst(Ped target)
		{
			TASK.TASK_COMBAT_PED(_ped.Handle, target.Handle, 0, 16);
		}
		public void FightAgainst(Ped target, int duration)
		{
			TASK.TASK_COMBAT_PED_TIMED(_ped.Handle, target.Handle, duration, 0);
		}
		public void FightAgainstHatedTargets(float radius)
		{
			TASK.TASK_COMBAT_HATED_TARGETS_AROUND_PED(_ped.Handle, radius, 0, 0);
		}
		public void FightAgainstHatedTargets(float radius, float duration)
		{
			TASK.TASK_COMBAT_HATED_TARGETS_AROUND_PED_TIMED(_ped.Handle, radius, duration, 0);
		}
		public void FleeFrom(Ped ped)
		{
			FleeFrom(ped, -1);
		}
		public void FleeFrom(Ped ped, int duration)
		{
			TASK.TASK_SMART_FLEE_PED(_ped.Handle, ped.Handle, 100.0f, duration, 0, 0, 0);
		}
		public void FleeFrom(Vector3 position)
		{
			FleeFrom(position, -1);
		}
		public void FleeFrom(Vector3 position, int duration)
		{
			TASK.TASK_SMART_FLEE_COORD(_ped.Handle, position.X, position.Y, position.Z, 100.0f, duration, 0, 0);
		}
		public void GoTo(Entity target)
		{
			GoTo(target, Vector3.Zero, -1);
		}
		public void GoTo(Entity target, Vector3 offset)
		{
			GoTo(target, offset, -1);
		}
		public void GoTo(Entity target, Vector3 offset, int timeout)
		{
			TASK.TASK_GOTO_ENTITY_OFFSET_XY(_ped.Handle, target.Handle, timeout, offset.X, offset.Y, offset.Z, 1.0f, true);
		}
		public void GoTo(Vector3 position)
		{
			GoTo(position, false, -1);
		}
		public void GoTo(Vector3 position, bool ignorePaths)
		{
			GoTo(position, ignorePaths, -1);
		}
		public void GoTo(Vector3 position, bool ignorePaths, int timeout)
		{
			if (ignorePaths)
			{
				TASK.TASK_GO_STRAIGHT_TO_COORD(_ped.Handle, position.X, position.Y, position.Z, 1.0f, timeout, 0.0f, 0.0f, 0);
			}
			else
			{
				TASK.TASK_FOLLOW_NAV_MESH_TO_COORD(_ped.Handle, position.X, position.Y, position.Z, 1.0f, timeout, 0.0f, 0, 0.0f);
			}
		}
		public void GuardCurrentPosition()
		{
			TASK.TASK_GUARD_CURRENT_POSITION(_ped.Handle, 15.0f, 10.0f, true);
		}
		public void HandsUp(int duration)
		{
			TASK.TASK_HANDS_UP(_ped.Handle, duration, 0, -1, false);
		}
		public void Jump()
		{
			TASK.TASK_JUMP(_ped.Handle, true);
		}
		public void LeaveVehicle(eExitConfigFlag flags = eExitConfigFlag.ECF_NONE)
		{
			TASK.TASK_LEAVE_ANY_VEHICLE(_ped.Handle, 0, (int)flags);
		}
		public void LeaveVehicle(Vehicle vehicle, eExitConfigFlag flags)
		{
			TASK.TASK_LEAVE_VEHICLE(_ped.Handle, vehicle.Handle, (int)flags, 0);
		}
		public void LookAt(Entity target)
		{
			LookAt(target, -1);
		}
		public void LookAt(Entity target, int duration)
		{
			TASK.TASK_LOOK_AT_ENTITY(_ped.Handle, target.Handle, duration, 0 /* flags */, 2, 0);
		}
		public void LookAt(Vector3 position)
		{
			LookAt(position, -1);
		}
		public void LookAt(Vector3 position, int duration)
		{
			TASK.TASK_LOOK_AT_COORD(_ped.Handle, position.X, position.Y, position.Z, (ulong)duration, 0 /* flags */, 2, 0);
		}
		public void PerformSequence(TaskSequence sequence)
		{
			if (!sequence.IsClosed)
			{
				sequence.Close();
			}

			ClearTasks(false);

			_ped.BlockPermanentEvents = true;

			TASK.TASK_PERFORM_SEQUENCE(_ped.Handle, sequence.Handle);
		}

		public void PlayAnimation(string animDict, string animName, float blendInSpeed, float blendOutSpeed, int duration, eScriptedAnimFlags flags, float playbackRate, float timeout = 1000f)
		{
			if (!STREAMING.HAS_ANIM_DICT_LOADED(animDict)) {
				STREAMING.REQUEST_ANIM_DICT(animDict);
			}

			var end = DateTime.UtcNow.AddMilliseconds(timeout);
			while (!STREAMING.HAS_ANIM_DICT_LOADED(animDict)) {
				if (DateTime.UtcNow >= end) {
					return;
				}
			}

			TASK.TASK_PLAY_ANIM(_ped.Handle, animDict, animName, blendInSpeed, blendOutSpeed, duration, (int)flags, playbackRate, false, 0, false, "", false);
		}
		public void PlayAnimation(string animDict, string animName)
		{
			PlayAnimation(animDict, animName, 8f, -8f, -1, eScriptedAnimFlags.AF_NONE, 0f);
		}
		public void PlayAnimation(string animDict, string animName, float speed, int duration, float playbackRate)
		{
			PlayAnimation(animDict, animName, speed, -speed, duration, eScriptedAnimFlags.AF_NONE, playbackRate);
		}
		public void PlayAnimation(string animDict, string animName, float blendInSpeed, int duration, eScriptedAnimFlags flags = eScriptedAnimFlags.AF_NONE)
		{
			PlayAnimation(animDict, animName, blendInSpeed, -8f, duration, flags, 0f);
		}

		public void StopAnimation(string animDict, string animName, float blendOutSpeed = -8f)
		{
			TASK.STOP_ANIM_TASK(_ped.Handle, animDict, animName, blendOutSpeed);
		}

		public void ReloadWeapon()
		{
			TASK.TASK_RELOAD_WEAPON(_ped.Handle, true);
		}
		public void RunTo(Vector3 position)
		{
			RunTo(position, false, -1);
		}
		public void RunTo(Vector3 position, bool ignorePaths)
		{
			RunTo(position, ignorePaths, -1);
		}
		public void RunTo(Vector3 position, bool ignorePaths, int timeout)
		{
			if (ignorePaths)
			{
				TASK.TASK_GO_STRAIGHT_TO_COORD(_ped.Handle, position.X, position.Y, position.Z, 1.0f, timeout, 0.0f, 0.0f, 0);
			}
			else
			{
				TASK.TASK_FOLLOW_NAV_MESH_TO_COORD(_ped.Handle, position.X, position.Y, position.Z, 4.0f, timeout, 0.0f, 0, 0.0f);
			}
		}
		public void ShootAt(Ped target)
		{
			ShootAt(target, -1, 0);
		}
		public void ShootAt(Ped target, int duration)
		{
			ShootAt(target, duration, 0);
		}
		public void ShootAt(Ped target, int duration, int pattern)
		{
			TASK.TASK_SHOOT_AT_ENTITY(_ped.Handle, target.Handle, duration, (uint)(pattern), false);
		}
		public void ShootAt(Vector3 position)
		{
			ShootAt(position, -1, 0);
		}
		public void ShootAt(Vector3 position, int duration)
		{
			ShootAt(position, duration, 0);
		}
		public void ShootAt(Vector3 position, int duration, int pattern)
		{
			TASK.TASK_SHOOT_AT_COORD(_ped.Handle, position.X, position.Y, position.Z, duration, (uint)(pattern), 0);
		}
		public void ShuffleToNextVehicleSeat(Vehicle vehicle)
		{
			TASK.TASK_SHUFFLE_TO_NEXT_VEHICLE_SEAT(_ped.Handle, vehicle.Handle);
		}
		public void SlideTo(Vector3 position, float heading)
		{
			TASK.TASK_PED_SLIDE_TO_COORD(_ped.Handle, position.X, position.Y, position.Z, heading, 0.7f);
		}
		public void StandStill(int duration)
		{
			TASK.TASK_STAND_STILL(_ped.Handle, duration);
		}

		public void TurnTo(Entity target)
		{
			TurnTo(target, -1);
		}
		public void TurnTo(Entity target, int duration)
		{
			TASK.TASK_TURN_PED_TO_FACE_ENTITY(_ped.Handle, target.Handle, duration, 0, 0, 0);
		}
		public void TurnTo(Vector3 position)
		{
			TurnTo(position, -1);
		}
		public void TurnTo(Vector3 position, int duration)
		{
			TASK.TASK_TURN_PED_TO_FACE_COORD(_ped.Handle, position.X, position.Y, position.Z, duration);
		}
		/*public void VehicleChase(Ped target)
		{
			TASK.TASK_VEHICLE_CHASE(_ped.Handle, target.Handle);
		}*/ // unknown native
		public void Wait(int duration)
		{
			TASK.TASK_PAUSE(_ped.Handle, duration);
		}
		public void WanderAround()
		{
			TASK.TASK_WANDER_STANDARD(_ped.Handle, 0, 0);
		}
		public void WanderAround(Vector3 position, float radius)
		{
			TASK.TASK_WANDER_IN_AREA(_ped.Handle, position.X, position.Y, position.Z, radius, 0.0f, 0.0f, 0);
		}
		public void WarpIntoVehicle(Vehicle vehicle, int seat)
		{
			TASK.TASK_WARP_PED_INTO_VEHICLE(_ped.Handle, vehicle.Handle, (int)(seat));
		}
		public void WarpOutOfVehicle(Vehicle vehicle)
		{
			TASK.TASK_LEAVE_VEHICLE(_ped.Handle, vehicle.Handle, 0, 0);
		}

		public void ClearTasks(bool bImmediately)
		{
			if (bImmediately) {
				TASK.CLEAR_PED_TASKS_IMMEDIATELY(_ped.Handle, true, true);
			}
			else {
				TASK.CLEAR_PED_TASKS(_ped.Handle, true, true);
			}
		}

		public void ClearLookAt()
		{
			TASK.TASK_CLEAR_LOOK_AT(_ped.Handle);
		}
		public void ClearSecondary()
		{
			TASK.CLEAR_PED_SECONDARY_TASK(_ped.Handle);
		}

		public void Whistle(eAudPedWhistleType whistleType)
		{
			TASK.TASK_WHISTLE_ANIM(_ped.Handle, (uint)whistleType, 1971704925); // UNSPECIFIED
		}

		public void HandsUp(int duration, Ped facingPed)
		{
			TASK.TASK_HANDS_UP(_ped.Handle, duration, facingPed == null ? 0 : facingPed.Handle, -1, false);
		}

		public void KnockOut(float angle, bool permanently)
		{
			TASK.TASK_KNOCKED_OUT(_ped.Handle, angle, permanently);
		}

		public void KnockOutAndHogtied(float angle, bool immediately)
		{
			TASK.TASK_KNOCKED_OUT_AND_HOGTIED(_ped.Handle, angle, immediately ? 1 : 0);
		}

		public void Combat(Ped target)
		{
			TASK.TASK_COMBAT_PED(_ped.Handle, target.Handle, 0, 0);
		}

		public void ReviveTarget(Ped target)
		{
			//TASK.TASK_REVIVE_TARGET(_ped.Handle, target.Handle, unchecked((uint)-1516555556));
			PED.REVIVE_INJURED_PED(target.Handle);
			ENTITY.SET_ENTITY_HEALTH(target.Handle, ENTITY.GET_ENTITY_MAX_HEALTH(target.Handle, true), 0);
		}

		public void SeekCoverFrom(Ped target, int duration)
		{
			TASK.TASK_SEEK_COVER_FROM_PED(_ped.Handle, (ulong)target.Handle, (ulong)duration, 0, 0, 0);
		}

		public void SeekCoverFrom(Vector3 pos, int duration)
		{
			TASK.TASK_SEEK_COVER_FROM_POS(_ped.Handle, pos.X, pos.Y, pos.Z, (ulong)duration, 0, 0, 0);
		}

		public void StandGuard(Vector3 pos = default)
		{
			pos = pos == default ? _ped.Position : pos;
			TASK.TASK_STAND_GUARD(_ped.Handle, pos.X, pos.Y, pos.Z, _ped.Heading, "DEFEND");
		}

		/*public void Rob(Ped target, int duration, int flag = 18)
		{
			TASK.TASK_ROB_PED(_ped.Handle, (ulong)target.Handle, flag, duration);
		}*/

		public void Flock()
		{
			FLOCK._0xE0961AED72642B80((ulong)_ped.Handle); // ???
		}

		public void Duck(int duration)
		{
			TASK.TASK_DUCK(_ped.Handle, duration);
		}

		public void EnterCover()
		{
			AICOVERPOINT.TASK_ENTER_COVER(_ped.Handle);
		}

		public void ExitCover()
		{
			AICOVERPOINT.TASK_EXIT_COVER(_ped.Handle);
		}

		public void ExitVehicle(Vehicle vehicle, eExitConfigFlag flag = eExitConfigFlag.ECF_NONE)
		{
			TASK.TASK_LEAVE_VEHICLE(_ped.Handle, vehicle.Handle, (int)flag, 0);
		}

		public void MountAnimal(Ped animal, int timeout = -1)
		{
			TASK.TASK_MOUNT_ANIMAL(_ped.Handle, animal.Handle, timeout, -1, 2f, 1, 0, 0);
		}

		public void DismountAnimal(Ped animal)
		{
			TASK.TASK_DISMOUNT_ANIMAL(_ped.Handle, animal.Handle, 0, 0, 0, 0);
		}

		public void HitchAnimal(Ped animal, int flag = 0)
		{
			TASK.TASK_HITCH_ANIMAL(_ped.Handle, animal.Handle, flag);
		}

		public void DriveToCoord(Vehicle vehicle, Vector3 pos, float speed, float radius = 6f, VehicleDrivingFlags drivingMode = VehicleDrivingFlags.None)
		{
			TASK.TASK_VEHICLE_DRIVE_TO_COORD(_ped.Handle, vehicle.Handle, pos.X, pos.Y, pos.Z, speed, (ulong)radius, (uint)vehicle.Model.Hash, (int)drivingMode, 0.0f, 0.0f);
		}

		public void FollowToEntity(Entity entity, float speed, Vector3 offset = default, int timeout = -1, float stoppingRange = 3f, bool keepFollowing = true)
		{
			TASK.TASK_FOLLOW_TO_OFFSET_OF_ENTITY(_ped.Handle, entity.Handle, offset.X, offset.Y, offset.Z, speed, timeout, stoppingRange, keepFollowing, true, false, false, true, false);
		}

		public void GoToWhistle(Entity target, int flag = 3)
		{
			TASK.TASK_GO_TO_WHISTLE(_ped.Handle, target.Handle, flag);
		}

		public void LeadHorse(Ped horse)
		{
			TASK.TASK_LEAD_HORSE(_ped.Handle, horse.Handle);
		}

		public void FlyAway(Entity awayFrom = null)
		{
			TASK.TASK_FLY_AWAY(awayFrom == null ? 0 : awayFrom.Handle, 0);
		}

		public void WalkAway(Entity awayFrom = null)
		{
			TASK.TASK_WALK_AWAY(awayFrom == null ? 0 : awayFrom.Handle, 0);
		}
	}

	public enum eAudPedWhistleType : uint
	{
		WHISTLEHORSERESPONSIVE = 0xFBB22B86,
		WHISTLEHORSETALK = 0x2628D6E0,
		WHISTLEHORSELONG = 0x33D023F4,
		WHISTLEHORSEDOUBLE = 0x3EAC666C,
		WHISTLEHORSESHORT = 0x659F956D,
		WHISTLEANIMALNOISES = 0x6BBE62DD,
	}

	public enum EventReaction
	{
		TaskCombatHigh = 1103872808,
		TaskCombatMedium = 623557147,
		TaskCombatReact = -1342511871,
		TaskCombatPanic = -996719768,
		DefaultShocked = -372548123,
		DefaultPanic = 1618376518,
		DefaultCurious = -1778605437,
		DefaultBrave = 1781933509,
		DefaultAngry = 1345150177,
		DefaultDefuse = -1675652957,
		DefaultScared = -1967172690,
		FleeHumanMajorThreat = -2111647205,
		FleeScared = 759577278
	}
}
