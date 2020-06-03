using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EX
{
    public partial class ThreeRadioButtonsOnlyOneSelected : UserControl
    {
        public ThreeRadioButtonsOnlyOneSelected(string i_TextRadioButton1, string i_TextRadioButton2, string i_TextRadioButton3)
        {
            InitializeComponent();
            radioButton1.Text = i_TextRadioButton1;
            radioButton2.Text = i_TextRadioButton2;
            radioButton3.Text = i_TextRadioButton3;

            radioButton1.Click += new EventHandler(turnOffRadio2AndRadio3);
            radioButton2.Click += new EventHandler(turnOffRadio1AndRadio3);
            radioButton3.Click += new EventHandler(turnOffRadio1AndRadio2);
        }

        public RadioButton Button1
        {
            get
            {
                return radioButton1;
            }
        }

        public RadioButton Button2
        {
            get
            {
                return radioButton2;
            }
        }

        public RadioButton Button3
        {
            get
            {
                return radioButton3;
            }
        }

        private void turnOffRadio1AndRadio2(object sender, EventArgs e)
        {
            changeOtherRadioButtons(Button1, Button2);
        }

        private void turnOffRadio2AndRadio3(object sender, EventArgs e)
        {
            changeOtherRadioButtons(Button2, Button3);
        }

        private void turnOffRadio1AndRadio3(object sender, EventArgs e)
        {
            changeOtherRadioButtons(Button1, Button3);
        }

        private void changeOtherRadioButtons(RadioButton i_RadioButton1, RadioButton i_RadioButton2)
        {
            i_RadioButton1.Checked = false;
            i_RadioButton2.Checked = false;
        }
    }
}
