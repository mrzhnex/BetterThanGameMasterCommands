﻿using Exiled.API.Interfaces;

namespace BetterThanGameMasterCommands
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
    }
}