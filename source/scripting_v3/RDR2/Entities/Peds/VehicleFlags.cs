using System;

namespace RDR2
{
	[Flags]
	public enum eEnterExitConfigFlags
	{
		ECF_NONE = 0,
		ECF_RESUME_IF_INTERRUPTED = (1 << 0),
		ECF_WARP_ENTRY_POINT = (1 << 1),
		ECF_INTERRUPT_DURING_GET_IN = (1 << 2),
		ECF_JACK_ANYONE = (1 << 3),
		ECF_WARP_PED = (1 << 4),
		ECF_INTERRUPT_DURING_GET_OUT = (1 << 5),
		ECF_DONT_WAIT_FOR_VEHICLE_TO_STOP = (1 << 6),
		ECF_ALLOW_EXTERIOR_ENTRY = (1 << 7),
		ECF_DONT_CLOSE_DOOR = (1 << 8),
		ECF_WARP_IF_DOOR_IS_BLOCKED = (1 << 9),
		ECF_ENTER_USING_NAVMESH = (1 << 10),
		ECF_JUMP_OUT = (1 << 12),
		ECF_PREFER_DISMOUNT_SIDE_WITH_FEWER_PEDS = (1 << 14),
		ECF_DONT_DEFAULT_WARP_IF_DOOR_BLOCKED = (1 << 16),
		ECF_USE_LEFT_ENTRY = (1 << 17),
		ECF_USE_RIGHT_ENTRY = (1 << 18),
		ECF_JUST_PULL_PED_OUT = (1 << 19),
		ECF_BLOCK_SEAT_SHUFFLING = (1 << 20),
		ECF_WARP_IF_SHUFFLE_LINK_IS_BLOCKED = (1 << 22),
		ECF_DONT_JACK_ANYONE = (1 << 23),
		ECF_WAIT_FOR_ENTRY_POINT_TO_BE_CLEAR = (1 << 24),
		ECF_USE_HITCH_DISMOUNT_VARIANT = (1 << 25),
		ECF_EXIT_SEAT_ON_TO_VEHICLE = (1 << 26),
		ECF_ALLOW_SCRIPTED_TASK_ABORT = (1 << 27),
		ECF_WILL_SHOOT_AT_TARGET_PEDS = (1 << 28),
		ECF_INTERRUPT_ALWAYS = (1 << 29),
		ECF_IGNORE_ENTRY_FROM_CLOSEST_POINT = (1 << 30),
		ECF_ALLOW_JACK_PLAYER_PED_ONLY = (1 << 31),
	}

	[Flags]
	public enum VehicleDrivingFlags
	{
		None = 0,

		DF_StopForCars = 1,
		DF_StopForPeds = 2,
		DF_SwerveAroundAllCars = 4,
		DF_SteerAroundStationaryCars = 8,
		DF_SteerAroundPeds = 16,
		DF_SteerAroundObjects = 32,
		DF_DontSteerAroundPlayerPed = 64,
		DF_StopAtLights = 128,
		DF_GoOffRoadWhenAvoiding = 256,
		DF_DriveIntoOncomingTraffic = 512,
		DF_DriveInReverse = 1024,
		DF_UseWanderFallbackInsteadOfStraightLine = 2048,
		DF_AvoidRestrictedAreas = 4096,
		DF_PreventBackgroundPathfinding = 8192,
		DF_AdjustCruiseSpeedBasedOnRoadSpeed = 16384,
		DF_UseShortCutLinks = 262144,
		DF_ChangeLanesAroundObstructions = 524288,
		DF_UseSwitchedOffNodes = 2097152,
		DF_PreferNavmeshRoute = 4194304,
		DF_PlaneTaxiMode = 8388608,
		DF_ForceStraightLine = 16777216,
		DF_UseStringPullingAtJunctions = 33554432,
		DF_AvoidHighways = 536870912,
		DF_ForceJoinInRoadDirection = 1073741824,

		DRIVINGMODE_STOPFORCARS = 786603,
		DRIVINGMODE_STOPFORCARS_STRICT = 262275,
		DRIVINGMODE_AVOIDCARS = 786469,
		DRIVINGMODE_AVOIDCARS_RECKLESS = 786468,
		DRIVINGMODE_PLOUGHTHROUGH = 262144,
		DRIVINGMODE_STOPFORCARS_IGNORELIGHTS = 786475,
		DRIVINGMODE_AVOIDCARS_OBEYLIGHTS = 786597,
		DRIVINGMODE_AVOIDCARS_STOPFORPEDS_OBEYLIGHTS = 786599
	}
}
