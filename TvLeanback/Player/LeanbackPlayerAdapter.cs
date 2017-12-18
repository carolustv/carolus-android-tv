using System;
using Android.Content;
using Android.OS;
using Android.Support.V17.Leanback.Media;
using Android.Views;
using Com.Google.Android.Exoplayer2;
using Java.Lang;

namespace TvLeanback.Player
{
    public class LeanbackPlayerAdapter : PlayerAdapter
    {
        Context context;
        SimpleExoPlayer player;
        Handler handler;
        ISurfaceHolderGlueHost surfaceHolderGlueHost;
        PlayerComponentListener componentListener;
        Runnable updatePlayerRunnable;

        bool initialized;
        bool isBuffering;
        bool hasSurface;

        int stateIdle = 1;
        int stateBuffering = 2;
        int stateReady = 3;
        int stateEnded = 4;

        public LeanbackPlayerAdapter(
            Context context,
            SimpleExoPlayer player,
            int updatePeriodMs)
        {
            this.context = context;
            this.player = player;
            handler = new Handler();
            componentListener = new PlayerComponentListener(this);
            Action action = null;
            action = () =>
                    {
                        Callback callback = GetCallback();
                        callback.OnCurrentPositionChanged(this);
                        callback.OnBufferedPositionChanged(this);
                        handler.PostDelayed(action, updatePeriodMs);
                    };
            updatePlayerRunnable = new Runnable(action);
        }

        public override void OnAttachedToHost(PlaybackGlueHost host)
        {
            if (host is ISurfaceHolderGlueHost) {
                surfaceHolderGlueHost = ((ISurfaceHolderGlueHost)host);
                surfaceHolderGlueHost.SetSurfaceHolderCallback(componentListener);
            }
            NotifyListeners();
            player.AddListener(componentListener);
        }

        public void NotifyListeners()
        {
            var oldIsPrepared = IsPrepared;
            int playbackState = player.PlaybackState;
            var isInitialized = playbackState != stateIdle;
            isBuffering = playbackState == stateBuffering;
            var hasEnded = playbackState == stateEnded;

            initialized = isInitialized;
            Callback callback = GetCallback();
            if (oldIsPrepared != IsPrepared)
            {
                callback.OnPreparedStateChanged(this);
            }
            callback.OnPlayStateChanged(this);
            callback.OnBufferingStateChanged(this, isBuffering || !initialized);

            if (hasEnded)
            {
                callback.OnPlayCompleted(this);
            }
        }

        public override void OnDetachedFromHost()
        {
            //player.RemoveListener(componentListener);
            if (surfaceHolderGlueHost != null)
            {
                surfaceHolderGlueHost.SetSurfaceHolderCallback(null);
                surfaceHolderGlueHost = null;
            }
            initialized = false;
            hasSurface = false;
            Callback callback = GetCallback();
            callback.OnBufferingStateChanged(this, false);
            callback.OnPlayStateChanged(this);
            callback.OnPreparedStateChanged(this);
        }

        public override void SetProgressUpdatingEnabled(bool enable)
        {
            handler.RemoveCallbacks(updatePlayerRunnable);
            if (enable)
            {
                handler.Post(updatePlayerRunnable);
            }
        }

        public override bool IsPlaying
        {
            get
            {
                return initialized && player.PlayWhenReady;
            }
        }

        public override long Duration
        {
            get
            {
                long durationMs = player.Duration;
                return durationMs != C.TimeUnset ? durationMs : -1;
            }
        }

        public override long CurrentPosition
        {
            get
            {
                return initialized ? player.CurrentPosition : -1;;
            }
        }

        public override void Pause()
        {
            player.PlayWhenReady = false;
            GetCallback().OnPlayStateChanged(this);
        }

        public override void Play()
        {
            if (player.PlaybackState == stateEnded)
            {
                player.SeekToDefaultPosition();
            }
            player.PlayWhenReady = true;
            GetCallback().OnPlayStateChanged(this);
        }

        public override void SeekTo(long positionInMs)
        {
            player.SeekTo(positionInMs);
        }

        public override long BufferedPosition
        {
            get
            {
                return player.BufferedPosition;
            }
        }

        public override bool IsPrepared
        {
            get
            {
                return initialized && (surfaceHolderGlueHost == null || hasSurface);
            }
        }

        public void SetVideoSurface(Surface surface)
        {
            hasSurface = surface != null;
            player.SetVideoSurface(surface);
            GetCallback().OnPreparedStateChanged(this);
        }
    }
}
