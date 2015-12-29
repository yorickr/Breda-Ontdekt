using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breda_Ontdekt.ViewModel
{
    class AppGlobal
    {
        private static AppGlobal instance;
        //private static Settings settings;
        //rest vd objecten

        private AppGlobal() { }

        public static AppGlobal getInstance()
        {
            if (instance == null)
                instance = new AppGlobal();
            return instance;
        }


    }
}
