using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace EX
{
    public class StalkerCache
    {
        private static readonly object sr_LockForInstance = new object();
        private static volatile Tuple<User, UserDataEvaluation> s_Instance = null;
       
        private StalkerCache()
            {
            }

        public static Tuple<User, UserDataEvaluation> GetStalker(User i_UserToStalk, ProgressBar i_ProgressBar)
        {
            if (s_Instance == null)
            {
                lock (sr_LockForInstance)
                {
                    if (s_Instance == null)
                    {
                        s_Instance = initStalkerObject(i_UserToStalk, i_ProgressBar);
                    }
                }
            }

            return s_Instance;
        }

        private static void calcDataFromUserCollection(
                                              FacebookObjectCollection<User> i_UserCollectionToEval,
                                              Dictionary<string, Tuple<User, UserDataEvaluation>> i_MapOfResults,
                                              Action<UserDataEvaluation> i_PropToSet)
        {
            foreach (User userWhoLiked in i_UserCollectionToEval)
            {
                Tuple<User, UserDataEvaluation> outForTryGet;
                if (!i_MapOfResults.TryGetValue(userWhoLiked.Name, out outForTryGet))
                {
                    outForTryGet = new Tuple<User, UserDataEvaluation>(userWhoLiked, new UserDataEvaluation());
                    i_MapOfResults.Add(userWhoLiked.Name, outForTryGet);
                }

                i_PropToSet(outForTryGet.Item2);
            }
        }

        private static void calcDataFromCommentCollection(
                                               FacebookObjectCollection<Comment> i_CommentCollectionToEval,
                                               Dictionary<string, Tuple<User, UserDataEvaluation>> i_MapOfResults,
                                               Action<UserDataEvaluation> i_PropToSet)
        {
            foreach (Comment currentComment in i_CommentCollectionToEval)
            {
                Tuple<User, UserDataEvaluation> outForTryGet;
                if (!i_MapOfResults.TryGetValue(currentComment.From.Name, out outForTryGet))
                {
                    outForTryGet = new Tuple<User, UserDataEvaluation>(currentComment.From, new UserDataEvaluation());
                    i_MapOfResults.Add(currentComment.From.Name, outForTryGet);
                }

                i_PropToSet(outForTryGet.Item2);
            }
        }

        private static Tuple<User, UserDataEvaluation> initStalkerObject(User i_UserToStalk, ProgressBar i_ProgressBar)
        {
            i_ProgressBar.Invoke(new Action(() => i_ProgressBar.Visible = true));
            i_ProgressBar.Invoke(new Action(() => i_ProgressBar.Maximum = i_UserToStalk.Posts.Count + i_UserToStalk.Albums.Count));

            MapWithMaxProxy<string, Tuple<User, UserDataEvaluation>> UserNameToDataEvalMap = new MapWithMaxProxy<string, Tuple<User, UserDataEvaluation>>()
            { MaxComperator = (pair1, pair2) => pair1.Value.Item2.CompareTo(pair2.Value.Item2) };
        
            foreach (Post currentPost in i_UserToStalk.Posts)
            {
                calcDataFromUserCollection(
                    currentPost.LikedBy,
                    UserNameToDataEvalMap,
                    new Action<UserDataEvaluation>(data => data.NumberOfPostsLikes++));

                calcDataFromCommentCollection(
                     currentPost.Comments,
                     UserNameToDataEvalMap,
                     new Action<UserDataEvaluation>(data => data.NumberOfPostsComments++));
                i_ProgressBar.Invoke(new Action(() => i_ProgressBar.Value++));
            }

            foreach (Album currentAlbum in i_UserToStalk.Albums)
            {
                foreach (Photo currentPhoto in currentAlbum.Photos)
                {
                    calcDataFromUserCollection(
                  currentPhoto.LikedBy,
                  UserNameToDataEvalMap,
                  new Action<UserDataEvaluation>(data => data.NumberOfPhotoLike++));

                    calcDataFromCommentCollection(
                         currentPhoto.Comments,
                         UserNameToDataEvalMap,
                         new Action<UserDataEvaluation>(data => data.NumberOfPhotoComments++));
                }

                i_ProgressBar.Invoke(new Action(() => i_ProgressBar.Value++));
            }

            Tuple<User, UserDataEvaluation> resultStalker;

            KeyValuePair<string, Tuple<User, UserDataEvaluation>> stalker =
                UserNameToDataEvalMap.GetMaxFromMap();

            if (UserNameToDataEvalMap.Count != 0)
            {
                string stalkerName = stalker.Key;
                User stalkerUser = i_UserToStalk.Friends.Find(data => data.Name == stalkerName);
                ////stalker is not one of your friends
                if (stalkerUser == null)
                {
                    stalkerUser = stalker.Value.Item1;
                }

                resultStalker = new Tuple<User, UserDataEvaluation>(stalkerUser, stalker.Value.Item2);
            }
            else
            {
                resultStalker = new Tuple<User, UserDataEvaluation>(i_UserToStalk, new UserDataEvaluation());
            }

            i_ProgressBar.Invoke(new Action(() => i_ProgressBar.Visible = false));
            return resultStalker;
        }
    }
}
