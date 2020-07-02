using EXILED;

namespace BetterThanGameMasterCommands
{
    public class MainSettings : Plugin
    {
        public override string getName => "BetterThanGameMasterCommands";
        private SetEvents SetEvents;

        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RemoteAdminCommandEvent += SetEvents.OnRemoteAdminCommand;
            Events.WaitingForPlayersEvent += SetEvents.OnWaitingForPlayers;
            Events.PlayerSpawnEvent += SetEvents.OnPlayerSpawn;
            Events.Scp079TriggerTeslaEvent += SetEvents.On079TeslaGate;
            Events.TriggerTeslaEvent += SetEvents.OnPlayerTriggerTesla;
        }

        public override void OnDisable()
        {
            Events.RemoteAdminCommandEvent -= SetEvents.OnRemoteAdminCommand;
            Events.WaitingForPlayersEvent -= SetEvents.OnWaitingForPlayers;
            Events.PlayerSpawnEvent -= SetEvents.OnPlayerSpawn;
            Events.Scp079TriggerTeslaEvent -= SetEvents.On079TeslaGate;
            Events.TriggerTeslaEvent -= SetEvents.OnPlayerTriggerTesla;
        }

        public override void OnReload() { }
    }
}