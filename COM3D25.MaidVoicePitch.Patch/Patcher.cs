using BepInEx.Logging;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace COM3D25.MaidVoicePitch.Patch
{
    public class Patcher
    {
        // List of assemblies to patch
        public static IEnumerable<string> TargetDLLs { get; } = new[] { "COM3D2.MaidVoicePitch.Plugin.dll" };

        public static ManualLogSource log = Logger.CreateLogSource("MaidVoicePitchFix");

        public static void Initialize()
        {
            log.LogMessage($"Initialize");
            log.LogMessage($"Initialize");
        }

        // Patches the assemblies
        public static void Patch(AssemblyDefinition assembly)
        {
            // Patcher code here
            log.LogMessage($"Initialize {assembly.Name}");

            TypeDefinition type2 = AssemblyDefinition.ReadAssembly(Assembly.GetExecutingAssembly().Location).MainModule.GetType("COM3D25.MaidVoicePitch.Patch.Hooks");
            MethodDefinition method2 = type2.Methods.FirstOrDefault((MethodDefinition m) => m.Name.Equals("ForeArmFix"));

            TypeDefinition type = assembly.MainModule.GetType("CM3D2.MaidVoicePitch.Plugin", "ForeArmFixOptimized");
            MethodDefinition method = type.Methods.FirstOrDefault((MethodDefinition m) => m.Name.Equals("ForeArmFix"));

            ILProcessor ilProcessor = method.Body.GetILProcessor();

            // if ForeArmFix method Exception 
            // than call ExSaveData.SetBool(maid, MaidVoicePitch.PluginName, "FARMFIX", false);

            ExceptionHandler handler = new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = method.Body.Instructions.First(),
                TryEnd = method.Body.Instructions.Last(),
                HandlerStart = method2.Body.Instructions.First(),
                HandlerEnd = method2.Body.Instructions.Last(),
                //CatchType = method.Import(typeof(Exception)),
            };

            method.Body.ExceptionHandlers.Add(handler);
        }

        public static void Finish()
        {
            log.LogMessage($"Finish");
        }
    }

}
