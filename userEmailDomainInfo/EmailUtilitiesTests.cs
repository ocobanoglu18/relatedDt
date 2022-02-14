using Nager.PublicSuffix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace userEmailDomainInfo.Tests
{
    public class EmailUtilitiesTests
    {
        [Theory]
        [InlineData("key_id_20171204_279846@euromsg.net", "euromsg")]
        [InlineData("aaaa_newmail_001068798@euromsg.com", "euromsg")]
        [InlineData("kaka@mail.ru", "mail")]
        [InlineData("rachaelprentice@hotmail.co.uk", "hotmail")]

        [InlineData("bbbb_newmail_001022954@ehb.itu.com.tr", "itu")]
        [InlineData("szbieg@poczta.onet.pl", "onet")]
        public void GetEmailDomainName_ValidEmailShouldReturnDomain(string email, string expectedDomain)
        {
            IEmailUtilites emailUtilities = new EmailUtilites(new DomainParser(new WebTldRuleProvider()));
            string actualDomain = emailUtilities.GetEmailDomainName(email, false);
            Assert.Equal(expectedDomain, actualDomain);
        }

        [Theory]
        [InlineData("key_id_20171204_279846@euromsg.net", "euromsg")]
        [InlineData("aaaa_newmail_001068798@euromsg.com", "euromsg")]
        [InlineData("kaka@mail.ru", "mail")]
        [InlineData("rachaelprentice@hotmail.co.uk", "hotmail")]

        [InlineData("bbbb_newmail_001022954@ehb.itu.com.tr", "ehb.itu")]
        [InlineData("szbieg@poczta.onet.pl", "poczta.onet")]
        public void GetEmailDomainName_ValidEmailShouldReturnSubdomainAndDomain(string email, string expectedDomain)
        {
            IEmailUtilites emailUtilities = new EmailUtilites(new DomainParser(new WebTldRuleProvider()));
            string actualDomain = emailUtilities.GetEmailDomainName(email, true);
            Assert.Equal(expectedDomain, actualDomain);
        }

        [Theory]
        [InlineData("not_an_email", null)]
        // TODO: should it return null or empty string?
        public void GetEmailDomainName_InvalidEmailShouldReturnNull(string email, string expectedDomain)
        {
            IEmailUtilites emailUtilities = new EmailUtilites(new DomainParser(new WebTldRuleProvider()));
            string actualDomain = emailUtilities.GetEmailDomainName(email, false);
            Assert.Equal(expectedDomain, actualDomain);
        }

    }
}
