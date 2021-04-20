using System;
using Android.Hardware;
using ApxLabs.FastAndroidCamera;

#if __FORK_FOR_ORION__
  using ZxingMobileAndroidResource = ZXing.Net.Mobile.Droid.Resource;
  using MobileBarcodeScannerForDroidPlatform = ZXing.Mobile.Droid.MobileBarcodeScannerDroid;
#else
  using ZxingMobileAndroidResource = ZXing.Net.Mobile.Resource;
  using MobileBarcodeScannerForDroidPlatform = ZXing.Mobile.MobileBarcodeScanner;
#endif

namespace ZXing.Mobile.CameraAccess
{
	public class CameraEventsListener : Java.Lang.Object, INonMarshalingPreviewCallback, Camera.IAutoFocusCallback
	{
		public event EventHandler<FastJavaByteArray> OnPreviewFrameReady;

		public void OnPreviewFrame(IntPtr data, Camera camera)
		{
			using (var fastArray = new FastJavaByteArray(data))
			{
				OnPreviewFrameReady?.Invoke(this, fastArray);

				camera.AddCallbackBuffer(fastArray);
			}
		}

		public void OnAutoFocus(bool success, Camera camera)
		{
			Android.Util.Log.Debug(MobileBarcodeScannerForDroidPlatform.TAG, "AutoFocus {0}", success ? "Succeeded" : "Failed");
		}
	}
}