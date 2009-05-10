using System;
using WindowlessControls;
using WindowlessControls.CommonControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace WindowlessControlsTutorial
{
    public partial class HelloWorld : Form
    {
        public HelloWorld()
        {
            InitializeComponent();

            // myHost is a Control that provides a transition from System.Windows.Forms to WindowlessControls.
            // myHost.Control is the WindowlessControls.WindowlessControl that is "hosted" in the Windows.Windows.Forms.Control.
            // put all the forms contents into a scrollHost, which will resize arbitrarily to fit its contents
            VerticalStackPanelHost scrollHost = new VerticalStackPanelHost();
            StackPanel stack = scrollHost.Control;
            stack.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Stretch;
            myHost.Control.Controls.Add(scrollHost);

            // enable auto scrolling on myHost so if the contents (scrollHost) are too big, scroll bars appear
            myHost.AutoScroll = true;

            // hello world!
            WindowlessLabel hello1 = new WindowlessLabel("Hello World!");
            stack.Controls.Add(hello1);

            // center this label and use a different font
            Font center = new Font(FontFamily.GenericSerif, 20, FontStyle.Regular);
            WindowlessLabel hello2 = new WindowlessLabel("Centered!", center);
            hello2.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Center;
            stack.Controls.Add(hello2);

            // right align this control
            WindowlessLabel right = new WindowlessLabel("Right Aligned!");
            right.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Right;
            stack.Controls.Add(right);

            // show that controls support margins
            WindowlessLabel margin = new WindowlessLabel("Margin!");
            margin.Margin = new Thickness(20, 20, 20, 20); // margins for the left, top, right, and bottom
            stack.Controls.Add(margin);

            // nest controls within another control and center the parent
            StackPanel child = new StackPanel();
            child.Controls.Add(new WindowlessLabel("Nested"));
            child.Controls.Add(new WindowlessLabel("Controls"));
            child.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Center;
            stack.Controls.Add(child);
            // create a clickable hyperlink
            HyperlinkButton button = new HyperlinkButton("Click Me!");
            child.Controls.Add(button);
            button.WindowlessClick += (s, e) =>
                {
                    MessageBox.Show("Hello!");
                };
            // when the hyperlink is clicked, the event will bubble up to every host in the hierarchy
            // watch for the event and handle it
            myHost.WindowlessClick += (s, e) =>
                {
                    if (s != myHost)
                    {
                        MessageBox.Show("A click event just bubbled up to me from " + s.GetType().ToString() + "!");
                    }
                };

            // draw a centered image
            PlatformBitmap bitmap = PlatformBitmap.FromResource("mybrainhurts.jpg");
            WindowlessImage image = new WindowlessImage(bitmap);
            image.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Center;
            stack.Controls.Add(image);
        }

        private void myCloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}