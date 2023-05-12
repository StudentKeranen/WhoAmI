using Entitylayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using Datalayer;

namespace Businesslayer
{
    public class DataController
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public List<String[]> CheckForFile(out Company company)
        {
            company = null!;
            List<String[]> list = new List<String[]>();
            string path = AppContext.BaseDirectory;
            
            if (File.Exists($@"{path}\Files\File.xml"))
            {
                var xml = XDocument.Load($@"{path}\Files\File.xml");

                foreach (XNode node in xml.Nodes())
                {
                    XmlReader xmlReader = node.CreateReader();
                    xmlReader.Read();
                    company = unitOfWork.CompanyRepository.FirstOrDefault(c => c.CompanyName == xmlReader.Name);
                    if (company == null)
                    { 
                        company = new Company() { CompanyName = xmlReader.Name };
                        unitOfWork.CompanyRepository.Add(company);
                    }
                    string[] strings = new string[2];
                    while (xmlReader.Read())
                    {
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                strings[0] = xmlReader.Name;
                                break;
                            case XmlNodeType.Text:
                                strings[1] = xmlReader.Value;
                                break;
                            case XmlNodeType.EndElement:
                                list.Add(strings);
                                strings = new string[2];
                                break;
                            default: break;
                        }
                    }
                }
            }
            return list;
        }

        public List<string[]> FetchPersonalData()
        {
            List<string[]> list = new List<string[]>();
            foreach (PersonalData personalData in unitOfWork.PersonalDataRepository.Find(pd => pd.User.UserId == SessionController.LoggedIn.UserId,pd => pd.User))
            {
                string[] strings = new string[2];
                strings[0] = personalData.DataName; 
                strings[1] = personalData.DataValue;
                list.Add(strings);
            }
            return list;
        }
    }
}
