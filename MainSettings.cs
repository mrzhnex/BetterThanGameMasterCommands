using EXILED;

namespace BetterThanGameMasterCommands
{
    public class MainSettings : Plugin
    {
        public override string getName => nameof(BetterThanGameMasterCommands);
        public SetEvents SetEvents { get; set; }

        public override void OnEnable()
        {
            SetEvents = new SetEvents();
            Events.RemoteAdminCommandEvent += SetEvents.OnRemoteAdminCommand;
            Events.WaitingForPlayersEvent += SetEvents.OnWaitingForPlayers;
            Events.PlayerSpawnEvent += SetEvents.OnPlayerSpawn;
            Events.Scp079TriggerTeslaEvent += SetEvents.On079TeslaGate;
            Events.TriggerTeslaEvent += SetEvents.OnPlayerTriggerTesla;
            Log.Info(getName + " on");
        }

        public override void OnDisable()
        {
            Events.RemoteAdminCommandEvent -= SetEvents.OnRemoteAdminCommand;
            Events.WaitingForPlayersEvent -= SetEvents.OnWaitingForPlayers;
            Events.PlayerSpawnEvent -= SetEvents.OnPlayerSpawn;
            Events.Scp079TriggerTeslaEvent -= SetEvents.On079TeslaGate;
            Events.TriggerTeslaEvent -= SetEvents.OnPlayerTriggerTesla;
            Log.Info(getName + " off");
        }

        public override void OnReload() { }
    }
}