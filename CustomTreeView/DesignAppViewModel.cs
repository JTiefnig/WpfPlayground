using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTreeView
{
    public class AppDesignViewModel : AppViewModel
    {


        public static AppDesignViewModel Instance => new AppDesignViewModel();

        private AppDesignViewModel()
            : base()
        { }


    }
}
