using System;
using System.Threading.Tasks;

namespace ZXing.Mobile
{
	public partial class MobileBarcodeScanner : MobileBarcodeScannerBase
	{
		NotSupportedException ex = new NotSupportedException("MobileBarcodeScanner is unsupported on this platform.");

#if __FORK_FOR_ORION__
		public static IMobileBarcodeScannerFactory PlatformScannerFactory { get; set; }
		private readonly PlatformMobileBarcodeScannerBase _platformScanner;

		public MobileBarcodeScanner()
        {
			_platformScanner = PlatformScannerFactory?.CreatePlafrormScanner()
				?? throw new System.ArgumentNullException(nameof(PlatformScannerFactory));
		}

		Task<Result> PlatformScan(MobileBarcodeScanningOptions options)
			=> _platformScanner.PlatformScan(options);

		void PlatformScanContinuously(MobileBarcodeScanningOptions options, Action<Result> scanHandler)
			=> _platformScanner.PlatformScanContinuously(options, scanHandler);

		void PlatformCancel()
			=> _platformScanner.PlatformCancel();

		void PlatformAutoFocus()
			=> _platformScanner.PlatformAutoFocus();

		void PlatformTorch(bool on)
		{
			bool shouldEnableTorch = on;
			_platformScanner.PlatformTorch(on: shouldEnableTorch);
		}

		void PlatformToggleTorch()
			=> _platformScanner.PlatformToggleTorch();

		void PlatformPauseAnalysis()
			=> _platformScanner.PlatformPauseAnalysis();

		void PlatformResumeAnalysis()
			=> _platformScanner.PlatformResumeAnalysis();

		bool PlatformIsTorchOn
			=> _platformScanner.PlatformIsTorchOn;
#else
		Task<Result> PlatformScan(MobileBarcodeScanningOptions options)
			=> throw ex;

		void PlatformScanContinuously(MobileBarcodeScanningOptions options, Action<Result> scanHandler)
			=> throw ex;

		void PlatformCancel()
			=> throw ex;

		void PlatformAutoFocus()
			=> throw ex;

		void PlatformTorch(bool on)
			=> throw ex;

		void PlatformToggleTorch()
			=> throw ex;

		void PlatformPauseAnalysis()
			=> throw ex;

		void PlatformResumeAnalysis()
			=> throw ex;

		bool PlatformIsTorchOn
			=> throw ex;
#endif
	}
}
