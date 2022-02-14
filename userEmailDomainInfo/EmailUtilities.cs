

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
                if (includeSubdomain && domainInfo.SubDomain != null)
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
