﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using TvLeanback.Models;

namespace TvLeanback.Player
{
    public class VideoLoaderCallbacks : Java.Lang.Object, LoaderManager.ILoaderCallbacks
    {
        readonly Playlist playlist;

        public VideoLoaderCallbacks(Playlist playlist)
        {
            this.playlist = playlist;
        }

        public Loader OnCreateLoader(int id, Bundle args)
        {
            throw new NotImplementedException();
        }

        public void OnLoaderReset(Loader loader)
        {
            throw new NotImplementedException();
        }

        public void OnLoadFinished(Loader loader, Java.Lang.Object data)
        {
            throw new NotImplementedException();
        }
    }
}
