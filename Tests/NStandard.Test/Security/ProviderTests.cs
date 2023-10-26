using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace NStandard.Security.Test;

public class ProviderTests
{
    [Fact]
    public void RsaTest()
    {
        var text = "Hello NStandard.";
        var data = Encoding.Default.GetBytes(text);

        var encryptRsa = new RsaProvider();
        encryptRsa.FromXmlString(@"<RSAKeyValue>
    <Modulus>wyL0Tl0oPhililRHagPFIlKjWotnDKeK84p9sMjCPUfQqTb9zTne/K7KdpNnHSE5zmjUNWPGFSFxkdec/XVrLz0ZD0euTJo8ZN7Wx1eVYGczh5seLIOazf6zMgEW5jqwVx69Zs0K39alJDPqOYF7Q/2bBD9/BWvhRymkfKlSI5Z9LNi0i5ZRzuXqz6RcvPR35O/FdzWypKQsuRU73szDEwStkwu1ZgsSdyV50RoMAWodnvPDhJnVNdLtpeE6d2EDXKSnmmh+64h4HAK753UlCgjPuJ/jBxSiveSO6yOp18Hs1/tLHfZblgz8BM3tzMPgrPU3xNE3CHXujYSVKi4zsQ==</Modulus>
    <Exponent>AQAB</Exponent>
    <P>6gZc6Zm7gt1nvEqwU5/HdDIZGx0hYwa5OIOL1J72km35JVwYrheC/40MtwLOBt6cnuVeVWy5Q/LaJOeRs3k3dZjo15CeUZ59jvg3JzkHdk2hgDN/Cw9JamM1fEanAdgVKzU70fvbZ5K+gqcNkbacV/hh7cyOwBZUpxIqld9UVe8=</P>
    <Q>1XXFALNiLtSE24/k7PQK6yriaf4T/obVZh8oJbB9z8Af+FdfT3NeNdwHrl6GcQHYVPHLNudD3XCLIzhWkysKaITKKx2JZgRFffb8wh5eyRvPWa0ymiZ1R6Ye2A36BeNXno1pT9f2OkbuUgkavZ3KcSlTvFXzHpYGFjEBmPwSsF8=</Q>
    <DP>3fg1Dnj7Ss/YAddR0a9+Ti7qczY5IaUR75GOApjYROE8bHwjCJVScjOF+NwXMJrbMTdbN2lNfC6PTGu8Xd++g7MKPtRz5fSPIRk7rt8/va06Xs/5UzMnrln1NDALXgtnYLk4SR46581fF6t9ilULi8ESmavpRjaoCmOHsunGI08=</DP>
    <DQ>k9850fpyka53OwK7u8pzpeXXY0W35CLTwiLjVPimrzyQ6SDdzdRF91mtmIWy7KYyjuXRuP8MbGKCgKuOjfTLCQy0YJndjOZ4nYJ0JqWTVA4H3j+1RkROCoxx4YoNIfcTw6qCweUBle19OydDdwfQLgRLFbUU7qNPJCBe0vQMnlE=</DQ>
    <InverseQ>DE+NWEF21QkpVCJRPbjq01Vy7gHfy0WnpMDW+NEVBEJCbBfza+h6/FsO+c5RtVT3QN0stP+EfhDzeZSqNG/0Pf5GKhDKF7YsxFB8yHs3kUf0fN8PuoIFFqPwRXB4PytmvKg9h5e/aSbA7P3W2iDt5gH+IVUODF5cKfPvilk7izc=</InverseQ>
    <D>hawGK0BJdvAvRikhmo/mlPKDEF16RALpfpeaLmX4GT5+w8v15IYGKJYb/0pOUngWPz00UTZ91K/KOpu24TF0MTHrXro9vh/Ry0+TVY67twQ7GmO6McgdXYtieZihdSky7xsRp8BB+L/y9G/TvXzjUdoCPNC6VJ5n/fWaxgK/T7xujQVKWa0MEY0BtPma0tqWb1t0UxGseuG7Kb2JyYZDKc3sCr4ILkvpL9+nXkG8jS8kZcYyNEzEKhwOTU4SyMcHHq0tyuE9Iq6KEnHDcAekaXRvZnG/Sw1SDH7Yg44NOtdfZxhUxIOP8Gj1yFiDy/QJUsxAW7V60+2UwFb6DnEGuQ==</D>
</RSAKeyValue>");
        var cipher = encryptRsa.Encrypt(data);
        var signature = encryptRsa.SignData(data, HashAlgorithmName.SHA1);
        var hash = Sha1Hasher.Hash(data);
        var hashSignature = encryptRsa.SignHash(hash, HashAlgorithmName.SHA1);

        var decryptRsa = new RsaProvider();
        decryptRsa.FromPemString(@"-----BEGIN PRIVATE KEY-----
MIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQDDIvROXSg+GKWK
VEdqA8UiUqNai2cMp4rzin2wyMI9R9CpNv3NOd78rsp2k2cdITnOaNQ1Y8YVIXGR
15z9dWsvPRkPR65Mmjxk3tbHV5VgZzOHmx4sg5rN/rMyARbmOrBXHr1mzQrf1qUk
M+o5gXtD/ZsEP38Fa+FHKaR8qVIjln0s2LSLllHO5erPpFy89Hfk78V3NbKkpCy5
FTvezMMTBK2TC7VmCxJ3JXnRGgwBah2e88OEmdU10u2l4Tp3YQNcpKeaaH7riHgc
ArvndSUKCM+4n+MHFKK95I7rI6nXwezX+0sd9luWDPwEze3Mw+Cs9TfE0TcIde6N
hJUqLjOxAgMBAAECggEBAIWsBitASXbwL0YpIZqP5pTygxBdekQC6X6Xmi5l+Bk+
fsPL9eSGBiiWG/9KTlJ4Fj89NFE2fdSvyjqbtuExdDEx6166Pb4f0ctPk1WOu7cE
OxpjujHIHV2LYnmYoXUpMu8bEafAQfi/8vRv071841HaAjzQulSeZ/31msYCv0+8
bo0FSlmtDBGNAbT5mtLalm9bdFMRrHrhuym9icmGQynN7Aq+CC5L6S/fp15BvI0v
JGXGMjRMxCocDk1OEsjHBx6tLcrhPSKuihJxw3AHpGl0b2Zxv0sNUgx+2IOODTrX
X2cYVMSDj/Bo9chYg8v0CVLMQFu1etPtlMBW+g5xBrkCgYEA6gZc6Zm7gt1nvEqw
U5/HdDIZGx0hYwa5OIOL1J72km35JVwYrheC/40MtwLOBt6cnuVeVWy5Q/LaJOeR
s3k3dZjo15CeUZ59jvg3JzkHdk2hgDN/Cw9JamM1fEanAdgVKzU70fvbZ5K+gqcN
kbacV/hh7cyOwBZUpxIqld9UVe8CgYEA1XXFALNiLtSE24/k7PQK6yriaf4T/obV
Zh8oJbB9z8Af+FdfT3NeNdwHrl6GcQHYVPHLNudD3XCLIzhWkysKaITKKx2JZgRF
ffb8wh5eyRvPWa0ymiZ1R6Ye2A36BeNXno1pT9f2OkbuUgkavZ3KcSlTvFXzHpYG
FjEBmPwSsF8CgYEA3fg1Dnj7Ss/YAddR0a9+Ti7qczY5IaUR75GOApjYROE8bHwj
CJVScjOF+NwXMJrbMTdbN2lNfC6PTGu8Xd++g7MKPtRz5fSPIRk7rt8/va06Xs/5
UzMnrln1NDALXgtnYLk4SR46581fF6t9ilULi8ESmavpRjaoCmOHsunGI08CgYEA
k9850fpyka53OwK7u8pzpeXXY0W35CLTwiLjVPimrzyQ6SDdzdRF91mtmIWy7KYy
juXRuP8MbGKCgKuOjfTLCQy0YJndjOZ4nYJ0JqWTVA4H3j+1RkROCoxx4YoNIfcT
w6qCweUBle19OydDdwfQLgRLFbUU7qNPJCBe0vQMnlECgYAMT41YQXbVCSlUIlE9
uOrTVXLuAd/LRaekwNb40RUEQkJsF/Nr6Hr8Ww75zlG1VPdA3Sy0/4R+EPN5lKo0
b/Q9/kYqEMoXtizEUHzIezeRR/R83w+6ggUWo/BFcHg/K2a8qD2Hl79pJsDs/dba
IO3mAf4hVQ4MXlwp8++KWTuLNw==
-----END PRIVATE KEY-----");
        var decipher = Encoding.Default.GetString(decryptRsa.Decrypt(cipher));
        Assert.Equal(text, decipher);
        Assert.True(encryptRsa.VerifyData(data, signature, HashAlgorithmName.SHA1));
        Assert.True(encryptRsa.VerifyHash(hash, hashSignature, HashAlgorithmName.SHA1));
        Assert.True(encryptRsa.ToXmlString(true) == decryptRsa.ToXmlString(true));
        Assert.True(encryptRsa.ToPemString(true) == decryptRsa.ToPemString(true));
    }

    [Fact]
    public void AesTest()
    {
        static void Test(AesProvider aesProvider)
        {
            var text = Guid.NewGuid().ToString();
            var cipher = aesProvider.Encrypt(Encoding.Default.GetBytes(text));

            var bytes = cipher.ToBytes();
            var _cipher = new AesCipher().FromBytes(bytes);

            var source = Encoding.Default.GetString(aesProvider.Decrypt(_cipher));

            Assert.Equal(text, source);
        }

        var aeses = new[]
        {
            new AesProvider(new byte[16].Let(i => (byte)i), CipherMode.CBC),
            new AesProvider(new byte[16].Let(i => (byte)i), CipherMode.ECB),

            new AesProvider(new byte[24].Let(i => (byte)i), CipherMode.CBC),
            new AesProvider(new byte[24].Let(i => (byte)i), CipherMode.ECB),

            new AesProvider(new byte[32].Let(i => (byte)i), CipherMode.CBC),
            new AesProvider(new byte[32].Let(i => (byte)i), CipherMode.ECB),
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
            var cipher = desProvider.Encrypt(Encoding.Default.GetBytes(text));

            var bytes = cipher.ToBytes();
            var _cipher = new DesCipher().FromBytes(bytes);

            var source = Encoding.Default.GetString(desProvider.Decrypt(_cipher));

            Assert.Equal(text, source);
        }

        var deses = new[]
        {
            new DesProvider(new byte[8].Let(i => (byte)i), CipherMode.CBC),
            new DesProvider(new byte[8].Let(i => (byte)i), CipherMode.ECB),
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
            var cipher = desProvider.Encrypt(Encoding.Default.GetBytes(text));

            var bytes = cipher.ToBytes();
            var _cipher = new TripleDesCipher().FromBytes(bytes);

            var source = Encoding.Default.GetString(desProvider.Decrypt(_cipher));

            Assert.Equal(text, source);
        }

        var tripleDeses = new[]
        {
            new TripleDesProvider(new byte[16].Let(i => (byte)i), CipherMode.CBC),
            new TripleDesProvider(new byte[16].Let(i => (byte)i), CipherMode.ECB),

            new TripleDesProvider(new byte[24].Let(i => (byte)i), CipherMode.CBC),
            new TripleDesProvider(new byte[24].Let(i => (byte)i), CipherMode.ECB),
        };

        foreach (var tripleDes in tripleDeses)
        {
            Test(tripleDes);
        }
    }

}
