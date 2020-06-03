using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;

namespace EX
{
    public partial class PostPanel : UserControl
    {
        public PostPanel(Post i_PostToShow)
        {
            InitializeComponent();

            PostIcon.ImageLocation = i_PostToShow.IconURL;
            PostMessege.Text = i_PostToShow.Message;
            PostCreatedTime.Text = i_PostToShow.CreatedTime.Value.ToShortDateString();
            PostUpdateTime.Text = i_PostToShow.UpdateTime.Value.ToShortDateString();
            PostPictureBox.ImageLocation = i_PostToShow.PictureURL;
            PostUserPicture.ImageLocation = i_PostToShow.From != null ? i_PostToShow.From.PictureNormalURL : string.Empty;
            PostCreator.Text = i_PostToShow.From != null ?
                i_PostToShow.From.FirstName + i_PostToShow.From.LastName :
                string.Empty;
        }
    }
}
