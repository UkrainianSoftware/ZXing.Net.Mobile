using System;

using UIKit;


#if __FORK_FOR_ORION__
  using MobileBarcodeScannerForIosPlatform = ZXing.Mobile.Ios.MobileBarcodeScannerIos;
#else
  using MobileBarcodeScannerForIosPlatform = ZXing.Mobile.MobileBarcodeScanner;
#endif


namespace ZXing.Mobile
{
	public interface IScannerViewController
	{
		void Torch(bool on);

		void ToggleTorch();
		void Cancel();

		bool IsTorchOn { get; }
		bool ContinuousScanning { get; set; }

		void PauseAnalysis();
		void ResumeAnalysis();

		event Action<ZXing.Result> OnScannedResult;

		MobileBarcodeScanningOptions ScanningOptions { get; set; }


		// TODO: [alex-d] [xm-899] is it ok to replace this class?
		// -
		MobileBarcodeScannerForIosPlatform Scanner { get; set; }

		UIViewController AsViewController();
	}
}

