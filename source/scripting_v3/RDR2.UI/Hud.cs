//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Native;

namespace RDR2.UI
{
	/// <summary>
	/// Methods to manipulate the HUD (heads-up-display) of the game.
	/// </summary>
	public static class Hud
	{
		/// <summary>
		/// Enables a <see cref="eHudContext"/>
		/// </summary>
		/// <param name="component">The <see cref="eHudContext"/> to enable</param>
		public static void EnableHudContext(eHudContext component)
		{
			HUD._ENABLE_HUD_CONTEXT((uint)component);
		}

		/// <summary>
		/// Disables a <see cref="eHudContext"/>
		/// </summary>
		/// <param name="component">The <see cref="eHudContext"/> to disable</param>
		public static void DisableHudContext(eHudContext component)
		{
			HUD._DISABLE_HUD_CONTEXT((uint)component);
		}

		/// <summary>
		/// Shows the mouse cursor this frame.
		/// </summary>
		public static void ShowCursorThisFrame()
		{
			_NAMESPACE30.SET_MOUSE_CURSOR_THIS_FRAME();
		}

		/// <summary>
		/// Gets or sets the sprite the cursor should used when drawn
		/// </summary>
		public static eCursor CursorSprite
		{
			set => _NAMESPACE30.SET_MOUSE_CURSOR_STYLE((int)value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether any HUD components should be rendered.
		/// </summary>
		public static bool IsVisible
		{
			get => !HUD.IS_HUD_HIDDEN();
			set => HUD.DISPLAY_HUD(value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether the radar is visible.
		/// </summary>
		public static bool IsRadarVisible
		{
			get => !HUD.IS_RADAR_HIDDEN();
			set => MAP.DISPLAY_RADAR(value);
		}

		/// <summary>
		/// Sets how far the minimap should be zoomed in.
		/// </summary>
		/// <value>
		/// The radar zoom; accepts values from 0 to 200.
		/// </value>
		public static int RadarZoom
		{
			set => MAP.SET_RADAR_ZOOM(value);
		}
	}

	public enum eHudContext : uint
	{
		HUD_CTX_AMBIENT_SPECTATOR_VIEW = 0x7C2CFF26,
		HUD_CTX_CHARACTER_CREATOR = 0x6A16C358,
		HUD_CTX_CODE_TOOLS = 0x15E17C71,
		HUD_CTX_CRAFTING_SEQUENCE = 0xE3FEFB3D,
		HUD_CTX_FADED_GAMEPLAY = 0x6CD87394,
		HUD_CTX_FEUD1_FISHING = 0x54C5126F,
		HUD_CTX_FIREFIGHT_CUTSCENE = 0x675A0CCC,
		HUD_CTX_FISHING = 0xCCC6D67D,
		HUD_CTX_GOLD_CURRENCY_CHANGE = 0x7BD554C2,
		HUD_CTX_HACK_RADAR_FORCE_HIDE = 0x1C43984E,
		HUD_CTX_HONOR_SHOW = 0x74132EF,
		HUD_CTX_INFINITE_AMMO = 0x3F129E06,
		HUD_CTX_INFO_CARD = 0xC6015EAF,
		HUD_CTX_INPUT_REVEAL_HUD = 0xFBE3143F,
		HUD_CTX_INSPECT_ITEM = 0x91DFD454,
		HUD_CTX_INVALID = 0x5891C49,
		HUD_CTX_IN_CAMP = 0x9F8612C6,
		HUD_CTX_IN_CAMP_WARDROBE = 0xF0F41710,
		HUD_CTX_IN_CAMP_WITH_SUPPLIES = 0xC7569E12,
		HUD_CTX_IN_CATALOGUE_SHOP_MENU = 0x6C34EBE5,
		HUD_CTX_IN_COMBAT_RESTRICTED_SHOP = 0x6AC32FE2,
		HUD_CTX_IN_FAST_TRAVEL_MENU = 0xBB47198C,
		HUD_CTX_IN_FISHING_MODE = 0x6000911,
		HUD_CTX_IN_GUARMA_AND_BROKE = 0x3CFBD871,
		HUD_CTX_IN_LOBBY = 0x1CB7181F,
		HUD_CTX_IN_MINIGAME = 0x1639CD7B,
		HUD_CTX_IN_MINIGAME_POKER_INTRO = 0x727F2897,
		HUD_CTX_IN_MINIGAME_POKER_OUTRO = 0x85CF6FB0,
		HUD_CTX_IN_MINIGAME_WITH_MP_GAME_UPDATES = 0xEC1BBE94,
		HUD_CTX_IN_MISSION_CUTSCENE = 0x9BE7CD1D,
		HUD_CTX_IN_MP_GAME_MODE = 0x502722F2,
		HUD_CTX_IN_MP_TUTORIAL_CUTSCENE = 0xEA3A7E58,
		HUD_CTX_IN_PHOTO_MODE = 0xAC428E61,
		HUD_CTX_IN_PHOTO_STUDIO = 0xE2A1A218,
		HUD_CTX_IN_PLAYER_CAMP = 0x77DFED42,
		HUD_CTX_IN_QUICK_TIME_EVENT = 0xE6B485B4,
		HUD_CTX_IN_RESPAWN = 0xFC83EE25,
		HUD_CTX_IN_SHOP = 0xAFAF9BE1,
		HUD_CTX_IN_STABLES = 0x1A2B3041,
		HUD_CTX_IN_TOWN = 0x6D4FF8E7,
		HUD_CTX_IN_VERSUS_CUTSCENE = 0x3BCCFB49,
		HUD_CTX_ITEM_CONSUMPTION_DEADEYE = 0xE7FB156F,
		HUD_CTX_ITEM_CONSUMPTION_DEADEYE_CORE = 0x59C84474,
		HUD_CTX_ITEM_CONSUMPTION_HEALTH = 0xBC4D0E72,
		HUD_CTX_ITEM_CONSUMPTION_HEALTH_CORE = 0x7129F41B,
		HUD_CTX_ITEM_CONSUMPTION_HORSE_HEALTH = 0x4C5BE7D8,
		HUD_CTX_ITEM_CONSUMPTION_HORSE_HEALTH_CORE = 0x1A2F32CC,
		HUD_CTX_ITEM_CONSUMPTION_HORSE_STAMINA = 0x53B01660,
		HUD_CTX_ITEM_CONSUMPTION_HORSE_STAMINA_CORE = 0x390A4E38,
		HUD_CTX_ITEM_CONSUMPTION_STAMINA = 0x8452FC9C,
		HUD_CTX_ITEM_CONSUMPTION_STAMINA_CORE = 0x1268EBC5,
		HUD_CTX_LOBBY_TEAM_SELECT = 0x60B1D7ED,
		HUD_CTX_MAYOR2_CUTSCENE_OBJECTIVES = 0x32E36756,
		HUD_CTX_MINIGAME_SHOOTING = 0xB32DA298,
		HUD_CTX_MISSION_CONTROLLER = 0x292E5336,
		HUD_CTX_MISSION_CONTROLLER_CUTSCENE = 0x23AB544C,
		HUD_CTX_MISSION_CONTROLLER_INTRO = 0xC4CC008A,
		HUD_CTX_MISSION_CONTROLLER_OUTRO = 0x410B582,
		HUD_CTX_MISSION_CREATOR = 0x72A6F36B,
		HUD_CTX_MISSION_CUTSCENE_WITH_RADAR = 0x68E2F9FE,
		HUD_CTX_MONEY_ANIMATION_PLAYING = 0x8D7D726A,
		HUD_CTX_MP_BOUNTY_JAILTIME = 0xFD205FD3,
		HUD_CTX_MP_COLLECTOR_SALESWOMAN = 0x4554173F,
		HUD_CTX_MP_COOP_MISSION_POST_COMPLETION_AWARD_FLOW = 0x19DE43AA,
		HUD_CTX_MP_COOP_MISSION_POST_COMPLETION_PRE_AWARD_FLOW = 0xF1ADCF2,
		HUD_CTX_MP_INSTANCED_HUD = 0x20048FCF,
		HUD_CTX_MP_INSTANCED_TOP_RIGHT_HUD = 0x96E17205,
		HUD_CTX_MP_IN_ROLE_CUTSCENE = 0xB15DD8E7,
		HUD_CTX_MP_LEADERBOARD_MINI = 0x22DAAAA,
		HUD_CTX_MP_MATCHMAKING_TRANSITION = 0x7AAA30E,
		HUD_CTX_MP_MINIGAME_SHOW_PLAYER_CASH = 0x77665B04,
		HUD_CTX_MP_MOONSHINE_BUSINESS = 0x2136CA63,
		HUD_CTX_MP_NATURALIST_ANIMAL_MODE = 0x3871E8F9,
		HUD_CTX_MP_OUT_OF_AREA_BOUNDS = 0x8162B55C,
		HUD_CTX_MP_PLAYSTYLE_ICON_TRANSITION = 0x91AB55B6,
		HUD_CTX_MP_RACES = 0xA98AC78D,
		HUD_CTX_MP_SHOW_HUD_ABILITY_CARD_INDICATOR = 0x5B6798A8,
		HUD_CTX_MP_SPECTATING = 0xE6E65A0D,
		HUD_CTX_MP_STEW_POT_PROXIMITY = 0xEA44E97E,
		HUD_CTX_MP_TRADER = 0xD15C1751,
		HUD_CTX_MP_UGC_PLAYER_OUTRO = 0xFB8AFC35,
		HUD_CTX_NON_COMBAT_MISSION = 0xB58A0FF5,
		HUD_CTX_NO_ALIVE_PLAYER_HORSE = 0xE51AA567,
		HUD_CTX_OUTDOOR_SHOP = 0x54C36872,
		HUD_CTX_PLAYER_CAMERA_MODE = 0x392F9A27,
		HUD_CTX_PLAYER_CAMERA_REQUIRES_HUD = 0x525C749F,
		HUD_CTX_PLAYER_MENU_CAMP_MAINTENANCE = 0xE0CD605F,
		HUD_CTX_PLAYER_WITHOUT_SATCHEL = 0x82721491,
		HUD_CTX_POSSE_CREATE = 0xB407BF27,
		HUD_CTX_PROMPT_MONEY = 0x9808A869,
		HUD_CTX_RESTING_BY_FIRE = 0xC476227E,
		HUD_CTX_ROBBERY_ACTION = 0xB1636C00,
		HUD_CTX_SCRIPTED_IN_GAME_CUTSCENE = 0x4F5FF206,
		HUD_CTX_SCRIPTED_PLAYER_CONTROL_DISABLED = 0x2B331B6E,
		HUD_CTX_SCRIPT_CME_CUTSCENE = 0x99FCE03B,
		HUD_CTX_SHARP_SHOOTER_EVENT = 0xFC0F918A,
		HUD_CTX_SHOP_OBJECTIVE = 0x21559C0D,
		HUD_CTX_SHOWDOWN_OUTRO = 0x3B945AA6,
		HUD_CTX_SHOW_MP_DEATH_SCREEN = 0xEB55A95E,
		HUD_CTX_SKINNING_PROCESS = 0x7B14E96E,
		HUD_CTX_SLEEPING = 0x29C7D336,
		HUD_CTX_SP_INTRO_HORSE_ITEMS_RESTRICTED = 0x17BA2997,
		HUD_CTX_TITHING = 0x638E718A,
		HUD_CTX_TITHING_NOGANG_CASH = 0x19193F29,
		HUD_CTX_TRANSLATE_OVERLAY = 0x16D28E19,
		HUD_CTX_WATCHING_A_SHOW = 0x6339DDEF,
	}

	public enum eCursor
	{
		ARROW,
		ARROW_DIMMED,
		ARROW_UP,
		ARROW_DOWN,
		ARROW_LEFT,
		ARROW_RIGHT,
		ARROW_POSITIVE,
		ARROW_NEGATIVE,
		ARROW_TRIMMING,
		HAND_OPEN,
		HAND_GRAB,
		HAND_FINGER,
		HAND_FINGER_DIMMED,
		HAND_FINGER_UP,
		HAND_FINGER_DOWN,
		HAND_FINGER_LEFT,
		HAND_FINGER_RIGHT,
		HAND_FINGER_POSITIVE,
		HAND_FINGER_NEGATIVE,
		HAND_FINGER_TRIMMING,
		HAND_FINGER_DOLLAR,
	}
}
