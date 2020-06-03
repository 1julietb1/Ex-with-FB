using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EX
{
    public class UserDataEvaluation : IComparable<UserDataEvaluation>
    {
        public int NumberOfPostsLikes
        {
            get;
            set;
        }

        public int NumberOfPostsComments
        {
            get;
            set;
        }

        public int NumberOfPhotoLike
        {
            get;
            set;
        }

        public int NumberOfPhotoComments
        {
            get;
            set;
        }

        public int CompareTo(UserDataEvaluation other)
        {
            int result = 1;
            if (other != null)
            {
                result = GetTotal().CompareTo(other.GetTotal());
            }

            return result;
        }

        public int GetTotal()
        {
            return NumberOfPostsLikes + NumberOfPostsComments + NumberOfPhotoLike + NumberOfPhotoComments;
        }
    }
}
