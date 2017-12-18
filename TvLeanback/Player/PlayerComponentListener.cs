using System;
using Android.Runtime;
using Android.Views;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Source;
using Com.Google.Android.Exoplayer2.Trackselection;

namespace TvLeanback.Player
{
    class PlayerComponentListener : IPlayerEventListener, SimpleExoPlayer.IVideoListener, ISurfaceHolderCallback
    {
        LeanbackPlayerAdapter adapter; 

        public PlayerComponentListener(LeanbackPlayerAdapter adapter) {
            this.adapter = adapter;
        }

        public IntPtr Handle => adapter.Handle;

        public void Dispose()
        {
        }

        public void InvokeOnRenderedFirstFrame()
        {
        }

        public void InvokeOnVideoSizeChanged(int width, int height, int unappliedRotationDegrees, float pixelWidthHeightRatio)
        {
            adapter.GetCallback().OnVideoSizeChanged(adapter, width, height);
        }

        public void OnLoadingChanged(bool p0)
        {
        }

        public void OnPlaybackParametersChanged(PlaybackParameters p0)
        {
        }

        public void OnPlayerError(ExoPlaybackException exception)
        {
            // TODO
        }

        public void OnPlayerStateChanged(bool p0, int p1)
        {
            adapter.NotifyListeners();
        }

        public void OnPositionDiscontinuity(int p0)
        {
            var callback = adapter.GetCallback();
            callback.OnCurrentPositionChanged(adapter);
            callback.OnBufferedPositionChanged(adapter);
        }

        public void OnRepeatModeChanged(int repeatMode)
        {
        }

        public void OnSeekProcessed()
        {
        }

        public void OnShuffleModeEnabledChanged(bool p0)
        {
        }

        public void OnTimelineChanged(Timeline p0, Java.Lang.Object p1)
        {
            var callback = adapter.GetCallback();
            callback.OnDurationChanged(adapter);
            callback.OnCurrentPositionChanged(adapter);
            callback.OnBufferedPositionChanged(adapter);
        }

        public void OnTracksChanged(TrackGroupArray p0, TrackSelectionArray p1)
        {
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Android.Graphics.Format format, int width, int height)
        {
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            adapter.SetVideoSurface((Surface)holder);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            adapter.SetVideoSurface(null);
        }
    }
}
