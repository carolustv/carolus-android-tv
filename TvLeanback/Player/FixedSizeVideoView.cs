using Android.Content;
using Android.Util;
using Android.Widget;

namespace TvLeanback.Player
{
    public class FixedSizeVideoView : VideoView
    {
        public FixedSizeVideoView(Context ctx) : base(ctx) { }
        public FixedSizeVideoView(Context ctx, IAttributeSet attrs) : base(ctx, attrs) { }
        public FixedSizeVideoView(Context ctx, IAttributeSet attrs, int defStyle) : base(ctx, attrs, defStyle) { }

        int videoWidth;
        int videoHeight;

        public void SetVideoSize(int videoWidth, int videoHeight){
            this.videoWidth = videoWidth;
            this.videoHeight = videoHeight;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            // Log.i("@@@", "onMeasure");
            int width = GetDefaultSize(videoWidth, widthMeasureSpec);
            int height = GetDefaultSize(videoHeight, heightMeasureSpec);
            if (videoWidth > 0 && videoHeight > 0)
            {
                if (videoWidth * height > width * videoHeight)
                {
                    // Log.i("@@@", "image too tall, correcting");
                    height = width * videoHeight / videoWidth;
                }
                else if (videoWidth * height < width * videoHeight)
                {
                    // Log.i("@@@", "image too wide, correcting");
                    width = height * videoWidth / videoHeight;
                }
                else
                {
                    // Log.i("@@@", "aspect ratio is correct: " +
                    // width+"/"+height+"="+
                    // mVideoWidth+"/"+mVideoHeight);
                }
            }
            // Log.i("@@@", "setting size: " + width + 'x' + height);
            SetMeasuredDimension(width, height);
        }
    }
}