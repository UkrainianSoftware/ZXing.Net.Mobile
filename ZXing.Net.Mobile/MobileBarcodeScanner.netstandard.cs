using System;
using System.Threading.Tasks;

namespace ZXing.Mobile
{
	public partial class MobileBarcodeScanner : MobileBarcodeScannerBase
	{
		NotSupportedException ex = new NotSupportedException("MobileBarcodeScanner is unsupported on this platform.");

		// TODO: [alex-d] [xm-899] Need to redirect these calls to a proper native class
		//		 does not happen automatically when `partial class` parts are ...
		//		 are in different `.csproj` (and hence `.dll`)
		// ---
		// Note: class name cannot be changed as it is used by app codebase
		//		 so it should remain `MobileBarcodeScanner`
		//		 it should be ok to change names/namespaces in ios/droid csproj
        //		 with `#if __FORK_FOR_ORION__` flags
		// Without the flag the code should remain the same (ideally)
		// -
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
	}
}
