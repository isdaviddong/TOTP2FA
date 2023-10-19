// See https://aka.ms/new-console-template for more information

using OtpNet;

Console.WriteLine($"\n ------------生成QR Code---------------");
string label = "LabelName";
string issuer = "MyApp";
string accountName = "user@example.com";
byte[] secretKey = KeyGeneration.GenerateRandomKey();
string keyAsBase32String = Base32Encoding.ToString(secretKey);
Console.WriteLine($"\n\n已自動生成 secretKey: '{keyAsBase32String}' ，應妥善保存於資料庫中  ");

var totp = new Totp(secretKey);
string provisioningUri = $"otpauth://totp/{Uri.EscapeDataString(label)}:{accountName}?issuer={Uri.EscapeDataString(accountName)}&secret={keyAsBase32String}&issuer={Uri.EscapeDataString(issuer)}";
Console.Write($"使用 secretKey 產生的 URL : \n{provisioningUri}");
Console.WriteLine($"\n\n請掃描此 URL 產生的 QR Code... ");

bool result = false;
while (result != true)
{
    long TimeStep = 0;
    Console.Write($"\n 輸入六位數一次性密碼 : ");
    var Inputkey = Console.ReadLine();
    result = totp.VerifyTotp(Inputkey, out TimeStep);
    Console.Write($"驗證結果 : {result}");
}
Console.WriteLine($"\n 當驗證結果為 true, 未來，即可隨時使用同樣的 secretKey '{keyAsBase32String}' 建立totp物件，進行身分驗證 ");

result = false;
Console.WriteLine($"\n ------------開始驗證---------------");
while (result != true)
{
    long TimeStep = 0;
    var totp2 = new Totp(secretKey);
    Console.Write($"\n 輸入六位數一次性密碼 : ");
    var Inputkey = Console.ReadLine();
    result = totp2.VerifyTotp(Inputkey, out TimeStep);
    Console.Write($"驗證結果 : {result}");
}
