using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SalesAdventure3000_UI.Views.ViewType;

namespace SalesAdventure3000_UI.Views
{
    internal class AdventureView
    {
        public static View Display(Session currentSession)
        {
            return View.Start;
        }
    }
}
