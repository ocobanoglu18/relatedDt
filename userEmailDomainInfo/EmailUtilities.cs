

using Nager.PublicSuffix;
using System.Net.Mail;

public interface IEmailUtilites
{
    string GetEmailDomainName(string email, bool includeSubdomain = false);
}
public class EmailUtilites : IEmailUtilites
{
    private readonly IDomainParser _domainParser;
    private bool _isValid;//Bu niye burada? _isValid'in scopu sadece GetEmailDomainName kadar. Yani _isValid'i instance'ın yaşamı boyunca kullanacak başka metod yok ve olmaz da. Metod'a verilen Email'in scopu kadardır bunun da scope'u.
    
    //Kullanıcının bu sınıfı kullanabilmesi için IDomainParser üretmesini zorunlu kılıyoruz. Kullanıcıyı bununla uğraşmaya zorlamasak iyi olur.
    //Kullanımı kolaylaştırmak için ikinci bir parametresiz constructor oluşturup burada IDomainParser'ı default değerlerle biz oluşturabiliriz.    
    public EmailUtilites(IDomainParser domainParser)
    {
        //DomainParser'ı create etmek çok maliyetli olduğu için instance'da bir defa create edip sonra hep aynı instance'ı kullanıyoruz!
        _domainParser = domainParser;
        _isValid = false;
    }
    public string GetEmailDomainName(string email, bool includeSubdomain = false)
    {
        string? emailDomainName = null;
        //Email validasyonu için MailAddress kullanma maliyeti nedir? 
        //Email validasyonu için tonla yöntem var. Regex vs.
        //ToDo: Başka bir yöntem bulmalı. Diğer yöntemlerle karşılaştırıp en performanslı çözüm üretililmeli.
        _isValid = MailAddress.TryCreate(email, result: out MailAddress addr);

        if (_isValid)
        {
            DomainInfo? domainInfo = _domainParser.Parse(addr.Host);
            if (domainInfo != null)
            {
                if (includeSubdomain && domainInfo.SubDomain != null)//IsNullOrEmpty daha doğru olur. 
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
