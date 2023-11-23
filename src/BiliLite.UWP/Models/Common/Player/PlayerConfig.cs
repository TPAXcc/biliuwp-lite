﻿namespace BiliLite.Models.Common.Player
{
    public class PlayerConfig
    {
        public bool EnableHw { get; set; }

        public LivePlayerMode PlayMode { get; set; }

        public int SelectedRouteLine { get; set; } = 0;

        public string UserAgent { get; set; }

        public string Referer { get; set; }
    }
}
