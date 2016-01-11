using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breda_Ontdekt.Model.Entities
{
    public class TransferClass
    {

        public ObjectInfo info { get; set; }
        public Route route { get; set; }
        public bool isReturn { get; set; }
        public string language { get; set; }
        public bool resetted { get; set; }
        public bool languageChanged { get; set; }

    }
}
