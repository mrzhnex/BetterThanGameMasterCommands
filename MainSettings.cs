using Exiled.API.Features;

namespace BetterThanGameMasterCommands
{
    public class MainSettings : Plugin<Config>
    {
        public override string Name => nameof(BetterThanGameMasterCommands);
        public SetEvents SetEvents { get; set; }

        public override void OnEnabled()
        {
            SetEvents = new SetEvents();
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += SetEvents.OnSendingRemoteAdminCommand;
            Exiled.Events.Handlers.Server.WaitingForPlayers += SetEvents.OnWaitingForPlayers;
            Exiled.Events.Handlers.Scp079.InteractingTesla += SetEvents.OnInteractionTesla;
            Exiled.Events.Handlers.Player.TriggeringTesla += SetEvents.OnTriggeringTesla;
            Exiled.Events.Handlers.Player.Spawning += SetEvents.OnSpawning;
            Log.Info(Name + " on");
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= SetEvents.OnSendingRemoteAdminCommand;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= SetEvents.OnWaitingForPlayers;
            Exiled.Events.Handlers.Scp079.InteractingTesla -= SetEvents.OnInteractionTesla;
            Exiled.Events.Handlers.Player.TriggeringTesla -= SetEvents.OnTriggeringTesla;
            Exiled.Events.Handlers.Player.Spawning -= SetEvents.OnSpawning;
            Log.Info(Name + " off");
        }
    }
}