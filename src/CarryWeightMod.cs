using MelonLoader;
using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using Il2CppTLD.Gear;
using static Il2CppSystem.Xml.XmlWellFormedWriter.AttributeValueCache;

namespace CarryWeightMod
{
    class CarryWeightMod : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
        }

        public static void EncumberUpdate(Encumber encumberComp)
        {
            if (Settings.options.carryKgAdd > 0 || Settings.options.infiniteCarry)
            {
                int carryAdd = Settings.options.carryKgAdd;
                if (Settings.options.infiniteCarry) carryAdd = 9970;

                encumberComp.m_MaxCarryCapacityKG = 30f + carryAdd;
                encumberComp.m_MaxCarryCapacityWhenExhaustedKG = 15f + carryAdd;
                encumberComp.m_NoSprintCarryCapacityKG = 40f + carryAdd;
                encumberComp.m_NoWalkCarryCapacityKG = 60f + carryAdd;
                encumberComp.m_EncumberLowThresholdKG = 31f + carryAdd;
                encumberComp.m_EncumberMedThresholdKG = 40f + carryAdd;
                encumberComp.m_EncumberHighThresholdKG = 60f + carryAdd;
            }
            else
            {
                encumberComp.m_MaxCarryCapacityKG = 30f;
                encumberComp.m_MaxCarryCapacityWhenExhaustedKG = 15f;
                encumberComp.m_NoSprintCarryCapacityKG = 40f;
                encumberComp.m_NoWalkCarryCapacityKG = 60f;
                encumberComp.m_EncumberLowThresholdKG = 31f;
                encumberComp.m_EncumberMedThresholdKG = 40f;
                encumberComp.m_EncumberHighThresholdKG = 60f;
            }
        }
    }
}
