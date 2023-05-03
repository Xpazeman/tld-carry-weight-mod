using Il2Cpp;
using ModSettings;

namespace CarryWeightMod
{
    internal class CarryWeightModSettings : JsonModSettings
    {
        [Section("Carry Capacity")]

        [Name("(Mostly) Infinite carry")]
        [Description("Toggling this on raises your carry capacity to 10000Kg.")]
        public bool infiniteCarry = false;

        [Name("Carry capacity to add (Kg)")]
        [Description("How many Kg will be added to your carry capacity.")]
        [Slider(0, 120)]
        public int carryKgAdd = 0;

        protected override void OnConfirm()
        {
            base.OnConfirm();

            Encumber encumberComp = GameManager.GetEncumberComponent();

            if (encumberComp != null)
            {
                CarryWeightMod.EncumberUpdate(encumberComp);
            }
        }
    }

    internal static class Settings
    {
        public static CarryWeightModSettings options;

        public static void OnLoad()
        {
            options = new CarryWeightModSettings();
            options.AddToModSettings("Carry Weight Mod");
        }
    }
}
