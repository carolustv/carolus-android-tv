using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace TvLeanback.Player
{
    public class VideoLoaderCallbacks : LoaderManager.ILoaderCallbacks
    {
        public IntPtr Handle => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
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
