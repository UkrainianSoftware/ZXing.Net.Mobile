using System;

namespace ZXing.Net.Mobile.Forms.iOS
{
	public static class Platform
	{
		public static void Init()
		{
			ZXing.Net.Mobile.Forms.iOS.ZXingScannerViewRenderer.Init();
			ZXing.Net.Mobile.Forms.iOS.ZXingBarcodeImageViewRenderer.Init();

#if __FORK_FOR_ORION__
			ZXing.Mobile.MobileBarcodeScanner.PlatformScannerFactory =
				new ZXing.Mobile.Ios.MobileBarcodeScannerFactoryIos();
#endif
		}
	}
}
