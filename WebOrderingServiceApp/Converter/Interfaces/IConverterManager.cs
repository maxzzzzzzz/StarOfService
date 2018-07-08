using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Interfaces
{
    public interface IConverterManager
    {
        void XmlToJsonServiceConverter();
        void JsonToXmlServiceConverter();
    }
}
