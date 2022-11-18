using System;

namespace RDR2
{
	[Flags]
	public enum eExitConfigFlag
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

		//if pathfinding fails, cruise randomly instead of going on a straight line
		DF_UseWanderFallbackInsteadOfStraightLine = 2048,

		DF_AvoidRestrictedAreas = 4096,

		// These only work on MISSION_CRUISE
		DF_PreventBackgroundPathfinding = 8192,
		DF_AdjustCruiseSpeedBasedOnRoadSpeed = 16384,

		DF_UseShortCutLinks = 262144,
		DF_ChangeLanesAroundObstructions = 524288,
		DF_UseSwitchedOffNodes = 2097152,   //cruise tasks ignore this anyway--only used for goto's
		DF_PreferNavmeshRoute = 4194304,    //if you're going to be primarily driving off road

		// Only works for planes using MISSION_GOTO, will cause them to drive along the ground instead of fly
		DF_PlaneTaxiMode = 8388608,

		DF_ForceStraightLine = 16777216,
		DF_UseStringPullingAtJunctions = 33554432,

		DF_AvoidHighways = 536870912,
		DF_ForceJoinInRoadDirection = 1073741824,

		//standard driving mode. stops for cars, peds, and lights, goes around stationary obstructions
		DRIVINGMODE_STOPFORCARS = 786603,//DF_StopForCars|DF_StopForPeds|DF_SteerAroundObjects|DF_SteerAroundStationaryCars|DF_StopAtLights|DF_UseShortCutLinks|DF_ChangeLanesAroundObstructions,		// Obey lights too

		//like the above, but doesn't steer around anything in its way--will only wait instead.
		DRIVINGMODE_STOPFORCARS_STRICT = 262275,//DF_StopForCars|DF_StopForPeds|DF_StopAtLights|DF_UseShortCutLinks,		// Doesn't deviate an inch.

		//default "alerted" driving mode. drives around everything, doesn't obey lights
		DRIVINGMODE_AVOIDCARS = 786469, //DF_SwerveAroundAllCars|DF_SteerAroundObjects|DF_UseShortCutLinks|DF_ChangeLanesAroundObstructions|DF_StopForCars,

		//very erratic driving. difference between this and AvoidCars is that it doesn't use the brakes at ALL to help with steering
		DRIVINGMODE_AVOIDCARS_RECKLESS = 786468, //DF_SwerveAroundAllCars|DF_SteerAroundObjects|DF_UseShortCutLinks|DF_ChangeLanesAroundObstructions,

		//smashes through everything
		DRIVINGMODE_PLOUGHTHROUGH = 262144, //DF_UseShortCutLinks

		//drives normally except for the fact that it ignores lights
		DRIVINGMODE_STOPFORCARS_IGNORELIGHTS = 786475,//DF_StopForCars|DF_SteerAroundStationaryCars|DF_StopForPeds|DF_SteerAroundObjects|DF_UseShortCutLinks|DF_ChangeLanesAroundObstructions

		//try to swerve around everything, but stop for lights if necessary
		DRIVINGMODE_AVOIDCARS_OBEYLIGHTS = 786597,//DF_SwerveAroundAllCars|DF_StopAtLights|DF_SteerAroundObjects|DF_UseShortCutLinks|DF_ChangeLanesAroundObstructions|DF_StopForCars

		//swerve around cars, be careful around peds, and stop for lights
		DRIVINGMODE_AVOIDCARS_STOPFORPEDS_OBEYLIGHTS = 786599//DF_SwerveAroundAllCars|DF_StopAtLights|DF_StopForPeds|DF_SteerAroundObjects|DF_UseShortCutLinks|DF_ChangeLanesAroundObstructions|DF_StopForCars,
	}
}
