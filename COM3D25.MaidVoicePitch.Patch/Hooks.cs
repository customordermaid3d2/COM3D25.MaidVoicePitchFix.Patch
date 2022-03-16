using CM3D2.ExternalSaveData.Managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D25.MaidVoicePitch.Patch
{
    internal class Hooks
    {
        static string[] plugins = new string[] { "CM3D2.MaidVoicePitch" };

        internal static void ForeArmFix(Maid maid)
        {
            foreach (string pluginName in plugins)
            {
                ExSaveData.SetBool(maid, pluginName, "FARMFIX", false);
            }
        }
    }
}
