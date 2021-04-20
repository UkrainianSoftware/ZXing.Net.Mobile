

#if __FORK_FOR_ORION__
namespace ZXing.Mobile.Ios
{
    public class MobileBarcodeScannerFactoryIos: IMobileBarcodeScannerFactory
    {
        public PlatformMobileBarcodeScannerBase CreatePlafrormScanner()
        {
            var result = new MobileBarcodeScannerIos();
            return result;
        }
    }
}
#endif
