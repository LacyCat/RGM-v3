using System.Collections.Generic;
using UnityEngine;
using Exiled.API.Features.Pools;
using HarmonyLib;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace RGM.Modes.ABattlePatches
{
    public class InventoryUpdatePatch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            var newInstructions = ListPool<CodeInstruction>.Pool.Get(instructions);

            var index = newInstructions.FindLastIndex(instruction => instruction.Calls(PropertyGetter(typeof(Behaviour), nameof(Behaviour.enabled))));

            var inequalityOp = typeof(UnityEngine.Object).GetMethod("op_Inequality", new[] { typeof(UnityEngine.Object), typeof(UnityEngine.Object) });

            index--;

            var jumpIndex = index + 5;

            var label = generator.DefineLabel();
            newInstructions[jumpIndex].WithLabels(label);

            newInstructions.InsertRange(index, [
                new CodeInstruction(OpCodes.Ldloc_1),
                    new CodeInstruction(OpCodes.Ldnull),
                    new CodeInstruction(OpCodes.Call, inequalityOp),
                    new CodeInstruction(OpCodes.Brfalse_S, label),
                ]);

            for (var i = 0; i < newInstructions.Count; i++)
                yield return newInstructions[i];

            ListPool<CodeInstruction>.Pool.Return(newInstructions);
        }
    }

}