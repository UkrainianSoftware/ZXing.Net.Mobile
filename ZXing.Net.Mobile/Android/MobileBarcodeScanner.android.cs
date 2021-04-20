using System;
using System.Threading.Tasks;
using Android.Content;
using ZXing;
using Android.OS;


#if __FORK_FOR_ORION__
namespace ZXing.Mobile.Droid
{ 
	public class MobileBarcodeScannerDroid : MobileBarcodeScannerBase
	{
#else
namespace ZXing.Mobile
{
	public partial class MobileBarcodeScanner : MobileBarcodeScannerBase
	{
#endif
		public const string TAG = "ZXing.Net.Mobile";

		[Obsolete("Use Xamarin.Essentials.Platform.Init instead")]
		public static void Initialize(Android.App.Application app)
			=> Xamarin.Essentials.Platform.Init(app);

		[Obsolete("No longer necessary.")]
		public static void Uninitialize(Android.App.Application app)
		{
		}

		public Android.Views.View CustomOverlay { get; set; }

		bool torch = false;

		Context GetContext(Context context)
			=> Xamarin.Essentials.Platform.CurrentActivity ?? Xamarin.Essentials.Platform.AppContext;

		internal void PlatformScanContinuously(MobileBarcodeScanningOptions options, Action<Result> scanHandler)
			=> ScanContinuously(null, options, scanHandler);

		public void ScanContinuously(Context context, MobileBarcodeScanningOptions options, Action<Result> scanHandler)
		{
			var ctx = GetContext(context);
			var scanIntent = new Intent(ctx, typeof(ZxingActivity));

			scanIntent.AddFlags(ActivityFlags.NewTask);

			ZxingActivity.UseCustomOverlayView = this.UseCustomOverlay;
			ZxingActivity.CustomOverlayView = this.CustomOverlay;
			ZxingActivity.ScanningOptions = options;
			ZxingActivity.ScanContinuously = true;
			ZxingActivity.TopText = TopText;
			ZxingActivity.BottomText = BottomText;

			ZxingActivity.ScanCompletedHandler = (Result result)
				=> scanHandler?.Invoke(result);

			ctx.StartActivity(scanIntent);
		}

		internal Task<Result> PlatformScan(MobileBarcodeScanningOptions options)
			=> Scan(null, options);

		public Task<Result> Scan(Context context, MobileBarcodeScanningOptions options)
		{
			var ctx = GetContext(context);

			var task = Task.Factory.StartNew(() =>
			{

				var waitScanResetEvent = new System.Threading.ManualResetEvent(false);

				var scanIntent = new Intent(ctx, typeof(ZxingActivity));

				scanIntent.AddFlags(ActivityFlags.NewTask);

				ZxingActivity.UseCustomOverlayView = this.UseCustomOverlay;
				ZxingActivity.CustomOverlayView = this.CustomOverlay;
				ZxingActivity.ScanningOptions = options;
				ZxingActivity.ScanContinuously = false;
				ZxingActivity.TopText = TopText;
				ZxingActivity.BottomText = BottomText;

				Result scanResult = null;

				ZxingActivity.CanceledHandler = () => waitScanResetEvent.Set();

				ZxingActivity.ScanCompletedHandler = (Result result) =>
				{
					scanResult = result;
					waitScanResetEvent.Set();
				};

				ctx.StartActivity(scanIntent);

				waitScanResetEvent.WaitOne();

				return scanResult;
			});

			return task;
		}

		internal void PlatformCancel()
			=> ZxingActivity.RequestCancel();

		internal void PlatformAutoFocus()
			=> ZxingActivity.RequestAutoFocus();

		internal void PlatformTorch(bool on)
		{
			torch = on;
			ZxingActivity.RequestTorch(on);
		}

		internal void PlatformToggleTorch()
			=> Torch(!torch);

		internal void PlatformPauseAnalysis()
			=> ZxingActivity.RequestPauseAnalysis();

		internal void PlatformResumeAnalysis()
			=> ZxingActivity.RequestResumeAnalysis();

		internal bool PlatformIsTorchOn
			=> torch;

        internal static void LogDebug(string format, params object[] args)
			=> Android.Util.Log.Debug("ZXING", format, args);

		internal static void LogError(string format, params object[] args)
			=> Android.Util.Log.Error("ZXING", format, args);

		internal static void LogInfo(string format, params object[] args)
			=> Android.Util.Log.Info("ZXING", format, args);

		internal static void LogWarn(string format, params object[] args)
			=> Android.Util.Log.Warn("ZXING", format, args);


#if __FORK_FOR_ORION__
		// Note: [alex-d] [xm-899] cannot inherit from MobileBarcodeScanner
		//		 since app uses ```new MobileBarcodeScanner()``` constructor
		// the methods are simple enough, so we can compromise DRY principle
		// ...this time
		// ---
		// otherwise the fork will be too different from original zxing
		// which might cause issues during merge/upgrade of the lib
		// ---
		// probably could use more pre-processor magic
		// but it's already getting somewhat complex
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
#endif
	}
}
