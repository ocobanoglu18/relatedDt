using System.Diagnostics;
using System.Net.Mail;
using Nager.PublicSuffix;
using System.IO;

namespace userEmailDomainInfo
{
    class Program
    {
        private void GenerateTestEmails()
        {

            using (var reader =
                new StreamReader(@"C:\Users\aysenur.gozel\Documents\GitHub\relatedDt\userEmailDomainInfo\1000000_EMAIL.csv"))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split('@');
                    if (values.Length > 1 && !listA.Contains(values[1]))
                    {
                        listA.Add(values[1]);
                        listB.Add(line);
                    }
                    else if (values.Length <= 1)
                    {
                        listA.Add(values[0]);
                        listB.Add(line);
                    }

                }
                //listB.ForEach(Console.WriteLine);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var item in listB)
                {
                    sb.AppendLine(item.ToString());
                }

                Console.WriteLine(sb.ToString());
                System.IO.File.WriteAllText(
                    @"C:\Users\aysenur.gozel\Documents\GitHub\relatedDt\userEmailDomainInfo\test_emails.csv",
                    sb.ToString());
            }
        }
        static void Main(string[] args)
        {
            // GenerateTestEmails();
            return;
        }


}
}
//    } }
        
//        static void Main(string[] args)
//        {
//            TimeSpan way1 = WithDomainLibrary(Helper.Emails);
//            Console.WriteLine("");
//            Console.WriteLine("");
//            TimeSpan way2 = Primitive(Helper.Emails);
//            TimeSpan way3 = WithDb(Helper.Emails);


//        }

//        private static TimeSpan WithDb(List<string> list)
//        {
//            throw new NotImplementedException();
//        }

//        private static TimeSpan WithDomainLibrary(List<string> list)
//        {
//            Console.WriteLine("DOMAIN SEARCH EXAMPLE");
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            foreach (string address in Helper.Emails)
//                {
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
//                    catch (Nager.PublicSuffix.ParseException e) {
//                        stopwatch.Stop();
//                        Console.WriteLine($"Time elapsed (For): {stopwatch.Elapsed}");
//                        return stopwatch.Elapsed;

//                    }
                   
//                }
//            }
//            bool isValid = MailAddress.TryCreate(address, result: out MailAddress addr);
//            var domainParser = new DomainParser(new WebTldRuleProvider());
//            var domainInfo = domainParser.Parse(addr.Host);
//            string pr = $"{domainInfo.SubDomain}.{domainInfo.Domain}";

//            stopwatch.Stop();
//            Console.WriteLine($"Time elapsed (For): {stopwatch.Elapsed}");
//            return stopwatch.Elapsed;

//        }

//        private static TimeSpan Primitive(List<string> list)
//        {
//            Console.WriteLine("PRIMITIVE SEARCH EXAMPLE");
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            foreach (string address in Helper.Emails)
//            {
//                Console.WriteLine("email adress : " +address);
//                string username =address.Split("@").ElementAtOrDefault(0);
//                if (username == null)username="";
                

//                string domain = address.Split("@").ElementAtOrDefault(1);
//                if (domain == null) domain = "";

//                string justdomain = domain.Split(".").ElementAtOrDefault(0);
//                if (justdomain == null) justdomain = "";
          
//                Console.WriteLine("username : " + username);
//                Console.WriteLine("domain : " + domain);
//                Console.WriteLine("justdomain : " + justdomain);
//                Console.WriteLine("");
//            }

//            stopwatch.Stop();
//            Console.WriteLine($"Time elapsed (For): {stopwatch.Elapsed}");
//            return stopwatch.Elapsed;

//        }

//    }
//}