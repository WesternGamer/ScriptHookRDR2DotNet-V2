using System;
using System.Runtime.CompilerServices;
using RDR2.Native;

namespace RDR2.UI
{
	public sealed class Prompt : IHandleable
	{
		public int Handle { get; private set; }

		/// <summary>
		/// Create a interactable HUD prompt.
		/// </summary>
		/// <param name="control">The prompt control action</param>
		/// <param name="mode">The prompt mode/type</param>
		/// <param name="text">The prompt text</param>
		/// <param name="numMashes">The number of mashes required to fill the prompt if <see cref="eUseContextMode"/> is mashable</param>
		/// <param name="holdTime">The amount of time it takes to fill the prompt by holding it</param>
		/// <param name="depletionTime">The amount of time it takes for the prompt to deplete</param>
		/// <param name="fillTime">The amount of time it takes to automatically fill the prompt</param>
		/// <param name="timedEvent">Timed event hash</param>
		public Prompt(eInputType control, eUseContextMode mode, string text, int numMashes = 0, int holdTime = 4000, int depletionTime = 4000, int fillTime = 4000, PromptTimingEvent timedEvent = 0)
		{
			Handle = HUD._UI_PROMPT_REGISTER_BEGIN();
			HUD._UI_PROMPT_SET_CONTROL_ACTION(Handle, (uint)control);
			Text = text;
			Priority = 3;

			switch (mode)
			{
				case eUseContextMode.Press:
					HUD._UI_PROMPT_SET_STANDARD_MODE(Handle, false);
					break;
				case eUseContextMode.TimedPress:
					HUD._UI_PROMPT_SET_PRESSED_TIMED_MODE(Handle, depletionTime);
					break;
				case eUseContextMode.Release:
					HUD._UI_PROMPT_SET_STANDARD_MODE(Handle, true);
					break;
				case eUseContextMode.Hold:
					HUD._UI_PROMPT_SET_HOLD_INDEFINITELY_MODE(Handle);
					break;
				case eUseContextMode.TimedEvent:
					HUD._UI_PROMPT_SET_STANDARDIZED_HOLD_MODE(Handle, (uint)timedEvent);
					break;
				case eUseContextMode.AutoFill:
					HUD._UI_PROMPT_SET_HOLD_AUTO_FILL_MODE(Handle, fillTime, holdTime);
					break;
				case eUseContextMode.AutoFillWithDecay:
					HUD._UI_PROMPT_SET_HOLD_AUTO_FILL_WITH_DECAY_MODE(Handle, fillTime, holdTime);
					break;
				case eUseContextMode.Mash:
					HUD._UI_PROMPT_SET_MASH_MODE(Handle, numMashes);
					break;
				case eUseContextMode.MashAutoFill:
					HUD._UI_PROMPT_SET_MASH_AUTO_FILL_MODE(Handle, fillTime, numMashes);
					break;
				case eUseContextMode.MashResistance:
					HUD._UI_PROMPT_SET_MASH_WITH_RESISTANCE_MODE(Handle, numMashes, 0.0f, 0.0f); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.MashResistanceCanFail:
					HUD._UI_PROMPT_SET_MASH_WITH_RESISTANCE_CAN_FAIL_MODE(Handle, numMashes, 0.0f, 0.0f); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.MashResistanceDynamic:
					HUD._UI_PROMPT_SET_MASH_MANUAL_MODE(Handle, (1.0f / 10.0f), 0.0f, 0.0f, 0); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.MashResistanceDynamicCanFail:
					HUD._UI_PROMPT_SET_MASH_MANUAL_CAN_FAIL_MODE(Handle, (1.0f / 10.0f), 0.0f, 0.0f, 0); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.MashIndefinitely:
					HUD._UI_PROMPT_SET_MASH_INDEFINITELY_MODE(Handle);
					break;
				case eUseContextMode.Rotate:
					HUD._UI_PROMPT_SET_ROTATE_MODE(Handle, 1.0f, false);
					HUD._UI_PROMPT_SET_ATTRIBUTE(Handle, 10, true);
					break;
				case eUseContextMode.TargetMeter:
					HUD._UI_PROMPT_SET_TARGET_MODE(Handle, 0.5f, 0.1f, 0);
					break;
				default:
					break;
			}

			HUD._UI_PROMPT_REGISTER_END(Handle);
			Visible = false;
			Enabled = false;
		}

		private string _text;

		/// <summary>
		/// Gets or sets the text on this <see cref="Prompt"/>.
		/// </summary>
		public string Text
		{
			get => _text;
			set {
				// TODO: Doesn't work?
				_text = value;
				string varString = MISC.VAR_STRING(10, "LITERAL_STRING", value);
				HUD._UI_PROMPT_SET_TEXT(Handle, varString);
			}
		}

		/// <summary>
		/// Gets or sets the visibility on this <see cref="Prompt"/>.
		/// </summary>
		public bool Visible
		{
			get => HUD._UI_PROMPT_IS_ACTIVE(Handle);
			set => HUD._UI_PROMPT_SET_VISIBLE(Handle, value);
		}

		/// <summary>
		/// Gets or sets if this <see cref="Prompt"/> is enabled.
		/// </summary>
		public bool Enabled
		{
			get => HUD._UI_PROMPT_IS_ENABLED(Handle);
			set => HUD._UI_PROMPT_SET_ENABLED(Handle, value);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/> is pressed.
		/// </summary>
		public bool IsPressed => HUD._UI_PROMPT_IS_PRESSED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/> is just pressed.
		/// </summary>
		public bool IsJustPressed => HUD._UI_PROMPT_IS_JUST_PRESSED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/> is released.
		/// </summary>
		public bool IsReleased => HUD._UI_PROMPT_IS_RELEASED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/> is just pressed.
		/// </summary>
		public bool IsJustReleased => HUD._UI_PROMPT_IS_JUST_RELEASED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/>s hold mode has complete.
		/// </summary>
		public bool HasHoldModeCompleted => HUD._UI_PROMPT_HAS_HOLD_MODE_COMPLETED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/>s mash mode has completed.
		/// </summary>
		public bool HasMashModeCompleted => HUD._UI_PROMPT_HAS_MASH_MODE_COMPLETED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/>s mash mode has failed.
		/// </summary>
		public bool HasMashModeFailed => HUD._UI_PROMPT_HAS_MASH_MODE_FAILED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/>s timed event completed.
		/// </summary>
		public bool HasTimedEventCompleted => HUD._UI_PROMPT_HAS_PRESSED_TIMED_MODE_COMPLETED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/>s timed event failed.
		/// </summary>
		public bool HasTimedEventFailed => HUD._UI_PROMPT_HAS_PRESSED_TIMED_MODE_FAILED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/>s <see cref="eUseContextMode.Press"/> or <see cref="eUseContextMode.Release"/> mode has been completed.
		/// </summary>
		public bool HasCompleted => HUD._UI_PROMPT_HAS_STANDARD_MODE_COMPLETED(Handle, 0);

		/// <summary>
		/// Sets the <see cref="Prompt"/> priority level
		/// </summary>
		public int Priority
		{
			set => HUD._UI_PROMPT_SET_PRIORITY(Handle, value);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Prompt"/> is pulsing.
		/// </summary>
		public bool IsPulsing
		{
			get => HUD._UI_PROMPT_GET_URGENT_PULSING_ENABLED(Handle);
			set => HUD._UI_PROMPT_SET_URGENT_PULSING_ENABLED(Handle, value);
		}

		public bool Exists()
		{
			return HUD._UI_PROMPT_IS_VALID(Handle);
		}

		public void Delete()
		{
			HUD._UI_PROMPT_DELETE(Handle);
		}

		public bool Equals(Prompt other)
		{
			return !ReferenceEquals(null, other) && other.Handle == Handle;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((Prompt)obj);
		}

		public override int GetHashCode()
		{
			return Handle.GetHashCode();
		}
	}

	public enum eUseContextMode : int
	{
		Press,
		TimedPress,
		Release,
		Hold,
		TimedEvent,
		AutoFill,
		AutoFillWithDecay,
		Mash,
		MashAutoFill,
		MashResistance,
		MashResistanceCanFail,
		MashResistanceDynamic,
		MashResistanceDynamicCanFail,
		MashIndefinitely,
		/// <summary>
		/// Gamepad Only
		/// </summary>
		Rotate,
		TargetMeter,
	}

	public enum PromptTimingEvent : uint
	{
		Short = 0x65943D74,
		Medium = 0xCF1E51DE,
		Long = 0x87A8DDB3,
		RustlingCalmTiming = 0xBA926371,
		PlayerFocusTiming = 0x687E21CA,
		PlayerReactionTiming = 0x70BF5857,
	}

}
