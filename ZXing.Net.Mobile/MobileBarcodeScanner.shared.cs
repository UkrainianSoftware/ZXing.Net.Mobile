using System;
using System.Threading.Tasks;

namespace ZXing.Mobile
{
	public partial class MobileBarcodeScanner : MobileBarcodeScannerBase
	{
		// TODO: [alex-d] [xm-899] Need to redirect these calls to a proper native class
		//		 does not happen automatically when `partial class` parts are ...
		//		 are in different `.csproj` (and hence `.dll`)
		// ---
		// Need to inject
        // 1. platform implementation in default constructor
		// 2. a static factory to get [1] done without changing public API
		// -

		public override Task<Result> Scan(MobileBarcodeScanningOptions options)
			=> PlatformScan(options);

		public override void ScanContinuously(MobileBarcodeScanningOptions options, Action<Result> scanHandler)
			=> PlatformScanContinuously(options, scanHandler);

		public override void Cancel()
			=> PlatformCancel();

		public override void AutoFocus()
			=> PlatformAutoFocus();

		public override void Torch(bool on)
			=> PlatformTorch(on);

		public override void ToggleTorch()
			=> PlatformToggleTorch();

		public override void PauseAnalysis()
			=> PlatformPauseAnalysis();

		public override void ResumeAnalysis()
			=> PlatformResumeAnalysis();

		public override bool IsTorchOn
			=> PlatformIsTorchOn;
	}
}
