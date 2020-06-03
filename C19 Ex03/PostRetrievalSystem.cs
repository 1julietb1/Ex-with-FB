using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace EX
{
    public class PostRetrievalSystem 
    {
        private readonly int r_NumberOfPostsInPage;
        private User m_LoggedInUser;
        private int m_ShownPostsNum = 0;
        private int m_TotalNumberOfPosts = -1;
        private IEnumerator<Post> m_PostEnumerator;

        public int NumOfPostsInPage
        {
            get
            {
                return r_NumberOfPostsInPage;
            }
        }

        public PostRetrievalSystem(User i_LoggedInUser, int i_NumberOfPostsInPage )
        {
            m_LoggedInUser = i_LoggedInUser;
            r_NumberOfPostsInPage = i_NumberOfPostsInPage;
            m_PostEnumerator = GetEnumerator();
        }

        public bool MorePostsToShowExist()
        {
            return m_ShownPostsNum < m_TotalNumberOfPosts;
        }

        public Post GetPost()
        {
            m_PostEnumerator.MoveNext();
            return m_PostEnumerator.Current;
        }

        public int CalculateHowManyCanBeUploaded()
        {
            int result = m_TotalNumberOfPosts - m_ShownPostsNum > r_NumberOfPostsInPage
                                            ? r_NumberOfPostsInPage
                                            : m_TotalNumberOfPosts - m_ShownPostsNum;
            return result;
        }

        public void ClearShown()
        {
            m_PostEnumerator = GetEnumerator();
            m_ShownPostsNum = 0;
        }

        public bool IsSystemInitialized()
        {
            return m_TotalNumberOfPosts == -1;
        }

        public void InitSystem()
        {
            m_TotalNumberOfPosts = m_LoggedInUser.NewsFeed.Count;   
        }

        public IEnumerator<Post> GetEnumerator()
        {
            Random random = new Random();
            HashSet<int> shownPosts = new HashSet<int>();

            while (shownPosts.Count < m_TotalNumberOfPosts)
            {
                bool isValidPost = false;
                int postNum = 0;

                while (!isValidPost)
                {
                    postNum = random.Next(m_TotalNumberOfPosts);
                    if (m_TotalNumberOfPosts != 0 &&
                        !shownPosts.Contains(postNum))
                    {
                        isValidPost = true;
                    }
                }

                shownPosts.Add(postNum);
                m_ShownPostsNum++;
                yield return m_LoggedInUser.NewsFeed.ElementAt(postNum);
            }
        }
    }
}
