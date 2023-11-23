﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using BiliLite.LibPlayers.MediaInfos;
using BiliLite.Models.Common;
using BiliLite.Models.Common.Player;
using BiliLite.Models.Common.Video.PlayUrlInfos;
using BiliLite.Models.Exceptions;
using BiliLite.Player;
using BiliLite.Player.Controllers;
using BiliLite.Player.States.ContentStates;
using BiliLite.Player.States.PauseStates;
using BiliLite.Player.States.PlayStates;
using BiliLite.Player.States.ScreenStates;
using BiliLite.Services;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板


namespace BiliLite.Controls
{
    public sealed partial class Player2 : UserControl, IPlayer
    {
        private readonly PlayerConfig m_playerConfig;
        private readonly BasePlayerController m_playerController;
        private readonly BiliVideoPlayer m_player;
        private readonly RealPlayInfo m_realPlayInfo;

        public Player2()
        {
            this.InitializeComponent();

            m_playerConfig = new PlayerConfig();
            //PreLoadSetting();
            m_playerController = PlayerControllerFactory.Create(PlayerType.Live);
            m_player = new BiliVideoPlayer(m_playerConfig, mediaPlayerVideo, mediaPlayerAudio, m_playerController);
            m_realPlayInfo = new RealPlayInfo();
            m_realPlayInfo.IsAutoPlay = true;
            m_playerController.SetPlayer(m_player);
            m_player.SetRealPlayInfo(m_realPlayInfo);
            InitPlayerEvent();
        }

        public PlayState PlayState { get; set; }

        public PlayMediaType PlayMediaType { get; set; }

        public VideoPlayHistoryHelper.ABPlayHistoryEntry ABPlay { get; set; }

        public double Position { get; set; }

        public double Duration { get; set; }

        public double Volume { get; set; }

        public bool Buffering { get; set; }

        public double BufferCache { get; set; }

        public double Rate { get; set; }

        public string MediaInfo { get; set; }

        public bool Opening { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<PlayState> PlayStateChanged;
        public event EventHandler PlayMediaOpened;
        public event EventHandler PlayMediaEnded;
        public event EventHandler<string> PlayMediaError;
        public event EventHandler<ChangePlayerEngine> ChangeEngine;

        private void InitPlayerEvent()
        {
            m_playerController.PlayStateChanged += PlayerController_PlayStateChanged;
            m_playerController.PauseStateChanged += PlayerController_PauseStateChanged;
            m_playerController.ContentStateChanged += PlayerController_ContentStateChanged;
            m_playerController.ScreenStateChanged += PlayerController_ScreenStateChanged;
            m_player.ErrorOccurred += Player_ErrorOccurred;
            m_playerController.MediaInfosUpdated += PlayerController_MediaInfosUpdated; 
        }

        private async void PlayerController_MediaInfosUpdated(object sender, MediaInfo e)
        {
        }

        private async void PlayerController_ScreenStateChanged(object sender, ScreenStateChangedEventArgs e)
        {
        }

        private async void PlayerController_ContentStateChanged(object sender, ContentStateChangedEventArgs e)
        {
        }

        private async void PlayerController_PauseStateChanged(object sender, PauseStateChangedEventArgs e)
        {
        }

        private async void Player_ErrorOccurred(object sender, PlayerException e)
        {
        }

        private async void PlayerController_PlayStateChanged(object sender, PlayStateChangedEventArgs e)
        {
        }

        public async Task<PlayerOpenResult> PlayerDashUseNative(BiliDashPlayUrlInfo dashInfo, string userAgent, string referer, double positon = 0)
        {
            m_realPlayInfo.PlayUrls.DashVideoUrl = dashInfo.Video.Url;
            m_realPlayInfo.PlayUrls.DashAudioUrl = dashInfo.Audio.Url;
            m_playerConfig.UserAgent = userAgent;
            m_playerConfig.Referer = referer;
            await m_playerController.PlayState.Load();
            return new PlayerOpenResult()
            {
                result = true
            };
        }

        public Task<PlayerOpenResult> PlayerSingleMp4UseNativeAsync(string url, double positon = 0, bool needConfig = true, bool isLocal = false)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerOpenResult> PlayDashUseFFmpegInterop(BiliDashPlayUrlInfo dashPlayUrlInfo, string userAgent, string referer, double positon = 0,
            bool needConfig = true, bool isLocal = false)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerOpenResult> PlayDashUrlUseFFmpegInterop(string url, string userAgent, string referer, double positon = 0,
            bool needConfig = true)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerOpenResult> PlaySingleFlvUseFFmpegInterop(string url, string userAgent, string referer, double positon = 0,
            bool needConfig = true)
        {
            throw new NotImplementedException();
        }

        public Task<PlayerOpenResult> PlaySingleFlvUseSYEngine(string url, string userAgent, string referer, double positon = 0, bool needConfig = true,
            string epId = "")
        {
            throw new NotImplementedException();
        }

        public Task<PlayerOpenResult> PlayVideoUseSYEngine(List<BiliFlvPlayUrlInfo> urls, string userAgent, string referer, double positon = 0, bool needConfig = true,
            string epId = "", bool isLocal = false)
        {
            throw new NotImplementedException();
        }

        public void SetRatioMode(int mode)
        {
        }

        public void SetPosition(double position)
        {
        }

        public void Pause()
        {
        }

        public void Play()
        {
        }

        public void SetRate(double value)
        {
        }

        public void ClosePlay()
        {
        }

        public void SetVolume(double volume)
        {
        }

        public string GetMediaInfo()
        {
            return "";
        }

        public void Dispose()
        {
        }
    }
}
