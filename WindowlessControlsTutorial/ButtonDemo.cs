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
    public partial class ButtonDemo : Form
    {
        public ButtonDemo()
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

            // focus state bitmap
            PlatformBitmap focused = PlatformBitmap.FromResource("Btn1.png");
            // normal unselected state bitmap
            PlatformBitmap unfocused = PlatformBitmap.FromResource("Btn1_Disabled.png");
            // clicked state bitmap
            PlatformBitmap clicked = PlatformBitmap.FromResource("Btn1_Pushed.png");

            // set up the image button by setting the properties manually
            ImageButton button1 = new ImageButton(unfocused, Stretch.None);
            button1.Control.FocusedBitmap = focused;
            button1.Control.ClickedBitmap = clicked;
            button1.BackColor = Color.Transparent;

            // OR set the button up by setting everything in the constructor
            ImageButton button2 = new ImageButton(unfocused, Stretch.None, focused, clicked);
            button2.BackColor = Color.Transparent;

            // add the buttons
            wrap.Controls.Add(button1);
            wrap.Controls.Add(button2);
        }

        private void myCloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}