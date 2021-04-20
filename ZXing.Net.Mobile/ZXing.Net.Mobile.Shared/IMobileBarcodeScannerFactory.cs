

namespace ZXing.Mobile
{
    public interface IMobileBarcodeScannerFactory
    {
        PlatformMobileBarcodeScannerBase CreatePlafrormScanner();
    }
}
