using System;
using Xunit;

namespace NStandard.Security.Test
{
    public class ProviderTests
    {
        [Fact]
        public void AesTest()
        {
            static void Test(AesProvider aesProvider)
            {
                var text = Guid.NewGuid().ToString();
                var cipherIVPair = aesProvider.Encrypt(text.Bytes());

                var bytes = AesIVHandler.Default.Combine(cipherIVPair);
                var _cipherIVPair = AesIVHandler.Default.Separate(bytes);

                var source = aesProvider.Decrypt(_cipherIVPair).String();

                Assert.Equal(text, source);
            }

            var aeses = new[]
            {
                new AesProvider().UseCBC().WithKey(new byte[16].Let(i => (byte)i)),
                new AesProvider().UseECB().WithKey(new byte[16].Let(i => (byte)i)),

                new AesProvider().UseCBC().WithKey(new byte[24].Let(i => (byte)i)),
                new AesProvider().UseECB().WithKey(new byte[24].Let(i => (byte)i)),

                new AesProvider().UseCBC().WithKey(new byte[32].Let(i => (byte)i)),
                new AesProvider().UseECB().WithKey(new byte[32].Let(i => (byte)i)),
            };

            foreach (var aes in aeses)
            {
                Test(aes);
            }
        }

        [Fact]
        public void DesTest()
        {
            static void Test(DesProvider desProvider)
            {
                var text = Guid.NewGuid().ToString();
                var cipherIVPair = desProvider.Encrypt(text.Bytes());

                var bytes = DesIVHandler.Default.Combine(cipherIVPair);
                var _cipherIVPair = DesIVHandler.Default.Separate(bytes);

                var source = desProvider.Decrypt(_cipherIVPair).String();

                Assert.Equal(text, source);
            }

            var deses = new[]
            {
                new DesProvider().UseCBC().WithKey(new byte[8].Let(i => (byte)i)),
                new DesProvider().UseECB().WithKey(new byte[8].Let(i => (byte)i)),
            };

            foreach (var des in deses)
            {
                Test(des);
            }
        }

        [Fact]
        public void TripleDesTest()
        {
            static void Test(TripleDesProvider desProvider)
            {
                var text = Guid.NewGuid().ToString();
                var cipherIVPair = desProvider.Encrypt(text.Bytes());

                var bytes = DesIVHandler.Default.Combine(cipherIVPair);
                var _cipherIVPair = DesIVHandler.Default.Separate(bytes);

                var source = desProvider.Decrypt(_cipherIVPair).String();

                Assert.Equal(text, source);
            }

            var tripleDeses = new[]
            {
                new TripleDesProvider().UseCBC().WithKey(new byte[16].Let(i => (byte)i)),
                new TripleDesProvider().UseECB().WithKey(new byte[16].Let(i => (byte)i)),

                new TripleDesProvider().UseCBC().WithKey(new byte[24].Let(i => (byte)i)),
                new TripleDesProvider().UseECB().WithKey(new byte[24].Let(i => (byte)i)),
            };

            foreach (var tripleDes in tripleDeses)
            {
                Test(tripleDes);
            }
        }

    }
}
