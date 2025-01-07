// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("RngccYqQEh/gDN7qMWJYG+o675vHdfbVx/rx/t1xv3EA+vb29vL39JCEqPi+5brG+2Y09BcDvE9joWKwl4HwdzTWQr95bKN/+io2+M+vWhnnPNrC/6KRPUGBdMQwx7GWGafse/Xm1Fh4WXimu+YzBhcQ0sSwiQhEdfb498d19v31dfb292c7YrBvqE73HjJutmV447u98YPrz8dAuJj+Z7dcAWwlvt9qFdT3c9vCLnVBPvLJ4urX1KpsAz1119hRCdBy82jc5gUwk58+Mt6nPKmbpx6O7BLEjt8HPr404dnro0Nfvme9Bvp0ldCVwNMlcJOn7Gyx+ssRNcZP0o5uWzDbBYhtjCXikBEMx1sT5rP/t2xCFl2Pj4bstB292z0YrvX09vf2");
        private static int[] order = new int[] { 5,5,6,7,12,6,6,8,10,11,11,13,12,13,14 };
        private static int key = 247;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
