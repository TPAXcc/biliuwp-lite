﻿using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using BiliLite.Controls;
using BiliLite.Models.Common;
using BiliLite.Models.Common.Player;
using BiliLite.Player.SubPlayers;
using BiliLite.Services;
using FFmpegInteropX;

namespace BiliLite.Extensions
{
    public static class PlayerExtensions
    {
        private static Dictionary<PlayerRatioMode, Action<MediaPlayerElement, RealPlayInfo>> RatioModeHandlerMap =
            new Dictionary<PlayerRatioMode, Action<MediaPlayerElement, RealPlayInfo>>()
            {
                { PlayerRatioMode.Default , (MediaPlayerElement playerElement,RealPlayInfo realPlayInfo) =>
                {
                    playerElement.Width = double.NaN;
                    playerElement.Height = double.NaN;
                    playerElement.Stretch = Stretch.Uniform;
                }}
            };

        private static void HandleRatioModeDefault(Player2 player, MediaPlayerElement playerElement, RealPlayInfo realPlayInfo)
        {
            playerElement.Width = double.NaN;
            playerElement.Height = double.NaN;
            playerElement.Stretch = Stretch.Uniform;
        }

        //private static void HandleRatioModeFill(Player2 player, MediaPlayerElement playerElement, RealPlayInfo realPlayInfo)
        //{
        //    playerElement.Width = double.NaN;
        //    playerElement.Height = double.NaN;
        //    playerElement.Stretch = Stretch.UniformToFill;
        //}

        //private static void HandleRatioMode16To9(Player2 player, MediaPlayerElement playerElement, RealPlayInfo realPlayInfo)
        //{
        //    playerElement.Width = double.NaN;
        //    playerElement.Height = double.NaN;
        //    playerElement.Stretch = Stretch.Uniform;
        //}

        //private static void HandleRatioMode4To3(Player2 player, MediaPlayerElement playerElement, RealPlayInfo realPlayInfo)
        //{
        //    playerElement.Width = double.NaN;
        //    playerElement.Height = double.NaN;
        //    playerElement.Stretch = Stretch.Uniform;
        //}

        //private static void HandleRatioModeCompatible(Player2 player, MediaPlayerElement playerElement, RealPlayInfo realPlayInfo)
        //{
        //    playerElement.Width = double.NaN;
        //    playerElement.Height = double.NaN;
        //    playerElement.Stretch = Stretch.Uniform;
        //}

        public static MediaSourceConfig ReloadFFmpegConfig(this MediaSourceConfig ffmpegConfig, string userAgent, string referer)
        {
            var passThrough = SettingService.GetValue<bool>(SettingConstants.Player.HARDWARE_DECODING, true);
            if (!string.IsNullOrEmpty(userAgent) && !ffmpegConfig.FFmpegOptions.ContainsKey("user_agent"))
            {
                ffmpegConfig.FFmpegOptions.Add("user_agent", userAgent);
            }
            if (!string.IsNullOrEmpty(referer) && !ffmpegConfig.FFmpegOptions.ContainsKey("referer"))
            {
                ffmpegConfig.FFmpegOptions.Add("referer", referer);
            }

            ffmpegConfig.VideoDecoderMode = passThrough ? VideoDecoderMode.ForceSystemDecoder : VideoDecoderMode.ForceFFmpegSoftwareDecoder;
            return ffmpegConfig;
        }

        public static void SetRatioModeCore(this Player2 player, PlayerRatioMode mode,MediaPlayerElement playerElement,RealPlayInfo realPlayInfo)
        {
            switch (mode)
            {
                case PlayerRatioMode.Default:
                    playerElement.Width = double.NaN;
                    playerElement.Height = double.NaN;
                    playerElement.Stretch = Stretch.Uniform;
                    break;
                case PlayerRatioMode.Fill:
                    playerElement.Width = double.NaN;
                    playerElement.Height = double.NaN;
                    playerElement.Stretch = Stretch.UniformToFill;
                    break;
                case PlayerRatioMode.Ratio16To9:
                    playerElement.Stretch = Stretch.Fill;
                    playerElement.Height = player.ActualHeight;
                    playerElement.Width = player.ActualHeight * 16 / 9;
                    break;
                case PlayerRatioMode.Ratio4To3:
                    playerElement.Stretch = Stretch.Fill;
                    playerElement.Height = player.ActualHeight;
                    playerElement.Width = player.ActualHeight * 4 / 3;
                    break;
                case PlayerRatioMode.Compatible:
                    if (realPlayInfo != null)
                    {
                        if (((double)player.ActualWidth / (double)player.ActualHeight) <= ((double)realPlayInfo.Width / (double)realPlayInfo.Height))
                        {
                            /// 原视频长宽比大于等于窗口长宽比
                            playerElement.Stretch = Stretch.Fill;
                            playerElement.Width = player.ActualWidth;
                            playerElement.Height = player.ActualWidth * (double)realPlayInfo.Height / (double)realPlayInfo.Width;
                        }
                        else
                        {
                            /// 原视频长宽比小于窗口长宽比
                            playerElement.Stretch = Stretch.Fill;
                            playerElement.Width = player.ActualHeight * (double)realPlayInfo.Width / (double)realPlayInfo.Height;
                            playerElement.Height = player.ActualHeight;
                        }
                    }
                    else
                    {
                        /// 未获取到DASH视频信息则不缩放
                        playerElement.Stretch = Stretch.None;
                        playerElement.Width = double.NaN;
                        playerElement.Height = double.NaN;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
