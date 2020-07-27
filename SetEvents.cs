using Exiled.API.Features;
using Exiled.Events.EventArgs;
using UnityEngine;

namespace BetterThanGameMasterCommands
{
    public class SetEvents
    {
        internal void OnSendingRemoteAdminCommand(SendingRemoteAdminCommandEventArgs ev)
        {
            bool silent = false;
            if (ev.Arguments.Count > 0 && ev.Arguments[0].ToLower() == "silent")
                silent = true;
            switch (ev.Name)
            {
                case "tesla":
                    ev.IsAllowed = false;
                    Global.enableTesla = !Global.enableTesla;
                    if (Global.enableTesla)
                    {
                        if (!silent)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("Tesla Gates have been activated", 0.0f, 0.0f);
                        ev.Sender.RemoteAdminMessage("Tesla enabled");
                    }
                    else
                    {
                        if (!silent)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("Tesla Gates have been deactivated", 0.0f, 0.0f);
                        ev.Sender.RemoteAdminMessage("Tesla disabled");
                    }
                    return;
                case "removeitems":
                    ev.IsAllowed = false;
                    if (ev.Arguments.Count != 1)
                    {
                        ev.Sender.RemoteAdminMessage("Неправильное использование", false);
                        return;
                    }
                    if (Player.Get(ev.Arguments[0]) == null)
                    {
                        ev.Sender.RemoteAdminMessage("Игрок не найден", false);
                        return;
                    }
                    Player.Get(ev.Arguments[0]).ClearInventory();
                    ev.Sender.RemoteAdminMessage("У игрока " + Player.Get(ev.Arguments[0]).Nickname + " были удалены все предметы");
                    return;
                case "mtf682":
                    ev.IsAllowed = false;
                    Global.mtf682spawn = !Global.mtf682spawn;
                    if (Global.mtf682spawn)
                        ev.Sender.RemoteAdminMessage("MTF Spawn set to GATE A");
                    else
                        ev.Sender.RemoteAdminMessage("MTF Spawn set to GATE B (default)");
                    return;
                case "slay":
                    ev.IsAllowed = false;
                    if (ev.Arguments.Count != 1)
                    {
                        ev.Sender.RemoteAdminMessage("Неправильное использование", false);
                        return;
                    }
                    if (Player.Get(ev.Arguments[0]) == null)
                    {
                        ev.Sender.RemoteAdminMessage("Игрок не найден", false);
                        return;
                    }
                    Player.Get(ev.Arguments[0]).IsGodModeEnabled = false;
                    Player.Get(ev.Arguments[0]).ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(999999, ev.Sender.Nickname, DamageTypes.None, Player.Get(ev.Arguments[0]).Id), Player.Get(ev.Arguments[0]).GameObject);
                    ev.Sender.RemoteAdminMessage("Игрок " + Player.Get(ev.Arguments[0]).Nickname + " был убит");
                    return;
            }
        }

        internal void OnSpawning(SpawningEventArgs ev)
        {
            if (Global.mtf682spawn)
            {
                if (ev.Player.Team == Team.MTF && ev.Player.Role != RoleType.FacilityGuard)
                {
                    ev.Position = new Vector3(Global.rand.Next(-15, -5), 988.5f, Global.rand.Next(-65, -55));
                }
            }
        }

        internal void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            ev.IsTriggerable = Global.enableTesla;
        }

        internal void OnInteractionTesla(InteractingTeslaEventArgs ev)
        {
            ev.IsAllowed = Global.enableTesla;
        }

        internal void OnWaitingForPlayers()
        {
            Global.enableTesla = true;
        }
    }
}