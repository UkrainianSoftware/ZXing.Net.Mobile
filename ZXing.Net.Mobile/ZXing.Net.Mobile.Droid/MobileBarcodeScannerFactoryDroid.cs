

#if __FORK_FOR_ORION__

namespace ZXing.Mobile.Droid
{
    public class MobileBarcodeScannerFactoryDroid : IMobileBarcodeScannerFactory
    {
        public PlatformMobileBarcodeScannerBase CreatePlafrormScanner()
        {
            var result = new MobileBarcodeScannerDroid();
            return result;
        }
    }
}

#endif

