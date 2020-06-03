using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace EX
{
    public class LocationDataEvaluation
    {
        public List<User> UsersWhoVisited { get; }

        public Page Location { get; }

        public LocationDataEvaluation(Page i_LocationPage)
        {
            Location = i_LocationPage;
            UsersWhoVisited = new List<User>();
        }
    }
}
