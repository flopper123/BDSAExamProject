namespace LitExplore.Tests.Util;

public static class RandomizerExtensions {
    public static UInt64 NextUInt64(this Random rnd) {
        var buffer = new byte[sizeof(UInt64)];
        rnd.NextBytes(buffer);
        return BitConverter.ToUInt64(buffer, 0);
    }
}