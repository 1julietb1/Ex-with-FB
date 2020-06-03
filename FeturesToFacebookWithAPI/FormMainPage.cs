using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace EX
{
    public partial class FormMainPage : Form
    {
        private User m_LoggedInUser;
        private PostRetrievalSystem m_PostSystem;
        private int m_TotalNumberOfPhotos = -1;
        private ThreeRadioButtonsOnlyOneSelected m_friendListRadioButtons =
            new ThreeRadioButtonsOnlyOneSelected("First Name only", "Last Name only", "Full Name");

        public int progressBarCurrent { get; set; }

        public FormMainPage()
        {
            InitializeComponent();
            initRadioButtons();
            FacebookService.s_CollectionLimit = 200;
        }

        private void initRadioButtons()
        {
            panelFriendList.Controls.Add(m_friendListRadioButtons);
            m_friendListRadioButtons.Location = new Point(91, 9);
            m_friendListRadioButtons.Button1.Click += new EventHandler(changeToFirstName);
            m_friendListRadioButtons.Button2.Click += new EventHandler(changeToLastName);
            m_friendListRadioButtons.Button3.Click += new EventHandler(changeToFullName);
            m_friendListRadioButtons.Button3.Checked = true;
        }

        private void changeToFullName(object sender, EventArgs e)
        {
                listBoxFriendList.DisplayMember = "Name";
        }

        private void changeToLastName(object sender, EventArgs e)
        {
                listBoxFriendList.DisplayMember = "LastName";
        }

        private void changeToFirstName(object sender, EventArgs e)
        {
                listBoxFriendList.DisplayMember = "FirstName";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            labelStatus.Text = "Connecting, please wait";
            string[] permissionsForLogIn = new string[]
                                                {
                                                "public_profile",
                                                "email",
                                                "user_birthday",
                                                "user_age_range",
                                                "user_gender",
                                                "user_link",
                                                "user_tagged_places",
                                                "user_videos",
                                                "groups_access_member_info",
                                                "user_friends",
                                                "user_events",
                                                "user_likes",
                                                "user_location",
                                                "user_photos",
                                                "user_posts",
                                                "user_hometown"
                                                };
            LoginResult result = FacebookService.Login("1450160541956417", permissionsForLogIn);

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                buttonLogin.Visible = false;
                buttonLogout.Visible = true;
                m_LoggedInUser = result.LoggedInUser;
                userBindingSource.DataSource = m_LoggedInUser;
                albumsBindingSource.DataSource = m_LoggedInUser.Albums;
                placeToAvoidBindingSource.DataSource = m_LoggedInUser.LikedPages;
                eventBindingSource.DataSource = m_LoggedInUser.Events;
                tabControlMainContent.Visible = true;
                labelStatus.Text = string.Empty;
                m_PostSystem = new PostRetrievalSystem(m_LoggedInUser, 3);
                m_PostSystem.InitSystem();
            }
            else
            {
                labelStatus.Text = "Connection Failed, Try Again";
            }
        }

        private void initFeed()
        {
            buttonMorePosts.Text = "More Posts";
            panelFeed.Visible = false;
            labelStatus.Text = "Loading Feed, please wait...";

            new Thread(feedInitLogicalPart).Start();
        }

        private void tabControlMainContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMainContent.SelectedIndex == 2)
            {
                initFeed();
            }
            else
            {
                if (tabControlMainContent.SelectedIndex == 6)
                {
                    initStalker();
                }

                m_PostSystem.ClearShown();
            }
        }

        private void initStalker()
        {
            buttonFindYourStalker.Enabled = true;
            panelStalkerResult.Visible = false;
        }

        private void getPostsToShow()
        {
            if (m_PostSystem.MorePostsToShowExist())
            { 
                int numberOfPostsToLoad = m_PostSystem.CalculateHowManyCanBeUploaded();
                for (int i = 0; i < numberOfPostsToLoad; i++)
                {
                    Post newPost = m_PostSystem.GetPost();
                    changePost(i, newPost);
                }

                if (!m_PostSystem.MorePostsToShowExist())
                {
                    buttonMorePosts.Invoke(new Action(() => 
                                {
                                    buttonMorePosts.Enabled = false;
                                    buttonMorePosts.Text = "No More Posts";
                                }));
                }
            }
        }

        private void changePost(int i_NumOfPost, Post i_PostToShow)
        {
            if (flowLayoutPanelForPosts.Controls.Count < m_PostSystem.NumOfPostsInPage)
            {
                flowLayoutPanelForPosts.Invoke(new Action(() => flowLayoutPanelForPosts.Controls.Add(new PostPanel(i_PostToShow))));
            }
            else
            {
                flowLayoutPanelForPosts.Invoke(new Action(() => changePostAfterAllFilled(i_NumOfPost, i_PostToShow)));
            }
        }

        private void changePostAfterAllFilled(int i_NumOfPost, Post i_PostToShow)
        {
            List<Control> collectionasList = new List<Control>();
            foreach (Control control in flowLayoutPanelForPosts.Controls)
            {
                collectionasList.Add(control);
            }

            collectionasList.RemoveAt(i_NumOfPost);
            collectionasList.Insert(i_NumOfPost, new PostPanel(i_PostToShow));
            flowLayoutPanelForPosts.Controls.Clear();
            flowLayoutPanelForPosts.Controls.AddRange(collectionasList.ToArray());
        }

        private void buttonMorePosts_Click(object sender, EventArgs e)
        {
            getPostsToShow();
        }

        private void albumsBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            photoBindingSource.DataSource = (albumsBindingSource.Current as Album).Photos;
        }

        private void monthCalendarEvents_DateSelected(object sender, DateRangeEventArgs e)
        {
            bool wasFound = false;
            FacebookObjectCollection<Event> collectionToMakeSource =
                new FacebookObjectCollection<Event>();

            foreach (DateTime dateToCompare in monthCalendarEvents.BoldedDates)
            {
                if (dateToCompare.ToShortDateString() == e.Start.ToShortDateString())
                {
                    collectionToMakeSource.Add(m_LoggedInUser.Events.Find(data => data.StartTime == dateToCompare));
                    wasFound = true;
                }
            }

            if (wasFound)
            {
                eventBindingSource.DataSource = collectionToMakeSource;
            }
        }

        private void buttonFindYourStalker_Click(object sender, EventArgs e)
        {
            buttonFindYourStalker.Enabled = false;
            new Thread(calculateStalker).Start();
        }

        private int getTotalNumberOfPhotos()
        {
            int result = 0;

            if (m_TotalNumberOfPhotos == -1)
            {
                foreach (Album currentAlbum in m_LoggedInUser.Albums)
                {
                    result += currentAlbum.Photos.Count;
                }
            }
            else
            {
                result = m_TotalNumberOfPhotos;
            }

            return result;
        }

        private void calculateStalker()
        {
            Tuple<User, UserDataEvaluation> stalker = StalkerCache.GetStalker(m_LoggedInUser, progressBarStalker);
            stalkerUserBindingSource.DataSource = stalker.Item1;
            userDataEvaluationBindingSource.DataSource = stalker.Item2;
            buttonFindYourStalker.Invoke(new Action(() => buttonFindYourStalker.Enabled = true));
            if (stalkerUserBindingSource.DataSource != null)
            {
                panelStalkerResult.Invoke(new Action(() => panelStalkerResult.Visible = true));
            }
            else
            {
                labelStatus.Invoke(new Action(() => labelStatus.Text = "No Stalker Found"));
            }
        }

        private void setProgressBarAndLabel(
                                         ProgressChangedEventArgs i_ProgressToUpdate,
                                         ProgressBar i_ProgressBar,
                                         Label i_LabeltoUpdate)
        {
            i_ProgressBar.Value = i_ProgressToUpdate.ProgressPercentage;
            i_LabeltoUpdate.Text = i_ProgressToUpdate.ProgressPercentage + "%";
        }

        private void feedInitLogicalPart()
        {
            if (!m_PostSystem.IsSystemInitialized())
            {
                m_PostSystem.InitSystem();
            }

            feedLoad();
        }

        private void feedLoad()
        {
            getPostsToShow();
            if (m_PostSystem.MorePostsToShowExist())
            {
                buttonMorePosts.Invoke(new Action(() => buttonMorePosts.Enabled = true));
            }

            panelFeed.Invoke(new Action(() => panelFeed.Visible = true));
            labelStatus.Invoke(new Action(() => labelStatus.Visible = false));
        }

        private void buttonAlonGet_Click(object sender, EventArgs e)
        {
            panelAloneCalculating.Visible = true;
            buttonAlonGet.Enabled = false;
            panelAloneResults.Visible = false;

            progressBarAlone.Maximum = getTotalLocationFromFriends();
            progressBarCurrent = 0;
            new Thread(calculateAlone).Start();  
        }

        private int getTotalLocationFromFriends()
        {
            int result = 0;
            foreach (User currentFriendToAdd in m_LoggedInUser.Friends)
            {
                result += currentFriendToAdd.Checkins.Count;
            }

            return result;
        }

        private void calculateAlone()
        {
            MapWithMaxProxy<string, LocationDataEvaluation> userToCheckinLocationMap =
                new MapWithMaxProxy<string, LocationDataEvaluation>()
                { MaxComperator = (pair1, pair2) => pair1.Value.UsersWhoVisited.Count.CompareTo(pair2.Value.UsersWhoVisited.Count) };

            foreach (User currentFriendToEval in m_LoggedInUser.Friends)
            {
                foreach (Checkin currentCheckInToAdd in currentFriendToEval.Checkins)
                {
                    LocationDataEvaluation outForTryGet;
                    if (!userToCheckinLocationMap.TryGetValue(currentCheckInToAdd.Place.Name, out outForTryGet))
                    {
                        outForTryGet = new LocationDataEvaluation(currentCheckInToAdd.Place);
                        userToCheckinLocationMap.Add(currentCheckInToAdd.Place.Name, outForTryGet);
                    }

                    outForTryGet.UsersWhoVisited.Add(currentFriendToEval);
                }
            }

            KeyValuePair<string, LocationDataEvaluation> max = userToCheckinLocationMap.GetMaxFromMap();

            if (userToCheckinLocationMap.Count != 0)
            {
                friendsToAvoidbindingSource.DataSource = max.Value.UsersWhoVisited;
                placeToAvoidBindingSource.DataSource = max.Value.Location;
            }

            panelAloneCalculating.Invoke(new Action(() => panelAloneCalculating.Visible = false));
            buttonAlonGet.Invoke(new Action(() => buttonAlonGet.Enabled = true));
            panelAloneResults.Invoke(new Action(() => panelAloneResults.Visible = true));
        }

        private void tabControlMainContent_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == 2)
            {
                labelStatus.Text = string.Empty;
            }
            else if (e.TabPageIndex == 6)
            {
                buttonFindYourStalker.Enabled = true;
                panelStalkerResult.Visible = false;
                labelStatus.Text = string.Empty;
            }
            else if (e.TabPageIndex == 7)
            {
                panelAloneCalculating.Visible = false;
                buttonAlonGet.Enabled = true;
                panelAloneResults.Visible = false;
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            buttonLogin.Visible = true;
            tabControlMainContent.Visible = false;
            buttonLogout.Visible = false;
            m_LoggedInUser = null;
            m_TotalNumberOfPhotos = -1;
        }
    }
}
