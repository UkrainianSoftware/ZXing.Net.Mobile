using System;
using System.Threading.Tasks;


namespace ZXing.Mobile
{
    public abstract class PlatformMobileBarcodeScannerBase : MobileBarcodeScannerBase
	{
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



		// Note: [alex-d] it's internal in zxing but...
		//       need to change that due to build error
		// ---
		// virtual or abstract methods cannot be private
		// -
		public abstract Task<Result> PlatformScan(MobileBarcodeScanningOptions options);
		public abstract void PlatformScanContinuously(MobileBarcodeScanningOptions options, Action<Result> scanHandler);
		public abstract void PlatformCancel();
		public abstract void PlatformAutoFocus();
		public abstract void PlatformTorch(bool on);
		public abstract void PlatformToggleTorch();
		public abstract void PlatformPauseAnalysis();
		public abstract void PlatformResumeAnalysis();
		public abstract bool PlatformIsTorchOn { get; }
	}
}
