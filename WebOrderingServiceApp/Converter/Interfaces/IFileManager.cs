using Converter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Interfaces
{
    public interface IFileManager
    {
        List<ServiceIndustry> GetServicesJson(string path);
        List<ServiceIndustry> GetServicesXml(string path);
    }
}
