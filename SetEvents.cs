using EXILED;
using EXILED.Extensions;
using UnityEngine;

namespace BetterThanGameMasterCommands
{
    internal class SetEvents
    {
        public void On079TeslaGate(ref Scp079TriggerTeslaEvent ev)
        {
            ev.Allow = Global.enableTesla;
        }
        public void OnPlayerTriggerTesla(ref TriggerTeslaEvent ev)
        {
            ev.Triggerable = Global.enableTesla;
        }
        internal void OnRemoteAdminCommand(ref RACommandEvent ev)
        {
            string[] args = ev.Command.Split(' ');
            bool silent = false;
            if (args.Length > 1 && args[1].ToLower() == "silent")
                silent = true;
            switch (args[0])
            {
                case "tesla":
                    Global.enableTesla = !Global.enableTesla;
                    if (Global.enableTesla)
                    {
                        if (!silent)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("Tesla Gates have been activated", 0.0f, 0.0f);
                        ev.Sender.RAMessage("Tesla enabled");
                    }
                    else
                    {
                        if (!silent)
                            NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase("Tesla Gates have been deactivated", 0.0f, 0.0f);
                        ev.Sender.RAMessage("Tesla disabled");
                    }
                    return;
                case "removeitems":
                    if (args.Length != 2)
                    {
                        ev.Sender.RAMessage("Неправильное использование", false);
                        return;
                    }
                    if (Player.GetPlayer(args[1]) == null)
                    {
                        ev.Sender.RAMessage("Игрок не найден", false);
                        return;
                    }
                    Player.GetPlayer(args[1]).ClearInventory();
                    ev.Sender.RAMessage("У игрока " + Player.GetPlayer(args[1]).nicknameSync.Network_myNickSync + " были удалены все предметы");
                    return;
                case "mtf682":
                    Global.mtf682spawn = !Global.mtf682spawn;
                    if (Global.mtf682spawn)
                        ev.Sender.RAMessage("MTF Spawn set to GATE A");
                    else
                        ev.Sender.RAMessage("MTF Spawn set to GATE B (default)");
                    return;
                case "slay":
                    if (args.Length != 2)
                    {
                        ev.Sender.RAMessage("Неправильное использование", false);
                        return;
                    }
                    if (Player.GetPlayer(args[1]) == null)
                    {
                        ev.Sender.RAMessage("Игрок не найден", false);
                        return;
                    }
                    Player.GetPlayer(args[1]).SetGodMode(false);
                    Player.GetPlayer(args[1]).playerStats.HurtPlayer(new PlayerStats.HitInfo(999999, ev.Sender.Nickname, DamageTypes.None, Player.GetPlayer(args[1]).GetPlayerId()), Player.GetPlayer(args[1]).gameObject);
                    ev.Sender.RAMessage("Игрок " + Player.GetPlayer(args[1]).nicknameSync.Network_myNickSync + " был убит");
                    return;
            }
        }

        internal void OnPlayerSpawn(PlayerSpawnEvent ev)
        {
            if (Global.mtf682spawn)
            {
                if (ev.Player.GetTeam() == Team.MTF && ev.Player.GetRole() != RoleType.FacilityGuard)
                {
                    ev.Spawnpoint = new Vector3(Global.rand.Next(-15, -5), 988.5f, Global.rand.Next(-65, -55));
                }
            }
        }

        internal void OnWaitingForPlayers()
        {
            Global.enableTesla = true;
        }
    }
}