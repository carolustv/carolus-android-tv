using System;
using Android.Support.V17.Leanback.App;
using Com.Google.Android.Exoplayer2;
using Com.Google.Android.Exoplayer2.Trackselection;
using Com.Google.Android.Exoplayer2.Util;
using TvLeanback.Player;

namespace TvLeanback
{
    public class PlaybackFragment : VideoFragment
    {
        private const int UPDATE_DELAY = 16;

        VideoPlayerGlue playerGlue;
        SimpleExoPlayer player;
        TrackSelector trackSelector;
        LeanbackPlayerAdapter playerAdapter;
        PlaylistActionListener playlistActionListener;

        Movie mMovie;

        public PlaybackFragment()
        {
        }

        public override void OnCreate(Android.OS.Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override void OnStart()
        {
            base.OnStart();

            if (Util.SdkInt > 23) {
                releasePlayer();
            }
        }

        public override void OnResume()
        {
            base.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
        }

        public override void OnStop()
        {
            base.OnStop();
        }

        void releasePlayer()
        {
            if (player != null)
            {
                player.Release();
                player = null;
                trackSelector = null;
                playerGlue = null;
                playerAdapter = null;
                playlistActionListener = null;
            }
        }
    }
}
