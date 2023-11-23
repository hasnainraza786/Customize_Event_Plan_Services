using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customize_Event_Plan_Project_Services.Models;

namespace Customize_Event_Plan_Project_Services.ViewModel
{
    public class MultipleTable
    {
        public IEnumerable< Venue_Detail> Venue_Detailses { get; set; }

        public IEnumerable<Menu_Detail> Menu_Detailses { get; set; }

        public IEnumerable<Deco_Details> Deco_Detailses { get; set; }
    }
}