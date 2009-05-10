using System;
using WindowlessControls;
using WindowlessControls.CommonControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowlessControlsTutorial
{
    public partial class AlphaDemo : Form
    {
        public AlphaDemo()
        {
            InitializeComponent();

            // we need to layer two controls on top of another: a background, with another panel containing more controls
            StackPanel stack = myHost.Control;
            OverlayPanel overlay = new OverlayPanel();
            stack.Controls.Add(overlay);

            // set up the background bitmap and control
            PlatformBitmap backgroundBitmap = PlatformBitmap.FromResource("Wallpaper.jpg");
            WindowlessImage background = new WindowlessImage(backgroundBitmap);
            background.Stretch = Stretch.Uniform;
            overlay.Controls.Add(background);

            // set up the foreground
            StackPanel foreground = new StackPanel();
            overlay.Controls.Add(foreground);

            // load the transparent images
            PlatformBitmap users = PlatformBitmap.FromResource("users.png");
            PlatformBitmap tip = PlatformBitmap.FromResource("tip.png");

            // the two buttons will be placed in a WrapPanel
            // a wrap panel will horizontally or vertically lay out elements in a given direction until it runs out of room in that direction.
            // when it runs out of room, it will "wrap" to the next line.
            // since this wrap panel is contained in a vertical stack panel, it will lay out elements horizontally, and wrap vertically to the next line.
            WrapPanel wrap = new WrapPanel();
            foreground.Controls.Add(wrap);

            // set up the users button
            ImageButton userButton = new ImageButton(users, Stretch.None);
            // controls must be explicitly marked as being transparent. transparency is disabled by default on controls for performance reasons.
            userButton.BackColor = Color.Transparent;
            wrap.Controls.Add(userButton);
            userButton.WindowlessClick += (s, e) =>
            {
                MessageBox.Show("You just clicked users!");
            };

            // set up a tip button next to the users
            ImageButton tipButton = new ImageButton(tip, Stretch.None);
            tipButton.BackColor = Color.Transparent;
            wrap.Controls.Add(tipButton);
            tipButton.WindowlessClick += (s, e) =>
            {
                MessageBox.Show("You just clicked the tip!");
            };
        }

        private void myCloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}