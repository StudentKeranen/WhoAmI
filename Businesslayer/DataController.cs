using Entitylayer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
                    while (xmlReader.Read())
                    {
                        string[] strings = new string[2];
                        switch (xmlReader.NodeType)
                        {
                            case XmlNodeType.Element:
                                strings[0] = xmlReader.Name;
                                break;
                            case XmlNodeType.Text:
                                strings[1] = xmlReader.Value;
                                break;
                            default: break;
                        }
                        list.Add(strings);
                    }
                }
            }
            return list;
        }

        public List<string[]> FetchPersonalData()
        {
            List<string[]> list = new List<string[]>();
            foreach (PersonalData personalData in unitOfWork.PersonalDataRepository.Find(pd => true))
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
