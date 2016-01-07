using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//#c0dfd9 light blue
//#e9ece5 light grey/white
//#b3c2bf Grey
//#3b3a36 Dark grey


namespace Breda_Ontdekt.ViewModel
{
    class AppGlobal
    {
        private static AppGlobal instance;
        //private static Settings settings;
        //rest vd objecten

        public static bool ZoomedIn;


        private AppGlobal() { }

        public static AppGlobal getInstance()
        {
            if (instance == null) { instance = new AppGlobal(); ZoomedIn = false; }
            return instance;
        }


    }
}
