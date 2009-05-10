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
    public partial class TutorialList : Form
    {
        public TutorialList()
        {
            InitializeComponent();

            // myHost is a Windows.Windows.Forms.Control that provides a transition from System.Windows.Forms to WindowlessControls.
            // myHost.Control is the WindowlessControls.WindowlessControl that is "hosted" in the Windows.Windows.Forms.Control.
            StackPanel stack = myHost.Control;

            HyperlinkButton helloWorldButton = new HyperlinkButton("Hello World");
            stack.Controls.Add(helloWorldButton);
            helloWorldButton.WindowlessClick += (s, e) =>
            {
                HelloWorld helloWorld = new HelloWorld();
                helloWorld.ShowDialog();
            };

            HyperlinkButton transparentButton = new HyperlinkButton("Alpha Demo");
            stack.Controls.Add(transparentButton);
            transparentButton.WindowlessClick += (s, e) =>
            {
                AlphaDemo transparent = new AlphaDemo();
                transparent.ShowDialog();
            };

            HyperlinkButton buttonButton = new HyperlinkButton("Image Button Demo");
            stack.Controls.Add(buttonButton);
            buttonButton.WindowlessClick += (s, e) =>
            {
                ButtonDemo imageButton = new ButtonDemo();
                imageButton.ShowDialog();
            };

            HyperlinkButton galleryButton = new HyperlinkButton("Image Gallery Demo");
            stack.Controls.Add(galleryButton);
            galleryButton.WindowlessClick += (s, e) =>
            {
                ImageGallery gallery = new ImageGallery();
                gallery.ShowDialog();
            };

            HyperlinkButton contactButton = new HyperlinkButton("Contact List Demo");
            stack.Controls.Add(contactButton);
            contactButton.WindowlessClick += (s, e) =>
            {
                ContactList contact = new ContactList();
                contact.ShowDialog();
            };
        }

        private void myExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}