

using Nager.PublicSuffix;
using System.Net.Mail;

public interface IEmailUtilites
{
    string GetEmailDomainName(string email, bool includeSubdomain = false);
}
public class EmailUtilites : IEmailUtilites
{
    private readonly IDomainParser _domainParser;
    private bool _isValid;
    public EmailUtilites(IDomainParser domainParser)
    {
        //DomainParser'ı create etmek çok maliyetli olduğu için instance'da bir defa create edip sonra hep aynı instance'ı kullanıyoruz!
        _domainParser = domainParser;
        _isValid = false;
    }
    public string GetEmailDomainName(string email, bool includeSubdomain = false)
    {
        string? emailDomainName = null;
        _isValid = MailAddress.TryCreate(email, result: out MailAddress addr);
        
        if (_isValid)
        {
            DomainInfo? domainInfo = _domainParser.Parse(addr.Host);
            if (domainInfo != null)
            {
                if (includeSubdomain)
                {
                    emailDomainName = $"{domainInfo.SubDomain}.{domainInfo.Domain}";
                }
                else
                {
                    emailDomainName = domainInfo.Domain;
                }
            }
            
        }
        return emailDomainName;
        
    }
}

//namespace userEmailDomainInfo
//{
//    internal class GetDomainInfoNager
//    {
//        public GetDomainInfo()
//        {

//        }
//        private static TimeSpan WithDomainLibrary(List<string> list)
//        {
//            Console.WriteLine("DOMAIN SEARCH EXAMPLE");
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            foreach (string address in Helper.Emails)
//            {
//                bool isValid = MailAddress.TryCreate(address, result: out MailAddress addr);
//                if (isValid)
//                {
//                    string host = addr.Host;
//                    try
//                    {
//                        var domainParser = new DomainParser(new WebTldRuleProvider());
//                        var domainInfo = domainParser.Parse(host);
//                        string pr = $"{domainInfo.SubDomain}.{domainInfo.Domain}";
//                        Console.WriteLine(pr);
//                    }
//                    catch (Nager.PublicSuffix.ParseException e)
//                    {
//                        stopwatch.Stop();
//                        Console.WriteLine($"Time elapsed (For): {stopwatch.Elapsed}");
//                        return stopwatch.Elapsed;

//                    }

//                }
//            }
//            stopwatch.Stop();
//            Console.WriteLine($"Time elapsed (For): {stopwatch.Elapsed}");
//            return stopwatch.Elapsed;

//        }
//    }
//}   
