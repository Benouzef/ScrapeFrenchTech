using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScrapeAfdel
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument doc = new HtmlDocument();
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;

            // Ile de France / App Mobile / 29 pages
            string str = string.Empty;
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= 29; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/22/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Application mobile");
                sb.Append(str);
            }

            

            // Ile de France / BDD / 29 pages
            for (int i = 1; i <= 29; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/6/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "BDD");
                sb.Append(str);
            }

            // Ile de France / BI Décisionnel / 11 pages
            for (int i = 1; i <= 11; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/7/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "BI Décisionnel");
                sb.Append(str);
            }

            // Ile de France / Compta & Finance / 9 pages
            for (int i = 1; i <= 9; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/8/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Compta & Finance");
                sb.Append(str);
            }

            // Ile de France / CRM / 11 pages
            for (int i = 1; i <= 11; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/9/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "CRM");
                sb.Append(str);
            }

            // Ile de France / EAI / 10 pages
            for (int i = 1; i <= 10; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/10/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "EAI");
                sb.Append(str);
            }

            // Ile de France / Enterprise Content Management / 9 pages
            for (int i = 1; i <= 9; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/11/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Enterprise Content Management");
                sb.Append(str);
            }

            // Ile de France / Moteur de recherche / 6 pages
            for (int i = 1; i <= 6; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/12/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Moteur de recherche");
                sb.Append(str);
            }

            // Ile de France / Multimédia / 6 pages
            for (int i = 1; i <= 6; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/13/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Multimédia");
                sb.Append(str);
            }

            // Ile de France / Outils de développement / 9 pages
            for (int i = 1; i <= 9; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/14/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Outils de développement");
                sb.Append(str);
            }

            // Ile de France / Paie RH / 7 pages
            for (int i = 1; i <= 7; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/15/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Paie RH");
                sb.Append(str);
            }

            // Ile de France / Plateforme / 13 pages
            for (int i = 1; i <= 13; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/16/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Plateforme");
                sb.Append(str);
            }

            // Ile de France / PLM / 6 pages
            for (int i = 1; i <= 6; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/17/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "PLM");
                sb.Append(str);
            }

            // Ile de France / Réseau social / 6 pages
            for (int i = 1; i <= 6; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/18/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Réseau social");
                sb.Append(str);
            }

            // Ile de France / SCM / 8 pages
            for (int i = 1; i <= 8; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/19/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "SCM");
                sb.Append(str);
            }

            // Ile de France / SCM / 8 pages
            for (int i = 1; i <= 8; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/19/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "SCM");
                sb.Append(str);
            }

            // Ile de France / Sécurité / 8 pages
            for (int i = 1; i <= 8; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/20/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Sécurité");
                sb.Append(str);
            }

            // Ile de France / Système et stockage / 6 pages
            for (int i = 1; i <= 6; i++)
            {
                doc.LoadHtml(wc.DownloadString("http://www.techinfrance.fr/membres/keywords//software_type/21/segment/0/location/17/letter//formId/240ac268de8c0a1ada61b0fd36c50e66/page/" + i));
                str = ProcessPage(doc, "Système et stockage");
                sb.Append(str);
            }

            

            System.IO.File.WriteAllText(@"C:\Temp\ProspectsTechInFrance.csv", sb.ToString(), Encoding.UTF8);

            
        }

        private static string ProcessPage(HtmlDocument doc, string applicationType)
        {
            string str = string.Empty;

            foreach (HtmlNode n in doc.DocumentNode.SelectNodes("//li[contains(@class, 'cell') and not(contains(@class, 'item')) and ../@class='list-partners list-partners-medium js-content']"))
            {
                string url = n.SelectSingleNode("./a").Attributes["href"].Value;
                string companyname = n.SelectSingleNode("./a/div/h3").InnerText.Trim().Replace(";", " ");
                string companydescription = n.SelectSingleNode("./a/div/p").InnerText.Trim().Replace(";", " ");
                //ul avec CSS "skill-list cropped jsinit"

                HtmlDocument docDetail = new HtmlDocument();
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                docDetail.LoadHtml(wc.DownloadString("http://www.techinfrance.fr" + url));

                HtmlNode article = docDetail.DocumentNode.SelectSingleNode("//article");
                string website = article.SelectSingleNode("//div[@class='box box-partner']//a").Attributes["href"].Value;



                //p avec CSS "profile-card__fullname"
                string coordonnees = string.Empty;
                foreach (HtmlNode address in article.SelectNodes("//div[@class='box-body']//p"))
                {
                    Console.WriteLine(address.InnerText);
                    coordonnees += address.InnerText.Replace('\r', ' ').Replace('\n', ' ').Replace(";", " ") + " ";
                }

                string contact = string.Empty;
                string rawmail = string.Empty;
                foreach (HtmlNode address in article.SelectNodes("//div[@class='box-body']//span"))
                {
                    Console.WriteLine(address.InnerText);

                    if (address.InnerText.Contains("E-mail"))
                    {
                        contact += " mail ";

                        string jsvar = address.InnerText.Replace("E-mail", "").Replace("//<![CDATA[", "").Replace("//]]>", "").Replace("document.write", "");

                        //string jsvar = System.IO.File.ReadAllText(@"C:\Temp\input.txt");

                        MB.JsEvaluator.Evaluator evaluator = new MB.JsEvaluator.Evaluator();


                        string result = string.Empty;

                        try
                        {
                          result =  evaluator.Eval(jsvar);
                        } catch (Exception)
                        {
                        }

                        if (result != string.Empty)
                        {
                            HtmlDocument mail = new HtmlDocument();
                            mail.LoadHtml(result);

                            // "this.href='mailto:elise@wiztopic.com'"

                            rawmail = mail.DocumentNode.SelectSingleNode("/a").Attributes["onmouseover"].Value;
                            rawmail = rawmail.Replace("this.href='mailto:", "").Replace("'", "");

                            Console.WriteLine(mail.DocumentNode.SelectSingleNode("/a").Attributes["onmouseover"].Value);
                        }
                    }
                    else
                    {
                        contact += address.InnerText.Replace('\r', ' ').Replace('\n', ' ').Replace(";", " ") + " ";
                    }
                }
                //string name = HttpUtility.HtmlDecode(profileCard.DocumentNode.SelectSingleNode("//p[@class='profile-card__fullname'").InnerText.Trim());

                //p avec CSS "profile-card__location"
                //string location = HttpUtility.HtmlDecode(profileCard.DocumentNode.SelectSingleNode("//p[@class='profile-card__location']").InnerText.Trim());

                /*
                
                string vote = profileCard.DocumentNode.SelectSingleNode("//div[@class='alternate-votes-display']").InnerText;
                */

                str += string.Format("{0};{1};{2};{3};{4};{5};{6};{7}\r\n", url, companyname, companydescription, applicationType, website, coordonnees, contact, rawmail);
            }
            return str;
        }
    }
}
