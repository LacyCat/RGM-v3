using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Firearms.Attachments;
using MEC;
using UnityEngine;

namespace RGM.Modes;

public class ABattleEventHandler(ABattle aBattle)
{
    internal void RegisterEvents()
    {
        Exiled.Events.Handlers.Player.Jumping += OnJumping;
    }

    private void OnJumping(JumpingEventArgs ev)
    {
        if (Physics.Raycast(ev.Player.Position, Vector3.down, out var hit, 5, (LayerMask)1))
        {
            if (hit.transform != null)
            {
                var controller = hit.transform.GetComponentInParent<WorkstationController>();

                if (controller != null)
                {
                    if (!aBattle.PlayerWorkstations.TryGetValue(ev.Player, out var workstations))
                    {
                        Log.Info("No key");

                        aBattle.PlayerWorkstations.Add(ev.Player, [controller]);

                        aBattle.StartSelect(ev.Player);
                    }
                    else
                    {
                        Log.Info("Key");

                        if (!workstations.Contains(controller))
                        {
                            Log.Info("No workstation");

                            workstations.Add(controller);

                            aBattle.StartSelect(ev.Player);
                        }
                        else
                        {
                            Log.Info("Workstation");
                        }
                    }
                }
                else
                {
                    Log.Info("No workstationcon hit");
                }
            }
            else
            {
                Log.Info("No workstation hit");
            }
        }
        else
        {
            Log.Info("No raycast hit");
        }
    }
}